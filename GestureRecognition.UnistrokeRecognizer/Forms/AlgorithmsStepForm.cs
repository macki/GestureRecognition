using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestureRecognition.Data.Models;

namespace GestureRecognition.UnistrokeRecognizer.Forms
{
    public partial class AlgorithmsStepForm : Form
    {
        public AlgorithmsStepForm()
        {
            InitializeComponent();
        }

        public void DrawPoints(List<Points> points)
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            foreach (var p in points)
            {
                graphics.FillEllipse(Brushes.DarkViolet, new Rectangle((int)p.X, (int)p.Y, 5, 5));
            }          
        }
    }
}
