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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 317);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Squres ";
            // 
            // SquareNumberLabel
            // 
            this.SquareNumberLabel.AutoSize = true;
            this.SquareNumberLabel.Location = new System.Drawing.Point(73, 317);
            this.SquareNumberLabel.Name = "SquareNumberLabel";
            this.SquareNumberLabel.Size = new System.Drawing.Size(13, 13);
            this.SquareNumberLabel.TabIndex = 1;
            this.SquareNumberLabel.Text = "0";
            // 
            // VideoAnalyserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 367);
            this.Controls.Add(this.SquareNumberLabel);
            this.Controls.Add(this.label1);
            this.Name = "VideoAnalyserForm";
            this.Text = "VideoAnalyserForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label SquareNumberLabel;
    }
}