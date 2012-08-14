using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data.Models;
using GestureRecognition.Data;
using System.Drawing;

namespace GestureRecognition.SquaresRecognizer.Logic
{
    interface IBodyRecognizer
    {
        List<SelectionSquares> GetKnownsPattern(string fileName);
        IEnumerable<Rectangle> RecognizeBodyPart();
    }
}
