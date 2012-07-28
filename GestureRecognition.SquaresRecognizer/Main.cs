using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using GestureRecognition.Data.Models;
using GestureRecognition.Data.DataSerialization;

namespace GestureRecognition.SquaresRecognizer
{
    public partial class Main : Form
    {
        private SquaresRecognizer.Logic.SquaresRecognizer _sqauresRecognizer = new SquaresRecognizer.Logic.SquaresRecognizer();
        private List<Rectangle> _rects = new List<Rectangle>();
        private List<Rectangle> _selectedRects = new List<Rectangle>();
        private List<Rectangle> _selectedRectsPattern = new List<Rectangle>();
        private int _cW = 320;
        private int _cH = 240;
        private int _stepSize = 0;

        private bool _isActiveSelecting = true;

        #region Constructors
        
        public Main()
        {
            InitializeComponent();   
        }

        #endregion

        #region Events
        
        private void button1_Click(object sender, EventArgs e)
        {
            DrawRecognizerGrid(int.Parse(SquareSizeTextBox.Text));
        }
        private void LoadSkeleton_OnClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Gestures (*.xml)|*.xml";
            dlg.Title = "Load Gestures";
            dlg.Multiselect = false;
            dlg.RestoreDirectory = false;
            dlg.InitialDirectory = System.Configuration.ConfigurationSettings.AppSettings.GetValues("SkeletonBodySquares")[0];

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var squareSkeleton = SerializeToXml<Rectangle>.Deserialize("test1");
                DrawLoadedSkeletonSquareBody(squareSkeleton);
            }
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            if (_rects.Any())
            {
                System.Drawing.Graphics formGraphics = this.CreateGraphics();
                Point cursorPos = this.PointToClient(Cursor.Position);

                for (int i = 0; i < _rects.Count; i++)
                {
                    if (_rects[i].Contains(cursorPos))
                    {
                        if (_selectedRects.Contains(_rects[i]))
                        {
                            formGraphics.FillRectangle(Brushes.White, _rects[i]);
                            _selectedRects.Remove(_rects[i]);
                        }
                        else if( _selectedRectsPattern.Contains(_rects[i]))
                        {
                            formGraphics.FillRectangle(Brushes.White, _rects[i]);
                            _selectedRectsPattern.Remove(_rects[i]);
                        }
                        else
                        {
                            if (_isActiveSelecting)
                            {
                                formGraphics.FillRectangle(Brushes.Red, _rects[i]);
                                var rect = new Rectangle(_rects[i].X, _rects[i].Y, _stepSize, _stepSize);
                                _selectedRects.Add(rect);
                                break;
                            }
                            else
                            {
                                formGraphics.FillRectangle(Brushes.Blue, _rects[i]);
                                var rect = new Rectangle(_rects[i].X, _rects[i].Y, _stepSize, _stepSize);
                                _selectedRectsPattern.Add(rect);
                                _selectedRects.Add(rect);
                            }
                        }
                    }

                }
            }

            //set other propertiies
            SquareCounter.Text = (_selectedRects.Count).ToString();
        }

        private void ActivePatternColor_Paint(object sender, EventArgs e)
        {
            _isActiveSelecting = true;
        }
        private void ActivePatternToLearnColor_Paint(object sender, EventArgs e)
        {
            _isActiveSelecting = false;
        }

        #endregion

        #region Events Learn

        private void Head_Learn_Click(object sender, EventArgs e)
        {
            _sqauresRecognizer.Learn(_selectedRects, _selectedRectsPattern, Data.Enums.BodyPart.Head);
        }

        #endregion

        #region Events Recognize

        private void RecognizeHead_Click(object sender, EventArgs e)
        {
            _sqauresRecognizer.Recognize(_selectedRects, Data.Enums.BodyPart.Head);
        }

        #endregion


        #region Methods

        private void DrawLoadedSkeletonSquareBody(List<Rectangle> squareSkeleton)
        {
            System.Drawing.Graphics formGraphics = this.CreateGraphics();

            int squareSkeleletonSize = squareSkeleton[0].Width;
            DrawRecognizerGrid(squareSkeleletonSize);

            foreach (var item in squareSkeleton)
            {
                if (_rects.Contains(item))
                {
                    formGraphics.FillRectangle(Brushes.Red, item);
                    var rect = new Rectangle(item.X, item.Y, _stepSize, _stepSize);
                    _selectedRects.Add(rect);
                }
            }
        }

        private void DrawRecognizerGrid(int squareSize)
        {
            _rects.Clear();
            _selectedRectsPattern.Clear();
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

        #endregion



    }
}
