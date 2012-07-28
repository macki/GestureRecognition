namespace GestureRecognition.SquaresRecognizer
{
    partial class Main
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
            this.button1 = new System.Windows.Forms.Button();
            this.SquareSizeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Head_Learn = new System.Windows.Forms.Button();
            this.ActivePatternColor = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.ActivePatternToLearnColor = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SquareCounter = new System.Windows.Forms.Label();
            this.LoadSkeletonButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(540, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Draw Grids";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SquareSizeTextBox
            // 
            this.SquareSizeTextBox.Location = new System.Drawing.Point(540, 125);
            this.SquareSizeTextBox.Name = "SquareSizeTextBox";
            this.SquareSizeTextBox.Size = new System.Drawing.Size(75, 20);
            this.SquareSizeTextBox.TabIndex = 1;
            this.SquareSizeTextBox.Text = "20";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(537, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Square size";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 395);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Head";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.RecognizeHead_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 379);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Recognize ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 327);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Learn";
            // 
            // Head_Learn
            // 
            this.Head_Learn.Location = new System.Drawing.Point(3, 343);
            this.Head_Learn.Name = "Head_Learn";
            this.Head_Learn.Size = new System.Drawing.Size(75, 23);
            this.Head_Learn.TabIndex = 6;
            this.Head_Learn.Text = "Head";
            this.Head_Learn.UseVisualStyleBackColor = true;
            this.Head_Learn.Click += new System.EventHandler(this.Head_Learn_Click);
            // 
            // ActivePatternColor
            // 
            this.ActivePatternColor.BackColor = System.Drawing.Color.Red;
            this.ActivePatternColor.Location = new System.Drawing.Point(590, 170);
            this.ActivePatternColor.Name = "ActivePatternColor";
            this.ActivePatternColor.Size = new System.Drawing.Size(22, 22);
            this.ActivePatternColor.TabIndex = 7;
            this.ActivePatternColor.Click += new System.EventHandler(this.ActivePatternColor_Paint);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(537, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Active";
            // 
            // ActivePatternToLearnColor
            // 
            this.ActivePatternToLearnColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ActivePatternToLearnColor.Location = new System.Drawing.Point(590, 213);
            this.ActivePatternToLearnColor.Name = "ActivePatternToLearnColor";
            this.ActivePatternToLearnColor.Size = new System.Drawing.Size(22, 22);
            this.ActivePatternToLearnColor.TabIndex = 9;
            this.ActivePatternToLearnColor.Click += new System.EventHandler(this.ActivePatternToLearnColor_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(537, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Pattern to Learn";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(539, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Square";
            // 
            // SquareCounter
            // 
            this.SquareCounter.AutoSize = true;
            this.SquareCounter.Location = new System.Drawing.Point(587, 241);
            this.SquareCounter.Name = "SquareCounter";
            this.SquareCounter.Size = new System.Drawing.Size(13, 13);
            this.SquareCounter.TabIndex = 12;
            this.SquareCounter.Text = "0";
            // 
            // LoadSkeletonButton
            // 
            this.LoadSkeletonButton.Location = new System.Drawing.Point(540, 41);
            this.LoadSkeletonButton.Name = "LoadSkeletonButton";
            this.LoadSkeletonButton.Size = new System.Drawing.Size(75, 55);
            this.LoadSkeletonButton.TabIndex = 13;
            this.LoadSkeletonButton.Text = "Load Skeleton";
            this.LoadSkeletonButton.UseVisualStyleBackColor = true;
            this.LoadSkeletonButton.Click += new System.EventHandler(this.LoadSkeleton_OnClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 424);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(137, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "Recognize Full Body";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(624, 453);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.LoadSkeletonButton);
            this.Controls.Add(this.SquareCounter);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ActivePatternToLearnColor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ActivePatternColor);
            this.Controls.Add(this.Head_Learn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SquareSizeTextBox);
            this.Controls.Add(this.button1);
            this.Name = "Main";
            this.Text = "Main";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox SquareSizeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Head_Learn;
        private System.Windows.Forms.Panel ActivePatternColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel ActivePatternToLearnColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label SquareCounter;
        private System.Windows.Forms.Button LoadSkeletonButton;
        private System.Windows.Forms.Button button3;


    }
}