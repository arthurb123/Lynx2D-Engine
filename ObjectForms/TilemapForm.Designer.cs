namespace Lynx2DEngine.forms
{
    partial class TilemapForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TilemapForm));
            this.label1 = new System.Windows.Forms.Label();
            this.sprite = new System.Windows.Forms.ComboBox();
            this.drawTimer = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.tilesize = new System.Windows.Forms.NumericUpDown();
            this.layer = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.x = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.y = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.scale = new System.Windows.Forms.NumericUpDown();
            this.collides = new System.Windows.Forms.CheckBox();
            this.refresh = new System.Windows.Forms.Button();
            this.tileSelection = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.tilesize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileSelection)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sprite";
            // 
            // sprite
            // 
            this.sprite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sprite.FormattingEnabled = true;
            this.sprite.Location = new System.Drawing.Point(50, 31);
            this.sprite.MaxDropDownItems = 100;
            this.sprite.Name = "sprite";
            this.sprite.Size = new System.Drawing.Size(131, 21);
            this.sprite.TabIndex = 1;
            this.sprite.SelectedIndexChanged += new System.EventHandler(this.sprite_SelectedIndexChanged);
            // 
            // drawTimer
            // 
            this.drawTimer.Interval = 32;
            this.drawTimer.Tick += new System.EventHandler(this.drawTimer_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tilesize";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tilesize
            // 
            this.tilesize.Location = new System.Drawing.Point(262, 32);
            this.tilesize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.tilesize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tilesize.Name = "tilesize";
            this.tilesize.Size = new System.Drawing.Size(58, 20);
            this.tilesize.TabIndex = 4;
            this.tilesize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tilesize.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.tilesize.ValueChanged += new System.EventHandler(this.tilesize_ValueChanged);
            // 
            // layer
            // 
            this.layer.Location = new System.Drawing.Point(262, 5);
            this.layer.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.layer.Name = "layer";
            this.layer.Size = new System.Drawing.Size(58, 20);
            this.layer.TabIndex = 6;
            this.layer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.layer.ValueChanged += new System.EventHandler(this.layer_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(219, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Layer";
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel2.LinkColor = System.Drawing.Color.Blue;
            this.linkLabel2.Location = new System.Drawing.Point(439, 38);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(47, 13);
            this.linkLabel2.TabIndex = 38;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Rename";
            this.linkLabel2.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.Red;
            this.linkLabel1.Location = new System.Drawing.Point(484, 38);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(38, 13);
            this.linkLabel1.TabIndex = 37;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Delete";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Red;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // x
            // 
            this.x.Location = new System.Drawing.Point(36, 6);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(70, 20);
            this.x.TabIndex = 40;
            this.x.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.x.ValueChanged += new System.EventHandler(this.x_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "XT";
            // 
            // y
            // 
            this.y.Location = new System.Drawing.Point(137, 5);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(70, 20);
            this.y.TabIndex = 42;
            this.y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.y.ValueChanged += new System.EventHandler(this.y_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(112, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "YT";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(332, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "Scale";
            // 
            // scale
            // 
            this.scale.Location = new System.Drawing.Point(372, 5);
            this.scale.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.scale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.scale.Name = "scale";
            this.scale.Size = new System.Drawing.Size(58, 20);
            this.scale.TabIndex = 45;
            this.scale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.scale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.scale.ValueChanged += new System.EventHandler(this.scale_ValueChanged);
            // 
            // collides
            // 
            this.collides.AutoSize = true;
            this.collides.Location = new System.Drawing.Point(335, 34);
            this.collides.Name = "collides";
            this.collides.Size = new System.Drawing.Size(62, 17);
            this.collides.TabIndex = 46;
            this.collides.Text = "Collides";
            this.collides.UseVisualStyleBackColor = true;
            this.collides.CheckedChanged += new System.EventHandler(this.collides_CheckedChanged);
            // 
            // refresh
            // 
            this.refresh.BackColor = System.Drawing.SystemColors.Control;
            this.refresh.BackgroundImage = global::Lynx2DEngine.Properties.Resources.refresh;
            this.refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refresh.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.refresh.FlatAppearance.BorderSize = 0;
            this.refresh.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.refresh.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refresh.Location = new System.Drawing.Point(187, 33);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(16, 16);
            this.refresh.TabIndex = 43;
            this.refresh.UseVisualStyleBackColor = false;
            this.refresh.Click += new System.EventHandler(this.refresh_Click);
            // 
            // tileSelection
            // 
            this.tileSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tileSelection.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.tileSelection.Location = new System.Drawing.Point(0, 57);
            this.tileSelection.Name = "tileSelection";
            this.tileSelection.Size = new System.Drawing.Size(534, 265);
            this.tileSelection.TabIndex = 2;
            this.tileSelection.TabStop = false;
            // 
            // TilemapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 321);
            this.Controls.Add(this.collides);
            this.Controls.Add(this.scale);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.refresh);
            this.Controls.Add(this.y);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.x);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.layer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tilesize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tileSelection);
            this.Controls.Add(this.sprite);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(550, 360);
            this.Name = "TilemapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TilemapForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TilemapForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tilesize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileSelection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox sprite;
        private System.Windows.Forms.Timer drawTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown tilesize;
        private System.Windows.Forms.PictureBox tileSelection;
        private System.Windows.Forms.NumericUpDown layer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.NumericUpDown x;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown y;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown scale;
        private System.Windows.Forms.CheckBox collides;
    }
}