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
    public class BodyPartSquaresRecognizer
    {
        protected SelectionSquares _bodyToRecognize = new SelectionSquares();
        private List<Rectangle> _selectedRects;

        public BodyPartSquaresRecognizer()
        {

        }

        public BodyPartSquaresRecognizer(List<Rectangle> _selectedRects)
        {
            // TODO: Complete member initialization
            _bodyToRecognize.WholePattern = _selectedRects;
        }

        public List<Rectangle> GeRegionWithTheSameDepthVariation(List<Rectangle> rects, Rectangle startingPoint, int depthVariation, int size)
        {
            var foundRects = new List<Rectangle>();
            for (int i = rects.Count - 1; i >= 0; i--)
            {
                // get only point which contains in depthVariation
                if (Math.Abs(startingPoint.Height - rects[i].Height) <= depthVariation)
                {
                    if (Math.Abs(startingPoint.X - rects[i].X) < size && Math.Abs(startingPoint.Y - rects[i].Y) < size)
                    {
                        foundRects.Add(rects[i]);
                    }
                }
            }
            return foundRects;
        }


        public List<Data.Models.SelectionSquares> GetKnownsPattern(string fileName)
        {
            return SerializeToXml<SelectionSquares>.Deserialize(fileName, false);
        }
    }
}
