namespace Lynx2DEngine
{
    partial class SpriteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteForm));
            this.h = new System.Windows.Forms.NumericUpDown();
            this.w = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cX = new System.Windows.Forms.NumericUpDown();
            this.cY = new System.Windows.Forms.NumericUpDown();
            this.cH = new System.Windows.Forms.NumericUpDown();
            this.cW = new System.Windows.Forms.NumericUpDown();
            this.clipped = new System.Windows.Forms.CheckBox();
            this.rotation = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.source = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.h)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.w)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotation)).BeginInit();
            this.SuspendLayout();
            // 
            // h
            // 
            this.h.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.h.Enabled = false;
            this.h.Location = new System.Drawing.Point(147, 27);
            this.h.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.h.Name = "h";
            this.h.Size = new System.Drawing.Size(72, 20);
            this.h.TabIndex = 18;
            this.h.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // w
            // 
            this.w.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.w.Enabled = false;
            this.w.Location = new System.Drawing.Point(67, 27);
            this.w.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.w.Name = "w";
            this.w.Size = new System.Drawing.Size(72, 20);
            this.w.TabIndex = 17;
            this.w.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Clip";
            // 
            // cX
            // 
            this.cX.BackColor = System.Drawing.SystemColors.Window;
            this.cX.Location = new System.Drawing.Point(67, 59);
            this.cX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.cX.Name = "cX";
            this.cX.Size = new System.Drawing.Size(72, 20);
            this.cX.TabIndex = 23;
            this.cX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cX.ValueChanged += new System.EventHandler(this.cX_ValueChanged);
            // 
            // cY
            // 
            this.cY.BackColor = System.Drawing.SystemColors.Window;
            this.cY.Location = new System.Drawing.Point(147, 59);
            this.cY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.cY.Name = "cY";
            this.cY.Size = new System.Drawing.Size(72, 20);
            this.cY.TabIndex = 24;
            this.cY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cY.ValueChanged += new System.EventHandler(this.cY_ValueChanged);
            // 
            // cH
            // 
            this.cH.BackColor = System.Drawing.SystemColors.Window;
            this.cH.Location = new System.Drawing.Point(147, 85);
            this.cH.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.cH.Name = "cH";
            this.cH.Size = new System.Drawing.Size(72, 20);
            this.cH.TabIndex = 26;
            this.cH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cH.ValueChanged += new System.EventHandler(this.cH_ValueChanged);
            // 
            // cW
            // 
            this.cW.BackColor = System.Drawing.SystemColors.Window;
            this.cW.Location = new System.Drawing.Point(67, 85);
            this.cW.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.cW.Name = "cW";
            this.cW.Size = new System.Drawing.Size(72, 20);
            this.cW.TabIndex = 25;
            this.cW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cW.ValueChanged += new System.EventHandler(this.cW_ValueChanged);
            // 
            // clipped
            // 
            this.clipped.AutoSize = true;
            this.clipped.Location = new System.Drawing.Point(67, 111);
            this.clipped.Name = "clipped";
            this.clipped.Size = new System.Drawing.Size(72, 17);
            this.clipped.TabIndex = 27;
            this.clipped.Text = "Is Clipped";
            this.clipped.UseVisualStyleBackColor = true;
            this.clipped.CheckedChanged += new System.EventHandler(this.clipped_CheckedChanged);
            // 
            // rotation
            // 
            this.rotation.BackColor = System.Drawing.SystemColors.Window;
            this.rotation.Location = new System.Drawing.Point(67, 135);
            this.rotation.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.rotation.Name = "rotation";
            this.rotation.Size = new System.Drawing.Size(72, 20);
            this.rotation.TabIndex = 29;
            this.rotation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rotation.ValueChanged += new System.EventHandler(this.rotation_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Rotation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(83, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "General";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.Red;
            this.linkLabel1.Location = new System.Drawing.Point(57, 240);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(38, 13);
            this.linkLabel1.TabIndex = 33;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Delete";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Red;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel2.LinkColor = System.Drawing.Color.Blue;
            this.linkLabel2.Location = new System.Drawing.Point(9, 240);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(47, 13);
            this.linkLabel2.TabIndex = 34;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Rename";
            this.linkLabel2.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // source
            // 
            this.source.FormattingEnabled = true;
            this.source.Location = new System.Drawing.Point(67, 167);
            this.source.Name = "source";
            this.source.Size = new System.Drawing.Size(152, 21);
            this.source.TabIndex = 35;
            this.source.SelectedIndexChanged += new System.EventHandler(this.sprite_SelectedIndexChanged);
            // 
            // SpriteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 261);
            this.Controls.Add(this.source);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rotation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.clipped);
            this.Controls.Add(this.cH);
            this.Controls.Add(this.cW);
            this.Controls.Add(this.cY);
            this.Controls.Add(this.cX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.h);
            this.Controls.Add(this.w);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(250, 300);
            this.MinimumSize = new System.Drawing.Size(250, 300);
            this.Name = "SpriteForm";
            this.Text = "Sprite Form";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SpriteForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.h)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.w)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown h;
        private System.Windows.Forms.NumericUpDown w;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown cX;
        private System.Windows.Forms.NumericUpDown cY;
        private System.Windows.Forms.NumericUpDown cH;
        private System.Windows.Forms.NumericUpDown cW;
        private System.Windows.Forms.CheckBox clipped;
        private System.Windows.Forms.NumericUpDown rotation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.ComboBox source;
    }
}