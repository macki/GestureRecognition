using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data.Models;

namespace GestureRecognition.BodyTracking
{
    public partial class TrackingSystem
    {
        private Points CalculateBodyCentroid()
        {
            var centroidPoint = new Points();

            return centroidPoint;
        }
        public void GetMaxPoint(ref Points p)
        {
            if (p.Y > _maxPoint)
            {
                _maxPoint = (int)p.Y;
            }
        }
        public void GetMinPoint(ref Points p)
        {
            if (p.Y < _minPoint)
            {
                _minPoint = (int)p.Y;
            }
        }
        public void GetMaxDepth(Points p)
        {
            if (p.Z > _maxDepth)
            {
                _maxDepth = (int)p.Z;
            }
        }
        public void GetMinDepth(Points p)
        {
            if (p.Z < _minDepth)
            {
                _minDepth = (int)p.Z;
            }
        }
        public int GetMostLeftPart(ref int oldValue, int newValue)
        {
            if (newValue < oldValue)
            {
                return newValue;
            }
            return oldValue;
        }
        public int GetMostRightPart(ref int oldValue, int newValue)
        {
            if (newValue > oldValue)
            {
                return newValue;
            }
            return oldValue;
        }
        public void SetAvarageFullDepth(int val)
        {
            _avarageFullDepth = val;
        }
    }
}
