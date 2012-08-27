using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GestureRecognition.CsvParser;
using GestureRecognition.Data.Models;
using GestureRecognition.VideoDataAnalyser.Forms;
using GestureRecognition.BodyTracking;
using GestureRecognition.UnistrokeRecognizer.Logic;
using GestureRecognition.Data.DataSerialization;
using AForge.Imaging;

namespace GestureRecognition.VideoDataAnalyser
{
    public class DataAnalyser
    {
        #region Fields
 
        public List<VideoFrames> _videoFrame {get; set;}
        public TrackingSystem _trackingSystem { get; set; }
        public List<Points> _bodyDepth { get; set; }
        private MaximaTrackingSystem _maximaTrackingSystem { get; set; }

        private CsvParsercs _cvsParser = null;
        private Records _record = null;
        private VideoAnalyserForm _videoAnalyserForm = null;
        private Graphics graphics = null;

        private int _minimumDistanceFromCamera = 0;
        private int _maximalDistanceFromCamera = 0;
        private int _accuracy = 0;

        private bool _playWithAutoSave = false;
        private int _squareSize = 5;

        #endregion

        #region Constructors

        public DataAnalyser(Records record)
        {
            _videoFrame =  new List<VideoFrames>();
            _record = record;

            GetDepthCameraInfo();
            CreateVideoForm();

            graphics = _videoAnalyserForm.CreateGraphics();
            _videoAnalyserForm.DataAnalyzer = this;
            _trackingSystem = new TrackingSystem();
            _maximaTrackingSystem = new MaximaTrackingSystem(_trackingSystem);
        }

        private void CreateVideoForm()
        {
            _videoAnalyserForm = new VideoAnalyserForm();
            _videoAnalyserForm.Show();
        }

        #endregion

        #region Methods

        private void GetDepthCameraInfo()
        {
            _cvsParser = new CsvParsercs();
            _cvsParser.parseCSV("depthCameraInfo.csv");

            var depthInfoRow = _cvsParser.GetFirstRow(_record.GetDataSetName(), _record.GetVideoName());

            _minimumDistanceFromCamera = int.Parse(depthInfoRow[2]);
            _maximalDistanceFromCamera = int.Parse(depthInfoRow[3]);
            _accuracy = int.Parse(depthInfoRow[4]);
        }

        public void AddFrame(Bitmap newFrameBitmap, bool playWithAutoSave)
        {
            // additional option 
            _playWithAutoSave = playWithAutoSave;

            // getting depth from video
            _videoFrame.Add(GetVideoFrameFromBitmap(newFrameBitmap));

            if (_videoAnalyserForm.IsPlaying && _videoAnalyserForm.IsSnchronize)
            {
                // getting body from depth
                _bodyDepth = _trackingSystem.GetAvarageDepth(_videoFrame.ElementAt(_videoFrame.Count - 1).DepthArray, 1, _squareSize);

                // store squares ready to save
                _videoAnalyserForm.UpdateFrame(_trackingSystem._selectionSquares);
                _videoAnalyserForm.CurrentFrame = _videoFrame.Count - 1;

                // Draw body (and squares)
                Draw(_videoFrame.ElementAt(_videoFrame.Count - 1).DepthArray, _bodyDepth, newFrameBitmap);
            }
        }

        public void Draw(List<Points> depthArray, List<Points> bodyArrays, Bitmap originalBitmap)
        {
            // update Square size for Drawing
            _squareSize = _videoAnalyserForm.GetSquareSize();

            // get Bod Depth as bitmap then Draw
            var bitmap = GetBitmapFromDepth(depthArray, bodyArrays, originalBitmap);
            graphics.DrawImage(bitmap, 0, 0);
        }

        private Bitmap GetBitmapFromDepth(List<Points> depthArray, List<Points> bodyArray, Bitmap orignalBitmap)
        {
            var bitmap = new Bitmap(320, 240);

            DrawDepth(bodyArray, bitmap);
            //DrawHistogram(bodyArray);
            DrawSquares(bitmap);
            DrawMaximaPoints(bitmap);

            AutoSaving();

            return bitmap;
        }

        private void DrawHistogram(List <Points> bodyArray)
        {
            var bitmap = new Bitmap(320, 240);
            var histogram = _videoAnalyserForm.GetHistogramPictureBox();
            //histogram.Image = 
            int avarageY = 0;
            int row = 0;
           // bodyArray.OrderBy(x => x.X);
            for (int i = 0; i < bodyArray.Count; i++)
            {
                var x = i / 320;
                avarageY = avarageY + (int)bodyArray[i].Z;

                if (x != row)
                {
                    var pixel = Color.FromArgb(255, 255, 100, 100);
                    bitmap.SetPixel(x, avarageY / 2000, pixel);

                    avarageY = 0;
                }

                row = x;
            }

            histogram.Image = bitmap;
        }

