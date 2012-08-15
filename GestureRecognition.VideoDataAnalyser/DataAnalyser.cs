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

namespace GestureRecognition.VideoDataAnalyser
{
    public class DataAnalyser
    {
        #region Fields
 
        private List<VideoFrames> _videoFrame = new List<VideoFrames>();
        private CsvParsercs _cvsParser = null;
        private Records _record = null;
        private VideoAnalyserForm _videoAnalyserForm = null;
        private TrackingSystem _trackingSystem = null;
        private Graphics graphics = null;

        private int _minimumDistanceFromCamera = 0;
        private int _maximalDistanceFromCamera = 0;
        private int _accuracy = 0;

        private bool _playWithAutoSave = false;
        private int _squareSize = 20;

        #endregion

        #region Constructors

        public DataAnalyser(Records record)
        {
            _record = record;

            GetDepthCameraInfo();
            CreateVideoForm();

            graphics = _videoAnalyserForm.CreateGraphics();
            _trackingSystem = new TrackingSystem();
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
            _playWithAutoSave = playWithAutoSave;

            var DepthArray = GetVideoFrameFromBitmap(newFrameBitmap);

            _videoFrame.Add(DepthArray);
            _videoAnalyserForm.UpdateSquaresBody(_trackingSystem._selectionSquares);

            Draw(DepthArray.DepthArray, newFrameBitmap);
            
        }

        private void Draw(List<Points> depthArray, Bitmap originalBitmap)
        {
           var  bodyDepth = _trackingSystem.GetAvarageDepth(depthArray, 1);

           var bitmap = GetBitmapFromDepth(depthArray, bodyDepth, originalBitmap);
            graphics.DrawImage(bitmap, 0, 0);
        }

        private Bitmap GetBitmapFromDepth(List<Points> depthArray, List<Points> bodyArray, Bitmap orignalBitmap)
        {
            var bitmap = new Bitmap(320, 240);
            int rescaleRation = 320 * 240 / depthArray.Count;

            // Draw depth 
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

            // Draw red square
            for (int i = 0; i <  _trackingSystem._selectionSquares.Count; i = i + 1)
            {
                var pixel2 = Color.FromArgb(255, (int)(((int)_trackingSystem._selectionSquares[i].Height) / 10), 100, 100);

                if ((int)_trackingSystem._selectionSquares[i].Height < _trackingSystem.GetMinimumZ() + 150)
                {
                    pixel2 = Color.FromArgb(255, 255, 100, 100);
                }


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

            // autoSave
            if(_playWithAutoSave)
            {
                AutoSave();
            }

                return bitmap;
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

        private void AutoSave()
        {
            var fileName = "C:\\Users\\macki\\Desktop\\magisterka\\GestureRecognition\\Output\\Learned\\FullBody\\Auto\\" + DateTime.Now.Ticks.ToString() + ".xml" ;
            SerializeToXml<Rectangle>.Serialize(_trackingSystem._selectionSquares, fileName, false);
        }


        #endregion

    }
}
