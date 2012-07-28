using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data;
using GestureRecognition.Data.Models;
using GestureRecognition.Data.DataSerialization;

namespace GestureRecognition.SquaresRecognizer.Logic
{
    public class BodyPartSquaresRecognizer
    {
        private List<SelectionSquares> _selectionSquares = new List<SelectionSquares>();

        public BodyPartSquaresRecognizer()
        {
           // TODO Recognize Head
        }

        public List<Data.Models.SelectionSquares> GetKnownsPattern(string fileName)
        {
            return SerializeToXml<SelectionSquares>.Deserialize(fileName, false);
        }
    }
}
