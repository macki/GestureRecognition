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
        private List<Rectangle> _loadedRectsBody;
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
            LoadBasicFullBody();
        }

        private void LoadBasicFullBody()
        {
            var squareSkeleton = SerializeToXml<Rectangle>.Deserialize(@"Learned\FullBody\1");
            _loadedRectsBody = new List<Rectangle>(squareSkeleton);
            DrawLoadedSkeletonSquareBody(squareSkeleton);
        }

        #endregion

        #region Events
        
        private void button1_Click(object sender, EventArgs e)
        {
            DrawRecognizerGrid(int.Parse(SquareSizeTextBox.Text));
        }

        private void LoadSkeleton_OnClick(object sender, EventArgs e)
        {
            ClearData();

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Gestures (*.xml)|*.xml";
            dlg.Title = "Load Gestures";
            dlg.Multiselect = false;
            dlg.RestoreDirectory = false;
            dlg.InitialDirectory = System.Configuration.ConfigurationSettings.AppSettings.GetValues("SkeletonBodySquares")[0];

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var squareSkeleton = SerializeToXml<Rectangle>.Deserialize(dlg.FileName, false);
                _loadedRectsBody = new List<Rectangle>(squareSkeleton);
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

        private void ResetGribds_OnClick(object sender, MouseEventArgs e)
        {
            if (_loadedRectsBody != null)
            {
                ClearData();
                DrawLoadedSkeletonSquareBody(_loadedRectsBody);
            }
            else
            {
                MessageBox.Show("Load body first plz...");
            }
        }

        private void ClearData()
        {
            _selectedRects.Clear();
            _rects.Clear();
            _selectedRectsPattern.Clear();
        }

        #endregion

        #region Events Learn

        private void Head_Learn_Click(object sender, EventArgs e)
        {
            _sqauresRecognizer.Learn(_selectedRects, _selectedRectsPattern, Data.Enums.BodyPart.Head);
        }
        private void TorsLearn_OnClick(object sender, EventArgs e)
        {
            _sqauresRecognizer.Learn(_selectedRects, _selectedRectsPattern, Data.Enums.BodyPart.Torso);
        }
        private void HandsLearn_OnClick(object sender, EventArgs e)
        {
            _sqauresRecognizer.Learn(_selectedRects, _selectedRectsPattern, Data.Enums.BodyPart.Hands);
        }
        private void LeftHandLarnButton(object sender, EventArgs e)
        {
            _sqauresRecognizer.Learn(_selectedRects, _selectedRectsPattern, Data.Enums.BodyPart.LeftHand);
        }
        private void RightHandLearn_OnClick(object sender, EventArgs e)
        {
            _sqauresRecognizer.Learn(_selectedRects, _selectedRectsPattern, Data.Enums.BodyPart.RightHand);
        }
        private void LegsLearn(object sender, EventArgs e)
        {
            _sqauresRecognizer.Learn(_selectedRects, _selectedRectsPattern, Data.Enums.BodyPart.Legs);
        }

        #endregion

        #region Events Recognize

        private void RecognizeHead_Click(object sender, EventArgs e)
        {
            var recognizeArea = _sqauresRecognizer.Recognize(_selectedRects, _selectedRectsPattern, Data.Enums.BodyPart.Head);
            DrawRecognizeBodyPart(recognizeArea.ToList());
        }
        private void TorsButtonRecognize_Onclick(object sender, EventArgs e)
        {
            var recognizeArea = _sqauresRecognizer.Recognize(_selectedRects, _selectedRectsPattern, Data.Enums.BodyPart.Torso);
            DrawRecognizeBodyPart(recognizeArea.ToList());
        }
        private void HandsButtonRecognize_Onclic(object sender, EventArgs e)
        {
            var recognizeArea = _sqauresRecognizer.Recognize(_selectedRects, _selectedRectsPattern, Data.Enums.BodyPart.Hands);
            DrawRecognizeBodyPart(recognizeArea.ToList());
        }
        private void LeftHand_ButtonRecognize_Onclic(object sender, EventArgs e)
        {
            var recognizeArea = _sqauresRecognizer.Recognize(_selectedRects, _selectedRectsPattern, Data.Enums.BodyPart.LeftHand);
            DrawRecognizeBodyPart(recognizeArea.ToList());
        }
        private void RightHand_ButtonRecognize_Onclic(object sender, EventArgs e)
        {
            var recognizeArea = _sqauresRecognizer.Recognize(_selectedRects, _selectedRectsPattern, Data.Enums.BodyPart.RightHand);
            DrawRecognizeBodyPart(recognizeArea.ToList());
        }
        private void Legs_ButtonRecognize_Onclic(object sender, EventArgs e)
        {
            var recognizeArea = _sqauresRecognizer.Recognize(_selectedRects, _selectedRectsPattern, Data.Enums.BodyPart.Legs);
            DrawRecognizeBodyPart(recognizeArea.ToList());
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
                if (_rects.Where(x=>x.X == item.X && x.Y == item.Y).Count() > 0)
                {
                    formGraphics.FillRectangle(Brushes.Red, _rects.Where(x=>x.X == item.X && x.Y == item.Y).First());
                    var rect = new Rectangle(item.X, item.Y, _stepSize, item.Height);
                    _selectedRects.Add(rect);
                }
            }
        }

        private void DrawRecognizeBodyPart(List<Rectangle> bodyParts)
        {
            System.Drawing.Graphics formGraphics = this.CreateGraphics();
            foreach (var item in bodyParts)
            {
                var rect = new Rectangle(item.X, item.Y, item.Width, item.Width);
                formGraphics.FillRectangle(Brushes.Green, rect);
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

        private void SetScoreLabel(double score)
        {
            ScoreLabel.Text = score.ToString();
        }

        #endregion

    }
}
