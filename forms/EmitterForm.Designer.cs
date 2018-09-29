namespace Lynx2DEngine.forms
{
    partial class EmitterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmitterForm));
            this.label5 = new System.Windows.Forms.Label();
            this.duration = new System.Windows.Forms.NumericUpDown();
            this.amount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.y = new System.Windows.Forms.NumericUpDown();
            this.x = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.maxX = new System.Windows.Forms.NumericUpDown();
            this.minX = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.maxY = new System.Windows.Forms.NumericUpDown();
            this.minY = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.maxSize = new System.Windows.Forms.NumericUpDown();
            this.minSize = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.duration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.amount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(85, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "General";
            // 
            // duration
            // 
            this.duration.Location = new System.Drawing.Point(125, 160);
            this.duration.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.duration.Name = "duration";
            this.duration.Size = new System.Drawing.Size(72, 20);
            this.duration.TabIndex = 53;
            this.duration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.duration.ValueChanged += new System.EventHandler(this.duration_ValueChanged);
            // 
            // amount
            // 
            this.amount.Location = new System.Drawing.Point(30, 160);
            this.amount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(72, 20);
            this.amount.TabIndex = 52;
            this.amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.amount.ValueChanged += new System.EventHandler(this.amount_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Amount";
            // 
            // y
            // 
            this.y.Location = new System.Drawing.Point(147, 34);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(72, 20);
            this.y.TabIndex = 50;
            this.y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.y.ValueChanged += new System.EventHandler(this.y_ValueChanged);
            // 
            // x
            // 
            this.x.Location = new System.Drawing.Point(67, 34);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(72, 20);
            this.x.TabIndex = 49;
            this.x.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.x.ValueChanged += new System.EventHandler(this.x_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Position";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Duration";
            // 
            // maxX
            // 
            this.maxX.Location = new System.Drawing.Point(147, 60);
            this.maxX.Name = "maxX";
            this.maxX.Size = new System.Drawing.Size(72, 20);
            this.maxX.TabIndex = 58;
            this.maxX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maxX.ValueChanged += new System.EventHandler(this.maxX_ValueChanged);
            // 
            // minX
            // 
            this.minX.Location = new System.Drawing.Point(67, 60);
            this.minX.Name = "minX";
            this.minX.Size = new System.Drawing.Size(72, 20);
            this.minX.TabIndex = 57;
            this.minX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.minX.ValueChanged += new System.EventHandler(this.minX_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 56;
            this.label4.Text = "XV Range";
            // 
            // maxY
            // 
            this.maxY.Location = new System.Drawing.Point(147, 86);
            this.maxY.Name = "maxY";
            this.maxY.Size = new System.Drawing.Size(72, 20);
            this.maxY.TabIndex = 61;
            this.maxY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maxY.ValueChanged += new System.EventHandler(this.maxY_ValueChanged);
            // 
            // minY
            // 
            this.minY.Location = new System.Drawing.Point(67, 86);
            this.minY.Name = "minY";
            this.minY.Size = new System.Drawing.Size(72, 20);
            this.minY.TabIndex = 60;
            this.minY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.minY.ValueChanged += new System.EventHandler(this.minY_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 59;
            this.label6.Text = "YV Range";
            // 
            // maxSize
            // 
            this.maxSize.Location = new System.Drawing.Point(147, 112);
            this.maxSize.Name = "maxSize";
            this.maxSize.Size = new System.Drawing.Size(72, 20);
            this.maxSize.TabIndex = 64;
            this.maxSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.maxSize.ValueChanged += new System.EventHandler(this.maxSize_ValueChanged);
            // 
            // minSize
            // 
            this.minSize.Location = new System.Drawing.Point(67, 112);
            this.minSize.Name = "minSize";
            this.minSize.Size = new System.Drawing.Size(72, 20);
            this.minSize.TabIndex = 63;
            this.minSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.minSize.ValueChanged += new System.EventHandler(this.minSize_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 62;
            this.label7.Text = "Size Range";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel2.LinkColor = System.Drawing.Color.Blue;
            this.linkLabel2.Location = new System.Drawing.Point(5, 224);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(47, 13);
            this.linkLabel2.TabIndex = 66;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Rename";
            this.linkLabel2.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.Red;
            this.linkLabel1.Location = new System.Drawing.Point(50, 224);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(38, 13);
            this.linkLabel1.TabIndex = 65;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Delete";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Red;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // EmitterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 246);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.maxSize);
            this.Controls.Add(this.minSize);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.maxY);
            this.Controls.Add(this.minY);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.maxX);
            this.Controls.Add(this.minX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.duration);
            this.Controls.Add(this.amount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.y);
            this.Controls.Add(this.x);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(250, 285);
            this.MinimumSize = new System.Drawing.Size(250, 285);
            this.Name = "EmitterForm";
            this.Text = "EmitterForm";
            this.Load += new System.EventHandler(this.EmitterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.duration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.amount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown duration;
        private System.Windows.Forms.NumericUpDown amount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown y;
        private System.Windows.Forms.NumericUpDown x;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown maxX;
        private System.Windows.Forms.NumericUpDown minX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown maxY;
        private System.Windows.Forms.NumericUpDown minY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown maxSize;
        private System.Windows.Forms.NumericUpDown minSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}