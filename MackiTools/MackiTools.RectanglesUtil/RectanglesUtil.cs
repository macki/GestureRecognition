using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MackiTools.MackiTools.RectanglesUtil
{
    public class RectanglesUtil
    {
        public static Point GetCentroid(List<System.Drawing.Rectangle> rects)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < rects.Count; i++)
            {
                x = rects[i].X + x;
                y = rects[i].Y + y;
            }
            return new Point((int) x / rects.Count, (int)y / rects.Count);
        }
        public static List<Rectangle> GeRegionWithTheSameDepthVariation(List<Rectangle> rects, Rectangle startingPoint, int depthVariation, int size)
        {
            var foundRects = new List<Rectangle>();
            for (int i = rects.Count - 1; i >= 0; i--)
            {
                // get only point which contains in depthVariation
                if (Math.Abs(startingPoint.Height - rects[i].Height) <= depthVariation)
                {
                    if (Math.Abs(startingPoint.X - rects[i].X) < size && Math.Abs(startingPoint.Y - rects[i].Y) < size)
                    {
                        foundRects.Add(rects[i]);
                    }
                }
            }
            return foundRects;
        }
    }
}
