using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data.Models;
using GestureRecognition.UnistrokeRecognizer.Logic;
using WobbrockLib.Extensions;
using System.Drawing;

namespace GestureRecognition.UnistrokeRecognizer.Algorithms
{
    public class PotractorRecognizer : BasicUnistrokeRecognizer
    {
        #region Constructors

        public PotractorRecognizer()
        {

        }

        public PotractorRecognizer(List<Points> pointsToRecognize, List<Gestures> knownGestures)
        {
            _gestureToRecognize = new Gestures() { Points = pointsToRecognize };
            _knonwGestures = knownGestures;

            Start();
        }

        #endregion

        #region Methods

        private void Start()
        {
            // Step 1
            //Protractor only needs n = 16 points to perform optimally
            _sizeOfResample = 16;
            var resampledPoints = TransformInputGestures(_gestureToRecognize.Points); 

            //Step2
            // Generate a vector representation for the gesture. The procedure takes two
            // parameters: points are resampled points from Step 1, and oSensitive specifies whether the 
            // gesture should be treated orientation sensitive or invariant. The procedure first translates 
            // the gesture so that its centroid is the origin, and then rotates the gesture to align its 
            // indicative angle with a base orientation. VECTORIZE returns a normalized vector with a 
            // length of 2n.
            var normalizedVector = VectorizeF(resampledPoints);

            // Step3
            //Match the vector of an unknown gesture against a set of templates. OPTIMALCOSINE-DISTANCE
            //provides a closed-form solution to find the minimum cosine distance 
            //between the vectors of a template and the unknown gesture by only rotating the template
            //once.
            var result =  RecognizeT(normalizedVector, _knonwGestures);

            _gestureToRecognize.Name = "Name :: " + result[1] + "  Score :: " + result[0];
        }

        private List<double> Vectorize(List<Points> resampledPoints, bool oSensitive )
        {
            var centroid = MathHelper.CalculateCentroid(resampledPoints);
            var translatedPoints = TranslateTo(resampledPoints, centroid);
            var indicativeAngle = Math.Atan2(translatedPoints[0].Y, translatedPoints[0].X);

            double delta = 0;
            if (oSensitive)
            {
               // var baseOrientation = (Math.PI / 4) * Math.Floor((indicativeAngle + Math.PI / 8) / (Math.PI / 4));
               // delta = baseOrientation - indicativeAngle;
                delta = indicativeAngle;
            }
            else
            {
                delta = indicativeAngle;
            }

            double sum = 0;
            var vector = new List<double>();
            for (int i = 0; i < translatedPoints.Count; i++)
            {
                //var newPoint = new Points();
                vector.Add( translatedPoints[i].X * Math.Cos(delta) - translatedPoints[i].Y * Math.Sin(delta));
                vector.Add( translatedPoints[i].Y * Math.Cos(delta) + translatedPoints[i].X * Math.Sin(delta));
               // vector.Add(newPoint);
                sum = sum + translatedPoints[i].X * translatedPoints[i].X + translatedPoints[i].Y * Math.Cos(delta) * translatedPoints[i].Y * Math.Cos(delta);  
            }

            var magnitude = Math.Sqrt(sum);
            for (int i = 0; i < vector.Count; i++ )
            {
                vector[i] = vector[i] / magnitude;
            }
            return vector;
        }

        public static List<double> VectorizeF(List<Points> points)
        {
            double sum = 0.0;
            List<double> vector = new List<double>(points.Count * 2);
            for (int i = 0; i < points.Count; i++)
            {
                vector.Add(points[i].X);
                vector.Add(points[i].Y);
                sum += points[i].X * points[i].X + points[i].Y * points[i].Y;
            }
            double magnitude = Math.Sqrt(sum);
            for (int i = 0; i < vector.Count; i++)
            {
                vector[i] /= magnitude;
            }
            return vector;
        }

        private string[] RecognizeT(List<double> normalizedVector, List<Gestures> _knownGestures)
        {
            double bestScore = 0;
            string gestureName = null;

            foreach (var knownGesture in _knownGestures)
            {
                var knownGestureVector = VectorizeF(TransformInputGestures(knownGesture.Points));
                double distance = OptimalCosineDistance(knownGestureVector, normalizedVector);
                var score = 1 / distance;

                if (score > bestScore)
                {
                    bestScore = score;
                    gestureName = knownGesture.Name;
                }
            }
            return new string[2] { bestScore.ToString(), gestureName };
        }

        private double OptimalCosineDistance(List<double> knownGestureVector, List<double> normalizedVector)
        {
            double a = 0;
            double b = 0;

            for (int i = 0; i < Math.Min(knownGestureVector.Count, normalizedVector.Count); i = i + 2)
            {
                a += knownGestureVector[i] * normalizedVector[i] + knownGestureVector[i + 1] * normalizedVector[i + 1];
                b += knownGestureVector[i] * normalizedVector[i + 1] - knownGestureVector[i + 1] * normalizedVector[i];
            }
            double angle = Math.Atan(b / a);
            double distance = Math.Acos( a * Math.Cos(angle) + b * Math.Sin(angle));

            return distance;
        }

        #endregion
    }
}
