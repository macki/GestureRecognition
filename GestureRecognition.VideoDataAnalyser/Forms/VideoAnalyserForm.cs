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
using GestureRecognition.Data.Application;

namespace GestureRecognition.VideoDataAnalyser.Forms
{
    public partial class VideoAnalyserForm : Form
    {
        public DataAnalyser DataAnalyzer { get; set; }
        public bool IsSnchronize { get; set; }
        public GestureModel LoadedGesture { get; set; }
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
        private List<Rectangle> _rects = new List<Rectangle>();
        private List<Rectangle> _loadedRectsBody;
        private List<Rectangle> _selectedRects = new List<Rectangle>();
        private List<Rectangle> _selectedRectsPattern = new List<Rectangle>();
        private int _cW = 320;
        private int _cH = 240;
        private int _stepSize = 0;
        private string _loadedGestureFileName;

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
            if (LoadedGesture == null)
            {
                DataAnalyzer._bodyDepth = DataAnalyzer._trackingSystem.GetAvarageDepth(DataAnalyzer._videoFrame.ElementAt(CurrentFrame).DepthArray, 1, GetSquareSize());
                UpdateFrame(DataAnalyzer._trackingSystem._selectionSquares);
                DataAnalyzer.Draw(DataAnalyzer._videoFrame[_currentFrame].DepthArray,
                                  DataAnalyzer._bodyDepth,
                                  new Bitmap(320, 240));
            }
            else
            {
                var rectToDraw = new List<Rectangle>();
                rectToDraw.AddRange(LoadedGesture.BodyPartSquaresRegions_LeftHand.ElementAt(_currentFrame));
                rectToDraw.AddRange(LoadedGesture.BodyPartSquaresRegions_RightHand.ElementAt(_currentFrame));

                DrawLoadedSkeletonSquareBody(LoadedGesture.WholeBody.ElementAt(_currentFrame), rectToDraw);

                if (LeftHandCheckBox.Checked)
                {
                    _selectedRects = LoadedGesture.BodyPartSquaresRegions_LeftHand.ElementAt(CurrentFrame);
                }
                else if (RightHandCheckBox.Checked)
                {
                    _selectedRects = LoadedGesture.BodyPartSquaresRegions_RightHand.ElementAt(CurrentFrame);
                }
                else if (HandCheckBox.Checked)
                {
                    _selectedRects = LoadedGesture.BodyPartSquaresRegions_Head.ElementAt(CurrentFrame);
                }
            }

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

        private void DrawLoadedSkeletonSquareBody(List<Rectangle> wholeBody, List<Rectangle> bodyPart )
        {
            System.Drawing.Graphics formGraphics = this.CreateGraphics();

            int squareSkeleletonSize = wholeBody[0].Width;
            DrawRecognizerGrid(squareSkeleletonSize);

            foreach (var item in wholeBody)
            {
                if (_rects.Where(x => x.X == item.X && x.Y == item.Y).Count() > 0)
                {
                    var brush = new SolidBrush(Color.FromArgb(255, 100 + item.Height / 15, 100, 100));

                   formGraphics.FillRectangle(brush, _rects.Where(x => x.X == item.X && x.Y == item.Y).First());
                   var rect = new Rectangle(item.X, item.Y, _stepSize, item.Height);
                   foreach(var bItem in bodyPart)
                    {
                        var bRect = new Rectangle(bItem.X, bItem.Y, _stepSize, bItem.Height);

                        if (bRect == rect)
                        {
                            var brush2 = new SolidBrush(Color.FromArgb(255, 100, 100));
                            formGraphics.FillRectangle(brush2, _rects.Where(x => x.X == item.X && x.Y == item.Y).First());
                            _selectedRects.Add(rect);
                        }
                    } 
                }
            }
        }

        private void DrawRecognizerGrid(int squareSize)
        {
            _rects.Clear();
            Refresh();

            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics = this.CreateGraphics();

            _stepSize = squareSize;

            for (int i = 0; i < _cW; i = i + _stepSize)
            {
                for (int j = 0; j < _cH; j = j + _stepSize)
                {
                    var pen = new Pen(Brushes.Black);
                    var r = new Rectangle(i, j, _stepSize, _stepSize);
                    formGraphics.DrawRectangle(pen, r);
                    _rects.Add(r);
                }
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

        private void LearnGestureClick(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Gestures (*.xml)|*.xml";
            saveDialog.Title = "Save Gesture As";
            saveDialog.AddExtension = true;
            saveDialog.RestoreDirectory = false;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                DataAnalyzer.SaveGesture(saveDialog.FileName);
            }    
        }

        private void LoadGestureCLick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Gestures (*.xml)|*.xml";
            dlg.Title = "Load Gestures";
            dlg.Multiselect = false;
            dlg.RestoreDirectory = false;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                _loadedGestureFileName = dlg.FileName;
                LoadedGesture = DataAnalyzer.Load(dlg.FileName);
            }

            _selectedRects.Clear();
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (_rects.Any())
            {
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                Point cursorPos = this.PointToClient(Cursor.Position);
                var frame = _currentFrame - 1;

                for (int i = 0; i < LoadedGesture.WholeBody.ElementAt(frame).Count; i++)
                {
                    if (Math.Abs(LoadedGesture.WholeBody.ElementAt(frame)[i].X - cursorPos.X) <= 5 && Math.Abs(LoadedGesture.WholeBody.ElementAt( frame )[i].Y - cursorPos.Y) <= 5)
                    {
                        var found = false;
                        foreach (var item in _selectedRects)
                        {
                            if (item == LoadedGesture.WholeBody.ElementAt(CurrentFrame)[i])
                            {
                                var rectToDraw = new Rectangle(LoadedGesture.WholeBody.ElementAt( frame )[i].X, LoadedGesture.WholeBody.ElementAt( frame )[i].Y, _rects.ElementAt(0).Width, _rects.ElementAt(0).Height);
                                formGraphics.FillRectangle(Brushes.White, rectToDraw);
                                _selectedRects.Remove(item);
                                found = true;
                                break;
                            }
                        }

                        if (found == false)
                        {
                            var rectToDraw = new Rectangle(LoadedGesture.WholeBody.ElementAt(frame)[i].X, LoadedGesture.WholeBody.ElementAt(frame)[i].Y, _rects.ElementAt(0).Width, _rects.ElementAt(0).Height);
                            formGraphics.FillRectangle(Brushes.Red,rectToDraw );
                            _selectedRects.Add(LoadedGesture.WholeBody.ElementAt(frame)[i]);

                        }
                        break;
                    }
                }
            }
        }

        private void SaveFrameOnClick_Click(object sender, EventArgs e)
        {
            if(LeftHandCheckBox.Checked)
            {
                LoadedGesture.Update(_loadedGestureFileName, CurrentFrame - 1, _selectedRects);
            }else if(RightHandCheckBox.Checked){
                LoadedGesture.Update(_loadedGestureFileName, CurrentFrame - 1, null, _selectedRects);
            }else if(HandCheckBox.Checked)
            {
                LoadedGesture.Update(_loadedGestureFileName, CurrentFrame - 1, null, null, _selectedRects);
            }

            
        }

        #endregion
    }
}
