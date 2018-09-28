namespace SQL_Con
{
    partial class video
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(video));
            this.Video1 = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.Video1)).BeginInit();
            this.SuspendLayout();
            // 
            // Video1
            // 
            this.Video1.Enabled = true;
            this.Video1.Location = new System.Drawing.Point(12, 12);
            this.Video1.Name = "Video1";
            this.Video1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Video1.OcxState")));
            this.Video1.Size = new System.Drawing.Size(776, 407);
            this.Video1.TabIndex = 0;
            // 
            // video
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Video1);
            this.Name = "video";
            this.Text = "video";
            this.Load += new System.EventHandler(this.video_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Video1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer Video1;
    }
}