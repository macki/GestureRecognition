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
    public class BodyPartSquaresRecognizer_Head : IBodyRecognizer
    {
        private SelectionSquares _bodyToRecognize;
        private List<SelectionSquares> _headTrainedItems;

        public BodyPartSquaresRecognizer_Head(List<System.Drawing.Rectangle> bodyToRecognize, List<System.Drawing.Rectangle> selectedPattern, List<SelectionSquares> _trainedItems)
        {
            this._bodyToRecognize = new SelectionSquares() {WholePattern = new List<Rectangle>(bodyToRecognize), ProperPattern = selectedPattern, BodyPart = (int)Enums.BodyPart.Head};
            this._headTrainedItems = _trainedItems;
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
            foreach (var item in _headTrainedItems)
            {
                avarageHeadWidth += item.PatternWidth;
                avarageBodyRatio += item.BodyRatio;
                avarageHeadHeight += item.PatternHeight;
                avarageBodyElement += item.WholePattern.Count;
            }

            RemoveSquaresUnderCentroid();
            RemoveSquaresOnTheSides(avarageHeadWidth / _headTrainedItems.Count);
            RemoveSquaresOnTheBottom(avarageHeadHeight / _headTrainedItems.Count);
            SelectHeadSquares(avarageBodyRatio / _headTrainedItems.Count, (double)avarageBodyElement / (double)_headTrainedItems.Count);


            return _bodyToRecognize.WholePattern;
        }

        private void SelectHeadSquares(double avarageBodyRatio, double avaregeBodyElements)
        {
            for (int i = 0; i < _bodyToRecognize.WholePattern.Count; i++)
            {
                if ((double)i / avaregeBodyElements > avarageBodyRatio)
                {
                    _bodyToRecognize.WholePattern.Remove(_bodyToRecognize.WholePattern[i]);
                }
            }
        }

        private void RemoveSquaresOnTheBottom(double avarageHeadHeight)
        {
            _bodyToRecognize.WholePattern.OrderBy(x => x.Y);

            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (Math.Abs(_bodyToRecognize.WholePattern[0].Y - _bodyToRecognize.WholePattern[i].Y) > avarageHeadHeight)
                {
                    _bodyToRecognize.WholePattern.Remove(_bodyToRecognize.WholePattern[i]);
                }
            }
        }

        private void RemoveSquaresOnTheSides(double avarageHeadWidth)
        {
            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (Math.Abs(_bodyToRecognize.WholePattern[i].X - _bodyToRecognize.FullBodyCentroid.X) > avarageHeadWidth  )
                {
                    _bodyToRecognize.WholePattern.Remove(_bodyToRecognize.WholePattern[i]);
                }
            }
        }

        private void RemoveSquaresUnderCentroid()
        {
            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i-- )
            {
                if (_bodyToRecognize.WholePattern[i].Y > _bodyToRecognize.FullBodyCentroid.Y)
                {
                    _bodyToRecognize.WholePattern.Remove(_bodyToRecognize.WholePattern[i]);
                }
            }
        }
    }
}
