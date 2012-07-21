using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data.Models;

namespace GestureRecognition.UnistrokeRecognizer.Logic
{
    public class MathHelper
    {
        public static double CalculatePathLength(List<Points> points)
        {
            double pathLength = 0;
            for (int i = 0; i < points.Count - 1; i++)
            {
                pathLength = CalculatePointsDistance(points[i], points[i + 1]) + pathLength;
            }
            return pathLength;
        }
        public static double CalculatePointsDistance(Points p1, Points p2)
        {
            double powX = (p1.X - p2.X) * (p1.X - p2.X);
            double powY = (p1.Y - p2.Y) * (p1.Y - p2.Y);

            return Math.Sqrt(powX + powY);
        }
        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
        public static Points CalculateCentroid(List<Points> points)
        {
            double x = 0;
            double y = 0;

            for (int i = 0; i < points.Count; i++)
            {
                x += points[i].X;
                y += points[i].Y;
            }
            return new Points(x / points.Count, y / points.Count, 0 ,0);
        }
        public static Points CalculateCentroidBody(List<Points> points)
        {
            double x = 0;
            double y = 0;
            int c = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if ((int)points[i].Z  != 0)
                {
                    x += points[i].X;
                    y += points[i].Y;
                    c++;
                }
            }
            return new Points(x / c, y / c, 0, 0);
        }
        public static RectangleD CalculateBoundingBox(List<Points> points)
        {
            double minX = double.MaxValue;
            double maxX = double.MinValue;
            double minY = double.MaxValue;
            double maxY = double.MinValue;

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X < minX)
                {
                    minX = points[i].X;
                }

                if (points[i].X > maxX)
                {
                    maxX = points[i].X;
                }

                if (points[i].Y < minY)
                {
                    minY = points[i].Y;
                }

                if (points[i].Y > maxY)
                {
                    maxY = points[i].Y;
                }
            }
            return new RectangleD(minX, minY, maxX - minX, maxY - minY);
        }
        public static List<Data.Models.Points> GetPointsFromRectangle(System.Drawing.Rectangle rect)
        {
            var rectPoints = new List<Data.Models.Points>();

            for (int j = 0; j < rect.Width; j++)
            {
                rectPoints.Add(new Points(rect.X + j, rect.Y, 0, 0));
                rectPoints.Add(new Points(rect.X + j, rect.Y + rect.Height, 0, 0));
            }

            for (int j = 0; j < rect.Height; j++)
            {
                rectPoints.Add(new Points(rect.X, rect.Y + j, 0, 0));
                rectPoints.Add(new Points(rect.X + rect.Width, rect.Y + j, 0, 0));
            }
            return rectPoints;
        }
    }

    public class RectangleD
    {
        public RectangleD()
        {

        }

        public RectangleD(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Heigth = height;
        }

        public double X {get; set;}
        public double Y {get; set;}
        public double Width {get; set;}
        public double Heigth {get; set;}
    }
}
