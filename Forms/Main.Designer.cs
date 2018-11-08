﻿namespace Lynx2DEngine
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
            this.settingsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addGameObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addColliderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSpriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEmitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTilemapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageSmoothingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawCollidersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDevToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadStandardResourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadFrameworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showChangelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.browserContainer = new System.Windows.Forms.Panel();
            this.hierarchy = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hierarchyScenes = new System.Windows.Forms.Button();
            this.hierarchyObjects = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
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
            this.sceneToolStripMenuItem,
            this.gameToolStripMenuItem,
            this.buildToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.loadProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.showProjectToolStripMenuItem,
            this.settingsToolStripMenuItem2});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.fileToolStripMenuItem.Text = "Engine";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // loadProjectToolStripMenuItem
            // 
            this.loadProjectToolStripMenuItem.Name = "loadProjectToolStripMenuItem";
            this.loadProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.loadProjectToolStripMenuItem.Text = "Load Project";
            this.loadProjectToolStripMenuItem.Click += new System.EventHandler(this.loadProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Enabled = false;
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // showProjectToolStripMenuItem
            // 
            this.showProjectToolStripMenuItem.Enabled = false;
            this.showProjectToolStripMenuItem.Name = "showProjectToolStripMenuItem";
            this.showProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.showProjectToolStripMenuItem.Text = "Show Project";
            this.showProjectToolStripMenuItem.Click += new System.EventHandler(this.showProjectToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem2
            // 
            this.settingsToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themeToolStripMenuItem});
            this.settingsToolStripMenuItem2.Name = "settingsToolStripMenuItem2";
            this.settingsToolStripMenuItem2.Size = new System.Drawing.Size(143, 22);
            this.settingsToolStripMenuItem2.Text = "Preferences";
            // 
            // themeToolStripMenuItem
            // 
            this.themeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lightToolStripMenuItem,
            this.darkToolStripMenuItem});
            this.themeToolStripMenuItem.Name = "themeToolStripMenuItem";
            this.themeToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.themeToolStripMenuItem.Text = "Theme";
            // 
            // lightToolStripMenuItem
            // 
            this.lightToolStripMenuItem.Checked = true;
            this.lightToolStripMenuItem.CheckOnClick = true;
            this.lightToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lightToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            this.lightToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.lightToolStripMenuItem.Text = "Light";
            this.lightToolStripMenuItem.Click += new System.EventHandler(this.lightToolStripMenuItem_Click);
            // 
            // darkToolStripMenuItem
            // 
            this.darkToolStripMenuItem.CheckOnClick = true;
            this.darkToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            this.darkToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.darkToolStripMenuItem.Text = "Dark";
            this.darkToolStripMenuItem.Click += new System.EventHandler(this.darkToolStripMenuItem_Click);
            // 
            // sceneToolStripMenuItem
            // 
            this.sceneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSceneToolStripMenuItem,
            this.removeSceneToolStripMenuItem});
            this.sceneToolStripMenuItem.Enabled = false;
            this.sceneToolStripMenuItem.Name = "sceneToolStripMenuItem";
            this.sceneToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.sceneToolStripMenuItem.Text = "Scene";
            // 
            // addSceneToolStripMenuItem
            // 
            this.addSceneToolStripMenuItem.Name = "addSceneToolStripMenuItem";
            this.addSceneToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addSceneToolStripMenuItem.Text = "Add Scene";
            this.addSceneToolStripMenuItem.Click += new System.EventHandler(this.addSceneToolStripMenuItem_Click);
            // 
            // removeSceneToolStripMenuItem
            // 
            this.removeSceneToolStripMenuItem.Name = "removeSceneToolStripMenuItem";
            this.removeSceneToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeSceneToolStripMenuItem.Text = "Remove Scene";
            this.removeSceneToolStripMenuItem.Click += new System.EventHandler(this.removeSceneToolStripMenuItem_Click);
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGameObjectToolStripMenuItem,
            this.addColliderToolStripMenuItem,
            this.addSpriteToolStripMenuItem,
            this.addScriptToolStripMenuItem,
            this.addEmitterToolStripMenuItem,
            this.addTilemapToolStripMenuItem,
            this.addSoundToolStripMenuItem,
            this.addFolderToolStripMenuItem});
            this.gameToolStripMenuItem.Enabled = false;
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // addGameObjectToolStripMenuItem
            // 
            this.addGameObjectToolStripMenuItem.Name = "addGameObjectToolStripMenuItem";
            this.addGameObjectToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.addGameObjectToolStripMenuItem.Text = "Add GameObject";
            this.addGameObjectToolStripMenuItem.Click += new System.EventHandler(this.addGameObjectToolStripMenuItem_Click);
            // 
            // addColliderToolStripMenuItem
            // 
            this.addColliderToolStripMenuItem.Name = "addColliderToolStripMenuItem";
            this.addColliderToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.addColliderToolStripMenuItem.Text = "Add Collider";
            this.addColliderToolStripMenuItem.Click += new System.EventHandler(this.addColliderToolStripMenuItem_Click);
            // 
            // addSpriteToolStripMenuItem
            // 
            this.addSpriteToolStripMenuItem.Name = "addSpriteToolStripMenuItem";
            this.addSpriteToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.addSpriteToolStripMenuItem.Text = "Add Sprite";
            this.addSpriteToolStripMenuItem.Click += new System.EventHandler(this.addSpriteToolStripMenuItem_Click);
            // 
            // addScriptToolStripMenuItem
            // 
            this.addScriptToolStripMenuItem.Name = "addScriptToolStripMenuItem";
            this.addScriptToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.addScriptToolStripMenuItem.Text = "Add Script";
            this.addScriptToolStripMenuItem.Click += new System.EventHandler(this.addScriptToolStripMenuItem_Click);
            // 
            // addEmitterToolStripMenuItem
            // 
            this.addEmitterToolStripMenuItem.Name = "addEmitterToolStripMenuItem";
            this.addEmitterToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.addEmitterToolStripMenuItem.Text = "Add Emitter";
            this.addEmitterToolStripMenuItem.Click += new System.EventHandler(this.addEmitterToolStripMenuItem_Click);
            // 
            // addTilemapToolStripMenuItem
            // 
            this.addTilemapToolStripMenuItem.Name = "addTilemapToolStripMenuItem";
            this.addTilemapToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.addTilemapToolStripMenuItem.Text = "Add Tilemap";
            this.addTilemapToolStripMenuItem.Click += new System.EventHandler(this.addTilemapToolStripMenuItem_Click);
            // 
            // addSoundToolStripMenuItem
            // 
            this.addSoundToolStripMenuItem.Name = "addSoundToolStripMenuItem";
            this.addSoundToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.addSoundToolStripMenuItem.Text = "Add Sound";
            this.addSoundToolStripMenuItem.Click += new System.EventHandler(this.addSoundToolStripMenuItem_Click);
            // 
            // addFolderToolStripMenuItem
            // 
            this.addFolderToolStripMenuItem.Name = "addFolderToolStripMenuItem";
            this.addFolderToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.addFolderToolStripMenuItem.Text = "Create Folder";
            this.addFolderToolStripMenuItem.Click += new System.EventHandler(this.addFolderToolStripMenuItem_Click);
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.Enabled = false;
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.buildToolStripMenuItem.Text = "Build Scene";
            this.buildToolStripMenuItem.Click += new System.EventHandler(this.buildToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportProjectToolStripMenuItem,
            this.exportSettingsToolStripMenuItem});
            this.exportToolStripMenuItem.Enabled = false;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // exportProjectToolStripMenuItem
            // 
            this.exportProjectToolStripMenuItem.Name = "exportProjectToolStripMenuItem";
            this.exportProjectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportProjectToolStripMenuItem.Text = "Export Project";
            this.exportProjectToolStripMenuItem.Click += new System.EventHandler(this.exportProjectToolStripMenuItem_Click);
            // 
            // exportSettingsToolStripMenuItem
            // 
            this.exportSettingsToolStripMenuItem.Name = "exportSettingsToolStripMenuItem";
            this.exportSettingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportSettingsToolStripMenuItem.Text = "Export Settings";
            this.exportSettingsToolStripMenuItem.Click += new System.EventHandler(this.exportSettingsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cameraToolStripMenuItem,
            this.gridToolStripMenuItem,
            this.imageSmoothingToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.drawCollidersToolStripMenuItem});
            this.settingsToolStripMenuItem.Enabled = false;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // cameraToolStripMenuItem
            // 
            this.cameraToolStripMenuItem.Checked = true;
            this.cameraToolStripMenuItem.CheckOnClick = true;
            this.cameraToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cameraToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
            this.cameraToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.cameraToolStripMenuItem.Text = "Camera";
            this.cameraToolStripMenuItem.Click += new System.EventHandler(this.cameraToolStripMenuItem_Click);
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.CheckOnClick = true;
            this.gridToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem1});
            this.gridToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.gridToolStripMenuItem.Text = "Grid";
            this.gridToolStripMenuItem.Click += new System.EventHandler(this.gridToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem1.Text = "Settings";
            this.settingsToolStripMenuItem1.Click += new System.EventHandler(this.settingsToolStripMenuItem1_Click);
            // 
            // imageSmoothingToolStripMenuItem
            // 
            this.imageSmoothingToolStripMenuItem.Checked = true;
            this.imageSmoothingToolStripMenuItem.CheckOnClick = true;
            this.imageSmoothingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.imageSmoothingToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.imageSmoothingToolStripMenuItem.Name = "imageSmoothingToolStripMenuItem";
            this.imageSmoothingToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.imageSmoothingToolStripMenuItem.Text = "Image Smoothing";
            this.imageSmoothingToolStripMenuItem.Click += new System.EventHandler(this.imageSmoothingToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.CheckOnClick = true;
            this.debugToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // drawCollidersToolStripMenuItem
            // 
            this.drawCollidersToolStripMenuItem.CheckOnClick = true;
            this.drawCollidersToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.drawCollidersToolStripMenuItem.Name = "drawCollidersToolStripMenuItem";
            this.drawCollidersToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.drawCollidersToolStripMenuItem.Text = "Draw Colliders";
            this.drawCollidersToolStripMenuItem.Click += new System.EventHandler(this.drawCollidersToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewDocumentationToolStripMenuItem,
            this.showDevToolsToolStripMenuItem,
            this.reloadStandardResourcesToolStripMenuItem,
            this.reloadFrameworkToolStripMenuItem,
            this.showChangelogToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewDocumentationToolStripMenuItem
            // 
            this.viewDocumentationToolStripMenuItem.Name = "viewDocumentationToolStripMenuItem";
            this.viewDocumentationToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.viewDocumentationToolStripMenuItem.Text = "View Documentation";
            this.viewDocumentationToolStripMenuItem.Click += new System.EventHandler(this.viewDocumentationToolStripMenuItem_Click);
            // 
            // showDevToolsToolStripMenuItem
            // 
            this.showDevToolsToolStripMenuItem.Enabled = false;
            this.showDevToolsToolStripMenuItem.Name = "showDevToolsToolStripMenuItem";
            this.showDevToolsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.showDevToolsToolStripMenuItem.Text = "Show DevTools";
            this.showDevToolsToolStripMenuItem.Click += new System.EventHandler(this.showDevToolsToolStripMenuItem_Click);
            // 
            // reloadStandardResourcesToolStripMenuItem
            // 
            this.reloadStandardResourcesToolStripMenuItem.Enabled = false;
            this.reloadStandardResourcesToolStripMenuItem.Name = "reloadStandardResourcesToolStripMenuItem";
            this.reloadStandardResourcesToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.reloadStandardResourcesToolStripMenuItem.Text = "Reload Resources";
            this.reloadStandardResourcesToolStripMenuItem.Click += new System.EventHandler(this.reloadStandardResourcesToolStripMenuItem_Click);
            // 
            // reloadFrameworkToolStripMenuItem
            // 
            this.reloadFrameworkToolStripMenuItem.Enabled = false;
            this.reloadFrameworkToolStripMenuItem.Name = "reloadFrameworkToolStripMenuItem";
            this.reloadFrameworkToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.reloadFrameworkToolStripMenuItem.Text = "Reload Framework";
            this.reloadFrameworkToolStripMenuItem.Click += new System.EventHandler(this.reloadFrameworkToolStripMenuItem_Click);
            // 
            // showChangelogToolStripMenuItem
            // 
            this.showChangelogToolStripMenuItem.Name = "showChangelogToolStripMenuItem";
            this.showChangelogToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.showChangelogToolStripMenuItem.Text = "Show Changelog";
            this.showChangelogToolStripMenuItem.Click += new System.EventHandler(this.showChangelogToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(396, 3);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusLabel.Size = new System.Drawing.Size(385, 18);
            this.statusLabel.TabIndex = 3;
            this.statusLabel.Text = "No project opened yet.";
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
            this.browserContainer.Location = new System.Drawing.Point(0, 24);
            this.browserContainer.Margin = new System.Windows.Forms.Padding(10);
            this.browserContainer.Name = "browserContainer";
            this.browserContainer.Size = new System.Drawing.Size(634, 537);
            this.browserContainer.TabIndex = 4;
            // 
            // hierarchy
            // 
            this.hierarchy.AllowDrop = true;
            this.hierarchy.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.hierarchy.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hierarchy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hierarchy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hierarchy.HotTracking = true;
            this.hierarchy.Indent = 14;
            this.hierarchy.ItemHeight = 20;
            this.hierarchy.LabelEdit = true;
            this.hierarchy.Location = new System.Drawing.Point(0, 26);
            this.hierarchy.Margin = new System.Windows.Forms.Padding(10);
            this.hierarchy.Name = "hierarchy";
            this.hierarchy.Size = new System.Drawing.Size(150, 511);
            this.hierarchy.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.Controls.Add(this.hierarchyScenes);
            this.panel1.Controls.Add(this.hierarchyObjects);
            this.panel1.Controls.Add(this.hierarchy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(634, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(10);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 26, 0, 0);
            this.panel1.Size = new System.Drawing.Size(150, 537);
            this.panel1.TabIndex = 2;
            // 
            // hierarchyScenes
            // 
            this.hierarchyScenes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hierarchyScenes.BackColor = System.Drawing.SystemColors.ControlDark;
            this.hierarchyScenes.FlatAppearance.BorderSize = 0;
            this.hierarchyScenes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hierarchyScenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hierarchyScenes.Location = new System.Drawing.Point(75, 0);
            this.hierarchyScenes.Margin = new System.Windows.Forms.Padding(0);
            this.hierarchyScenes.Name = "hierarchyScenes";
            this.hierarchyScenes.Size = new System.Drawing.Size(75, 22);
            this.hierarchyScenes.TabIndex = 2;
            this.hierarchyScenes.Text = "Scenes";
            this.hierarchyScenes.UseVisualStyleBackColor = true;
            this.hierarchyScenes.Visible = false;
            this.hierarchyScenes.Click += new System.EventHandler(this.hierachyScenes_Click);
            // 
            // hierarchyObjects
            // 
            this.hierarchyObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hierarchyObjects.BackColor = System.Drawing.SystemColors.Control;
            this.hierarchyObjects.FlatAppearance.BorderSize = 0;
            this.hierarchyObjects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hierarchyObjects.Location = new System.Drawing.Point(0, 0);
            this.hierarchyObjects.Margin = new System.Windows.Forms.Padding(0);
            this.hierarchyObjects.Name = "hierarchyObjects";
            this.hierarchyObjects.Size = new System.Drawing.Size(75, 22);
            this.hierarchyObjects.TabIndex = 1;
            this.hierarchyObjects.Text = "Objects";
            this.hierarchyObjects.UseVisualStyleBackColor = false;
            this.hierarchyObjects.Visible = false;
            this.hierarchyObjects.Click += new System.EventHandler(this.hierarchyObjects_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 1500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.browserContainer);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(797, 580);
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
        private System.Windows.Forms.ToolStripMenuItem addColliderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawCollidersToolStripMenuItem;
        private System.Windows.Forms.TreeView hierarchy;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageSmoothingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem reloadFrameworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addEmitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadStandardResourcesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTilemapToolStripMenuItem;
        private System.Windows.Forms.Button hierarchyScenes;
        private System.Windows.Forms.Button hierarchyObjects;
        private System.Windows.Forms.ToolStripMenuItem sceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSceneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showChangelogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkToolStripMenuItem;
        private System.Windows.Forms.Timer timer2;
    }
}
