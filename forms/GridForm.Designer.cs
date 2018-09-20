namespace Lynx2DEngine.forms
{
    partial class GridForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridForm));
            this.label1 = new System.Windows.Forms.Label();
            this.gridSize = new System.Windows.Forms.NumericUpDown();
            this.gridWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.gridHeight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.gridStrokeSize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gridColor = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.gridSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridStrokeSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Grid Size";
            // 
            // gridSize
            // 
            this.gridSize.Location = new System.Drawing.Point(77, 15);
            this.gridSize.Name = "gridSize";
            this.gridSize.Size = new System.Drawing.Size(79, 20);
            this.gridSize.TabIndex = 1;
            this.gridSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gridSize.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.gridSize.ValueChanged += new System.EventHandler(this.gridSize_ValueChanged);
            // 
            // gridWidth
            // 
            this.gridWidth.Location = new System.Drawing.Point(77, 41);
            this.gridWidth.Name = "gridWidth";
            this.gridWidth.Size = new System.Drawing.Size(79, 20);
            this.gridWidth.TabIndex = 3;
            this.gridWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gridWidth.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.gridWidth.ValueChanged += new System.EventHandler(this.gridWidth_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Grid Width";
            // 
            // gridHeight
            // 
            this.gridHeight.Location = new System.Drawing.Point(243, 41);
            this.gridHeight.Name = "gridHeight";
            this.gridHeight.Size = new System.Drawing.Size(79, 20);
            this.gridHeight.TabIndex = 5;
            this.gridHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gridHeight.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.gridHeight.ValueChanged += new System.EventHandler(this.gridHeight_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(174, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Grid Height";
            // 
            // gridStrokeSize
            // 
            this.gridStrokeSize.Location = new System.Drawing.Point(99, 86);
            this.gridStrokeSize.Name = "gridStrokeSize";
            this.gridStrokeSize.Size = new System.Drawing.Size(57, 20);
            this.gridStrokeSize.TabIndex = 7;
            this.gridStrokeSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gridStrokeSize.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.gridStrokeSize.ValueChanged += new System.EventHandler(this.gridStrokeSize_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Grid Stroke Size";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(178, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Grid Color";
            // 
            // gridColor
            // 
            this.gridColor.Location = new System.Drawing.Point(243, 86);
            this.gridColor.Name = "gridColor";
            this.gridColor.Size = new System.Drawing.Size(29, 18);
            this.gridColor.TabIndex = 9;
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.Location = new System.Drawing.Point(278, 88);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(44, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Change";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // GridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 121);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.gridColor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gridStrokeSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gridHeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gridWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gridSize);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(360, 160);
            this.MinimumSize = new System.Drawing.Size(360, 160);
            this.Name = "GridForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lynx2D Engine - Grid Settings";
            this.Load += new System.EventHandler(this.GridForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridStrokeSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown gridSize;
        private System.Windows.Forms.NumericUpDown gridWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown gridHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown gridStrokeSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel gridColor;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}