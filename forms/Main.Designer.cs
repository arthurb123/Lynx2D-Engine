namespace Lynx2DEngine
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addGameObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addColliderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSpriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawCollidersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDevToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.browserContainer = new System.Windows.Forms.Panel();
            this.hierarchy = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gameToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(11, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(1437, 42);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.loadProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.showProjectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(56, 34);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(224, 34);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // loadProjectToolStripMenuItem
            // 
            this.loadProjectToolStripMenuItem.Name = "loadProjectToolStripMenuItem";
            this.loadProjectToolStripMenuItem.Size = new System.Drawing.Size(224, 34);
            this.loadProjectToolStripMenuItem.Text = "Load Project";
            this.loadProjectToolStripMenuItem.Click += new System.EventHandler(this.loadProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Enabled = false;
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(224, 34);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // showProjectToolStripMenuItem
            // 
            this.showProjectToolStripMenuItem.Enabled = false;
            this.showProjectToolStripMenuItem.Name = "showProjectToolStripMenuItem";
            this.showProjectToolStripMenuItem.Size = new System.Drawing.Size(224, 34);
            this.showProjectToolStripMenuItem.Text = "Show Project";
            this.showProjectToolStripMenuItem.Click += new System.EventHandler(this.showProjectToolStripMenuItem_Click);
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGameObjectToolStripMenuItem,
            this.addColliderToolStripMenuItem,
            this.addSpriteToolStripMenuItem,
            this.addScriptToolStripMenuItem});
            this.gameToolStripMenuItem.Enabled = false;
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(79, 34);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // addGameObjectToolStripMenuItem
            // 
            this.addGameObjectToolStripMenuItem.Name = "addGameObjectToolStripMenuItem";
            this.addGameObjectToolStripMenuItem.Size = new System.Drawing.Size(263, 34);
            this.addGameObjectToolStripMenuItem.Text = "Add GameObject";
            this.addGameObjectToolStripMenuItem.Click += new System.EventHandler(this.addGameObjectToolStripMenuItem_Click);
            // 
            // addColliderToolStripMenuItem
            // 
            this.addColliderToolStripMenuItem.Name = "addColliderToolStripMenuItem";
            this.addColliderToolStripMenuItem.Size = new System.Drawing.Size(263, 34);
            this.addColliderToolStripMenuItem.Text = "Add Collider";
            this.addColliderToolStripMenuItem.Click += new System.EventHandler(this.addColliderToolStripMenuItem_Click);
            // 
            // addSpriteToolStripMenuItem
            // 
            this.addSpriteToolStripMenuItem.Name = "addSpriteToolStripMenuItem";
            this.addSpriteToolStripMenuItem.Size = new System.Drawing.Size(263, 34);
            this.addSpriteToolStripMenuItem.Text = "Add Sprite";
            this.addSpriteToolStripMenuItem.Click += new System.EventHandler(this.addSpriteToolStripMenuItem_Click);
            // 
            // addScriptToolStripMenuItem
            // 
            this.addScriptToolStripMenuItem.Name = "addScriptToolStripMenuItem";
            this.addScriptToolStripMenuItem.Size = new System.Drawing.Size(263, 34);
            this.addScriptToolStripMenuItem.Text = "Add Script";
            this.addScriptToolStripMenuItem.Click += new System.EventHandler(this.addScriptToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cameraToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.drawCollidersToolStripMenuItem,
            this.refreshGameToolStripMenuItem});
            this.settingsToolStripMenuItem.Enabled = false;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(99, 34);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // cameraToolStripMenuItem
            // 
            this.cameraToolStripMenuItem.Checked = true;
            this.cameraToolStripMenuItem.CheckOnClick = true;
            this.cameraToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
            this.cameraToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.cameraToolStripMenuItem.Text = "Camera";
            this.cameraToolStripMenuItem.Click += new System.EventHandler(this.cameraToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.CheckOnClick = true;
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // drawCollidersToolStripMenuItem
            // 
            this.drawCollidersToolStripMenuItem.CheckOnClick = true;
            this.drawCollidersToolStripMenuItem.Name = "drawCollidersToolStripMenuItem";
            this.drawCollidersToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.drawCollidersToolStripMenuItem.Text = "Draw Colliders";
            this.drawCollidersToolStripMenuItem.Click += new System.EventHandler(this.drawCollidersToolStripMenuItem_Click);
            // 
            // refreshGameToolStripMenuItem
            // 
            this.refreshGameToolStripMenuItem.Name = "refreshGameToolStripMenuItem";
            this.refreshGameToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.refreshGameToolStripMenuItem.Text = "Refresh Game";
            this.refreshGameToolStripMenuItem.Click += new System.EventHandler(this.refreshGameToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewDocumentationToolStripMenuItem,
            this.showDevToolsToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(68, 34);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewDocumentationToolStripMenuItem
            // 
            this.viewDocumentationToolStripMenuItem.Name = "viewDocumentationToolStripMenuItem";
            this.viewDocumentationToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.viewDocumentationToolStripMenuItem.Text = "View Documentation";
            this.viewDocumentationToolStripMenuItem.Click += new System.EventHandler(this.viewDocumentationToolStripMenuItem_Click);
            // 
            // showDevToolsToolStripMenuItem
            // 
            this.showDevToolsToolStripMenuItem.Enabled = false;
            this.showDevToolsToolStripMenuItem.Name = "showDevToolsToolStripMenuItem";
            this.showDevToolsToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.showDevToolsToolStripMenuItem.Text = "Show DevTools";
            this.showDevToolsToolStripMenuItem.Click += new System.EventHandler(this.showDevToolsToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(298, 34);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(622, 6);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusLabel.Size = new System.Drawing.Size(810, 33);
            this.statusLabel.TabIndex = 3;
            this.statusLabel.Text = "No project has been opened.";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // timer1
            // 
            this.timer1.Interval = 8000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // browserContainer
            // 
            this.browserContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserContainer.Location = new System.Drawing.Point(0, 42);
            this.browserContainer.Margin = new System.Windows.Forms.Padding(18);
            this.browserContainer.Name = "browserContainer";
            this.browserContainer.Size = new System.Drawing.Size(1180, 994);
            this.browserContainer.TabIndex = 4;
            // 
            // hierarchy
            // 
            this.hierarchy.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.hierarchy.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hierarchy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hierarchy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hierarchy.LabelEdit = true;
            this.hierarchy.Location = new System.Drawing.Point(0, 0);
            this.hierarchy.Margin = new System.Windows.Forms.Padding(18);
            this.hierarchy.Name = "hierarchy";
            this.hierarchy.Size = new System.Drawing.Size(257, 994);
            this.hierarchy.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.hierarchy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1180, 42);
            this.panel1.Margin = new System.Windows.Forms.Padding(18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 994);
            this.panel1.TabIndex = 2;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(1437, 1036);
            this.Controls.Add(this.browserContainer);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimumSize = new System.Drawing.Size(1447, 1054);
            this.Name = "Main";
            this.Text = "Lynx2D Engine";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addGameObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showProjectToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem addSpriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewDocumentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDevToolsToolStripMenuItem;
        private System.Windows.Forms.Panel browserContainer;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addColliderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawCollidersToolStripMenuItem;
        private System.Windows.Forms.TreeView hierarchy;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
    }
}

