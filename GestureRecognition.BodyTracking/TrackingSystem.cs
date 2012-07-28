using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data.Models;
using GestureRecognition.UnistrokeRecognizer.Logic;
using System.Drawing;

namespace GestureRecognition.BodyTracking
{
    public partial class TrackingSystem
    {
        private List<int> _depthArray;
        public  List<Rectangle> _selectionSquares;
        private List<Points> _depthPoints;
        private int _cameraWidth = 320;
        private int _cameraHeight = 240;
        private int _depthBodyDifference = 600;
        private int _minPoint = Int16.MaxValue;
        private int _maxPoint = Int16.MinValue;
        private int _minDepth = int.MaxValue;
        private int _maxDepth = int.MinValue;
        private int _avarageFullDepth = 0;
        private int _mostLeftPartOfBody = 999;
        private int _mostRightPartOfBody = 0;
        private int _coeficientOfRemovingDisortion = 20;

        public void GetCurrentDepth(List<int> depthArray)
        {
            _depthArray = depthArray;
            _depthPoints = new List<Points>();
            DeptyArrayToDepthPoints(depthArray);
        }

        public void DeptyArrayToDepthPoints(List<int> depthArray)
        {
            int yPos = 0;
            

            for (int i = 0; i < depthArray.Count; i++)
            {
                RecalculatePosition(i, ref yPos);

                var point = new Points(i - yPos * _cameraWidth, yPos, depthArray[i], 0);
            }

            //GetAvarageDepth(depthArray);
        }

        public List<Points> GetAvarageDepth(List<Points> depthArray, int avrageCoefficient)
        {
            var bodyDepth = new List<Points>();
            double partialSumOfDepth = 0;
            int partialDepthRemovingCounter = 0;
            int yForPartialCalculation = -1;
            int counterYForPariatCalculation = 0;

            for (int i = 0; i < depthArray.Count - 1; i = i + avrageCoefficient)
            {
                int depth = (int)((depthArray[i].Z + depthArray[i + 1].Z / 2));
                var p = new Points(depthArray[i].X, depthArray[i].Y, depthArray[i].Z, 0);
                p.Z = depth;

                if (Math.Abs(p.Z - _avarageFullDepth) < _depthBodyDifference)
                {
                   // if (yForPartialCalculation == -1)
                   // {
                   //     yForPartialCalculation = (int)depthArray[i].Y;
                   // }

                   // // calculate diffrent between point in that same row
                   // if ((int)depthArray[i].Y == yForPartialCalculation)
                   // {
                   //     partialSumOfDepth = depthArray[i].Z + partialSumOfDepth;
                   //     counterYForPariatCalculation++;
                   // }

                   // if (partialDepthRemovingCounter == _coeficientOfRemovingDisortion)
                   // {
                   //     var avParital = partialSumOfDepth / counterYForPariatCalculation;

                   //     // 1400 trainf coeff
                   //     if (avParital < 1600)
                   //     {
                   //         for (int j = i - _coeficientOfRemovingDisortion; j <= i; j = j + avrageCoefficient)
                   //         {
                   //             // int depth = (int)((depthArray[j - 2].Z + depthArray[j - 1].Z + depthArray[j].Z + depthArray[j + 1].Z + depthArray[j+2].Z) / 5);
                   //             // depthArray[j].Z = depth;
                   //              bodyDepth.Add(depthArray[j]);
                   //         }
                   //     }
                   //     partialDepthRemovingCounter = 0;
                   //     partialSumOfDepth = 0;
                   //     counterYForPariatCalculation = 0;
                   //     yForPartialCalculation = -1;

                   // }

                    bodyDepth.Add(depthArray[i]);
 
                   // partialDepthRemovingCounter++;

                   // _mostLeftPartOfBody = GetMostLeftPart(ref _mostLeftPartOfBody, (int)p.X);
                   // _mostRightPartOfBody = GetMostRightPart(ref _mostRightPartOfBody, (int)p.X);
                }
                else
                {
                    depthArray[i].Z = 0;
                    bodyDepth.Add(depthArray[i]);
                }
            }

            return GetBody(ref bodyDepth);
        }

        public List<Points> GetBody(ref List<Points> bodyDept)
        {
            //var centroidOfBody = MathHelper.CalculateCentroid(bodyDept);
            _selectionSquares = new List<Rectangle>();

            int posY = -1;
            bool add = true;
            int squareSize = 20;
            for (int i = 0; i < bodyDept.Count; i = i + squareSize)
            {
                if (posY == -1)
                {
                    posY = (int)bodyDept[i].Y;
                    add = true;
                }

                // find squares on the body
                if ((int)bodyDept[i + (int)(squareSize * 0.4)].Z != 0 || (int)bodyDept[i + (int)(squareSize * 0.6)].Z != 0)
                {
                    if(add == true && ((int)bodyDept[i].Y == posY))
                    {
                        _selectionSquares.Add(new Rectangle((int)bodyDept[i].X, (int)bodyDept[i].Y, squareSize, squareSize));
                    }
                }

                // allow starting new row
                if ((int)bodyDept[i].Y == posY + squareSize)
                {
                    add = false;
                    posY = -1;
                }
            }

            return bodyDept;
        }

        public Points GetBodyCentroid()
        {
            return CalculateBodyCentroid();
        }

        private void RecalculatePosition(int i, ref int yPos)
        {
            if (i % _cameraWidth == 0 && i != 0)
            {
                yPos++;
            }
        }
    }
}
