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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.VideoPanel = new System.Windows.Forms.Panel();
            this.IsRgbCheckBox = new System.Windows.Forms.CheckBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.VideoSourceTime = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.VideoSource = new AForge.Controls.VideoSourcePlayer();
            this.RecordsGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCsvBodyPartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gesturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.GetCsvData_button = new System.Windows.Forms.Button();
            this.VideoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecordsGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // VideoPanel
            // 
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RecordsGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.RecordsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RecordsGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.RecordsGridView.Location = new System.Drawing.Point(12, 418);
            this.RecordsGridView.Name = "RecordsGridView";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RecordsGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.RecordsGridView.Size = new System.Drawing.Size(788, 182);
            this.RecordsGridView.TabIndex = 1;
            this.RecordsGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.RecordsGridView_DataBindingComplete);
            this.RecordsGridView.SelectionChanged += new System.EventHandler(this.RecirdsGridView_SelectionChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gesturesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(969, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openRecordToolStripMenuItem,
            this.openCsvBodyPartToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openRecordToolStripMenuItem
            // 
            this.openRecordToolStripMenuItem.Name = "openRecordToolStripMenuItem";
            this.openRecordToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.openRecordToolStripMenuItem.Text = "Open Record";
            this.openRecordToolStripMenuItem.Click += new System.EventHandler(this.OpenRecord_Click);
            // 
            // openCsvBodyPartToolStripMenuItem
            // 
            this.openCsvBodyPartToolStripMenuItem.Name = "openCsvBodyPartToolStripMenuItem";
            this.openCsvBodyPartToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.openCsvBodyPartToolStripMenuItem.Text = "Open Csv Body Part";
            this.openCsvBodyPartToolStripMenuItem.Click += new System.EventHandler(this.OpenCsvBodyPart_Click);
            // 
            // gesturesToolStripMenuItem
            // 
            this.gesturesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recordToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.gesturesToolStripMenuItem.Name = "gesturesToolStripMenuItem";
            this.gesturesToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.gesturesToolStripMenuItem.Text = "Gestures";
            // 
            // recordToolStripMenuItem
            // 
            this.recordToolStripMenuItem.Name = "recordToolStripMenuItem";
            this.recordToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.recordToolStripMenuItem.Text = "Record";
            this.recordToolStripMenuItem.Click += new System.EventHandler(this.GesturesRecord_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.GesturesLoad_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.viewToolStripMenuItem.Text = "Recognize";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.GestureRecognize_Click);
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
            this.ClientSize = new System.Drawing.Size(969, 610);
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
        private System.Windows.Forms.ToolStripMenuItem openCsvBodyPartToolStripMenuItem;
        private System.Windows.Forms.Button GetCsvData_button;
        private System.Windows.Forms.ToolStripMenuItem gesturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;

    }
}