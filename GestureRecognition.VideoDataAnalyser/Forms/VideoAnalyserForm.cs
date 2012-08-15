using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestureRecognition.Data.DataSerialization;

namespace GestureRecognition.VideoDataAnalyser.Forms
{
    public partial class VideoAnalyserForm : Form
    {
        private List<Rectangle> _squaresBody;

        public VideoAnalyserForm()
        {
            InitializeComponent();
        }

        public void UpdateSquaresBody(List<Rectangle> rects)
        {
            _squaresBody = rects;
        }

        public void SetSquareNumber(string value)
        {
            ThreadSafeFunction(value);
        }

        public bool IsOnlySquare()
        {
            return OnlySquaresCheckBox.Checked;
        }


        private void ThreadSafeFunction(string value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate() { ThreadSafeFunction(value); }));
            }
            else
            {
                SquareNumberLabel.Text = value;
            }
        }

        private void SaveSquaresButton_OnClick(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Gestures (*.xml)|*.xml";
            saveDialog.Title = "Save Gesture As";
            saveDialog.AddExtension = true;
            saveDialog.RestoreDirectory = false;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                SerializeToXml<Rectangle>.Serialize(_squaresBody, saveDialog.FileName, false);
            }        
        }

    }
}
