using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data.Interfaces;

namespace GestureRecognition.Data.Models
{
    public class Gestures : IModels
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public  List<Points> Points;

        public Gestures()
        {
            Points = new List<Points>();
        }

        public Gestures(string name, List<Points> points)
        {
            Name = name;
            Points = points;
        }
    }
}
