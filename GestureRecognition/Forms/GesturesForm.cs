using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestureRecognition.Logic;
using GestureRecognition.Data.Models;
using System.Xml;
using System.Reflection;
using System.Diagnostics;

namespace GestureRecognition.Forms
{
    public partial class GesturesForm : Form
    {
        #region Fields

        private static List<Gestures> _knownGestures = new List<Gestures>();

        private GesturesForm _gesturesForm;
        private Gestures _newGesture = new Gestures();
        private bool _isDrawing = false;
        private int _mininumPointsValue = 30;
        private Enums.GestureFormOption _gestureOption = 0;
        private GestureRecognition.UnistrokeRecognizer.Logic.Enums.RecognizeMode _recognizeMethods;
        private long _tickingCounterStart = 0;
        private long _tickingCounter = 0;
        private Points _previousPoint = null;

        #endregion

        #region Constructors

        public GesturesForm()
        {
            InitializeComponent();
        }

        public GesturesForm(GesturesForm gesturesForm, Enums.GestureFormOption option, GestureRecognition.UnistrokeRecognizer.Logic.Enums.RecognizeMode recognizeMethod)
        {
            InitializeComponent();

            this._gesturesForm = gesturesForm;
            this._gestureOption = option;
            this._recognizeMethods = recognizeMethod;
            
            BuildSkeletonSave.Visible = false;

            switch (option)
            {
                case Enums.GestureFormOption.Record:
                    {
                        InitRecording();
                    } break;
                case Enums.GestureFormOption.Load:
                    {
                         InitLoading();
                    } break;
                case Enums.GestureFormOption.Recognize:
                    {
                        InitRecognizing();
                    } break;
            }
        }

        private void InitLoading()
        {
            GestureInfo.Text = "Loading...";
        }

        private void InitRecognizing()
        {
            GestureInfo.Text = "Draw a Gesture...";
        }

        /// <summary>
        /// Initlize recording
        /// </summary>
        private void InitRecording()
        {
            GestureInfo.Text = "Recording...";
        }

        #endregion

        #region Events

        private void GestureForm_Closed(object sender, FormClosedEventArgs e)
        {
            this._gesturesForm = null;
        }

        private void GesturesForm_Paint(object sender, PaintEventArgs e)
        {
            if (_newGesture.Points.Count > 0)
            {
                e.Graphics.FillEllipse((_gestureOption == Enums.GestureFormOption.Record) ? Brushes.Firebrick : Brushes.DarkBlue, (int)_newGesture.Points[0].X - 5f, (int)_newGesture.Points[0].Y - 5f, 10f, 10f);
            }

            foreach (Points p in _newGesture.Points)
            {
                e.Graphics.FillEllipse((_gestureOption == Enums.GestureFormOption.Record) ? Brushes.Firebrick : Brushes.DarkBlue, (int)(p.X) - 2f, (int)(p.Y) - 2f, 4f, 4f);
            }
        }

        private void GesturesForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (_gestureOption == Enums.GestureFormOption.Record || _gestureOption == Enums.GestureFormOption.Recognize)
            {
                _isDrawing = true;
                _newGesture.Points.Clear();
                _tickingCounter = 0;
                _tickingCounterStart = DateTime.Now.Ticks;

                Invalidate();
            }
            else if (_gestureOption == Enums.GestureFormOption.RecordSkeletonBuild)
            {
                BuildSkeletonSave.Text = "Save Skeleton";
                BuildSkeletonSave.Visible = true;
                _isDrawing = true;
            }
            else if (_gestureOption == Enums.GestureFormOption.RecognizeMultiStroke)
            {
                BuildSkeletonSave.Text = "Recognize Skeleton";
                BuildSkeletonSave.Visible = true;
                _isDrawing = true;
            }
        }
        private void GesturesForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                if (_gestureOption == Enums.GestureFormOption.Record)
                {
                    GestureInfo.Text = "Recording...";
                }
                else if (_gestureOption == Enums.GestureFormOption.Recognize)
                {
                    GestureInfo.Text = "Recognizing...";
                }
       
                _previousPoint = new Points(e.X, e.Y, 0, (DateTime.Now.Ticks - _tickingCounterStart) / 10000);
                _newGesture.Points.Add(_previousPoint);

