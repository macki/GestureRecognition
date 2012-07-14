using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data.Models;
using GestureRecognition.UnistrokeRecognizer.Algorithms;
using System.Threading;

namespace GestureRecognition.UnistrokeRecognizer
{
    class Program
    {        
        [STAThread]
        static void Main()
        {
            Console.WriteLine("UnistrokeRecognize");

            var point = new Points(0, 0, 0, 0);
            var point2 = new Points(2, 2, 0, 0);

            

            var points = new List<Points>();
            for (int i = 0; i < 10; i++)
            {
                points.Add(new Points(i * 20, i * 20, 0, 0));
            }

            
            var form = new Forms.AlgorithmsStepForm();
            form.Show();
            form.DrawPoints(points);

            Resample(points, 64);

            //var tt2 = new UnistrokeRecognizer();
            //tt2.Recognize()

            Console.ReadKey();
        }

        private static void Resample(List<Points> points, int sampleNum)
        {
            double Interval = CalculatePathLength(points) / (sampleNum);

            var newPoints = new List<Points>();
            newPoints.Add(points[0]);

            double manageDistance = 0;
            for (int i = 1; i < points.Count; i++)
            {
                double distance = CalculatePointsDistance(points[i - 1], points[i]);
                if ((manageDistance + distance) >= Interval)
                {
                    var newKinectPoint = new Points();
                    newKinectPoint.X = points[i - 1].X + ((Interval - manageDistance) / distance) * (points[i].X - points[i - 1].X);
                    newKinectPoint.Y = points[i - 1].Y + ((Interval - manageDistance) / distance) * (points[i].Y - points[i - 1].Y);

                    newPoints.Add(newKinectPoint);
                    points.Insert(i, newKinectPoint);
                    manageDistance = 0;
                }
                else
                {
                    manageDistance = manageDistance + distance;
                }
            }

            var form = new Forms.AlgorithmsStepForm();
            form.Show();
            form.DrawPoints(newPoints);

        }

        private static double CalculatePathLength(List<Points> points)
        {
            double pathLength = 0;
            for (int i = 0; i < points.Count - 1; i++)
            {
                pathLength = CalculatePointsDistance(points[i], points[i + 1]) + pathLength;
            }
            return pathLength;
        }
        private static double CalculatePointsDistance(Points p1, Points p2)
        {
            double powX = (p1.X - p2.X) * (p1.X - p2.X);
            double powY = (p1.Y - p2.Y) * (p1.Y - p2.Y);

            return Math.Sqrt(powX + powY);
        }
    }
}


//logger.Debug("Sample debug message");
//logger.Info("Sample informational message");
//logger.Warn("Sample warning message");
//logger.Error("Sample error message");
//logger.Fatal("Sample fatal error message");   
