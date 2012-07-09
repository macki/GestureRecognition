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
    }
}
