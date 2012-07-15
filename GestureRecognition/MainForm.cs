using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MackiTools.MackiTools;
using MackiTools.MackiTools.DataGridViewUtil;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Diagnostics;
using GestureRecognition.Data.Models;
using GestureRecognition.Forms;
using GestureRecognition.Logic;
using GestureRecognition.VideoDataAnalyser;

namespace GestureRecognition
{
    public partial class MainForm : Form
    {
        private Timer _videoSourceTime = new Timer();
        private int _videoTimeInSec = 0;
        private Records _selectedRecord = null;
        private Records _recordToSave = null;
        private GesturesForm _gestureForm = null;
        private DataAnalyser _videDataAnalyser = null;

        public MainForm()
        {
            InitializeComponent();
            InitializeData();
        }

        #region Record GridView 

        private void InitializeData()
        {
            RecordsGridView.DataSource = _dataProvider.GetRecords();
            DataGridViewUtil.SetColumn(DataGridViewUtil.GetColumn(RecordsGridView, "AbsolutePath"), "Width:445");
        }

        private void RecordsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (RecordsGridView.Rows.Count > 0)
            { 
                var row = (DataGridView)sender;
                RecordsGridView.Rows[0].Selected = true;
                _selectedRecord = ((List<GestureRecognition.Data.Models.Records>)row.DataSource)[0];
            }
        }

        private void RecirdsGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (RecordsGridView.Rows.Count > 0)
            {
                var row = (DataGridView)sender;
                if (row.CurrentRow != null)
                {
                    _selectedRecord = ((List<GestureRecognition.Data.Models.Records>)row.DataSource)[row.CurrentRow.Index];
                }
            }
        }

        #endregion

        #region  Player

        private void PlayRecord(object sender, EventArgs e)
        {

            VideoCaptureDeviceForm form = new VideoCaptureDeviceForm();

            // create new instance of video data analyser
            _videDataAnalyser = new DataAnalyser(_selectedRecord);

            // create video source
            FileVideoSource fileSource = new FileVideoSource(_selectedRecord.AbsolutePath);

            // open
            OpenVideoSource(fileSource);
        }

        private void OpenVideoSource(IVideoSource fileSource)
        {
            CloseCurrentVideoSource();
            VideoSource.VideoSource = fileSource;
            VideoSource.Start();

            _videoSourceTime.Start();
            _videoTimeInSec = 0;
            _videoSourceTime.Interval = 1000;
            _videoSourceTime.Tick += new EventHandler(VideoSourceTime_Tick);
        }

        private void VideoSource_NewFrame_1(object sender, ref Bitmap image)
        {
            DateTime now = DateTime.Now;
            Graphics g = Graphics.FromImage(image);

            _videDataAnalyser.AddFrame(image);

            // paint current time
            SolidBrush brush = new SolidBrush(Color.Red);
            g.DrawString(now.ToString(), this.Font, brush, new PointF(5, 5));
            brush.Dispose();

            g.Dispose();
        }

        private void VideoSource_PlayingFinished(object sender, ReasonToFinishPlaying reason)
        {
            _videoSourceTime.Stop();
            _videoSourceTime.Dispose();
            _videoSourceTime = new Timer();
        }

        private void VideoSourceTime_Tick(Object myObject,EventArgs myEventArgs)
        {
            VideoSourceTime.Text = "Time  " +  _videoTimeInSec.ToString() + " sec";
            _videoTimeInSec++;
        }

        /// <summary>
        /// Stop currently running video
        /// </summary>
        private void CloseCurrentVideoSource()
        {
            if (VideoSource.VideoSource != null)
            {
                VideoSource.SignalToStop();

                // wait ~ 3 seconds
                for (int i = 0; i < 30; i++)
                {
                    if (!VideoSource.IsRunning)
                        break;
                    System.Threading.Thread.Sleep(100);
                }

                if (VideoSource.IsRunning)
                {
                    VideoSource.Stop();
                }

                VideoSource.VideoSource = null;
            }
        }

        #endregion

        #region Movie [openRecord/Save]

        private void OpenRecord_Click(object sender, EventArgs e)
        {
            VideoCaptureDeviceForm form = new VideoCaptureDeviceForm();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // create video source
                FileVideoSource fileSource = new FileVideoSource(openFileDialog.FileName);
                _recordToSave = new Records { AbsolutePath = openFileDialog.FileName };
                // open it
                OpenVideoSource(fileSource);
            }

            if (RecordsGridView.Rows.Count > 0)
            {
                RecordsGridView.CurrentRow.Selected = false;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (_recordToSave != null)
            {
                Program.logger.Debug(_dataProvider.AddNewRecord(_recordToSave.AbsolutePath, IsRgbCheckBox.Checked));
                InitializeData();
            }
        }
        #endregion

        #region CSV parser

        private void GetSkeletonData_Click(object sender, EventArgs e)
        {
            var dataSetName = _selectedRecord.GetDataSetName();
            var videoName = _selectedRecord.GetVideoName();

            var parser = new CsvParser.CsvParsercs();
            parser.parseCSV("body_parts.csv");
            var validData = parser.GetDataSetVidoeParsedData(dataSetName, videoName);
        }

        #endregion

        private void GesturesRecord_Click(object sender, EventArgs e)
        {
            _gestureForm = new GesturesForm(this._gestureForm, Enums.GestureFormOption.Record, 0);
            _gestureForm.Show();
        }

        private void GesturesLoad_Click(object sender, EventArgs e)
        {
            _gestureForm = new GesturesForm(this._gestureForm, Enums.GestureFormOption.Load, 0);
            _gestureForm.Show();

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Gestures (*.xml)|*.xml";
            dlg.Title = "Load Gestures";
            dlg.Multiselect = true;
            dlg.RestoreDirectory = false;

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                for (int i = 0; i < dlg.FileNames.Length; i++)
                {
                    string name = dlg.FileNames[i];
                    _gestureForm.LoadGesture(name);
                }
            }
        }

        private void UnistrokeRecognizer_Click(object sender, EventArgs e)
        {
            _gestureForm = new GesturesForm(this._gestureForm,
                                            Enums.GestureFormOption.Recognize,
                                            GestureRecognition.UnistrokeRecognizer.Logic.Enums.RecognizeMode.Unistroke_DollarOne);
            _gestureForm.Show();
        }

        private void UnistrokeProtractor_Recognizer(object sender, EventArgs e)
        {
            _gestureForm = new GesturesForm(this._gestureForm,
                                            Enums.GestureFormOption.Recognize,
                                            GestureRecognition.UnistrokeRecognizer.Logic.Enums.RecognizeMode.Unistroke_Protractor);
            _gestureForm.Show();
        }

        private void MultistrokeProtractorRecognizer_Click(object sender, EventArgs e)
        {
            _gestureForm = new GesturesForm(this._gestureForm,
                                Enums.GestureFormOption.RecognizeMultiStroke,
                                GestureRecognition.UnistrokeRecognizer.Logic.Enums.RecognizeMode.Unistroke_Protractor);
            _gestureForm.Show();
        }

        private void BuildSkeleton_Click(object sender, EventArgs e)
        {
            _gestureForm = new GesturesForm(this._gestureForm, Enums.GestureFormOption.RecordSkeletonBuild, 0);
            _gestureForm.Show();
        }

        private void ClearMemory_Click(object sender, EventArgs e)
        {
            GesturesForm.ClearGestures();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (VideoSource.IsRunning)
            {
               
            }
            else
            {
                VideoSource.Start();
            }
          
        }

    }
}