        private void DrawMaximaPoints(Bitmap bitmap)
        {
            var bodyParts = _maximaTrackingSystem.Recognize();
            DrawRectangleSquare(bodyParts, bitmap);
        }
        private void DrawDepth(List<Points> bodyArray, Bitmap bitmap)
        {
            if (!_videoAnalyserForm.IsOnlySquare())
            {
                for (int i = 0; i < bodyArray.Count; i++)
                {
                    double color = (double)bodyArray[i].Z / 10;
                    var pixel = Color.FromArgb(255, (byte)color, (byte)color, (byte)color);
                    bitmap.SetPixel((int)bodyArray[i].X, (int)bodyArray[i].Y, pixel);
                }
            }
            else
            {
                for (int i = 0; i < bodyArray.Count; i++)
                {
                    var pixel = Color.FromArgb(255, 0, 0, 0);
                    bitmap.SetPixel((int)bodyArray[i].X, (int)bodyArray[i].Y, pixel);
                }
            }            
        }
        private void DrawSquares(Bitmap bitmap)
        {
            if (_videoAnalyserForm.IsNotSquare() == false)
            {
                for (int i = 0; i < _trackingSystem._selectionSquares.Count; i = i + 1)
                {
                    var pixel2 = Color.FromArgb(255, (byte)(255 * (_trackingSystem._selectionSquares[i].Height / _trackingSystem.GetMaxZ())), 100, 100);

                    //if ((int)_trackingSystem._selectionSquares[i].Height < _trackingSystem.GetMinimumZ() + 150)
                    //{
                    //pixel2 = Color.FromArgb(255, pixel2, 100, 100);
                    //}


                    for (int j = 0; j < _squareSize; j++)
                    {
                        if ((int)_trackingSystem._selectionSquares[i].X + j < 320 && (int)_trackingSystem._selectionSquares[i].Y + j < 240)
                        {
                            try
                            {
                                bitmap.SetPixel((int)_trackingSystem._selectionSquares[i].X + j, (int)_trackingSystem._selectionSquares[i].Y, pixel2);
                                bitmap.SetPixel((int)_trackingSystem._selectionSquares[i].X + +_squareSize, (int)_trackingSystem._selectionSquares[i].Y + j, pixel2);
                                bitmap.SetPixel((int)_trackingSystem._selectionSquares[i].X + 0, (int)_trackingSystem._selectionSquares[i].Y + j, pixel2);
                                bitmap.SetPixel((int)_trackingSystem._selectionSquares[i].X + j, (int)_trackingSystem._selectionSquares[i].Y + _squareSize, pixel2);
                            }
                            catch (Exception e) { }
                        }
                    }

                    _videoAnalyserForm.SetSquareNumber(_trackingSystem._selectionSquares.Count.ToString());
                }
            }
        }
        private void DrawRectangleSquare(List<Rectangle> rects, Bitmap bitmap)
        {
            for (int i = 0; i < rects.Count; i = i + 1)
            {
                Color pixel = Color.Yellow ;
                if (i == 0 || i==1)
                {
                    pixel = Color.FromArgb(255, 50, 250, 50);
                }else if(i == 2 || i==3)
                {
                     pixel = Color.FromArgb(255, 50, 50, 250);
                }
                else if(i == 4 || i==5)
                {
                     pixel = Color.FromArgb(255, 250, 50, 50);
                }

                for (int j = 0; j < _squareSize; j++)
                {
                    if ((int)rects[i].X + j < 320 && (int)rects[i].Y + j < 240)
                    {
                        try
                        {
                            bitmap.SetPixel((int)rects[i].X + j, (int)rects[i].Y, pixel);
                            bitmap.SetPixel((int)rects[i].X + +_squareSize, (int)rects[i].Y + j, pixel);
                            bitmap.SetPixel((int)rects[i].X + 0, (int)rects[i].Y + j, pixel);
                            bitmap.SetPixel((int)rects[i].X + j, (int)rects[i].Y + _squareSize, pixel);
                        }
                        catch (Exception e) { }
                    }
                }
            }
        }
        private VideoFrames GetVideoFrameFromBitmap(Bitmap bitmap)
        {
            var videoFrame = new VideoFrames();
            int avarageFullDepth = 0;

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    var pixel = bitmap.GetPixel(j, i);
                    double v = (pixel.R + pixel.G + pixel.B) / 3;
                    double zDepth = v / 255 * (_maximalDistanceFromCamera - _minimumDistanceFromCamera) + _minimumDistanceFromCamera;

                    var p = new Points(j, i, (int)zDepth, 0);

                    _trackingSystem.GetMaxDepth(p);
                    _trackingSystem.GetMinDepth(p);

                    avarageFullDepth = avarageFullDepth + (int)zDepth;

                    videoFrame.DepthArray.Add(p);
                }
            }

            _trackingSystem.SetAvarageFullDepth(avarageFullDepth / videoFrame.DepthArray.Count);

            return videoFrame;
        }
        private void AutoSaving()
        {
            // autoSave
            if (_playWithAutoSave)
            {
                AutoSave();
            }
        }
        private void AutoSave()
        {
            var fileName = "C:\\Users\\macki\\Desktop\\magisterka\\GestureRecognition\\Output\\Learned\\FullBody\\Auto\\" + DateTime.Now.Ticks.ToString() + ".xml" ;
            SerializeToXml<Rectangle>.Serialize(_trackingSystem._selectionSquares, fileName, false);
        }

        #endregion

    }
}
