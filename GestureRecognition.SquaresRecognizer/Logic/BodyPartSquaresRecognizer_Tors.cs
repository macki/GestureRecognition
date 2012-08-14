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
    public class BodyPartSquaresRecognizer_Tors : IBodyRecognizer
    {
        private SelectionSquares _bodyToRecognize;
        private List<SelectionSquares> _TrainedItems;
        private List<Rectangle> _Tors;

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
            RemoveSquaresOnTheSides(avarageHeadWidth / count);

            // 5 - Removes hands
            RemoveSquaresOnTheHand();

            RemoveSquaresOnTheBottom(avarageHeadHeight / count);

            return _bodyToRecognize.WholePattern;
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

        private void RemoveSquaresOnTheHand()
        {
            var mininumLeft = _bodyToRecognize.WholePattern.Min(x => x.X);
            var maxRight = _bodyToRecognize.WholePattern.Max(x => x.X);

            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (_Tors.Contains(_bodyToRecognize.WholePattern[i]))
                {
                    if (mininumLeft == _bodyToRecognize.WholePattern[i].X || maxRight == _bodyToRecognize.WholePattern[i].X)
                    {
                        _Tors.Remove(_bodyToRecognize.WholePattern[i]);
                    }  
                }
            }

            _bodyToRecognize.WholePattern = _Tors;
        }


        private void RemoveHeadElements()
        {
            var headRecognizer = new BodyPartSquaresRecognizer_Head(_bodyToRecognize.WholePattern, _bodyToRecognize.ProperPattern, this._TrainedItems.Where(x=>x.BodyPart == (int)Enums.BodyPart.Head).ToList());
            headRecognizer.RecognizeBodyPart().ToList().ForEach(x=>_bodyToRecognize.WholePattern.Remove(x));
        }

       

        private void RemoveSquaresOnTheBottom(double avarageHeadHeight)
        {
            _bodyToRecognize.WholePattern.OrderBy(x => x.Y);

            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (Math.Abs(_bodyToRecognize.WholePattern[_bodyToRecognize.WholePattern.Count - 1].Y - _bodyToRecognize.WholePattern[i].Y) > avarageHeadHeight)
                {
                    _bodyToRecognize.WholePattern.Remove(_bodyToRecognize.WholePattern[i]);
                }
            }
        }

        private void RemoveSquaresOnTheSides(double avarageHeadWidth)
        {
            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (Math.Abs(_bodyToRecognize.WholePattern[i].X - _bodyToRecognize.FullBodyCentroid.X) < avarageHeadWidth / 2 )
                {
                    // add rects which should be removed
                    _Tors.Add(_bodyToRecognize.WholePattern[i]);
                }
            }
        }

    }
}
