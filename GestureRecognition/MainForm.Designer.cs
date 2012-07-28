using GestureRecognition.Data.DataProvider;
namespace GestureRecognition
{
    partial class MainForm
    {
        private DataProvider _dataProvider = new DataProvider();

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.VideoPanel = new System.Windows.Forms.Panel();
            this.PauseButton = new System.Windows.Forms.Button();
            this.IsRgbCheckBox = new System.Windows.Forms.CheckBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.VideoSourceTime = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.VideoSource = new AForge.Controls.VideoSourcePlayer();
            this.RecordsGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gesturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildSkeletonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recognizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unistrokeRecognizerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unistrokeProtractorRecognizerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multstrokeProtractorRecognizerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.squarePatternRecognizerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.GetCsvData_button = new System.Windows.Forms.Button();
            this.VideoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecordsGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // VideoPanel
            // 
            this.VideoPanel.Controls.Add(this.PauseButton);
            this.VideoPanel.Controls.Add(this.IsRgbCheckBox);
            this.VideoPanel.Controls.Add(this.SaveButton);
            this.VideoPanel.Controls.Add(this.VideoSourceTime);
            this.VideoPanel.Controls.Add(this.button1);
            this.VideoPanel.Controls.Add(this.VideoSource);
            this.VideoPanel.Location = new System.Drawing.Point(12, 23);
            this.VideoPanel.Name = "VideoPanel";
            this.VideoPanel.Size = new System.Drawing.Size(427, 389);
            this.VideoPanel.TabIndex = 0;
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(335, 352);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(75, 23);
            this.PauseButton.TabIndex = 5;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // IsRgbCheckBox
            // 
            this.IsRgbCheckBox.AutoSize = true;
            this.IsRgbCheckBox.Location = new System.Drawing.Point(19, 351);
            this.IsRgbCheckBox.Name = "IsRgbCheckBox";
            this.IsRgbCheckBox.Size = new System.Drawing.Size(57, 17);
            this.IsRgbCheckBox.TabIndex = 4;
            this.IsRgbCheckBox.Text = "Is Rgb";
            this.IsRgbCheckBox.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(19, 322);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(104, 23);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "Save Record";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // VideoSourceTime
            // 
            this.VideoSourceTime.AutoSize = true;
            this.VideoSourceTime.Location = new System.Drawing.Point(16, 0);
            this.VideoSourceTime.Name = "VideoSourceTime";
            this.VideoSourceTime.Size = new System.Drawing.Size(30, 13);
            this.VideoSourceTime.TabIndex = 3;
            this.VideoSourceTime.Text = "Time";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(335, 322);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Play ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.PlayRecord);
            // 
            // VideoSource
            // 
            this.VideoSource.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.VideoSource.Location = new System.Drawing.Point(19, 16);
            this.VideoSource.Name = "VideoSource";
            this.VideoSource.Size = new System.Drawing.Size(398, 305);
            this.VideoSource.TabIndex = 0;
            this.VideoSource.Text = "videoSourcePlayer1";
            this.VideoSource.VideoSource = null;
            this.VideoSource.NewFrame += new AForge.Controls.VideoSourcePlayer.NewFrameHandler(this.VideoSource_NewFrame_1);
            this.VideoSource.PlayingFinished += new AForge.Video.PlayingFinishedEventHandler(this.VideoSource_PlayingFinished);
            // 
            // RecordsGridView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RecordsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.RecordsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RecordsGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.RecordsGridView.Location = new System.Drawing.Point(12, 418);
            this.RecordsGridView.Name = "RecordsGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RecordsGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.RecordsGridView.Size = new System.Drawing.Size(788, 182);
            this.RecordsGridView.TabIndex = 1;
            this.RecordsGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.RecordsGridView_DataBindingComplete);
            this.RecordsGridView.SelectionChanged += new System.EventHandler(this.RecirdsGridView_SelectionChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gesturesToolStripMenuItem,
            this.recognizeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(971, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openRecordToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openRecordToolStripMenuItem
            // 
            this.openRecordToolStripMenuItem.Name = "openRecordToolStripMenuItem";
            this.openRecordToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.openRecordToolStripMenuItem.Text = "Open Record";
            this.openRecordToolStripMenuItem.Click += new System.EventHandler(this.OpenRecord_Click);
            // 
            // gesturesToolStripMenuItem
            // 
            this.gesturesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.recordToolStripMenuItem,
            this.buildSkeletonToolStripMenuItem,
            this.clearMemoryToolStripMenuItem});
            this.gesturesToolStripMenuItem.Name = "gesturesToolStripMenuItem";
            this.gesturesToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.gesturesToolStripMenuItem.Text = "Gestures";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.GesturesLoad_Click);
            // 
            // recordToolStripMenuItem
            // 
            this.recordToolStripMenuItem.Name = "recordToolStripMenuItem";
            this.recordToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.recordToolStripMenuItem.Text = "Record";
            this.recordToolStripMenuItem.Click += new System.EventHandler(this.GesturesRecord_Click);
            // 
            // buildSkeletonToolStripMenuItem
            // 
            this.buildSkeletonToolStripMenuItem.Name = "buildSkeletonToolStripMenuItem";
            this.buildSkeletonToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.buildSkeletonToolStripMenuItem.Text = "Build Skeleton";
            this.buildSkeletonToolStripMenuItem.Click += new System.EventHandler(this.BuildSkeleton_Click);
            // 
            // clearMemoryToolStripMenuItem
            // 
            this.clearMemoryToolStripMenuItem.Name = "clearMemoryToolStripMenuItem";
            this.clearMemoryToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.clearMemoryToolStripMenuItem.Text = "Clear Memory";
            this.clearMemoryToolStripMenuItem.Click += new System.EventHandler(this.ClearMemory_Click);
            // 
            // recognizeToolStripMenuItem
            // 
            this.recognizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unistrokeRecognizerToolStripMenuItem,
            this.unistrokeProtractorRecognizerToolStripMenuItem,
            this.multstrokeProtractorRecognizerToolStripMenuItem,
            this.squarePatternRecognizerToolStripMenuItem});
            this.recognizeToolStripMenuItem.Name = "recognizeToolStripMenuItem";
            this.recognizeToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.recognizeToolStripMenuItem.Text = "Recognize";
            // 
            // unistrokeRecognizerToolStripMenuItem
            // 
            this.unistrokeRecognizerToolStripMenuItem.Name = "unistrokeRecognizerToolStripMenuItem";
            this.unistrokeRecognizerToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.unistrokeRecognizerToolStripMenuItem.Text = "Unistroke Recognizer";
            this.unistrokeRecognizerToolStripMenuItem.Click += new System.EventHandler(this.UnistrokeRecognizer_Click);
            // 
            // unistrokeProtractorRecognizerToolStripMenuItem
            // 
            this.unistrokeProtractorRecognizerToolStripMenuItem.Name = "unistrokeProtractorRecognizerToolStripMenuItem";
            this.unistrokeProtractorRecognizerToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.unistrokeProtractorRecognizerToolStripMenuItem.Text = "Unistroke Protractor Recognizer";
            this.unistrokeProtractorRecognizerToolStripMenuItem.Click += new System.EventHandler(this.UnistrokeProtractor_Recognizer);
            // 
            // multstrokeProtractorRecognizerToolStripMenuItem
            // 
            this.multstrokeProtractorRecognizerToolStripMenuItem.Name = "multstrokeProtractorRecognizerToolStripMenuItem";
            this.multstrokeProtractorRecognizerToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.multstrokeProtractorRecognizerToolStripMenuItem.Text = "Multstroke Protractor Recognizer";
            this.multstrokeProtractorRecognizerToolStripMenuItem.Click += new System.EventHandler(this.MultistrokeProtractorRecognizer_Click);
            // 
            // squarePatternRecognizerToolStripMenuItem
            // 
            this.squarePatternRecognizerToolStripMenuItem.Name = "squarePatternRecognizerToolStripMenuItem";
            this.squarePatternRecognizerToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.squarePatternRecognizerToolStripMenuItem.Text = "Square Pattern recognizer";
            this.squarePatternRecognizerToolStripMenuItem.Click += new System.EventHandler(this.squarePatternRecognizerToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // GetCsvData_button
            // 
            this.GetCsvData_button.Location = new System.Drawing.Point(809, 418);
            this.GetCsvData_button.Name = "GetCsvData_button";
            this.GetCsvData_button.Size = new System.Drawing.Size(148, 23);
            this.GetCsvData_button.TabIndex = 5;
            this.GetCsvData_button.Text = "Get Skeleton Data";
            this.GetCsvData_button.UseVisualStyleBackColor = true;
            this.GetCsvData_button.Click += new System.EventHandler(this.GetSkeletonData_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 610);
            this.Controls.Add(this.GetCsvData_button);
            this.Controls.Add(this.RecordsGridView);
            this.Controls.Add(this.VideoPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.VideoPanel.ResumeLayout(false);
            this.VideoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecordsGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel VideoPanel;
        private AForge.Controls.VideoSourcePlayer VideoSource;
        private System.Windows.Forms.DataGridView RecordsGridView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label VideoSourceTime;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRecordToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.CheckBox IsRgbCheckBox;
        private System.Windows.Forms.Button GetCsvData_button;
        private System.Windows.Forms.ToolStripMenuItem gesturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recognizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unistrokeRecognizerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unistrokeProtractorRecognizerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem multstrokeProtractorRecognizerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildSkeletonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearMemoryToolStripMenuItem;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.ToolStripMenuItem squarePatternRecognizerToolStripMenuItem;

    }
}