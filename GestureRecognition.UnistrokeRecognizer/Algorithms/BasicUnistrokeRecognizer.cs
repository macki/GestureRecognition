using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data.Models;
using GestureRecognition.UnistrokeRecognizer.Forms;
using GestureRecognition.UnistrokeRecognizer.Logic;
using WobbrockLib.Extensions;
using System.Drawing;

namespace GestureRecognition.UnistrokeRecognizer.Algorithms
{
    public class BasicUnistrokeRecognizer
    {
        private List<Gestures> _knonwGestures;
        private Gestures _gestureToRecognize;

        private const int _sizeOfResample = 64;
        private double _angle = 45;
        private double _anglePrecision = 2;
        private double _goldenRation = 0.5 * (-1 + Math.Sqrt(5.0));

        public BasicUnistrokeRecognizer()
        {

        }
        public BasicUnistrokeRecognizer(List<Points> pointsToRecognize, List<Gestures> knownGestures)
        {
            _gestureToRecognize = new Gestures() { Points = pointsToRecognize };
            _knonwGestures = knownGestures;

            Start();
        }

        /// <summary>
        /// Starting algorithm
        /// </summary>
        private void Start()
        {
            var resampledPoints = ResamplePoints(_gestureToRecognize.Points, _sizeOfResample);
            var centroidPoint = MathHelper.CalculateCentroid(resampledPoints);
            var theta = GeotrigEx.Angle(new PointF((float)centroidPoint.X, (float)centroidPoint.Y),
                                        new PointF((float)resampledPoints[0].X,(float)resampledPoints[0].Y), false); 

            var rotatedPoints = RotateBy(resampledPoints, -theta);
            //DrawPoints(rotatedPoints);
            var scaledPoints = ScaleTo(rotatedPoints, 250);
            var translatedPoints = TranslateTo(scaledPoints, new Points(0, 0, 0, 0));
            //DrawPoints(scaledPoints);
            var finalScore = RecognizeT(translatedPoints, 250);
            _gestureToRecognize.Name = "Score :: " + finalScore;
        }

        /// <summary>
        /// Resample point to N - sampleSize
        /// </summary>
        /// <param name="points"></param>
        /// <param name="sampleSize"></param>
        /// <returns></returns>
        private List<Points> ResamplePoints(List<Points> points, int sampleSize)
        {
            double Interval = MathHelper.CalculatePathLength(points) / (sampleSize);

            var newPoints = new List<Points>();
            newPoints.Add(points[0]);

            double manageDistance = 0;
            for (int i = 1; i < points.Count; i++)
            {
                double distance = MathHelper.CalculatePointsDistance(points[i - 1], points[i]);
                if ((manageDistance + distance) >= Interval)
                {
                    var newPoint = new Points();
                    newPoint.X = points[i - 1].X + ((Interval - manageDistance) / distance) * (points[i].X - points[i - 1].X);
                    newPoint.Y = points[i - 1].Y + ((Interval - manageDistance) / distance) * (points[i].Y - points[i - 1].Y);

                    newPoints.Add(newPoint);
                    points.Insert(i, newPoint);
                    manageDistance = 0;
                }
                else
                {
                    manageDistance = manageDistance + distance;
                }
            }
            return newPoints;
        }
        /// <summary>
        /// Find and save the indicative angle  ω from the  points’ 
        /// centroid to first point. Then rotate by –ω to set this angle to 0°
        /// </summary>
        /// <param name="pointsAfterStep_One"></param>
        /// <returns></returns>
        private double RotateToZero(List<Points> pointsAfterStep_One)
        {
            var centroidPoint = MathHelper.CalculateCentroid(pointsAfterStep_One);
            var theta = Math.Atan2(centroidPoint.Y - pointsAfterStep_One[0].Y, centroidPoint.X - pointsAfterStep_One[0].X);
            return theta;
        }
        /// <summary>
        /// Rotate Points accroding to Theta i centroid point
        /// </summary>
        /// <param name="pointsAfterStep_One"></param>
        /// <param name="theta"></param>
        /// <returns></returns>
        private List<Points> RotateBy(List<Points> pointsAfterStep_One, double theta)
        {
            // optimaze
            var centroid = MathHelper.CalculateCentroid(pointsAfterStep_One);
            var pointsCount = pointsAfterStep_One.Count;
            var cos = Math.Cos(theta);
            var sin = Math.Sin(theta);

            var newPoints = new List<Points>();
            var newPoint = new Points();
            for (int i = 0; i < pointsCount; i++)
            {
                newPoint = new Points();
                newPoint.X = (pointsAfterStep_One[i].X - centroid.X) * cos - (pointsAfterStep_One[i].Y - centroid.Y) * sin + centroid.X;
                newPoint.Y = (pointsAfterStep_One[i].X - centroid.X) * sin + (pointsAfterStep_One[i].Y - centroid.Y) * cos + centroid.Y;
                newPoints.Add(newPoint);
            }
            return newPoints;
        }
        private List<Points> ScaleTo(List<Points> rotatedPoints, int boundingSize)
        {
            var boundingBox = MathHelper.CalculateBoundingBox(rotatedPoints);

            var newPoints = new List<Points>();
            var newPoint = new Points();
            for (int i = 0; i < rotatedPoints.Count; i++)
            {
                newPoint = new Points();
                newPoint.X = rotatedPoints[i].X * boundingSize / boundingBox.Width;
                newPoint.Y = rotatedPoints[i].Y * boundingSize / boundingBox.Heigth;
                newPoints.Add(newPoint);
            }
            return newPoints;
        }
        /// <summary>
        /// Translate  points to the origin [Points (0,0)]
        /// </summary>
        /// <param name="scaledPoints"></param>
        /// <param name="originPoint"></param>
        /// <returns></returns>
        private List<Points> TranslateTo(List<Points> scaledPoints, Points originPoint)
        {
            var centroidPoint = MathHelper.CalculateCentroid(scaledPoints);
            var newPoints = new List<Points>();
            Points point = null;

            for (int i = 0; i < scaledPoints.Count; i++)
            {
                point = new Points();
                point.X = scaledPoints[i].X + originPoint.X - centroidPoint.X;
                point.Y = scaledPoints[i].Y + originPoint.Y - centroidPoint.Y;
                newPoints.Add(point);
            }
            return newPoints;
        }
        /// <summary>
        /// f RECOGNIZE refers to the size passed to SCALE-TO in 
        ///    Step 3. The symbol  ϕ  equals  ½(-1  +  √5). We use  θ=±45° and 
        ///    θ∆=2° on line 3 of RECOGNIZE. Due to using RESAMPLE, we can 
        ///    assume that A and B in PATH-DISTANCE contain the same number 
        ///    of points, i.e., |A|=|B|.
        /// </summary>
        /// <param name="translatedPoints"></param>
        /// <param name="boundingSize"></param>
        /// <returns></returns>
        private double RecognizeT(List<Points> translatedPoints, double boundingSize)
        {
            double accB = double.MaxValue;
            int loopCounter = 0;
            int index = 0;
            // Stored Geture
            foreach (var gesture in _knonwGestures)
            {
                var distance = DistanceAtBestAngle(translatedPoints,  gesture.Points, -_angle, _angle, _anglePrecision);

                if (distance < accB)
                {
                    accB = distance;
                    index = loopCounter;
                }
                loopCounter++;
            }
            var score = 1 - (accB / Math.Sqrt(boundingSize * boundingSize + boundingSize * boundingSize));
            return score;
        }

