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
    }
}
