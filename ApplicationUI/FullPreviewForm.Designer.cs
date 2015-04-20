namespace ApplicationUI
{
    partial class FullPreviewForm
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
            this.fullPreview_pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.fullPreview_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // fullPreview_pictureBox
            // 
            this.fullPreview_pictureBox.BackColor = System.Drawing.Color.White;
            this.fullPreview_pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fullPreview_pictureBox.Location = new System.Drawing.Point(0, 0);
            this.fullPreview_pictureBox.Name = "fullPreview_pictureBox";
            this.fullPreview_pictureBox.Size = new System.Drawing.Size(284, 261);
            this.fullPreview_pictureBox.TabIndex = 0;
            this.fullPreview_pictureBox.TabStop = false;
            // 
            // FullPreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.fullPreview_pictureBox);
            this.Name = "FullPreviewForm";
            this.Text = "FullPreviewForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FullPreviewForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.fullPreview_pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox fullPreview_pictureBox;
    }
}