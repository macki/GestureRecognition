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
    public class BodyPartSquaresRecognizer_Tors : BodyPartSquaresRecognizer, IBodyRecognizer
    {
        public List<Rectangle> _TorsWithDepthAnalyzing;
        private List<SelectionSquares> _TrainedItems;
        private List<Rectangle> _Tors;
        private int _avarageDepth = 0;
        private int _endOfHead = 0;

        public BodyPartSquaresRecognizer_Tors(List<System.Drawing.Rectangle> bodyToRecognize, List<System.Drawing.Rectangle> selectedPattern, List<SelectionSquares> _trainedItems)
        {
            this._bodyToRecognize = new SelectionSquares() {WholePattern = new List<Rectangle>(bodyToRecognize), ProperPattern = selectedPattern, BodyPart = (int)Enums.BodyPart.Torso};
            this._TrainedItems = _trainedItems;
            this._Tors = new List<Rectangle>();
        }

        public List<Data.Models.SelectionSquares> GetKnownsPattern(string fileName)
        {
            return SerializeToXml<SelectionSquares>.Deserialize(fileName, false);
        }

        public IEnumerable<Rectangle> RecognizeBodyPart()
        {
            //_bodyToRecognize.CalculateBodyParameters();
            _bodyToRecognize.CalculateFullBodyCentroid();

            double avarageHeadWidth = 0;
            double avarageHeadHeight = 0;
            double avarageBodyRatio = 0;
            double avarageBodyElement = 0;
            foreach (var item in _TrainedItems.Where(x => x.BodyPart == (int)Enums.BodyPart.Torso).ToList())
            {
                avarageHeadWidth += item.PatternWidth;
                avarageBodyRatio += item.BodyRatio;
                avarageHeadHeight += item.PatternHeight;
                avarageBodyElement += item.WholePattern.Count;
            }

            var count = _TrainedItems.Where(x => x.BodyPart == (int)Enums.BodyPart.Torso).ToList().Count;

            //1 - Removes square without Naighbors
            RemovesTooFarSquares(avarageHeadWidth / count);

            // 2 - Remove Head Elements
            RemoveHeadElements();

            // 3 - Remove Legs
            RemoveLegsElements((int)(avarageHeadHeight / count));

            // 4 - Removing suares on the sides
            RemoveSquaresOnTheSides(avarageHeadWidth / count);

            // Get TORS 
            // 6 - removes elements with too much depth values
            RemovesElementsBasedOnDepth();

            return _TorsWithDepthAnalyzing;
        }

        private void RemoveLegsElements(int avarageBodyPartHeight)
        {
            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (_bodyToRecognize.WholePattern[i].Y > _endOfHead + avarageBodyPartHeight)
                {
                    _bodyToRecognize.WholePattern.RemoveAt(i);
                }
            }
        }

        private void RemovesTooFarSquares(double avarageHeadWidth)
        {
            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (Math.Abs(_bodyToRecognize.WholePattern[i].X - _bodyToRecognize.FullBodyCentroid.X) > avarageHeadWidth * 1.5)
                {
                    _bodyToRecognize.WholePattern.RemoveAt(i);
                }
            }
        }
        private void RemoveHeadElements()
        {
            var headRecognizer = new BodyPartSquaresRecognizer_Head(_bodyToRecognize.WholePattern, _bodyToRecognize.ProperPattern, this._TrainedItems.Where(x=>x.BodyPart == (int)Enums.BodyPart.Head).ToList());
            headRecognizer.RecognizeBodyPart().ToList().ForEach(x=>_bodyToRecognize.WholePattern.Remove(x));

            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (headRecognizer._HeadCentroid.Y + headRecognizer.AvarageHeadHeight / 2 > _bodyToRecognize.WholePattern[i].Y)
                {
                  if( Math.Abs(_bodyToRecognize.WholePattern[i].X - headRecognizer._HeadCentroid.X) >= headRecognizer.AvarageHeadWidth / 2 )
                    {
                       _bodyToRecognize.WholePattern.RemoveAt(i);
                    }
                }
            }

            _endOfHead = headRecognizer._HeadCentroid.Y + (int)headRecognizer.AvarageHeadHeight / 2;

        }
        private void RemoveSquaresOnTheSides(double avarageHeadWidth)
        {
            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (Math.Abs(_bodyToRecognize.WholePattern[i].X - _bodyToRecognize.FullBodyCentroid.X) < avarageHeadWidth / 2)
                {
                    // add rects which should be removed
                    _avarageDepth += _bodyToRecognize.WholePattern[i].Height;
                    _Tors.Add(_bodyToRecognize.WholePattern[i]);
                    _bodyToRecognize.WholePattern.RemoveAt(i);
                }
            }
            _avarageDepth = _avarageDepth / _Tors.Count;
        }
        private void RemovesElementsBasedOnDepth()
        {
            _TorsWithDepthAnalyzing = new List<Rectangle>();
            for (int i = _Tors.Count - 1; i >= 0; i--)
            {
                if (Math.Abs(_Tors[i].Height - _avarageDepth) < (_bodyToRecognize.MaximaPointXYZ().ElementAt(5) - _avarageDepth) / 3)
                {
                    _TorsWithDepthAnalyzing.Add(_Tors[i]);
                }
            }
        }
    }
}
