namespace Lynx2DEngine
{
    partial class GameObjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameObjectForm));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.layer = new System.Windows.Forms.NumericUpDown();
            this.visible = new System.Windows.Forms.CheckBox();
            this.x = new System.Windows.Forms.NumericUpDown();
            this.y = new System.Windows.Forms.NumericUpDown();
            this.h = new System.Windows.Forms.NumericUpDown();
            this.w = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.sprite = new System.Windows.Forms.ComboBox();
            this.collider = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.removeCollider = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.layer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.h)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.w)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Position";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Layer";
            // 
            // layer
            // 
            this.layer.Location = new System.Drawing.Point(70, 92);
            this.layer.Name = "layer";
            this.layer.Size = new System.Drawing.Size(72, 20);
            this.layer.TabIndex = 6;
            this.layer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.layer.ValueChanged += new System.EventHandler(this.layer_ValueChanged);
            // 
            // visible
            // 
            this.visible.AutoSize = true;
            this.visible.Location = new System.Drawing.Point(155, 94);
            this.visible.Name = "visible";
            this.visible.Size = new System.Drawing.Size(56, 17);
            this.visible.TabIndex = 7;
            this.visible.Text = "Visible";
            this.visible.UseVisualStyleBackColor = true;
            this.visible.CheckedChanged += new System.EventHandler(this.visible_CheckedChanged);
            // 
            // x
            // 
            this.x.Location = new System.Drawing.Point(70, 26);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(72, 20);
            this.x.TabIndex = 8;
            this.x.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.x.ValueChanged += new System.EventHandler(this.x_ValueChanged);
            // 
            // y
            // 
            this.y.Location = new System.Drawing.Point(150, 26);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(72, 20);
            this.y.TabIndex = 9;
            this.y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.y.ValueChanged += new System.EventHandler(this.y_ValueChanged);
            // 
            // h
            // 
            this.h.Location = new System.Drawing.Point(150, 58);
            this.h.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.h.Name = "h";
            this.h.Size = new System.Drawing.Size(72, 20);
            this.h.TabIndex = 12;
            this.h.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.h.ValueChanged += new System.EventHandler(this.h_ValueChanged);
            // 
            // w
            // 
            this.w.Location = new System.Drawing.Point(70, 58);
            this.w.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.w.Name = "w";
            this.w.Size = new System.Drawing.Size(72, 20);
            this.w.TabIndex = 11;
            this.w.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.w.ValueChanged += new System.EventHandler(this.w_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Size";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(88, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "General";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.Red;
            this.linkLabel1.Location = new System.Drawing.Point(54, 259);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(38, 13);
            this.linkLabel1.TabIndex = 32;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Delete";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Red;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Sprite";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel2.LinkColor = System.Drawing.Color.Blue;
            this.linkLabel2.Location = new System.Drawing.Point(9, 259);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(47, 13);
            this.linkLabel2.TabIndex = 36;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Rename";
            this.linkLabel2.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // sprite
            // 
            this.sprite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sprite.FormattingEnabled = true;
            this.sprite.Location = new System.Drawing.Point(70, 124);
            this.sprite.Name = "sprite";
            this.sprite.Size = new System.Drawing.Size(151, 21);
            this.sprite.TabIndex = 37;
            this.sprite.SelectedIndexChanged += new System.EventHandler(this.sprite_SelectedIndexChanged);
            // 
            // collider
            // 
            this.collider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.collider.FormattingEnabled = true;
            this.collider.Location = new System.Drawing.Point(69, 156);
            this.collider.Name = "collider";
            this.collider.Size = new System.Drawing.Size(126, 21);
            this.collider.TabIndex = 39;
            this.collider.SelectedIndexChanged += new System.EventHandler(this.collider_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 38;
            this.label6.Text = "Collider";
            // 
            // removeCollider
            // 
            this.removeCollider.AutoSize = true;
            this.removeCollider.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeCollider.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.removeCollider.LinkColor = System.Drawing.Color.Red;
            this.removeCollider.Location = new System.Drawing.Point(195, 156);
            this.removeCollider.Name = "removeCollider";
            this.removeCollider.Size = new System.Drawing.Size(32, 18);
            this.removeCollider.TabIndex = 40;
            this.removeCollider.TabStop = true;
            this.removeCollider.Text = "[X]";
            this.removeCollider.VisitedLinkColor = System.Drawing.Color.Red;
            this.removeCollider.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.removeCollider_LinkClicked);
            // 
            // GameObjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 281);
            this.Controls.Add(this.collider);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.sprite);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.h);
            this.Controls.Add(this.w);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.y);
            this.Controls.Add(this.x);
            this.Controls.Add(this.visible);
            this.Controls.Add(this.layer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.removeCollider);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(250, 320);
            this.MinimumSize = new System.Drawing.Size(250, 320);
            this.Name = "GameObjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game Object";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.GameObjectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.h)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.w)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown layer;
        private System.Windows.Forms.CheckBox visible;
        private System.Windows.Forms.NumericUpDown x;
        private System.Windows.Forms.NumericUpDown y;
        private System.Windows.Forms.NumericUpDown h;
        private System.Windows.Forms.NumericUpDown w;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.ComboBox sprite;
        private System.Windows.Forms.ComboBox collider;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel removeCollider;
    }
}