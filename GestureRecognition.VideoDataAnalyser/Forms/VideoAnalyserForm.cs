using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestureRecognition.Data.DataSerialization;
using System.Threading;

namespace GestureRecognition.VideoDataAnalyser.Forms
{
    public partial class VideoAnalyserForm : Form
    {
        public DataAnalyser DataAnalyzer { get; set; }
        public bool IsSnchronize { get; set; }
        public Boolean IsPlaying
        {
            get
            {
                return _isPlaying;
            }
            set
            {

            }
        }
        public int CurrentFrame
        {
            get
            {
                return _currentFrame;
            }
            set
            {
                _currentFrame = value;
            }
        }
        public int SquareSize { get; set; }

        private List<Rectangle> _squaresBody;
        private Boolean _isPlaying = true;
        private Boolean _formIsClosed = false;
        private int _currentFrame = 0;

        #region Constructors
       
        public VideoAnalyserForm()
        {
            InitializeComponent();
            IsSnchronize = true;
            SquareSize = 5;

            Thread t = new Thread(() => MainLoop());
            t.IsBackground = true;
            t.Start();
        }

        private void MainLoop()
        {
            do
            {
                if (DataAnalyzer != null)
                {
                    if (IsSnchronize == false && _isPlaying)
                    {
                        Play();
                    }
                }

              Thread.Sleep(100);
            } while (_formIsClosed == false);
        }

        private void Play()
        {
            DataAnalyzer._bodyDepth = DataAnalyzer._trackingSystem.GetAvarageDepth(DataAnalyzer._videoFrame.ElementAt(CurrentFrame).DepthArray, 1, GetSquareSize());
            UpdateFrame(DataAnalyzer._trackingSystem._selectionSquares);
            DataAnalyzer.Draw(DataAnalyzer._videoFrame[_currentFrame].DepthArray,
                              DataAnalyzer._bodyDepth,
                              new Bitmap(320, 240));

            if (DataAnalyzer._videoFrame.Count - 2 > _currentFrame)
            {
               _currentFrame++;
            }

            CurrentFrameLabel.Text = CurrentFrame.ToString();
        }

        #endregion

        /// <summary>
        /// Store Squares Body
        /// </summary>
        /// <param name="rects"></param>
        public void UpdateFrame(List<Rectangle> rects)
        {
            _squaresBody = rects;
            ThreadSafeFunction( CurrentFrame.ToString(), CurrentFrameLabel);
        }

        public bool IsNotSquare()
        {
            return NoSquares.Checked;
        }

        /// <summary>
        /// Set current Square Number in frame window
        /// </summary>
        /// <param name="value"></param>
        public void SetSquareNumber(string value)
        {
            ThreadSafeFunction(value, SquareNumberLabel);
        }

        public PictureBox GetHistogramPictureBox()
        {
            return HistogramPictureBox;
        }

        public int GetSquareSize()
        {
            int squareSize = 0;
            if (int.TryParse(SquareSizeTextbox.Text, out squareSize))
            {
                SquareSize = squareSize;
                return squareSize;
            }
            return 5;
        }

        /// <summary>
        /// Get status os  viewMode [displaying only squares or full depth as well]
        /// </summary>
        /// <returns></returns>
        public bool IsOnlySquare()
        {
            return OnlySquaresCheckBox.Checked;
        }

        /// <summary>
        /// Responsible from updating form not directly from frame class
        /// </summary>
        /// <param name="value"></param>
        private void ThreadSafeFunction(string value, Control formControl)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate() { ThreadSafeFunction(value, formControl); }));
            }
            else
            {
                formControl.Text = value;
            }
        }

        #region Events
        
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

        private void StopButton_OnClick(object sender, EventArgs e)
        {
            if (_isPlaying == true)
            {
                _isPlaying = false;
                IsSnchronize = false;
                ThreadSafeFunction("Play", StopButton);
            }
            else
            {
                _isPlaying = true;
                ThreadSafeFunction("Stop", StopButton);
            }         
        }

        private void RestartButton_OnClick(object sender, EventArgs e)
        {
            _currentFrame = 0;
            _isPlaying = true;
            ThreadSafeFunction("Stop", StopButton);
            IsSnchronize = false;
            
        }

        private void PrevFrame_OnClick(object sender, EventArgs e)
        {
            CurrentFrame = CurrentFrame - 2;
            if (CurrentFrame < 0) CurrentFrame = 0;
            Play();
            _isPlaying = false;
            ThreadSafeFunction("Play", StopButton);
        }

        private void NextFrame_OnClick(object sender, EventArgs e)
        {
            Play();
            _isPlaying = false;
            ThreadSafeFunction("Play", StopButton);
        }

        private void FormIsClosed(object sender, FormClosedEventArgs e)
        {
            _formIsClosed = true;
        }

        #endregion


    }
}
