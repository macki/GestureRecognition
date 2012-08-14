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
        [NotMappedAttribute]
        public List<double> ComparisonVector { get; set;}

        public SelectionSquares()
        {
            SquarePoints = new List<Points>();
            WholePattern = new List<Rectangle>();
            ProperPattern = new List<Rectangle>();
        }
        public void CalculateBodyParameters()
        {
            this.CalculateBodyRatio();
            this.CalculateFullBodyCentroid();
            this.CalculatePatternCentroid();
            this.CalculatePatternHeight();
            this.CalculatePatternWidth();
        }
        public IEnumerable<double> CompareBodyParameters(SelectionSquares newBody)
        {
            ComparisonVector = new List<double>();

            DiffBodyRatio(ref newBody, ComparisonVector);
            DiffPatternWidth(ref newBody, ComparisonVector);
            DiffPatternHeight(ref newBody, ComparisonVector);
            DiffPatternCentroid(ref newBody, ComparisonVector);

            return ComparisonVector;
        }

        private void CalculateBodyRatio()
        {
           BodyRatio =  (double)ProperPattern.Count / (double)WholePattern.Count;
        }
        private void CalculatePatternWidth()
        {
            int minWidth = ProperPattern.Min(x => x.X);
            int maxWidth = ProperPattern.Max(x => x.X);

            PatternWidth = maxWidth - minWidth;
        }
        private void CalculatePatternHeight()
        {
            int minWidth = ProperPattern.Min(x => x.Y);
            int maxWidth = ProperPattern.Max(x => x.Y);

            PatternHeight = maxWidth - minWidth;
        }
        public void CalculateFullBodyCentroid()
        {
            FullBodyCentroid = RectanglesUtil.GetCentroid(WholePattern);
        }
        private void CalculatePatternCentroid()
        {
            PatternCentroid = RectanglesUtil.GetCentroid(ProperPattern);
        }
        private void DiffBodyRatio(ref SelectionSquares newBody, List<double> ComparisonVector)
        {
            var difBodyRatio = Math.Abs((newBody.BodyRatio - this.BodyRatio));
            ComparisonVector.Add(difBodyRatio);
        }
        private void DiffPatternWidth(ref SelectionSquares newBody, List<double> ComparisonVector)
        {
            var dif = Math.Abs((newBody.PatternWidth - this.PatternWidth));
            ComparisonVector.Add(dif);
        }
        private void DiffPatternCentroid(ref SelectionSquares newBody, List<double> ComparisonVector)
        {
            var difPointX =  Math.Abs((newBody.PatternCentroid.X - this.PatternCentroid.X));
            var difPointY = Math.Abs((newBody.PatternCentroid.Y - this.PatternCentroid.Y));
            ComparisonVector.Add(difPointX);
            ComparisonVector.Add(difPointY);
        }
        private void DiffPatternHeight(ref SelectionSquares newBody, List<double> ComparisonVector)
        {
            var dif = Math.Abs((newBody.PatternHeight - this.PatternHeight));
            ComparisonVector.Add(dif);
        }
    }
}
