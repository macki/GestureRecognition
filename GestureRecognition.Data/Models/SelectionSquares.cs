using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GestureRecognition.Data.Interfaces;

namespace GestureRecognition.Data.Models
{
    public class SelectionSquares : IModels
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Points> SquarePoints { get; set; }
        public List<Rectangle> Rectangles { get; set; }

        public Point Centroid { get; set; }
        public List<int> CornerDistanceFromCentroid { get; set; }

        public SelectionSquares()
        {
            SquarePoints = new List<Points>();
            Rectangles = new List<Rectangle>();
        }
    }
}
