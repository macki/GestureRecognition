using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GestureRecognition.BodyTracking
{
    public class MaximaTrackingSystem
    {
        private TrackingSystem _trackingSystem;
        private int step = 20;
        private int centerX = 0;
        private int centerY = 0;
        
        public MaximaTrackingSystem(TrackingSystem trackingSystem)
        {
            this._trackingSystem = trackingSystem;
        }

        public List<Rectangle> Recognize()
        {
            var bodyPartList = new List<Rectangle>();

            // Get min, max X andY
            var setOfMinY = new List<Rectangle>();
            var setOfMaxY = new List<Rectangle>();
            var setOfMinX = new List<Rectangle>();
            var setOfMaxX = new List<Rectangle>();
            GetWholeBodyMinimumYSquares(ref setOfMinY,ref setOfMinX,ref setOfMaxX, ref setOfMaxY);
            GetCentroid(ref setOfMinX, ref setOfMaxX);

            bodyPartList.AddRange(RecognizeLeftHand(ref setOfMinY,ref  setOfMaxX, ref setOfMaxY));

            return bodyPartList;
        }

        private void GetCentroid( ref List<Rectangle> setOfMinX, ref List<Rectangle> setOfMaxX)
        {
            for (int i = 0; i < setOfMaxX.Count; i++)
            {
                centerX = setOfMaxX.ElementAt(i).X + setOfMinX.ElementAt(i).X + centerX;
            }

            centerX = centerX / (setOfMinX.Count * 2);
        }

        private void GetWholeBodyMinimumYSquares(ref List<Rectangle> setOfMinimalY, ref List<Rectangle> setOfMinX, ref List<Rectangle> setOfMaxX, ref List<Rectangle> setOfMaxY)
        {
            for (int i = 0; i < 320; i = i + step)
            {
                setOfMinimalY.Add(new Rectangle(i, 999, 0, 0));
                setOfMaxY.Add(new Rectangle(i, 0, 0, 0));

                if (i <= 240)
                {
                    setOfMaxX.Add(new Rectangle(0, i, 0, 0));
                    setOfMinX.Add(new Rectangle(999, i, 0, 0));
                }

                for (int j = 0; j < _trackingSystem._selectionSquares.Count; j++)
                {
                    for (int k = 0; k < setOfMinimalY.Count; k++)
                    {
                        // get min Y
                        if (setOfMinimalY.ElementAt(k).X == i && setOfMinimalY.ElementAt(k).Y > _trackingSystem._selectionSquares.ElementAt(j).Y && _trackingSystem._selectionSquares.ElementAt(j).X == i)
                        {
                            setOfMinimalY[k] = _trackingSystem._selectionSquares.ElementAt(j);
                        }

                        // get max Y
                        if (setOfMaxY.ElementAt(k).X == i && setOfMaxY.ElementAt(k).Y < _trackingSystem._selectionSquares.ElementAt(j).Y && _trackingSystem._selectionSquares.ElementAt(j).X == i)
                        {
                            setOfMaxY[k] = _trackingSystem._selectionSquares.ElementAt(j);
                        }

                        // get min X
                        if (i <= 240)
                        {
                            if (setOfMinX.ElementAt(k).Y == i && setOfMinX.ElementAt(k).X > _trackingSystem._selectionSquares.ElementAt(j).X && _trackingSystem._selectionSquares.ElementAt(j).Y == i)
                            {
                                setOfMinX[k] = _trackingSystem._selectionSquares.ElementAt(j);
                            }
                        }

                        // get max X
                        if (i <= 240)
                        {
                            if (setOfMaxX.ElementAt(k).Y == i && setOfMaxX.ElementAt(k).X < _trackingSystem._selectionSquares.ElementAt(j).X && _trackingSystem._selectionSquares.ElementAt(j).Y == i)
                            {
                                setOfMaxX[k] = _trackingSystem._selectionSquares.ElementAt(j);
                            }
                        }
                    }
                }
            }
        }

        private IEnumerable<Rectangle> RecognizeLeftHand(ref List<Rectangle> setOfMinimalY,ref List<Rectangle> setOfMaxX,ref  List<Rectangle> setOfMaxY)
        {
            // 1 - Remove from MinY squres resposnsible for Head
            for (int i = setOfMinimalY.Count - 1; i >= 0; i--)
            {
                if (setOfMinimalY.ElementAt(i).X < centerX * 4/5)
                {
                    setOfMinimalY.RemoveAt(i);
                    setOfMaxY.RemoveAt(i);
                }
            }
            // 2 -
            var orderedMaxX =  setOfMaxX.OrderByDescending(x => x.X);
            var orderedMaxY = setOfMaxY.OrderByDescending(x => x.Y);

            setOfMinimalY.AddRange(orderedMaxX.Take(4));
            


            return setOfMinimalY;
        }
    }

}
