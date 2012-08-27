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
    public class BodyPartSquaresRecognizer_LeftHand : BodyPartSquaresRecognizer, IBodyRecognizer
    {
        private List<SelectionSquares> _TrainedItems;
        private List<Rectangle> _Hands;
        private List<Rectangle> _Head;
        private List<Rectangle> _Tors;
        private List<Rectangle> _BottomTors;
        private List<Rectangle> _BodyPartOnTheLeftFromCentroid = new List<Rectangle>();
        private List<Rectangle> _BasicListWholePattern = new List<Rectangle>();

        private Point _recognizePatternCentroid = new Point(0, 0);
        private int _recognizePatternWidth = 0;
        private int _recognizePatternHeight = 0;

        public BodyPartSquaresRecognizer_LeftHand(List<System.Drawing.Rectangle> bodyToRecognize, List<System.Drawing.Rectangle> selectedPattern, List<SelectionSquares> _trainedItems)
        {
            this._bodyToRecognize = new SelectionSquares() {WholePattern = new List<Rectangle>(bodyToRecognize), ProperPattern = selectedPattern, BodyPart = (int)Enums.BodyPart.Hands};
            this._TrainedItems = _trainedItems;

            _BasicListWholePattern.AddRange(bodyToRecognize);
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
            var leftHandsPattern = _TrainedItems.Where(x => x.BodyPart == (int)Enums.BodyPart.LeftHand).ToList();
            foreach (var item in leftHandsPattern)
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
            RemovingNotProperPartOfHand( avarageHeadWidth / leftHandsPattern.Count);

            // 5 - Add Body Parts which were placed on the left from centroid
            AddLeftCentroidBodyPart();

            //6 - Removes One or 2 Squares parts
            RemoveOneTwoSquaresPart();

            return _bodyToRecognize.WholePattern; 
        }

        private void RemoveOneTwoSquaresPart()
        {
            int counter = 0;
            bool oneCheck = false;
            int currentY = _bodyToRecognize.WholePattern[0].Y;
            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (currentY != _bodyToRecognize.WholePattern[i].Y)
                {
                    if (counter < 3)
                    {
                        currentY = _bodyToRecognize.WholePattern[i].Y;
                        if (i < _bodyToRecognize.WholePattern.Count - 4)
                        {
                            _bodyToRecognize.WholePattern.RemoveAt(i + 3);
                            _bodyToRecognize.WholePattern.RemoveAt(i + 2);
                            _bodyToRecognize.WholePattern.RemoveAt(i  + 1);
                        }
                        counter = 0;  
                    }
                }
                else
                {
                    counter++;
                }
            }
        }

        private void AddLeftCentroidBodyPart()
        {
            var foundRects = new List<Rectangle>();
            int fCounter = 0;
            for (int i = 0; i < _bodyToRecognize.WholePattern.Count; i++)
            {
                foundRects.AddRange(GeRegionWithTheSameDepthVariation(_BasicListWholePattern,_bodyToRecognize.WholePattern[i], 30, 10));
                fCounter += foundRects.Count;

                foreach (var item in foundRects)
                {
                    if (!_bodyToRecognize.WholePattern.Contains(item))
                    {
                        if (fCounter < 60)
                        {
                            _bodyToRecognize.WholePattern.Add(item);
                        }
                    }
                }

            }

            //foreach (var item in foundRects)
            //{
            //    if (!_bodyToRecognize.WholePattern.Contains(item))
            //    {
            //        _bodyToRecognize.WholePattern.Add(item);
            //    }
            //}

            //_bodyToRecognize.WholePattern.AddRange(foundRects);
        }

        private void RemovingNotProperPartOfHand(double bodyElementWidth)
        {
            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (_recognizePatternWidth < _recognizePatternHeight)
                {
                    double axisRation = _recognizePatternHeight / _recognizePatternWidth;
                    if (_bodyToRecognize.WholePattern[i].X < _recognizePatternCentroid.X && _bodyToRecognize.WholePattern[i].Y > _recognizePatternCentroid.Y)
                    {
                        _bodyToRecognize.WholePattern.RemoveAt(i);
                        continue;
                    }
                }
                // remove to far squres
                if (Math.Abs(_bodyToRecognize.WholePattern[i].X - _recognizePatternCentroid.X) > bodyElementWidth * 1.6)
                {
                    _bodyToRecognize.WholePattern.RemoveAt(i);
                }
            }
        }

        private void GetPointOnTheLleftFromCentroid()
        {
            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (_bodyToRecognize.WholePattern[i].X < _bodyToRecognize.FullBodyCentroid.X)
                {
                    _BodyPartOnTheLeftFromCentroid.Add(_bodyToRecognize.WholePattern.ElementAt(i));
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
            headRecognizer.RecognizeBodyPart();
            _Head.AddRange(headRecognizer._HeadWithDepthAnalyzing);

            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (headRecognizer._HeadCentroid.Y + headRecognizer.AvarageHeadHeight / 2 > _bodyToRecognize.WholePattern[i].Y)
                {
                    if (Math.Abs(_bodyToRecognize.WholePattern[i].X - headRecognizer._HeadCentroid.X) <= headRecognizer.AvarageHeadWidth / 2 * 3)
                    {
                        _Head.Add(_bodyToRecognize.WholePattern[i]);
                    }
                }
            }
        }
        void GetTorsoElements()
        {
            var headRecognizer = new BodyPartSquaresRecognizer_Tors(_bodyToRecognize.WholePattern, _bodyToRecognize.ProperPattern, _TrainedItems);
            //_Tors.AddRange(headRecognizer.RecognizeBodyPart());
            headRecognizer.RecognizeBodyPart();
            _Tors.AddRange(headRecognizer._TorsWithDepthAnalyzing);
             
        }
    }
}
