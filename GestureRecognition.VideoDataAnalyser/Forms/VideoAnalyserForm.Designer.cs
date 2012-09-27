namespace GestureRecognition.VideoDataAnalyser.Forms
{
    partial class VideoAnalyserForm
    {
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
            this.label1 = new System.Windows.Forms.Label();
            this.SquareNumberLabel = new System.Windows.Forms.Label();
            this.SaveSquaresButton = new System.Windows.Forms.Button();
            this.OnlySquaresCheckBox = new System.Windows.Forms.CheckBox();
            this.StopButton = new System.Windows.Forms.Button();
            this.RestartButton = new System.Windows.Forms.Button();
            this.NextFrame = new System.Windows.Forms.Button();
            this.PrevFrame = new System.Windows.Forms.Button();
            this.SlowMotionDelayTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SquareSizeTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CurrentFrameLabel = new System.Windows.Forms.Label();
            this.NoSquares = new System.Windows.Forms.CheckBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.HistogramPictureBox = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LeftHandCheckBox = new System.Windows.Forms.CheckBox();
            this.RightHandCheckBox = new System.Windows.Forms.CheckBox();
            this.LearnGestureButton = new System.Windows.Forms.Button();
            this.LoadGestureClick = new System.Windows.Forms.Button();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.SaveFrameOnClick = new System.Windows.Forms.Button();
            this.HandCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.HistogramPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Squres ";
            // 
            // SquareNumberLabel
            // 
            this.SquareNumberLabel.AutoSize = true;
            this.SquareNumberLabel.Location = new System.Drawing.Point(160, 290);
            this.SquareNumberLabel.Name = "SquareNumberLabel";
            this.SquareNumberLabel.Size = new System.Drawing.Size(13, 13);
            this.SquareNumberLabel.TabIndex = 1;
            this.SquareNumberLabel.Text = "0";
            // 
            // SaveSquaresButton
            // 
            this.SaveSquaresButton.Location = new System.Drawing.Point(16, 381);
            this.SaveSquaresButton.Name = "SaveSquaresButton";
            this.SaveSquaresButton.Size = new System.Drawing.Size(75, 23);
            this.SaveSquaresButton.TabIndex = 2;
            this.SaveSquaresButton.Text = "Save Body";
            this.SaveSquaresButton.UseVisualStyleBackColor = true;
            this.SaveSquaresButton.Click += new System.EventHandler(this.SaveSquaresButton_OnClick);
            // 
            // OnlySquaresCheckBox
            // 
            this.OnlySquaresCheckBox.AutoSize = true;
            this.OnlySquaresCheckBox.Location = new System.Drawing.Point(16, 278);
            this.OnlySquaresCheckBox.Name = "OnlySquaresCheckBox";
            this.OnlySquaresCheckBox.Size = new System.Drawing.Size(89, 17);
            this.OnlySquaresCheckBox.TabIndex = 3;
            this.OnlySquaresCheckBox.Text = "Only Squares";
            this.OnlySquaresCheckBox.UseVisualStyleBackColor = true;
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(16, 323);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 4;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_OnClick);
            // 
            // RestartButton
            // 
            this.RestartButton.Location = new System.Drawing.Point(16, 352);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(75, 23);
            this.RestartButton.TabIndex = 5;
            this.RestartButton.Text = "Restart";
            this.RestartButton.UseVisualStyleBackColor = true;
            this.RestartButton.Click += new System.EventHandler(this.RestartButton_OnClick);
            // 
            // NextFrame
            // 
            this.NextFrame.Location = new System.Drawing.Point(217, 323);
            this.NextFrame.Name = "NextFrame";
            this.NextFrame.Size = new System.Drawing.Size(75, 23);
            this.NextFrame.TabIndex = 6;
            this.NextFrame.Text = "Next Frame";
            this.NextFrame.UseVisualStyleBackColor = true;
            this.NextFrame.Click += new System.EventHandler(this.NextFrame_OnClick);
            // 
            // PrevFrame
            // 
            this.PrevFrame.Location = new System.Drawing.Point(123, 323);
            this.PrevFrame.Name = "PrevFrame";
            this.PrevFrame.Size = new System.Drawing.Size(88, 23);
            this.PrevFrame.TabIndex = 7;
            this.PrevFrame.Text = "Previous Frame";
            this.PrevFrame.UseVisualStyleBackColor = true;
            this.PrevFrame.Click += new System.EventHandler(this.PrevFrame_OnClick);
            // 
            // SlowMotionDelayTextbox
            // 
            this.SlowMotionDelayTextbox.Location = new System.Drawing.Point(217, 354);
            this.SlowMotionDelayTextbox.Name = "SlowMotionDelayTextbox";
            this.SlowMotionDelayTextbox.Size = new System.Drawing.Size(75, 20);
            this.SlowMotionDelayTextbox.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 357);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Slow Motion Delay";
            // 
            // SquareSizeTextbox
            // 
            this.SquareSizeTextbox.Location = new System.Drawing.Point(258, 286);
            this.SquareSizeTextbox.Name = "SquareSizeTextbox";
            this.SquareSizeTextbox.Size = new System.Drawing.Size(33, 20);
            this.SquareSizeTextbox.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Square Size";
            // 
            // CurrentFrameLabel
            // 
            this.CurrentFrameLabel.AutoSize = true;
            this.CurrentFrameLabel.Location = new System.Drawing.Point(13, 262);
            this.CurrentFrameLabel.Name = "CurrentFrameLabel";
            this.CurrentFrameLabel.Size = new System.Drawing.Size(36, 13);
            this.CurrentFrameLabel.TabIndex = 12;
            this.CurrentFrameLabel.Text = "Frame";
            // 
            // NoSquares
            // 
            this.NoSquares.AutoSize = true;
            this.NoSquares.Location = new System.Drawing.Point(16, 300);
            this.NoSquares.Name = "NoSquares";
            this.NoSquares.Size = new System.Drawing.Size(82, 17);
            this.NoSquares.TabIndex = 13;
            this.NoSquares.Text = "No Squares";
            this.NoSquares.UseVisualStyleBackColor = true;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(686, 435);
            this.shapeContainer1.TabIndex = 14;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 321;
            this.lineShape1.X2 = 319;
            this.lineShape1.Y1 = 278;
            this.lineShape1.Y2 = 410;
            // 
            // HistogramPictureBox
            // 
            this.HistogramPictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.HistogramPictureBox.Location = new System.Drawing.Point(348, 25);
            this.HistogramPictureBox.Name = "HistogramPictureBox";
            this.HistogramPictureBox.Size = new System.Drawing.Size(324, 184);
            this.HistogramPictureBox.TabIndex = 15;
            this.HistogramPictureBox.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Histogram";
            // 
            // LeftHandCheckBox
            // 
            this.LeftHandCheckBox.AutoSize = true;
            this.LeftHandCheckBox.Location = new System.Drawing.Point(348, 278);
            this.LeftHandCheckBox.Name = "LeftHandCheckBox";
            this.LeftHandCheckBox.Size = new System.Drawing.Size(71, 17);
            this.LeftHandCheckBox.TabIndex = 17;
            this.LeftHandCheckBox.Text = "Left hand";
            this.LeftHandCheckBox.UseVisualStyleBackColor = true;
            // 
            // RightHandCheckBox
            // 
            this.RightHandCheckBox.AutoSize = true;
            this.RightHandCheckBox.Location = new System.Drawing.Point(434, 278);
            this.RightHandCheckBox.Name = "RightHandCheckBox";
            this.RightHandCheckBox.Size = new System.Drawing.Size(80, 17);
            this.RightHandCheckBox.TabIndex = 18;
            this.RightHandCheckBox.Text = "Right Hand";
            this.RightHandCheckBox.UseVisualStyleBackColor = true;
            // 
            // LearnGestureButton
            // 
            this.LearnGestureButton.Location = new System.Drawing.Point(348, 334);
            this.LearnGestureButton.Name = "LearnGestureButton";
            this.LearnGestureButton.Size = new System.Drawing.Size(119, 23);
            this.LearnGestureButton.TabIndex = 20;
            this.LearnGestureButton.Text = "Learn Gesture";
            this.LearnGestureButton.UseVisualStyleBackColor = true;
            this.LearnGestureButton.Click += new System.EventHandler(this.LearnGestureClick);
            // 
            // LoadGestureClick
            // 
            this.LoadGestureClick.Location = new System.Drawing.Point(578, 323);
            this.LoadGestureClick.Name = "LoadGestureClick";
            this.LoadGestureClick.Size = new System.Drawing.Size(94, 23);
            this.LoadGestureClick.TabIndex = 21;
            this.LoadGestureClick.Text = "Load Gesture";
            this.LoadGestureClick.UseVisualStyleBackColor = true;
            this.LoadGestureClick.Click += new System.EventHandler(this.LoadGestureCLick);
            // 
            // lineShape2
            // 
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 705;
            this.lineShape2.X2 = 704;
            this.lineShape2.Y1 = 25;
            this.lineShape2.Y2 = 406;
            // 
            // SaveFrameOnClick
            // 
            this.SaveFrameOnClick.Location = new System.Drawing.Point(578, 272);
            this.SaveFrameOnClick.Name = "SaveFrameOnClick";
            this.SaveFrameOnClick.Size = new System.Drawing.Size(94, 23);
            this.SaveFrameOnClick.TabIndex = 22;
            this.SaveFrameOnClick.Text = "Save Frame";
            this.SaveFrameOnClick.UseVisualStyleBackColor = true;
            this.SaveFrameOnClick.Click += new System.EventHandler(this.SaveFrameOnClick_Click);
            // 
            // HandCheckBox
            // 
            this.HandCheckBox.AutoSize = true;
            this.HandCheckBox.Location = new System.Drawing.Point(348, 301);
            this.HandCheckBox.Name = "HandCheckBox";
            this.HandCheckBox.Size = new System.Drawing.Size(52, 17);
            this.HandCheckBox.TabIndex = 23;
            this.HandCheckBox.Text = "Head";
            this.HandCheckBox.UseVisualStyleBackColor = true;
            // 
            // VideoAnalyserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 435);
            this.Controls.Add(this.HandCheckBox);
            this.Controls.Add(this.SaveFrameOnClick);
            this.Controls.Add(this.LoadGestureClick);
            this.Controls.Add(this.LearnGestureButton);
            this.Controls.Add(this.RightHandCheckBox);
            this.Controls.Add(this.LeftHandCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.HistogramPictureBox);
            this.Controls.Add(this.NoSquares);
            this.Controls.Add(this.CurrentFrameLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SquareSizeTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SlowMotionDelayTextbox);
            this.Controls.Add(this.PrevFrame);
            this.Controls.Add(this.NextFrame);
            this.Controls.Add(this.RestartButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.OnlySquaresCheckBox);
            this.Controls.Add(this.SaveSquaresButton);
            this.Controls.Add(this.SquareNumberLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "VideoAnalyserForm";
            this.Text = "VideoAnalyserForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormIsClosed);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.HistogramPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label SquareNumberLabel;
        private System.Windows.Forms.Button SaveSquaresButton;
        private System.Windows.Forms.CheckBox OnlySquaresCheckBox;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button RestartButton;
        private System.Windows.Forms.Button NextFrame;
        private System.Windows.Forms.Button PrevFrame;
        private System.Windows.Forms.TextBox SlowMotionDelayTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SquareSizeTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label CurrentFrameLabel;
        private System.Windows.Forms.CheckBox NoSquares;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.PictureBox HistogramPictureBox;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.CheckBox LeftHandCheckBox;
        public System.Windows.Forms.CheckBox RightHandCheckBox;
        private System.Windows.Forms.Button LearnGestureButton;
        private System.Windows.Forms.Button LoadGestureClick;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private System.Windows.Forms.Button SaveFrameOnClick;
        public System.Windows.Forms.CheckBox HandCheckBox;
    }
}