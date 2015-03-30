namespace CG_Lab2
{
    partial class Form1
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
            this.picture = new System.Windows.Forms.Label();
            this.openFileButton = new System.Windows.Forms.Button();
            this.pbRed = new System.Windows.Forms.PictureBox();
            this.pbGreen = new System.Windows.Forms.PictureBox();
            this.pbBlue = new System.Windows.Forms.PictureBox();
            this.averageRed = new System.Windows.Forms.Label();
            this.averageGreen = new System.Windows.Forms.Label();
            this.averageBlue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBlue)).BeginInit();
            this.SuspendLayout();
            // 
            // picture
            // 
            this.picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture.Location = new System.Drawing.Point(13, 13);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(369, 328);
            this.picture.TabIndex = 0;
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(12, 353);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(75, 23);
            this.openFileButton.TabIndex = 1;
            this.openFileButton.Text = "Open";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // pbRed
            // 
            this.pbRed.Location = new System.Drawing.Point(401, 14);
            this.pbRed.Name = "pbRed";
            this.pbRed.Size = new System.Drawing.Size(256, 124);
            this.pbRed.TabIndex = 2;
            this.pbRed.TabStop = false;
            // 
            // pbGreen
            // 
            this.pbGreen.Location = new System.Drawing.Point(401, 161);
            this.pbGreen.Name = "pbGreen";
            this.pbGreen.Size = new System.Drawing.Size(256, 124);
            this.pbGreen.TabIndex = 3;
            this.pbGreen.TabStop = false;
            // 
            // pbBlue
            // 
            this.pbBlue.Location = new System.Drawing.Point(399, 309);
            this.pbBlue.Name = "pbBlue";
            this.pbBlue.Size = new System.Drawing.Size(256, 124);
            this.pbBlue.TabIndex = 4;
            this.pbBlue.TabStop = false;
            // 
            // averageRed
            // 
            this.averageRed.Location = new System.Drawing.Point(401, 141);
            this.averageRed.Name = "averageRed";
            this.averageRed.Size = new System.Drawing.Size(257, 17);
            this.averageRed.TabIndex = 5;
            // 
            // averageGreen
            // 
            this.averageGreen.Location = new System.Drawing.Point(398, 289);
            this.averageGreen.Name = "averageGreen";
            this.averageGreen.Size = new System.Drawing.Size(257, 17);
            this.averageGreen.TabIndex = 6;
            // 
            // averageBlue
            // 
            this.averageBlue.Location = new System.Drawing.Point(398, 436);
            this.averageBlue.Name = "averageBlue";
            this.averageBlue.Size = new System.Drawing.Size(257, 17);
            this.averageBlue.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 476);
            this.Controls.Add(this.averageBlue);
            this.Controls.Add(this.averageGreen);
            this.Controls.Add(this.averageRed);
            this.Controls.Add(this.pbBlue);
            this.Controls.Add(this.pbGreen);
            this.Controls.Add(this.pbRed);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.picture);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pbRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBlue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label picture;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.PictureBox pbRed;
        private System.Windows.Forms.PictureBox pbGreen;
        private System.Windows.Forms.PictureBox pbBlue;
        private System.Windows.Forms.Label averageRed;
        private System.Windows.Forms.Label averageGreen;
        private System.Windows.Forms.Label averageBlue;
    }
}

