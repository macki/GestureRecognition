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
            this.VideoSource = new AForge.Controls.VideoSourcePlayer();
            this.RecordsGridView = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.VideoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecordsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // VideoPanel
            // 
            this.VideoPanel.Controls.Add(this.VideoSource);
            this.VideoPanel.Location = new System.Drawing.Point(12, 12);
            this.VideoPanel.Name = "VideoPanel";
            this.VideoPanel.Size = new System.Drawing.Size(443, 338);
            this.VideoPanel.TabIndex = 0;
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
            this.RecordsGridView.Location = new System.Drawing.Point(12, 366);
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(478, 337);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Play ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.PlayRecord);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 612);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.RecordsGridView);
            this.Controls.Add(this.VideoPanel);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.VideoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RecordsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel VideoPanel;
        private AForge.Controls.VideoSourcePlayer VideoSource;
        private System.Windows.Forms.DataGridView RecordsGridView;
        private System.Windows.Forms.Button button1;

    }
}