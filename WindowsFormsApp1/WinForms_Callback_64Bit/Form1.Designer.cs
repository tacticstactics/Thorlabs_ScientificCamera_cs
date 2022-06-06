namespace Example_DotNet_Camera_Interface
{
    partial class Form1_Callback
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.pictureBoxLiveImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLiveImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxLiveImage
            // 
            this.pictureBoxLiveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxLiveImage.Location = new System.Drawing.Point(10, 9);
            this.pictureBoxLiveImage.Name = "pictureBoxLiveImage";
            this.pictureBoxLiveImage.Size = new System.Drawing.Size(465, 409);
            this.pictureBoxLiveImage.TabIndex = 0;
            this.pictureBoxLiveImage.TabStop = false;
            this.pictureBoxLiveImage.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxLiveImage_Paint);
            // 
            // Form1_Callback
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 426);
            this.Controls.Add(this.pictureBoxLiveImage);
            this.Name = "Form1_Callback";
            this.Text = "TSICamera Callback Example";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLiveImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxLiveImage;
    }
}