        private double DistanceAtBestAngle(List<Points> translatedPoints, List<Points> gesturePoints, double minAngle, double maxAngle, double anglePrecision)
        {
            double x1 = _goldenRation * minAngle + (1 - _goldenRation) * maxAngle;
            double f1 = DistanceAtAngle(translatedPoints, gesturePoints, x1);
            double x2 = (1 - _goldenRation) * minAngle + _goldenRation * maxAngle;
            double f2 = DistanceAtAngle(translatedPoints, gesturePoints, x2);

            do
            {
                if (f1 < f2)
                {
                    maxAngle = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = _goldenRation * minAngle + (1 - _goldenRation) * maxAngle;
                    // translatedPoints = RotateBy(translatedPoints, x1);
                    double addModf = 1;
                    f1 = DistanceAtAngle(translatedPoints, gesturePoints, x1) * addModf;
                }
                else
                {
                    minAngle = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = (1 - _goldenRation) * minAngle + _goldenRation * maxAngle;
                    //translatedPoints = RotateBy(translatedPoints, x1);
                    double addModf = 1;
                    f2 = DistanceAtAngle(translatedPoints, gesturePoints, x2) * addModf;
                }

            } while (maxAngle - minAngle > anglePrecision);

            return Math.Min(f1, f2);
        }
        private double DistanceAtAngle(List<Points> translatedPoints, List<Points> gesturePoints, double x1)
        {
            var newPoints = new List<Points>();
            var rotatedPoints = RotateBy(translatedPoints, x1);
            var pathDistance = CalculatePathDistance(translatedPoints, gesturePoints);
            return pathDistance;
        }
        private double CalculatePathDistance(List<Points> translatedPoints, List<Points> gesturePoints)
        {
            double d = 0;
            // assume newPoints.Count <= gesturePoints.Count
            for (int i = 0; i < translatedPoints.Count; i++)
            {
                if (i < gesturePoints.Count)
                {
                    d += MathHelper.CalculatePointsDistance(translatedPoints[i], gesturePoints[i]);
                }
            }
            return d / translatedPoints.Count;
        }



        private void DrawPoints(List<Points> points)
        {
            var testingForm = new AlgorithmsStepForm();
            testingForm.Show();
            testingForm.DrawPoints(points);

        }
        public Gestures Result()
        {
            return _gestureToRecognize;
        }
        public List<Points> TransformInputGestures(List<Points> points)
        {
            var resampledPoints = ResamplePoints(points, _sizeOfResample);
            var centroidPoint = MathHelper.CalculateCentroid(resampledPoints);
            var theta = GeotrigEx.Angle(new PointF((float)centroidPoint.X, (float)centroidPoint.Y),
                                        new PointF((float)resampledPoints[0].X, (float)resampledPoints[0].Y), false);

            var rotatedPoints = RotateBy(resampledPoints, -theta);
            //DrawPoints(rotatedPoints);
            var scaledPoints = ScaleTo(rotatedPoints, 250);
            var translatedPoints = TranslateTo(scaledPoints, new Points(0, 0, 0, 0));

            return translatedPoints;
        }
    }
}
