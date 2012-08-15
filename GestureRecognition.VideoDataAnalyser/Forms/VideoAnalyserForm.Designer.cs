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
            // SaveSquaresButton
            // 
            this.SaveSquaresButton.Location = new System.Drawing.Point(235, 307);
            this.SaveSquaresButton.Name = "SaveSquaresButton";
            this.SaveSquaresButton.Size = new System.Drawing.Size(75, 23);
            this.SaveSquaresButton.TabIndex = 2;
            this.SaveSquaresButton.Text = "Save";
            this.SaveSquaresButton.UseVisualStyleBackColor = true;
            this.SaveSquaresButton.Click += new System.EventHandler(this.SaveSquaresButton_OnClick);
            // 
            // OnlySquaresCheckBox
            // 
            this.OnlySquaresCheckBox.AutoSize = true;
            this.OnlySquaresCheckBox.Location = new System.Drawing.Point(16, 289);
            this.OnlySquaresCheckBox.Name = "OnlySquaresCheckBox";
            this.OnlySquaresCheckBox.Size = new System.Drawing.Size(89, 17);
            this.OnlySquaresCheckBox.TabIndex = 3;
            this.OnlySquaresCheckBox.Text = "Only Squares";
            this.OnlySquaresCheckBox.UseVisualStyleBackColor = true;
            // 
            // VideoAnalyserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 338);
            this.Controls.Add(this.OnlySquaresCheckBox);
            this.Controls.Add(this.SaveSquaresButton);
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
        private System.Windows.Forms.Button SaveSquaresButton;
        private System.Windows.Forms.CheckBox OnlySquaresCheckBox;
    }
}