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

namespace GestureRecognition
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            RecordsGridView.DataSource = _dataProvider.GetRecords();
            DataGridViewUtil.SetColumn(DataGridViewUtil.GetColumn(RecordsGridView, "AbsolutePath"), "Width:445");
        }

        private void RecordsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            RecordsGridView.Rows[0].Selected = true;
        }

        private void RecirdsGridView_SelectionChanged(object sender, EventArgs e)
        {
            var row = (DataGridView)sender;
            var recordModel = ((List<GestureRecognition.Data.Models.Records>)row.DataSource)[0];
        }

        #region  Player

        private void PlayRecord(object sender, EventArgs e)
        {
            var row = RecordsGridView.SelectedRows[0];
            var recordModel = ((List<GestureRecognition.Data.Models.Records>)row.DataGridView.DataSource)[0];

            VideoCaptureDeviceForm form = new VideoCaptureDeviceForm();

            // create video source
            FileVideoSource fileSource = new FileVideoSource(recordModel.AbsolutePath);
            this.VideoSource.NewFrame += new AForge.Controls.VideoSourcePlayer.NewFrameHandler(this.VideoSource_NewFrame);
            // open
            OpenVideoSource(fileSource);
        }

        private void OpenVideoSource(IVideoSource fileSource)
        {
            CloseCurrentVideoSource();
            VideoSource.VideoSource = fileSource;
            VideoSource.Start();
        }

        private void VideoSource_NewFrame(object sender, ref Bitmap image)
        {
            DateTime now = DateTime.Now;
            Graphics g = Graphics.FromImage(image);

            // paint current time
            SolidBrush brush = new SolidBrush(Color.Red);
            g.DrawString(now.ToString(), this.Font, brush, new PointF(5, 5));
            brush.Dispose();

            g.Dispose();
        }

        /// <summary>
        /// Stop currently running video
        /// </summary>
        private void CloseCurrentVideoSource()
        {
            if (VideoSource.VideoSource != null)
            {
                VideoSource.SignalToStop();

                System.Threading.Thread.Sleep(3000);

                if (VideoSource.IsRunning)
                {
                    VideoSource.Stop();
                }

                VideoSource.VideoSource = null;
            }
        }

        #endregion
    }
}
