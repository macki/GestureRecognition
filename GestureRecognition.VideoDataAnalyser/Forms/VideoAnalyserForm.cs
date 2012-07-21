using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestureRecognition.VideoDataAnalyser.Forms
{
    public partial class VideoAnalyserForm : Form
    {
        public VideoAnalyserForm()
        {
            InitializeComponent();
        }

        public void SetSquareNumber(string value)
        {
            SquareNumberLabel.Text = value;
        }
    }
}