                Invalidate(new Rectangle(e.X - 2, e.Y - 2, 4, 4));
            }
        }
        private void GesturesForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                _isDrawing = false;
                    if (_newGesture.Points.Count > _mininumPointsValue)
                    {
                        if (_gestureOption == Enums.GestureFormOption.Record)
                        {
                            SaveFileDialog saveDialog = new SaveFileDialog();
                            saveDialog.Filter = "Gestures (*.xml)|*.xml";
                            saveDialog.Title = "Save Gesture As";
                            saveDialog.AddExtension = true;
                            saveDialog.RestoreDirectory = false;

                            if (saveDialog.ShowDialog(this) == DialogResult.OK)
                            {
                                SaveGesture(saveDialog.FileName, _newGesture.Points);
                            }

                            saveDialog.Dispose();
                            _gestureOption = Enums.GestureFormOption.None;
                            BuildSkeletonSave.Visible = false ;
                            Invalidate();
                        }
                        else if (_gestureOption == Enums.GestureFormOption.Recognize)
                        {
                            Recognize();
                        }
                    else
                    {
                        GestureInfo.Text = "You need more points to draw a gesture (min " + _mininumPointsValue + ")";
                    }
                }
            }
        }

        private void SaveSkeleton_Click(object sender, EventArgs e)
        {
            _isDrawing = false;

            if (_gestureOption == Enums.GestureFormOption.RecordSkeletonBuild)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Gestures (*.xml)|*.xml";
                saveDialog.Title = "Save Gesture As";
                saveDialog.AddExtension = true;
                saveDialog.RestoreDirectory = false;

                if (saveDialog.ShowDialog(this) == DialogResult.OK)
                {
                    SaveGesture(saveDialog.FileName, _newGesture.Points);
                }

                saveDialog.Dispose();
                _gestureOption = Enums.GestureFormOption.None;
                Invalidate();
            }
            else if (_gestureOption == Enums.GestureFormOption.RecognizeMultiStroke)
            {
                Recognize();
            }

            Invalidate();
            _newGesture.Points.Clear();
        }

        #endregion

        #region Methods

        #region Save/Load Gesture

        public bool LoadGesture(string filename)
        {
            bool success = true;
            XmlTextReader reader = null;
            try
            {
                reader = new XmlTextReader(filename);
                reader.WhitespaceHandling = WhitespaceHandling.None;
                reader.MoveToContent();

                var splittedFileName = filename.Split('\\');
                _knownGestures.Add(new Gestures(splittedFileName[splittedFileName.Length - 1], ReadGesture(reader)));
                DrawPoints();
            }
            catch (XmlException xex)
            {
                Console.Write(xex.Message);
                success = false;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                success = false;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return success;
        }
        private bool SaveGesture(string filename, List<Points> points)
        {
            // do the xml writing
            bool success = true;
            XmlTextWriter writer = null;
            try
            {
                writer = new XmlTextWriter(filename, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument(true);
                writer.WriteStartElement("Gesture");
                writer.WriteAttributeString("Name", filename);
                writer.WriteAttributeString("NumPts", XmlConvert.ToString(points.Count));
                writer.WriteAttributeString("Millseconds", XmlConvert.ToString(points[points.Count - 1].MSecTime - points[0].MSecTime));
                writer.WriteAttributeString("AppName", Assembly.GetExecutingAssembly().GetName().Name);
                writer.WriteAttributeString("AppVer", Assembly.GetExecutingAssembly().GetName().Version.ToString());
                writer.WriteAttributeString("Date", DateTime.Now.ToLongDateString());
                writer.WriteAttributeString("TimeOfDay", DateTime.Now.ToLongTimeString());

                foreach (var p in points)
                {
                    writer.WriteStartElement("Point");
                    writer.WriteAttributeString("X", XmlConvert.ToString(p.X));
                    writer.WriteAttributeString("Y", XmlConvert.ToString(p.Y));
                    //writer.WriteAttributeString("Z", XmlConvert.ToString(p.Z));
                    writer.WriteAttributeString("T", XmlConvert.ToString(p.MSecTime));
                    writer.WriteEndElement(); // <Point />
                }

                writer.WriteEndDocument(); // </Gesture>
            }
            catch (XmlException xex)
            {
                Console.Write(xex.Message);
                success = false;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                success = false;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
            return success;
        }
        private List<Points> ReadGesture(XmlTextReader reader)
        {
            string name = reader.GetAttribute("Name");
            var points = new List<Points>(XmlConvert.ToInt32(reader.GetAttribute("NumPts")));

            reader.Read();
            Debug.Assert(reader.LocalName == "Point");

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                var p = new Points();
                p.X = XmlConvert.ToSingle(reader.GetAttribute("X"));
                p.Y = XmlConvert.ToSingle(reader.GetAttribute("Y"));
                //p.Z = XmlConvert.ToSingle(reader.GetAttribute("Z"));
                p.MSecTime = XmlConvert.ToInt64(reader.GetAttribute("T"));
                points.Add(p);
                reader.ReadStartElement("Point");
            }

            // transform
            return points;
        }

        #endregion

        public static void ClearGestures()
        {
            _knownGestures.Clear();
            _knownGestures = new List<Gestures>();        
        }

        private void Recognize()
        {
            if (!_knownGestures.Any())
            {
                GestureInfo.Text = "Load known gestures to memory first...";
            }
            else
            {
                var recgonizer = new UnistrokeRecognizer.UnistrokeRecognizer();
                var gesture =  recgonizer.Recognize(_newGesture.Points, _knownGestures, _recognizeMethods);
                GestureInfo.Text = gesture.Name;
            }

        }

        private void DrawPoints()
        {
            System.Drawing.Graphics graphics = this.CreateGraphics();
            foreach (var g in _knownGestures)
            {
                foreach (var p in g.Points)
                {
                    graphics.FillEllipse(Brushes.DarkViolet, new Rectangle((int)p.X, (int)p.Y, 5, 5));
                }
            }
        }

        #endregion



    }
}
