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
    public class BodyPartSquaresRecognizer_Legs : IBodyRecognizer
    {
        private SelectionSquares _bodyToRecognize;
        private List<SelectionSquares> _TrainedItems;
        private List<Rectangle> _legs;

        public BodyPartSquaresRecognizer_Legs(List<System.Drawing.Rectangle> bodyToRecognize, List<System.Drawing.Rectangle> selectedPattern, List<SelectionSquares> _trainedItems)
        {
            this._bodyToRecognize = new SelectionSquares() {WholePattern = new List<Rectangle>(bodyToRecognize), ProperPattern = selectedPattern, BodyPart = (int)Enums.BodyPart.Torso};
            this._TrainedItems = _trainedItems;
            this._legs = new List<Rectangle>();
        }

        public List<Data.Models.SelectionSquares> GetKnownsPattern(string fileName)
        {
            return SerializeToXml<SelectionSquares>.Deserialize(fileName, false);
        }

        public IEnumerable<Rectangle> RecognizeBodyPart()
        {
            //_bodyToRecognize.CalculateBodyParameters();
            _bodyToRecognize.CalculateFullBodyCentroid();

            double avarageBodyWidth = 0;
            double avarageBodyHeight = 0;
            double avarageBodyRatio = 0;
            double avarageBodyElement = 0;
            double avarageMinY = 0;
            foreach (var item in _TrainedItems.Where(x => x.BodyPart == (int)Enums.BodyPart.Torso).ToList())
            {
                avarageBodyWidth += item.PatternWidth;
                avarageBodyRatio += item.BodyRatio;
                avarageBodyHeight += item.PatternHeight;
                avarageBodyElement += item.WholePattern.Count;
                
            }

            var count = _TrainedItems.Where(x => x.BodyPart == (int)Enums.BodyPart.Torso).ToList().Count;

            //1 - Removes square without Naighbors
            RemovesTooFarSquares(avarageBodyWidth / count);

            //3 Remove Hands
            RemoveHandElements();

            // 2 - Remove Head Elements
            GetLegsSquares(avarageBodyWidth / count);

            return _legs;
        }

        private void RemoveHandElements()
        {
            var HandsRecognizer = new BodyPartSquaresRecognizer_Hands(_bodyToRecognize.WholePattern, _bodyToRecognize.ProperPattern, this._TrainedItems.ToList());
            HandsRecognizer.RecognizeBodyPart().ToList().ForEach(x => _bodyToRecognize.WholePattern.Remove(x));
        }

        private void GetLegsSquares(double bodyPartHeight)
        {
            for (int i = _bodyToRecognize.WholePattern.Count -1; i >= 0; i--)
            {
                if (_bodyToRecognize.WholePattern[i].Y > _bodyToRecognize.FullBodyCentroid.Y)
                {
                    // assumpted that min is always at 240
                    if (Math.Abs(240 - _bodyToRecognize.WholePattern[i].Y) < bodyPartHeight)
                    {
                        _legs.Add(_bodyToRecognize.WholePattern[i]);
                    }
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
        }
        private void RemoveTorsoElements()
        {
           var torsoRecognizer = new BodyPartSquaresRecognizer_Tors(_bodyToRecognize.WholePattern, _bodyToRecognize.ProperPattern, this._TrainedItems.ToList());
           _bodyToRecognize.WholePattern = torsoRecognizer.RecognizeBodyPart().ToList();
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
                    //_Tors.Add(_bodyToRecognize.WholePattern[i]);
                }
            }
        }

    }
}
