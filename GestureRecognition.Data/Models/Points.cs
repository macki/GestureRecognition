using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestureRecognition.Data.Models
{
    public class Points
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public long MSecTime { get; set; }

        public virtual Gestures Gesture { get; set; }


        public Points()
        {

        }

        public Points(double x, double y, double z,long msc)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.MSecTime = msc;
        }

        public Points(Points p)
        {
            this.X = p.X;
            this.Y = p.Y;
            this.Z = p.Z;
            this.MSecTime = p.MSecTime;
        }
    }
}
