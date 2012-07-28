using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GestureRecognition.Data.Interfaces;
using MackiTools.MackiTools.RectanglesUtil;
using System.ComponentModel.DataAnnotations;


namespace GestureRecognition.Data.Models
{
    public class SelectionSquares : IModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int BodyPart { get; set; }

        [NotMappedAttribute]
        public List<Points> SquarePoints { get; set; }
        [NotMappedAttribute]
        public List<Rectangle> ProperPattern{ get; set; }
        [NotMappedAttribute]
        public List<Rectangle> WholePattern { get; set; }
        [NotMappedAttribute]
        public double BodyRatio { get; set; }
        [NotMappedAttribute]
        public Point FullBodyCentroid { get; set; }
        [NotMappedAttribute]
        public Point PatternCentroid { get; set; }
        [NotMappedAttribute]
        public double PatternWidth { get; set;}
        [NotMappedAttribute]
        public double PatternHeight { get; set;}

        [NotMappedAttribute]
        public List<int> CornerDistanceFromCentroid { get; set; }

        [NotMappedAttribute]
        public double Score { get; set; }

        public SelectionSquares()
        {
            SquarePoints = new List<Points>();
            WholePattern = new List<Rectangle>();
            ProperPattern = new List<Rectangle>();
        }

        public void CalculateBodyRatio()
        {
           BodyRatio =  (double)ProperPattern.Count / (double)WholePattern.Count;
        }
        public void CalculatePatternWidth()
        {
            int minWidth = ProperPattern.Min(x => x.X);
            int maxWidth = ProperPattern.Max(x => x.X);

            PatternWidth = maxWidth - minWidth;
        }
        public void CalculatePatternHeight()
        {
            int minWidth = ProperPattern.Min(x => x.Y);
            int maxWidth = ProperPattern.Max(x => x.Y);

            PatternHeight = maxWidth - minWidth;
        }
        public void CalculateFullBodyCentroid()
        {
            FullBodyCentroid = RectanglesUtil.GetCentroid(WholePattern);
        }
        public void CalculatePatternCentroid()
        {
            PatternCentroid = RectanglesUtil.GetCentroid(ProperPattern);
        }
    }
}
