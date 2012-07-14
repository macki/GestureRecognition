using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestureRecognition.Data.Models
{
    public class Records
    {
        public int Id { get; set; }

        public string AbsolutePath { get; set; }
        public string RelativePath { get; set; }
        public bool IsRgb { get; set; }

        public string GetVideoName()
        {
            var splitedPath = AbsolutePath.Split('\\');
            var videoName = splitedPath.ElementAt(splitedPath.Count() - 1);
            videoName = videoName.Substring(2, videoName.Length - 2);
            return videoName.Replace(".avi", "");
        }

        public string GetDataSetName()
        {
            var splitedPath = AbsolutePath.Split('\\');
            return splitedPath.ElementAt(splitedPath.Length - 2);
        }
    }
}
