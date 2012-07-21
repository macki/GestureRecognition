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

        public void AddFrame(Bitmap newFrameBitmap)
        {
            var DepthArray = GetVideoFrameFromBitmap(newFrameBitmap);
            _videoFrame.Add(DepthArray);

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

            int rowCounter = 0;
            int columnCounter = 0;
            int rescaleRation = 320 * 240 / depthArray.Count;

            //for (int i = 0; i < depthArray.Count; i++)
            //{
            //    for (int j = 0; j < rescaleRation; j++)
            //    {
            //        double color = (double)depthArray[i].Z / 10;
            //        var pixel = Color.FromArgb(255, (byte)color, (byte)color, (byte)color);
            //        bitmap.SetPixel((int)depthArray[i].X + j, (int)depthArray[i].Y, pixel);
            //    }
            //    rowCounter++;
            //}

            

            var pixel2 = Color.FromArgb(255, 255, 100, 100);

            for (int i = 0; i < 240; i++)
            {
                for (int j = 0; j < 320; j++)
                {
                    var pixel = Color.FromArgb(255, 0, 0, 0);
                    bitmap.SetPixel(j, i, pixel);
                }
            }

            for (int i = 0; i < bodyArray.Count; i++)
            {
                double color = (double)bodyArray[i].Z / 10;
                var pixel = Color.FromArgb(255, (byte)color, (byte)color, (byte)color);
                bitmap.SetPixel((int)bodyArray[i].X, (int)bodyArray[i].Y, pixel);
            }

            var tt = MathHelper.CalculateCentroidBody(bodyArray);

            for (int i = 0; i < 10; i++)
            {
                bitmap.SetPixel((int)tt.X + i, (int)tt.Y + i, pixel2);
            }

            // red squayre
            for (int i = 0; i <  _trackingSystem._selectionSquares.Count; i = i + 1)
            {
                for (int j = 0; j < 20; j++)
                {
                    if ((int)_trackingSystem._selectionSquares[i].X + j < 320 && (int)_trackingSystem._selectionSquares[i].Y + j < 240)
                    {
                        try
                        {
                            bitmap.SetPixel((int)_trackingSystem._selectionSquares[i].X + j, (int)_trackingSystem._selectionSquares[i].Y, pixel2);
                            bitmap.SetPixel((int)_trackingSystem._selectionSquares[i].X + +20, (int)_trackingSystem._selectionSquares[i].Y + j, pixel2);
                            bitmap.SetPixel((int)_trackingSystem._selectionSquares[i].X + 0, (int)_trackingSystem._selectionSquares[i].Y + j, pixel2);
                            bitmap.SetPixel((int)_trackingSystem._selectionSquares[i].X + j, (int)_trackingSystem._selectionSquares[i].Y + 20, pixel2);
                        }
                        catch (Exception e) { }
                    }
                }

                _videoAnalyserForm.SetSquareNumber(_trackingSystem._selectionSquares.Count.ToString());
                //for (int j = 0; j < 20; j++)
                //{
                //    if ((int)_trackingSystem._selectionSquares[i].X + j < 320 && (int)_trackingSystem._selectionSquares[i].Y + j < 240)
                //    {
                //        try
                //        {
                //            bitmap.SetPixel((int)_trackingSystem._selectionSquares[i].X + j, (int)_trackingSystem._selectionSquares[i].Y, pixel2);
                //            bitmap.SetPixel((int)_trackingSystem._selectionSquares[i].X + +20, (int)_trackingSystem._selectionSquares[i].Y + j, pixel2);
                //            bitmap.SetPixel((int)_trackingSystem._selectionSquares[i].X + 0, (int)_trackingSystem._selectionSquares[i].Y + j, pixel2);
                //            bitmap.SetPixel((int)_trackingSystem._selectionSquares[i].X + j, (int)_trackingSystem._selectionSquares[i].Y + 20, pixel2);
                //        }catch(Exception e){}
                //    }
                //}
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

        #endregion

    }
}
