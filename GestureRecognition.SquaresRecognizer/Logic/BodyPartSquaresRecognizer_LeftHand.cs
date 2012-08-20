using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data;
using GestureRecognition.Data.Models;
using GestureRecognition.Data.DataSerialization;
using System.Drawing;

namespace GestureRecognition.SquaresRecognizer.Logic
{
    public class BodyPartSquaresRecognizer_LeftHand : IBodyRecognizer
    {
        private SelectionSquares _bodyToRecognize;
        private List<SelectionSquares> _TrainedItems;
        private List<Rectangle> _Hands;
        private List<Rectangle> _Head;
        private List<Rectangle> _Tors;
        private List<Rectangle> _BottomTors;

        private Point _recognizePatternCentroid = new Point(0, 0);
        private int _recognizePatternWidth = 0;
        private int _recognizePatternHeight = 0;

        public BodyPartSquaresRecognizer_LeftHand(List<System.Drawing.Rectangle> bodyToRecognize, List<System.Drawing.Rectangle> selectedPattern, List<SelectionSquares> _trainedItems)
        {
            this._bodyToRecognize = new SelectionSquares() {WholePattern = new List<Rectangle>(bodyToRecognize), ProperPattern = selectedPattern, BodyPart = (int)Enums.BodyPart.Hands};
            this._TrainedItems = _trainedItems;

            _Hands = new List<Rectangle>();
            _Head = new List<Rectangle>();
            _Tors = new List<Rectangle>();
            _BottomTors = new List<Rectangle>();
        }

        public List<Data.Models.SelectionSquares> GetKnownsPattern(string fileName)
        {
            return SerializeToXml<SelectionSquares>.Deserialize(fileName, false);
        }

        public IEnumerable<Rectangle> RecognizeBodyPart()
        {
            _bodyToRecognize.CalculateFullBodyCentroid();
           
            double avarageHeadWidth = 0;
            double avarageHeadHeight = 0;
            double avarageBodyRatio = 0;
            double avarageBodyElement = 0;
            Point bodyPartCentroid = new Point(0, 0);
            foreach (var item in _TrainedItems.Where(x => x.BodyPart == (int)Enums.BodyPart.LeftHand).ToList())
            {
                avarageHeadWidth += item.PatternWidth;
                avarageBodyRatio += item.BodyRatio;
                avarageHeadHeight += item.PatternHeight;
                avarageBodyElement += item.WholePattern.Count;
            }

            var count = _TrainedItems.Where(x => x.BodyPart == (int)Enums.BodyPart.LeftHand).ToList().Count;

            //1 - Removes Head with Torso
            GetHeadElements();
            GetTorsoElements();
            GetBottomTorso();
            RemoveHeadTorso();

            //2 - hands analysis
            RemoveSquaresWithoutNeighbor();

            //3 - getting Left Hand [The simples way -> Getting points on the left from centroid]
            GetPointOnTheLleftFromCentroid();

            // use for checking if hand is more vertical or horizontal
            _recognizePatternWidth = _bodyToRecognize.MaximaPointXYZ().ElementAt(1) - _bodyToRecognize.MaximaPointXYZ().ElementAt(0);
            _recognizePatternHeight = _bodyToRecognize.MaximaPointXYZ().ElementAt(3) - _bodyToRecognize.MaximaPointXYZ().ElementAt(2);

            // 4 - Removing not propers hands part
            RemovingNotProperPartOfHand();

            return _bodyToRecognize.WholePattern; 
        }

        private void RemovingNotProperPartOfHand()
        {
            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (_recognizePatternWidth < _recognizePatternHeight)
                {
                    double axisRation = _recognizePatternHeight / _recognizePatternWidth;
                    if (_bodyToRecognize.WholePattern[i].X < _recognizePatternCentroid.X && _bodyToRecognize.WholePattern[i].Y > _recognizePatternCentroid.Y)
                    {
                        _bodyToRecognize.WholePattern.RemoveAt(i);
                    }
                }
            }
        }

