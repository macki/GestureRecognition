namespace GestureRecognition.Forms
{
    partial class GesturesForm
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
            this.GestureInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GestureInfo
            // 
            this.GestureInfo.AutoSize = true;
            this.GestureInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.GestureInfo.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.GestureInfo.Location = new System.Drawing.Point(12, 9);
            this.GestureInfo.Name = "GestureInfo";
            this.GestureInfo.Size = new System.Drawing.Size(0, 28);
            this.GestureInfo.TabIndex = 0;
            this.GestureInfo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.GestureInfo.UseCompatibleTextRendering = true;
            // 
            // GesturesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(562, 431);
            this.Controls.Add(this.GestureInfo);
            this.Name = "GesturesForm";
            this.Text = "GesturesForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GestureForm_Closed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GesturesForm_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GesturesForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GesturesForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GesturesForm_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GestureInfo;
    }
}