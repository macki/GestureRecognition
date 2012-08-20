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

        private double _avarageBodyDepth = 0;
        private Rectangle _minZ = new Rectangle(0, 0, 0 , int.MaxValue);
        private Rectangle _maxZ = new Rectangle(0, 0, 0, int.MinValue);
        private Rectangle _maxY = new Rectangle(0, int.MinValue,0, 0);
        private Rectangle _minY = new Rectangle(0, int.MaxValue,0, 0);
        private Rectangle _minX = new Rectangle(int.MaxValue, 0, 0, 0);
        private Rectangle _maxX = new Rectangle(int.MinValue, 0, 0, 0);
        private Rectangle _center = new Rectangle(0, 0, 0, 0);

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
        public List<Points> GetAvarageDepth(List<Points> depthArray, int avrageCoefficient, int squareSize)
        {
            var bodyDepth = new List<Points>();
            ResetMaximaPoint();

            for (int i = 0; i < depthArray.Count - 1; i = i + avrageCoefficient)
            {
                int depth = (int)((depthArray[i].Z + depthArray[i + 1].Z / 2));
                var p = new Points(depthArray[i].X, depthArray[i].Y, depthArray[i].Z, 0);
                p.Z = depth;

                if (Math.Abs(p.Z - _avarageFullDepth) < _depthBodyDifference)
                {
                    bodyDepth.Add(depthArray[i]);
                }
                else
                {
                    depthArray[i].Z = 0;
                    bodyDepth.Add(depthArray[i]);
                }
            }

            // Get basic squares Body
            GetBodySquare(ref depthArray, squareSize);

            // Remove not proper body squares and get maximas
            RemoveNotProperBodySquares();

            // Searching Body Path
            SearchingBodyPath();

            return bodyDepth;
        }

        public void GetBodySquare(ref List<Points> bodyDept, int squareSize)
        {
            _selectionSquares = new List<Rectangle>();

            int posY = -1;
            bool add = true;
            int notZeroZCounter = 0;

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
                        // width = height [always square], height is used to store Z
                        _selectionSquares.Add(new Rectangle((int)bodyDept[i].X, (int)bodyDept[i].Y, squareSize, (int)bodyDept[i].Z));
                        _avarageBodyDepth += bodyDept[i].Z;
                        notZeroZCounter++;
                    }
                }

                // allow starting new row
                if ((int)bodyDept[i].Y == posY + squareSize)
                {
                    add = false;
                    posY = -1;
                }
            }
            _avarageBodyDepth = _avarageBodyDepth / notZeroZCounter;
        }

        private void RemoveNotProperBodySquares()
        {
            for (int i = _selectionSquares.Count - 2; i >= 0; i--)
            {
                var currentSquare = _selectionSquares[i];
                if(i == 0)
                {
                    var nextSquare = _selectionSquares[i + 1];
                    if (nextSquare.X != currentSquare.X + currentSquare.Width)
                    {
                        _selectionSquares.RemoveAt(i);
                    }
                }else
                {              
                    var prevSquare = _selectionSquares[i - 1];
                    var nextSquare = _selectionSquares[i + 1];

                    // Check if current square got naighbor in the same row 
                    if (prevSquare.X != currentSquare.X - currentSquare.Width && nextSquare.X != currentSquare.X + currentSquare.Width)
                    {
                        _selectionSquares.RemoveAt(i);
                    }
                }

                // find maxima sqares
                //X
                if ((int)_selectionSquares[i].X <= _minX.X) { _minX = _selectionSquares[i]; }
                if ((int)_selectionSquares[i].X >= _maxX.X) { _maxX =_selectionSquares[i]; }
                //Y
                if ((int)_selectionSquares[i].Y <= _minY.Y) { _minY = _selectionSquares[i]; }
                if ((int)_selectionSquares[i].Y >= _maxY.Y) { _maxY = _selectionSquares[i]; }
                //Z
                if ((int)_selectionSquares[i].Height <= _minZ.Height) { _minZ = _selectionSquares[i]; }
                if ((int)_selectionSquares[i].Height >= _maxZ.Height) { _maxZ = _selectionSquares[i]; }
                // Center
                _center.X += (int)_selectionSquares[i].X;
                _center.Y += (int)_selectionSquares[i].Y; 
            }

            _center.X = (int)(_center.X / _selectionSquares.Count());
            _center.Y = (int)(_center.Y / _selectionSquares.Count());
        }
        private void SearchingBodyPath()
        {
            for (int i = 0; i >= _selectionSquares.Count; i++)
            {


            }
        }
        private void ResetMaximaPoint()
        {
            _minZ = new Rectangle(0, 0, 0 , int.MaxValue);
            _maxZ = new Rectangle(0, 0, 0, int.MinValue);
            _maxY = new Rectangle(0, int.MinValue,0, 0);
            _minY = new Rectangle(0, int.MaxValue,0, 0);
            _minX = new Rectangle(int.MaxValue, 0, 0, 0);
            _maxX = new Rectangle(int.MinValue, 0, 0, 0);
            _center = new Rectangle(0, 0, 0, 0);

        }
        public List<Rectangle> GetMaximaPoints()
        {
            return new List<Rectangle> { _minX, _maxX, _minY, _maxY, _minZ, _maxZ, _center };
        }
        public Points GetBodyCentroid()
        {
            return CalculateBodyCentroid();
        }
        public double GetAvarageBodyDepth()
        {
            return _avarageBodyDepth;
        }
        public double GetMinimumZ()
        {
            return _minZ.Height;
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