        private void GetPointOnTheLleftFromCentroid()
        {
            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (_bodyToRecognize.WholePattern[i].X < _bodyToRecognize.FullBodyCentroid.X)
                {
                    _bodyToRecognize.WholePattern.RemoveAt(i);
                }
                else
                {
                    // calculate centroid point
                    _recognizePatternCentroid.X += _bodyToRecognize.WholePattern[i].X;
                    _recognizePatternCentroid.Y += _bodyToRecognize.WholePattern[i].Y;
                }
            }

            _recognizePatternCentroid.X = _recognizePatternCentroid.X / _bodyToRecognize.WholePattern.Count;
            _recognizePatternCentroid.Y = _recognizePatternCentroid.Y / _bodyToRecognize.WholePattern.Count;
        }

        private void RemoveSquaresWithoutNeighbor()
        {
            var size = _bodyToRecognize.WholePattern[0].Width;
            
            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                bool hasNaighbor = false;

                for (int j = _bodyToRecognize.WholePattern.Count - 1; j >= 0; j--)
                {
                    if ((
                        _bodyToRecognize.WholePattern[i].X + size == _bodyToRecognize.WholePattern[j].X ||
                        _bodyToRecognize.WholePattern[i].X - size == _bodyToRecognize.WholePattern[j].X) && (
                        _bodyToRecognize.WholePattern[i].Y == _bodyToRecognize.WholePattern[j].Y ||
                        _bodyToRecognize.WholePattern[i].Y - size == _bodyToRecognize.WholePattern[j].Y ||
                        _bodyToRecognize.WholePattern[i].Y + size == _bodyToRecognize.WholePattern[j].Y)
                        )
                    {
                        break;
                    }
                    else if (_bodyToRecognize.WholePattern[i].X == _bodyToRecognize.WholePattern[j].X && (
                            _bodyToRecognize.WholePattern[i].Y - size == _bodyToRecognize.WholePattern[j].Y ||
                            _bodyToRecognize.WholePattern[i].Y + size == _bodyToRecognize.WholePattern[j].Y)
                        )
                    {
                        break;
                    }
                    else
                    {
                        if (j == 0)
                        {
                            _bodyToRecognize.WholePattern.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }

        private void RemoveHeadTorso()
        {
            _Tors.AddRange(_Head);
            _Tors.AddRange(_BottomTors);

            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (_Tors.Contains(_bodyToRecognize.WholePattern[i]))
                {
                    _bodyToRecognize.WholePattern.RemoveAt(i);
                }
            }
        }

        private void GetBottomTorso()
        {
            var minTorso = _Tors.Min(x=>x.X);
            var maxTorso = _Tors.Max(x=>x.X);

            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                 if(!_Tors.Contains(_bodyToRecognize.WholePattern[i]))
                 {
                     if (Math.Abs(_bodyToRecognize.WholePattern[i].X - _bodyToRecognize.FullBodyCentroid.X) <= (maxTorso - minTorso) / 2)
                     {
                         if (_bodyToRecognize.WholePattern[i].Y >= _bodyToRecognize.FullBodyCentroid.Y)
                         {
                             _BottomTors.Add(_bodyToRecognize.WholePattern[i]);
                         }
                     }
                 }
            }
        }
        void GetHeadElements()
        {
            var headRecognizer = new BodyPartSquaresRecognizer_Head(_bodyToRecognize.WholePattern, _bodyToRecognize.ProperPattern, this._TrainedItems.Where(x => x.BodyPart == (int)Enums.BodyPart.Head).ToList());
            _Head.AddRange(headRecognizer.RecognizeBodyPart());

            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (headRecognizer.HeadCentroid.Y + headRecognizer.AvarageHeadHeight / 2 > _bodyToRecognize.WholePattern[i].Y)
                {
                    if (Math.Abs(_bodyToRecognize.WholePattern[i].X - headRecognizer.HeadCentroid.X) <= headRecognizer.AvarageHeadWidth / 2 * 3)
                    {
                        _Head.Add(_bodyToRecognize.WholePattern[i]);
                    }
                }
            }
        }
        void GetTorsoElements()
        {
            var headRecognizer = new BodyPartSquaresRecognizer_Tors(_bodyToRecognize.WholePattern, _bodyToRecognize.ProperPattern, _TrainedItems);
            _Tors.AddRange(headRecognizer.RecognizeBodyPart());
        }
    }
}
