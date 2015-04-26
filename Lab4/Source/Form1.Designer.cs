namespace CG_Lab4
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
            this.openButton = new System.Windows.Forms.Button();
            this.pbOriginalImage = new System.Windows.Forms.PictureBox();
            this.pbNewImage = new System.Windows.Forms.PictureBox();
            this.processingBtn = new System.Windows.Forms.Button();
            this.rbErosion = new System.Windows.Forms.RadioButton();
            this.nUDStructElSize = new System.Windows.Forms.NumericUpDown();
            this.rbDilation = new System.Windows.Forms.RadioButton();
            this.rbSquare = new System.Windows.Forms.RadioButton();
            this.rbCross = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbClosing = new System.Windows.Forms.RadioButton();
            this.rbOpening = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbMorphology = new System.Windows.Forms.RadioButton();
            this.rbMax = new System.Windows.Forms.RadioButton();
            this.rbMin = new System.Windows.Forms.RadioButton();
            this.rbMedian = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginalImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDStructElSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(13, 347);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.openButton_MouseClick);
            // 
            // pbOriginalImage
            // 
            this.pbOriginalImage.Location = new System.Drawing.Point(13, 13);
            this.pbOriginalImage.Name = "pbOriginalImage";
            this.pbOriginalImage.Size = new System.Drawing.Size(366, 328);
            this.pbOriginalImage.TabIndex = 1;
            this.pbOriginalImage.TabStop = false;
            // 
            // pbNewImage
            // 
            this.pbNewImage.Location = new System.Drawing.Point(385, 13);
            this.pbNewImage.Name = "pbNewImage";
            this.pbNewImage.Size = new System.Drawing.Size(387, 328);
            this.pbNewImage.TabIndex = 2;
            this.pbNewImage.TabStop = false;
            // 
            // processingBtn
            // 
            this.processingBtn.Enabled = false;
            this.processingBtn.Location = new System.Drawing.Point(794, 318);
            this.processingBtn.Name = "processingBtn";
            this.processingBtn.Size = new System.Drawing.Size(75, 23);
            this.processingBtn.TabIndex = 3;
            this.processingBtn.Text = "Processing";
            this.processingBtn.UseVisualStyleBackColor = true;
            this.processingBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.processingBtn_MouseClick);
            // 
            // rbErosion
            // 
            this.rbErosion.AutoSize = true;
            this.rbErosion.Checked = true;
            this.rbErosion.Location = new System.Drawing.Point(15, 20);
            this.rbErosion.Name = "rbErosion";
            this.rbErosion.Size = new System.Drawing.Size(60, 17);
            this.rbErosion.TabIndex = 4;
            this.rbErosion.TabStop = true;
            this.rbErosion.Text = "Erosion";
            this.rbErosion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbErosion.UseVisualStyleBackColor = true;
            this.rbErosion.CheckedChanged += new System.EventHandler(this.rbMorphology_CheckedChanged);
            // 
            // nUDStructElSize
            // 
            this.nUDStructElSize.Location = new System.Drawing.Point(104, 20);
            this.nUDStructElSize.Name = "nUDStructElSize";
            this.nUDStructElSize.Size = new System.Drawing.Size(90, 21);
            this.nUDStructElSize.TabIndex = 5;
            this.nUDStructElSize.ValueChanged += new System.EventHandler(this.nUDStructElSize_ValueChanged);
            // 
            // rbDilation
            // 
            this.rbDilation.AutoSize = true;
            this.rbDilation.Location = new System.Drawing.Point(15, 43);
            this.rbDilation.Name = "rbDilation";
            this.rbDilation.Size = new System.Drawing.Size(60, 17);
            this.rbDilation.TabIndex = 6;
            this.rbDilation.Text = "Dilation";
            this.rbDilation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbDilation.UseVisualStyleBackColor = true;
            this.rbDilation.CheckedChanged += new System.EventHandler(this.rbMorphology_CheckedChanged);
            // 
            // rbSquare
            // 
            this.rbSquare.AutoSize = true;
            this.rbSquare.Checked = true;
            this.rbSquare.Location = new System.Drawing.Point(15, 20);
            this.rbSquare.Name = "rbSquare";
            this.rbSquare.Size = new System.Drawing.Size(59, 17);
            this.rbSquare.TabIndex = 7;
            this.rbSquare.TabStop = true;
            this.rbSquare.Text = "Square";
            this.rbSquare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbSquare.UseVisualStyleBackColor = true;
            this.rbSquare.CheckedChanged += new System.EventHandler(this.rbStructurElement_CheckedChanged);
            // 
            // rbCross
            // 
            this.rbCross.AutoSize = true;
            this.rbCross.Location = new System.Drawing.Point(14, 43);
            this.rbCross.Name = "rbCross";
            this.rbCross.Size = new System.Drawing.Size(52, 17);
            this.rbCross.TabIndex = 8;
            this.rbCross.Text = "Cross";
            this.rbCross.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbCross.UseVisualStyleBackColor = true;
            this.rbCross.CheckedChanged += new System.EventHandler(this.rbStructurElement_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSquare);
            this.groupBox1.Controls.Add(this.rbCross);
            this.groupBox1.Controls.Add(this.nUDStructElSize);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(782, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 86);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "StructureElements";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbClosing);
            this.groupBox2.Controls.Add(this.rbOpening);
            this.groupBox2.Controls.Add(this.rbErosion);
            this.groupBox2.Controls.Add(this.rbDilation);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(782, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Morphology";
            // 
            // rbClosing
            // 
            this.rbClosing.AutoSize = true;
            this.rbClosing.Location = new System.Drawing.Point(104, 43);
            this.rbClosing.Name = "rbClosing";
            this.rbClosing.Size = new System.Drawing.Size(59, 17);
            this.rbClosing.TabIndex = 8;
            this.rbClosing.Text = "Closing";
            this.rbClosing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbClosing.UseVisualStyleBackColor = true;
            this.rbClosing.CheckedChanged += new System.EventHandler(this.rbMorphology_CheckedChanged);
            // 
            // rbOpening
            // 
            this.rbOpening.AutoSize = true;
            this.rbOpening.Location = new System.Drawing.Point(104, 20);
            this.rbOpening.Name = "rbOpening";
            this.rbOpening.Size = new System.Drawing.Size(65, 17);
            this.rbOpening.TabIndex = 7;
            this.rbOpening.Text = "Opening";
            this.rbOpening.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbOpening.UseVisualStyleBackColor = true;
            this.rbOpening.CheckedChanged += new System.EventHandler(this.rbMorphology_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbMorphology);
            this.groupBox3.Controls.Add(this.rbMax);
            this.groupBox3.Controls.Add(this.rbMin);
            this.groupBox3.Controls.Add(this.rbMedian);
            this.groupBox3.Location = new System.Drawing.Point(782, 206);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filter";
            // 
            // rbMorphology
            // 
            this.rbMorphology.AutoSize = true;
            this.rbMorphology.Location = new System.Drawing.Point(110, 43);
            this.rbMorphology.Name = "rbMorphology";
            this.rbMorphology.Size = new System.Drawing.Size(81, 17);
            this.rbMorphology.TabIndex = 12;
            this.rbMorphology.Text = "Morphology";
            this.rbMorphology.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbMorphology.UseVisualStyleBackColor = true;
            this.rbMorphology.CheckedChanged += new System.EventHandler(this.rbFilter_CheckedChanged);
            // 
            // rbMax
            // 
            this.rbMax.AutoSize = true;
            this.rbMax.Location = new System.Drawing.Point(6, 66);
            this.rbMax.Name = "rbMax";
            this.rbMax.Size = new System.Drawing.Size(45, 17);
            this.rbMax.TabIndex = 11;
            this.rbMax.Text = "Max";
            this.rbMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbMax.UseVisualStyleBackColor = true;
            this.rbMax.CheckedChanged += new System.EventHandler(this.rbFilter_CheckedChanged);
            // 
            // rbMin
            // 
            this.rbMin.AutoSize = true;
            this.rbMin.Location = new System.Drawing.Point(6, 43);
            this.rbMin.Name = "rbMin";
            this.rbMin.Size = new System.Drawing.Size(41, 17);
            this.rbMin.TabIndex = 10;
            this.rbMin.Text = "Min";
            this.rbMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbMin.UseVisualStyleBackColor = true;
            this.rbMin.CheckedChanged += new System.EventHandler(this.rbFilter_CheckedChanged);
            // 
            // rbMedian
            // 
            this.rbMedian.AutoSize = true;
            this.rbMedian.Checked = true;
            this.rbMedian.Location = new System.Drawing.Point(6, 20);
            this.rbMedian.Name = "rbMedian";
            this.rbMedian.Size = new System.Drawing.Size(59, 17);
            this.rbMedian.TabIndex = 9;
            this.rbMedian.TabStop = true;
            this.rbMedian.Text = "Median";
            this.rbMedian.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbMedian.UseVisualStyleBackColor = true;
            this.rbMedian.CheckedChanged += new System.EventHandler(this.rbFilter_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 382);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.processingBtn);
            this.Controls.Add(this.pbNewImage);
            this.Controls.Add(this.pbOriginalImage);
            this.Controls.Add(this.openButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginalImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDStructElSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.PictureBox pbOriginalImage;
        private System.Windows.Forms.PictureBox pbNewImage;
        private System.Windows.Forms.Button processingBtn;
        private System.Windows.Forms.RadioButton rbErosion;
        private System.Windows.Forms.NumericUpDown nUDStructElSize;
        private System.Windows.Forms.RadioButton rbDilation;
        private System.Windows.Forms.RadioButton rbSquare;
        private System.Windows.Forms.RadioButton rbCross;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbClosing;
        private System.Windows.Forms.RadioButton rbOpening;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbMorphology;
        private System.Windows.Forms.RadioButton rbMax;
        private System.Windows.Forms.RadioButton rbMin;
        private System.Windows.Forms.RadioButton rbMedian;
    }
}

