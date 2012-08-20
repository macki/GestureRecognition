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

        public Point HeadCentroid = new Point(0, 0);
        public double AvarageHeadHeight;
        public double AvarageHeadWidth;

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
                HeadCentroid.X += item.PatternCentroid.X;
                HeadCentroid.Y += item.PatternCentroid.Y;
            }
            int count = _headTrainedItems.Count;

            HeadCentroid.X = HeadCentroid.X / count;
            HeadCentroid.Y = HeadCentroid.Y / count;
            AvarageHeadHeight = avarageHeadHeight / count;
            AvarageHeadWidth = avarageHeadWidth / count;

            RemoveSquaresUnderCentroid();
            RemoveSquaresOnTheSides(AvarageHeadWidth);
            RemoveSquaresOnTheBottom(AvarageHeadHeight);

            return _bodyToRecognize.WholePattern;
        }

        private void RemoveSquaresOnTheBottom(double avarageHeadHeight)
        {
            _bodyToRecognize.WholePattern.OrderBy(x => x.Y);

            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (Math.Abs(_bodyToRecognize.WholePattern[0].Y - _bodyToRecognize.WholePattern[i].Y) >= avarageHeadHeight)
                {
                    _bodyToRecognize.WholePattern.Remove(_bodyToRecognize.WholePattern[i]);
                }
            }
        }

        private void RemoveSquaresOnTheSides(double avarageHeadWidth)
        {
            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (Math.Abs(_bodyToRecognize.WholePattern[i].X - _bodyToRecognize.FullBodyCentroid.X) >= avarageHeadWidth  )
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
