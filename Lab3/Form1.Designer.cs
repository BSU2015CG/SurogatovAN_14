namespace CG_Lab3
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
            this.clearBtn = new System.Windows.Forms.Button();
            this.rbDDA = new System.Windows.Forms.RadioButton();
            this.rbBresenham = new System.Windows.Forms.RadioButton();
            this.rbStepByStep = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(658, 302);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(73, 24);
            this.clearBtn.TabIndex = 1;
            this.clearBtn.TabStop = false;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // rbDDA
            // 
            this.rbDDA.AutoSize = true;
            this.rbDDA.Checked = true;
            this.rbDDA.Location = new System.Drawing.Point(659, 17);
            this.rbDDA.Name = "rbDDA";
            this.rbDDA.Size = new System.Drawing.Size(46, 17);
            this.rbDDA.TabIndex = 2;
            this.rbDDA.Text = "DDA";
            this.rbDDA.UseVisualStyleBackColor = true;
            this.rbDDA.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbBresenham
            // 
            this.rbBresenham.AutoSize = true;
            this.rbBresenham.Location = new System.Drawing.Point(659, 40);
            this.rbBresenham.Name = "rbBresenham";
            this.rbBresenham.Size = new System.Drawing.Size(78, 17);
            this.rbBresenham.TabIndex = 3;
            this.rbBresenham.Text = "Bresenham";
            this.rbBresenham.UseVisualStyleBackColor = true;
            this.rbBresenham.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbStepByStep
            // 
            this.rbStepByStep.AutoSize = true;
            this.rbStepByStep.Location = new System.Drawing.Point(659, 63);
            this.rbStepByStep.Name = "rbStepByStep";
            this.rbStepByStep.Size = new System.Drawing.Size(81, 17);
            this.rbStepByStep.TabIndex = 4;
            this.rbStepByStep.Text = "StepByStep";
            this.rbStepByStep.UseVisualStyleBackColor = true;
            this.rbStepByStep.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(659, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Time: ";
            // 
            // tbTime
            // 
            this.tbTime.Location = new System.Drawing.Point(701, 96);
            this.tbTime.Name = "tbTime";
            this.tbTime.ReadOnly = true;
            this.tbTime.Size = new System.Drawing.Size(98, 21);
            this.tbTime.TabIndex = 6;
            this.tbTime.TabStop = false;
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(26, 17);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(596, 428);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 457);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.tbTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbStepByStep);
            this.Controls.Add(this.rbBresenham);
            this.Controls.Add(this.rbDDA);
            this.Controls.Add(this.clearBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.RadioButton rbDDA;
        private System.Windows.Forms.RadioButton rbBresenham;
        private System.Windows.Forms.RadioButton rbStepByStep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
    }
}

