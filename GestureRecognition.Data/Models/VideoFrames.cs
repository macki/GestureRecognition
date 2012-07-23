using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using GestureRecognition.Data.Interfaces;

namespace GestureRecognition.Data.Models
{
    public class VideoFrames : IModels
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public int Msc { get; set; }

        public List<Points> DepthArray { get; set; }

        public Records Record { get; set; }

        public VideoFrames()
        {
            DepthArray = new List<Points>();
        }
    }
}
