namespace Lynx2DEngine.forms
{
    partial class BuildSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildSettingsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.lineBreaks = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.obfuscates = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.hasIcon = new System.Windows.Forms.CheckBox();
            this.iconLocation = new System.Windows.Forms.TextBox();
            this.iconImage = new System.Windows.Forms.PictureBox();
            this.iconMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lineBreaks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Line Breaks:";
            // 
            // lineBreaks
            // 
            this.lineBreaks.Location = new System.Drawing.Point(87, 114);
            this.lineBreaks.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.lineBreaks.Name = "lineBreaks";
            this.lineBreaks.Size = new System.Drawing.Size(51, 20);
            this.lineBreaks.TabIndex = 1;
            this.lineBreaks.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lineBreaks.ValueChanged += new System.EventHandler(this.lineBreaks_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Build parameters";
            // 
            // obfuscates
            // 
            this.obfuscates.AutoSize = true;
            this.obfuscates.Location = new System.Drawing.Point(160, 116);
            this.obfuscates.Name = "obfuscates";
            this.obfuscates.Size = new System.Drawing.Size(159, 17);
            this.obfuscates.TabIndex = 3;
            this.obfuscates.Text = "Obfuscate (requires internet)";
            this.obfuscates.UseVisualStyleBackColor = true;
            this.obfuscates.Visible = false;
            this.obfuscates.CheckedChanged += new System.EventHandler(this.obfuscates_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Build info";
            // 
            // hasIcon
            // 
            this.hasIcon.AutoSize = true;
            this.hasIcon.Location = new System.Drawing.Point(15, 34);
            this.hasIcon.Name = "hasIcon";
            this.hasIcon.Size = new System.Drawing.Size(69, 17);
            this.hasIcon.TabIndex = 5;
            this.hasIcon.Text = "Has Icon";
            this.hasIcon.UseVisualStyleBackColor = true;
            this.hasIcon.CheckedChanged += new System.EventHandler(this.hasIcon_CheckedChanged);
            // 
            // iconLocation
            // 
            this.iconLocation.Enabled = false;
            this.iconLocation.Location = new System.Drawing.Point(87, 32);
            this.iconLocation.Name = "iconLocation";
            this.iconLocation.Size = new System.Drawing.Size(209, 20);
            this.iconLocation.TabIndex = 6;
            this.iconLocation.TextChanged += new System.EventHandler(this.iconLocation_TextChanged);
            // 
            // iconImage
            // 
            this.iconImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.iconImage.Location = new System.Drawing.Point(304, 29);
            this.iconImage.Name = "iconImage";
            this.iconImage.Size = new System.Drawing.Size(25, 25);
            this.iconImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconImage.TabIndex = 7;
            this.iconImage.TabStop = false;
            // 
            // iconMessage
            // 
            this.iconMessage.AutoSize = true;
            this.iconMessage.Location = new System.Drawing.Point(84, 55);
            this.iconMessage.Name = "iconMessage";
            this.iconMessage.Size = new System.Drawing.Size(148, 13);
            this.iconMessage.TabIndex = 8;
            this.iconMessage.Text = "Uh oh something went wrong!";
            this.iconMessage.Visible = false;
            // 
            // BuildSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 151);
            this.Controls.Add(this.iconMessage);
            this.Controls.Add(this.iconImage);
            this.Controls.Add(this.iconLocation);
            this.Controls.Add(this.hasIcon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.obfuscates);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lineBreaks);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(360, 190);
            this.MinimumSize = new System.Drawing.Size(360, 190);
            this.Name = "BuildSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lynx2D Engine - Build Settings";
            this.Load += new System.EventHandler(this.BuildSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lineBreaks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown lineBreaks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox obfuscates;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox hasIcon;
        private System.Windows.Forms.TextBox iconLocation;
        private System.Windows.Forms.PictureBox iconImage;
        private System.Windows.Forms.Label iconMessage;
    }
}