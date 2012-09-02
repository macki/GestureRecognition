using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MackiTools.MackiTools.RectanglesUtil;

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
            var setOfMinZ = new List<Rectangle>();
            GetWholeBodyMinimumYSquares(ref setOfMinY,ref setOfMinX,ref setOfMaxX, ref setOfMaxY, ref setOfMinZ);
            GetCentroid(ref setOfMinX, ref setOfMaxX);

            bodyPartList.AddRange(RecognizeLeftHand(ref setOfMinY, ref  setOfMaxX, ref setOfMaxY, ref setOfMinZ));


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

        private void GetWholeBodyMinimumYSquares(ref List<Rectangle> setOfMinimalY, ref List<Rectangle> setOfMinX, ref List<Rectangle> setOfMaxX, ref List<Rectangle> setOfMaxY, ref List<Rectangle> setOfMinZ)
        {
            for (int i = 0; i < 320; i = i + step)
            {
                setOfMinimalY.Add(new Rectangle(i, 999, 0, 0));
                setOfMaxY.Add(new Rectangle(i, 0, 0, 0));
                setOfMinZ.Add(new Rectangle(i, 0, 0, 9999));

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
                            if (_trackingSystem._selectionSquares.ElementAt(j).Height != 0)
                            {
                                setOfMinimalY[k] = _trackingSystem._selectionSquares.ElementAt(j);
                            }
                        }

                        // get max Y
                        if (setOfMaxY.ElementAt(k).X == i && setOfMaxY.ElementAt(k).Y < _trackingSystem._selectionSquares.ElementAt(j).Y && _trackingSystem._selectionSquares.ElementAt(j).X == i)
                        {
                            if (_trackingSystem._selectionSquares.ElementAt(j).Height != 0)
                            {
                                setOfMaxY[k] = _trackingSystem._selectionSquares.ElementAt(j);
                            }
                        }

                        // get minz Z
                        if (setOfMinZ.ElementAt(k).X == i && setOfMinZ.ElementAt(k).Height > _trackingSystem._selectionSquares.ElementAt(j).Height && _trackingSystem._selectionSquares.ElementAt(j).X == i)
                        {
                            if (_trackingSystem._selectionSquares.ElementAt(j).Height != 0)
                            {
                                setOfMinZ[k] = _trackingSystem._selectionSquares.ElementAt(j);
                            }
                        }

                        // get min X
                        if (i <= 240)
                        {
                            if (setOfMinX.ElementAt(k).Y == i && setOfMinX.ElementAt(k).X > _trackingSystem._selectionSquares.ElementAt(j).X && _trackingSystem._selectionSquares.ElementAt(j).Y == i)
                            {
                                if (_trackingSystem._selectionSquares.ElementAt(j).Height != 0)
                                {
                                    setOfMinX[k] = _trackingSystem._selectionSquares.ElementAt(j);
                                }
                            }
                        }

                        // get max X
                        if (i <= 240)
                        {
                            if (setOfMaxX.ElementAt(k).Y == i && setOfMaxX.ElementAt(k).X < _trackingSystem._selectionSquares.ElementAt(j).X && _trackingSystem._selectionSquares.ElementAt(j).Y == i)
                            {
                                if (_trackingSystem._selectionSquares.ElementAt(j).Height != 0)
                                {
                                    setOfMaxX[k] = _trackingSystem._selectionSquares.ElementAt(j);
                                }
                            }
                        }
                    }
                }
            }
        }

        private IEnumerable<Rectangle> RecognizeLeftHand(ref List<Rectangle> setOfMinimalY, ref List<Rectangle> setOfMaxX, ref  List<Rectangle> setOfMaxY, ref List<Rectangle> setOfMinZ)
        {
            var rects = new List<Rectangle>();

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
            var orderedMaxY = (setOfMaxY.OrderByDescending(x => x.Y)).ToList();
            orderedMaxY = (from p in orderedMaxY where p.Y != 0 select p).ToList();
            var properMinZ = from p in setOfMinZ where p.Height != 9999 select p;
            setOfMinimalY = (from p in setOfMinimalY where p.Y != 999 select p).ToList();

            // calculate variation in minX and maxX
            var choosenMaxX = new List<Rectangle>();
            var dif = orderedMaxX.Take(6).Max(x => x.X) - orderedMaxX.Take(6).Min(x => x.X);
            if (dif >= 15)
            {
                choosenMaxX = orderedMaxX.Take(2).ToList();
            }
            else
            {
                choosenMaxX = orderedMaxX.Take(7).ToList();
            }

            // remove minimalY which are too far for maxX
            var minX = choosenMaxX.Min(x => x.X);
            for (int i = setOfMinimalY.Count - 1; i >= 0; i--)
            {
                if (Math.Abs(setOfMinimalY.ElementAt(i).X - minX) > 20)
                {
                    setOfMinimalY.RemoveAt(i);
                }
            }


            //rects.AddRange(setOfMinimalY);
            rects.AddRange(choosenMaxX);
            
            //rects.AddRange(orderedMaxY);

            // each point searching their minima Z in the naighborhood (20, 20) which is not actually on the list
            for (int i = 0; i < rects.Count; i++)
            {
                for (int j = 0; j < _trackingSystem._selectionSquares.Count; j++)
                {
                    if(Math.Abs(_trackingSystem._selectionSquares[j].X - rects[i].X) < 10 && Math.Abs(_trackingSystem._selectionSquares[j].Y - rects[i].Y) < 10 )
                    {
                        if (_trackingSystem._selectionSquares[j].Height < rects[i].Height)
                        {
                            if (_trackingSystem._selectionSquares[j].Height != 0)
                            {
                                //rects[i] = _trackingSystem._selectionSquares[j];
                            }
                        }
                    }
                }
            }

            // Gets The samve depth variation region
            for (int i = 0; i < rects.Count; i++)
            {
                if (rects.Count < 80)
                {
                    var rect = RectanglesUtil.GeRegionWithTheSameDepthVariation(_trackingSystem._selectionSquares, rects[i], 40, 10);

                    foreach (var item in rect)
                    {
                        if (!rects.Contains(item))
                        {
                            rects.Add(item);
                        }
                    }
                }
            }

            var centroid = RectanglesUtil.GetCentroid(rects);
            var centroidRect = new Rectangle(centroid.X, centroid.Y, 0, 0);

            // calculate centroid
            for (int i = 0; i < _trackingSystem._selectionSquares.Count; i++)
            {
                if ((Math.Abs(centroid.X - _trackingSystem._selectionSquares[i].X) < 2) && (Math.Abs(centroid.Y - _trackingSystem._selectionSquares[i].Y) < 2))
                {
                    centroidRect = _trackingSystem._selectionSquares[i];
                }
            }

            // Centroid is on the right part somewhere [for sure the rest of the hand would on the left in some interval 40-90]
            var pointIncludesHand = new List<Rectangle>();
            for (int i = 0; i < _trackingSystem._selectionSquares.Count; i++)
            {
                if (centroid.X - _trackingSystem._selectionSquares.ElementAt(i).X < 60 && Math.Abs(centroid.Y - _trackingSystem._selectionSquares.ElementAt(i).Y) < 100 )
                {
                    pointIncludesHand.Add(_trackingSystem._selectionSquares.ElementAt(i));
                }
            }
            // 
            pointIncludesHand = pointIncludesHand.OrderBy(x => x.Height).Take(80).ToList();
            //
            var avarageHandYHeight = orderedMaxX.Take(4).Average(x => x.Y);



            //rects.AddRange(setOfMinimalY);
            //rects.AddRange(orderedMaxX.Take(4));
            //setOfMinimalY.AddRange(properMinZ);
            //rects.AddRange(orderedMaxY);
            //setOfMinimalY.Clear();
            //rects.Add(centroidRect);

           //rects.AddRange(pointIncludesHand);

            return rects;
        }
    }

}
