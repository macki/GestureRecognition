using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data;
using GestureRecognition.Data.Models;
using GestureRecognition.Data.DataSerialization;

namespace GestureRecognition.SquaresRecognizer.Logic
{
    public class BodyPartSquaresRecognizer_Head : IBodyRecognizer
    {
        private Enums.BodyPart _body = Enums.BodyPart.Head;
        private List<SelectionSquares> _selectionSquares = new List<SelectionSquares>();

        public BodyPartSquaresRecognizer_Head()
        {
           // _selectionSquares.Add(GetKnownsPattern();
        }

        public List<Data.Models.SelectionSquares> GetKnownsPattern(string fileName)
        {
            return SerializeToXml<SelectionSquares>.Deserialize(fileName, false);
        }
    }
}
