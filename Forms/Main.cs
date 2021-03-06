﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CefSharp;
using CefSharp.Enums;
using CefSharp.WinForms;
using Lynx2DEngine.classes;
using Lynx2DEngine.forms;

namespace Lynx2DEngine
{
    public partial class Main : Form
    {
        public ChromiumWebBrowser browser = null;

        public ConsoleForm console;
        private bool consoleVisible;

        public bool canViewObjects = true;
        private HierarchyState hierarchyView = HierarchyState.Objects;

        public ImageList hierarchyList;

        private EngineObject copied = null;
        private HierarchyFolder copiedFolder = null;
        private int copiedFromScene = 0;

        private Dictionary<string, bool> registeredConsoleOutput = new Dictionary<string, bool>();

        #region "Main Stuff"
        public Main()
        {
            InitializeComponent();

            //Create console
            console = new ConsoleForm();

            //Set events
            FormClosing += Main_FormClosing;

            hierarchy.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(hierarchy_MouseDoubleClick);
            hierarchy.KeyDown += new KeyEventHandler(hierarchy_KeyDown);
            hierarchy.AfterLabelEdit += new NodeLabelEditEventHandler(hierarchy_LabelEdit);
            hierarchy.ItemDrag += new ItemDragEventHandler(hierarchy_ItemDrag);
            hierarchy.DragEnter += new DragEventHandler(hierarchy_DragEnter);
            hierarchy.DragDrop += new DragEventHandler(hierarchy_DragDrop);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Project.form = this;
            Engine.form = this;
            Feed.form = this;
            Tilemapper.form = this;

            EngineStartup();

            Feed.EvaluateStartup();

            VersionTimer.Enabled = true;

            CefSettings settings = new CefSettings()
            {
                WindowlessRenderingEnabled = true
            };
            Cef.Initialize(settings);

            hierarchyList = new ImageList
            {
                ImageSize = new Size(16, 16)
            };

            hierarchyList.Images.Add(new Bitmap(1, 1));

            hierarchyList.Images.Add(Properties.Resources.folder);
            hierarchyList.Images.Add(Properties.Resources.go);
            hierarchyList.Images.Add(Properties.Resources.image);
            hierarchyList.Images.Add(Properties.Resources.collider);
            hierarchyList.Images.Add(Properties.Resources.emitter);
            hierarchyList.Images.Add(Properties.Resources.script);
            hierarchyList.Images.Add(Properties.Resources.tilemap);
            hierarchyList.Images.Add(Properties.Resources.scene);
            hierarchyList.Images.Add(Properties.Resources.sound);

            hierarchy.ImageList = hierarchyList;
        }

        private void EngineStartup()
        {
            bool hasPreferences = Engine.EvaluateEnginePreferences();

            if (hasPreferences)
            {
                LoadTheme(Engine.ePreferences.theme);
                showExceptionsToolStripMenuItem.Checked = Engine.ePreferences.suppressExceptions;
                //...
            }
        }

        public void SetTitle()
        {
            Text = "Lynx2D Engine - " + Project.Name() + " (" + Engine.scenes[Engine.eSettings.currentScene].Variable() + ")";
        }

        public void SetGameAvailability(bool available)
        {
            gameToolStripMenuItem.Enabled = available;
            settingsToolStripMenuItem.Enabled = available;
            saveProjectToolStripMenuItem.Enabled = available;
            showProjectToolStripMenuItem.Enabled = available;
            showDevToolsToolStripMenuItem.Enabled = available;
            buildToolStripMenuItem.Enabled = available;
            reloadFrameworkToolStripMenuItem.Enabled = available;
            reloadStandardResourcesToolStripMenuItem.Enabled = available;
            restoreBackupToolStripMenuItem.Enabled = available;
            sceneToolStripMenuItem.Enabled = available;
            exportToolStripMenuItem.Enabled = available;
            managementToolStripMenuItem.Enabled = available;

            hierarchyObjects.Visible = available;
            hierarchyScenes.Visible = available;
        }

        private void Main_FormClosing(object sender, CancelEventArgs e)
        {
            Engine.SaveEnginePreferences(true);

            if (!Feed.wantsToExtract)
                Project.RequestSave();

            Project.RemoveEngineHTML();
            Manager.ClearAppData();
            
            Cef.Shutdown();
        }

        private bool DragDropFile(string[] files)
        {
            int done = 0;

            foreach (string file in files)
            {
                if (!file.Contains('.'))
                    continue;

                string ext = file.Substring(file.IndexOf('.'), file.Length - file.IndexOf('.')).ToLower(),
                       name = file.Substring(file.LastIndexOf('\\')+1, file.Length - ext.Length - file.LastIndexOf('\\') - 1),
                       dest;

                int id;

                switch (ext)
                {
                    case ".png":
                    case ".bmp":
                    case ".jpg":
                    case ".jpeg":
                        //Sprite
                        dest = "res/" + name + ext;

                        Manager.CopyFile(file, "projects/" + Project.Name() + Path.DirectorySeparatorChar + dest);

                        id = AddSprite();

                        Engine.scenes[Engine.eSettings.currentScene].objects[id].Rename(name);
                        Engine.scenes[Engine.eSettings.currentScene].objects[id].source = dest;

                        SetStatus("Imported Sprite from '" + name + ext + "'", StatusType.Message);
                        done++;

                        break;
                    case ".mp3":
                    case "wav":
                    case "ogg":
                        //Sound
                        dest = "res/" + name + ext;

                        Manager.CopyFile(file, "projects/" + Project.Name() + Path.DirectorySeparatorChar + dest);

                        id = AddSound();
                    
                        Engine.scenes[Engine.eSettings.currentScene].objects[id].Rename(name);
                        Engine.scenes[Engine.eSettings.currentScene].objects[id].source = dest;

                        SetStatus("Imported Sound from '" + name + ext + "'", StatusType.Message);
                        done++;

                        break;
                    case ".txt":
                    case ".js":
                        //Script
                        dest = "res/" + name + ext;

                        Manager.CopyFile(file, "projects/" + Project.Name() + Path.DirectorySeparatorChar + dest);

                        id = AddScript();

                        Engine.scenes[Engine.eSettings.currentScene].objects[id].Rename(name);
                        Engine.scenes[Engine.eSettings.currentScene].objects[id].code = File.ReadAllText("projects/" + Project.Name() + Path.DirectorySeparatorChar + dest);

                        File.Delete("projects/" + Project.Name() + Path.DirectorySeparatorChar + dest);

                        SetStatus("Imported Script from '" + name + ext + "'", StatusType.Message);
                        done++;

                        break;
                    case ".lx2d":
                        //Lynx2D Engine Object
                        Engine.ImportEngineObject(file);
                        done++;

                        break;
                    default:
                        MessageBox.Show("This file format is not supported and can not be imported.", "Lynx2D Engine - Message");
                        break;
                }
            }

            if (done != 0)
            {
                UpdateHierarchy();
                RefreshBrowser();

                return true;
            }

            return false;
        }
        #endregion

        #region "Status Stuff"
        public void SetStatus(string text, StatusType type)
        {
            try
            {
                switch (type)
                {
                    case StatusType.Message:
                        statusLabel.ForeColor = (Engine.ePreferences.theme == Theme.Light ? LightTheme.font : DarkTheme.font);
                        break;
                    case StatusType.Warning:
                        statusLabel.ForeColor = Color.DarkOrange;
                        break;
                    case StatusType.Alert:
                        statusLabel.ForeColor = Color.DarkRed;
                        break;
                }

                statusLabel.Visible = true;
                statusLabel.Text = text;

                StatusTimer.Enabled = true;
            }
            catch
            {
                //...
            }
        }

        public enum StatusType
        {
            Message = 0,
            Warning,
            Alert
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            StatusTimer.Enabled = false;
            statusLabel.Visible = false;
        }
        #endregion

        #region "Hierarchy Stuff"
        public bool SwitchHierarchyView(HierarchyState state)
        {
            if (hierarchyView == state)
                return false;

            switch (state)
            {
                case HierarchyState.Objects:
                    if (Engine.ePreferences.theme == Theme.Light)
                    {
                        hierarchyObjects.BackColor = LightTheme.background;
                        hierarchyScenes.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
                    }
                    else
                    {
                        hierarchyObjects.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
                        hierarchyScenes.BackColor = DarkTheme.background;
                    }
                    break;
                case HierarchyState.Scenes:
                    if (Engine.ePreferences.theme == Theme.Light)
                    {
                        hierarchyObjects.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
                        hierarchyScenes.BackColor = LightTheme.background;
                    }
                    else
                    {
                        hierarchyObjects.BackColor = DarkTheme.background;
                        hierarchyScenes.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
                    }
                    break;
            }

            hierarchyView = state;

            UpdateHierarchy();

            return true;
        }

        delegate void UpdateHierarchyCallback();

        public void UpdateHierarchy()
        {
            try
            {
                if (hierarchy.InvokeRequired)
                {
                    UpdateHierarchyCallback d = new UpdateHierarchyCallback(UpdateHierarchy);
                    Invoke(d);
                }
                else
                {
                    hierarchy.Nodes.Clear();

                    switch (hierarchyView)
                    {
                        case HierarchyState.Objects:
                            if (canViewObjects) UpdateHierarchyWithObjects();
                            break;
                        case HierarchyState.Scenes:
                            UpdateHierarchyWithScenes();
                            break;
                    }
                }
            }
            catch (Exception exc)
            {
                Feed.GiveException("Hierarchy", exc);
            }
        }

        private void UpdateHierarchyWithObjects()
        {
            Hierarchy h = Engine.scenes[Engine.eSettings.currentScene].hierarchy;

            List<TreeNode> newHierarchy = new List<TreeNode>();

            //First add the folders
            for (int e = 0; e < h.folders.Count; e++)
                newHierarchy.Add(ConvertFolderToTreeNode(e, h.folders[e]));

            //Then add the engine objects
            foreach (HierarchyItem i in h.items)
            {
                TreeNode item = ConvertItemToTreeNode(i);

                if (item != null) newHierarchy.Add(item);
            }

            hierarchy.Nodes.AddRange(newHierarchy.ToArray());
        }

        private void UpdateHierarchyWithScenes()
        {
            Scene[] scenes = Engine.scenes;

            for (int i = 0; i < scenes.Length; i++)
            {
                if (scenes[i] == null) continue;

                TreeNode node = new TreeNode(scenes[i].Variable());

                if (Engine.eSettings.currentScene == i)
                    node.NodeFont = new Font(hierarchy.Font, FontStyle.Underline);

                node.Tag = i;
                node.ImageIndex = 8;
                node.SelectedImageIndex = node.ImageIndex;

                hierarchy.Nodes.Add(node);
            }
        }

        private TreeNode ConvertFolderToTreeNode(int id, HierarchyFolder f)
        {
            List<TreeNode> nodes = new List<TreeNode>();

            foreach (HierarchyItem i in f.content)
            {
                TreeNode item = ConvertItemToTreeNode(i);

                if (item != null) nodes.Add(item);
            }

            TreeNode ftn = new TreeNode(f.name, nodes.ToArray())
            {
                Tag = id,
                ImageIndex = 1
            };

            ftn.SelectedImageIndex = ftn.ImageIndex;
            return ftn;
        }

        private TreeNode ConvertItemToTreeNode(HierarchyItem i)
        {
            EngineObject obj = Engine.GetEngineObject(i.engineId);

            if (obj != null && obj.parent == -1)
            {
                List<TreeNode> children = new List<TreeNode>();

                if (obj.child != -1)
                {
                    EngineObject childEO = Engine.GetEngineObject(obj.child);

                    TreeNode child = new TreeNode(childEO.Variable())
                    {
                        Tag = obj.child
                    };

                    if (childEO.type == EngineObjectType.Sprite)
                        child.ImageIndex = 3;
                    if (childEO.type == EngineObjectType.Script)
                        child.ImageIndex = 6;

                    child.SelectedImageIndex = child.ImageIndex;
                    children.Add(child);
                }

                TreeNode node = new TreeNode(obj.Variable(), children.ToArray())
                {
                    Tag = i.engineId
                };

                if (obj.type == EngineObjectType.GameObject)
                    node.ImageIndex = 2;
                if (obj.type == EngineObjectType.Sprite)
                    node.ImageIndex = 3;
                if (obj.type == EngineObjectType.BoxCollider)
                    node.ImageIndex = 4;
                if (obj.type == EngineObjectType.Emitter)
                    node.ImageIndex = 5;
                if (obj.type == EngineObjectType.Script)
                    node.ImageIndex = 6;
                if (obj.type == EngineObjectType.Tilemap)
                    node.ImageIndex = 7;
                if (obj.type == EngineObjectType.Sound)
                    node.ImageIndex = 9;

                node.SelectedImageIndex = node.ImageIndex;
                return node;
            }

            return null;
        }

        private void hierarchy_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void hierarchy_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.Move;
        }

        private void hierarchy_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    DragDropFile((string[])e.Data.GetData(DataFormats.FileDrop));

                    return;
                }

                Point targetPoint = hierarchy.PointToClient(new Point(e.X, e.Y));

                TreeNode targetNode = hierarchy.GetNodeAt(targetPoint);
                TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

                if (!draggedNode.Equals(targetNode) &&
                    targetNode != null &&
                    targetNode.ImageIndex == 1 &&
                    draggedNode.ImageIndex != 1)
                {
                    Point item = Engine.scenes[Engine.eSettings.currentScene].hierarchy.GetItemIdentifierWithEngineId((int)draggedNode.Tag);

                    if (item.X == -1)
                    {
                        Engine.scenes[Engine.eSettings.currentScene].hierarchy.folders[(int)targetNode.Tag].AddItem(Engine.scenes[Engine.eSettings.currentScene].hierarchy.items[item.Y]);
                        Engine.scenes[Engine.eSettings.currentScene].hierarchy.RemoveItem((int)draggedNode.Tag, false);
                    }
                    else
                    {
                        Engine.scenes[Engine.eSettings.currentScene].hierarchy.folders[(int)targetNode.Tag].AddItem(Engine.scenes[Engine.eSettings.currentScene].hierarchy.folders[item.X].content[item.Y]);
                        Engine.scenes[Engine.eSettings.currentScene].hierarchy.folders[item.X].RemoveItem((int)draggedNode.Tag);
                    }

                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);
                    targetNode.Expand();
                }
            }
            catch (Exception exc)
            {
                Feed.GiveException("Hierarchy Management", exc);
            }
        }

        private void hierarchy_MouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (hierarchyView == HierarchyState.Scenes)
            {
                killChildren();
                Engine.LoadScene((int)e.Node.Tag);

                SetTitle();

                return;
            }

            if (e.Node.ImageIndex == 1)
                return;

            try
            {
                int id = (int)e.Node.Tag;
                EngineObject obj = Engine.GetEngineObjects()[id];

                switch (obj.type)
                {
                    case EngineObjectType.GameObject:
                        GameObjectForm go = new GameObjectForm();

                        go.FormClosed += new FormClosedEventHandler(checkCameraInjection);
                        go.FormClosed += new FormClosedEventHandler(removeMarkerInjection);

                        go.Show();
                        go.Initialize(id);

                        Engine.ExecuteScript(obj.Variable() + ".Focus();");
                        Marker.Inject(obj.Variable());

                        break;
                    case EngineObjectType.Sprite:
                        SpriteForm sprite = new SpriteForm();

                        sprite.Show();
                        sprite.Initialize(obj.id);

                        break;
                    case EngineObjectType.Script:
                        ScriptForm eChild = (ScriptForm)checkChild(obj.Variable());

                        if (eChild != null && eChild.engineId == id)
                        {
                            eChild.Focus();

                            break;
                        }

                        ScriptForm script = new ScriptForm();

                        script.Show();
                        script.Initialize(id);

                        break;
                    case EngineObjectType.BoxCollider:
                        ColliderForm coll = new ColliderForm();

                        coll.FormClosed += new FormClosedEventHandler(checkCameraInjection);
                        coll.FormClosed += new FormClosedEventHandler(removeMarkerInjection);

                        Engine.ExecuteScript("lx.GAME.DRAW_COLLIDERS=true;");
                        coll.FormClosed += new FormClosedEventHandler(drawCollidersToolStripMenuItem_Click);

                        coll.Show();
                        coll.Initialize(id);

                        Engine.ExecuteScript("lx.GAME.FOCUS = " + obj.Variable());
                        Marker.Inject(obj.Variable());

                        break;
                    case EngineObjectType.Emitter:
                        EmitterForm emit = new EmitterForm();

                        emit.FormClosed += new FormClosedEventHandler(checkCameraInjection);
                        emit.FormClosed += new FormClosedEventHandler(removeMarkerInjection);

                        emit.Show();
                        emit.Initialize(obj.id);

                        Engine.ExecuteScript("lx.GAME.FOCUS = " + obj.Variable());
                        Marker.Inject(obj.Variable());

                        break;
                    case EngineObjectType.Tilemap:
                        TilemapForm tilemap = new TilemapForm();

                        tilemap.Show();
                        tilemap.Initialize(id);
                        
                        break;
                    case EngineObjectType.Sound:
                        SoundForm sound = new SoundForm();

                        sound.FormClosed += new FormClosedEventHandler(checkCameraInjection);
                        sound.FormClosed += new FormClosedEventHandler(removeMarkerInjection);

                        sound.Show();
                        sound.Initialize(id);

                        Engine.ExecuteScript("lx.GAME.FOCUS = " + obj.Variable());
                        Marker.Inject(obj.Variable());

                        break;
                }
            }
            catch (Exception exc)
            {
                Feed.GiveException("Hierarchy Open", exc);
            }
        }

        private void hierarchy_KeyDown(object sender, KeyEventArgs e)
        {

            if (hierarchyView == HierarchyState.Scenes && hierarchy.SelectedNode != null)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    Engine.RemoveScene((int)hierarchy.SelectedNode.Tag);

                    SetTitle();
                }

                return;
            }

            if (e.KeyCode == Keys.Delete && hierarchy.SelectedNode != null)
            {
                int imgIndex = hierarchy.SelectedNode.ImageIndex,
                    tag = (int)hierarchy.SelectedNode.Tag;

                TreeNode tn = hierarchy.SelectedNode;

                if (!Input.YesNo("Are you sure you want to delete '" + tn.Text + "'?", "Lynx2D Engine - Question"))
                    return;

                if (imgIndex == 1)
                    Engine.scenes[Engine.eSettings.currentScene].hierarchy.RemoveFolderWithIdentifier(tag);
                else
                {
                    Engine.RemoveEngineObject(tag, true, false);

                    hierarchy.Nodes.Remove(tn);
                }
            }

            if (e.Control && e.KeyCode == Keys.C && hierarchy.SelectedNode != null)
            {
                copiedFromScene = Engine.eSettings.currentScene;

                if (hierarchy.SelectedNode.ImageIndex == 1)
                {
                    copiedFolder = Engine.scenes[Engine.eSettings.currentScene].hierarchy.folders[(int)hierarchy.SelectedNode.Tag];

                    copied = null;

                    SetStatus("'" + copiedFolder.name + "' has been copied.", StatusType.Message);
                }
                else
                {
                    copied = Engine.GetEngineObject((int)hierarchy.SelectedNode.Tag);

                    copiedFolder = null;

                    SetStatus("'" + copied.Variable() + "' has been copied.", StatusType.Message);
                }
            }

            if (e.Control && e.KeyCode == Keys.V)
            {
                e.Handled = true;
                PasteCopied();
            }
        }

        private void PasteCopied()
        {
            try { 
                if (copied == null)
                {
                    if (copiedFolder != null)
                    {
                        Engine.scenes[Engine.eSettings.currentScene].hierarchy.CopyFolder(copiedFromScene, copiedFolder);

                        UpdateHierarchy();
                        RefreshBrowser();
                    }

                    return;
                }

                List<int> r;
                int result = -1;
                int child = copied.child;

                switch (copied.type)
                {
                    case EngineObjectType.GameObject:
                        r = AddGameObject();
                        result = r[0];
                        child = r[1];
                        break;
                    case EngineObjectType.Sprite:
                        result = AddSprite();
                        break;
                    case EngineObjectType.Script:
                        result = AddScript();
                        break;
                    case EngineObjectType.BoxCollider:
                        r = AddCollider();
                        result = r[0];
                        child = r[1];
                        break;
                    case EngineObjectType.Emitter:
                        r = AddEmitter();
                        result = r[0];
                        child = r[1];
                        break;
                    case EngineObjectType.Tilemap:
                        result = AddTilemap();
                        break;
                    case EngineObjectType.Sound:
                        result = AddSound();
                        break;
                }

                if (result == -1) return;

                EngineObject temp = copied.Clone();
                EngineObject tempChild = null;

                if (temp.type == EngineObjectType.Tilemap)
                    temp.tileMap = Engine.GetEngineObject(result).tileMap;

                if (child != -1)
                {
                    tempChild = Engine.scenes[copiedFromScene].objects[copied.child].Clone();

                    tempChild.parent = result;
                }

                temp.child = child;

                string copies = "";
                int amount = 0;
                while (Engine.GetEngineObjectWithVarName(copied.Variable() + copies) != null)
                {
                    amount++;

                    copies = "_" + amount;
                }

                if (copies != "")
                {
                    temp.Rename(temp.Variable() + copies);
                    if (tempChild != null) tempChild.Rename(tempChild.Variable() + copies);
                }

                Engine.SetEngineObject(result, temp);
                if (tempChild != null) Engine.SetEngineObject(child, tempChild);

                SetStatus("'" + temp.Variable() + "' has been pasted.", StatusType.Message);

                RefreshBrowser();
                UpdateHierarchy();
            }
            catch (Exception exc)
            {
                Feed.GiveException("Hierarchy Pasting", exc);
            }
        }

        private void hierarchy_LabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
                return;

            if (e.Label == string.Empty || e.Label.ToString().Length == 0) return;

            try
            {
                if (hierarchyView == HierarchyState.Objects)
                {
                    if (e.Node.ImageIndex == 1)
                        Engine.scenes[Engine.eSettings.currentScene].hierarchy.folders[(int)e.Node.Tag].Rename(e.Label);
                    else
                        Engine.RenameEngineObject((int)e.Node.Tag, e.Label, false);
                }
                else if (hierarchyView == HierarchyState.Scenes)
                    Engine.RenameScene((int)e.Node.Tag, e.Label);
            }
            catch (Exception exc)
            {
                Feed.GiveException("Hierarchy Renaming", exc);
            }
        }

        private void hierarchyObjects_Click(object sender, EventArgs e)
        {
            SwitchHierarchyView(HierarchyState.Objects);
        }

        private void hierachyScenes_Click(object sender, EventArgs e)
        {
            SwitchHierarchyView(HierarchyState.Scenes);
        }
        #endregion

        #region "Engine Toolstrip Stuff"
        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.Create();
        }

        private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.Load(true);
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.Save();
        }

        public void showProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manager.OpenDirectory(@Project.WorkDirectory());
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadTheme(Theme.Light);
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadTheme(Theme.Dark);
        }

        private void exportFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point result = Input.HierarchySelection("Export", "Select an engine item to export");

            if (result.X == -1 && result.Y == -1)
                return;

            Engine.SaveEngineObject(result.X, result.Y);
        }

        private void importFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Lynx2D Item (*.lx2d)|*.lx2d|Generic Files (*.*)|*.*";
            ofd.Title = "Import a Lynx2D item";
            ofd.Multiselect = true;
            ofd.ShowDialog();

            if (ofd.FileName == "" && ofd.FileNames.Length == 0)
                return;

            if (ofd.FileNames.Length > 1)
            {
                foreach (string file in ofd.FileNames)
                    if (file.Contains(".lx2d"))
                        Engine.ImportEngineObject(file);

                DragDropFile(ofd.FileNames);

                UpdateHierarchy();
                RefreshBrowser();
            }
            else
            {
                if (ofd.FileName.Contains(".lx2d"))
                {
                    Engine.ImportEngineObject(ofd.FileName);

                    UpdateHierarchy();
                    RefreshBrowser();
                }
                else
                    DragDropFile(new string[] { ofd.FileName });
            }
        }

        private void showExceptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.ePreferences.suppressExceptions = showExceptionsToolStripMenuItem.Checked;
        }
        #endregion

        #region "Scene Toolstrip Stuff"
        private void addSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.CreateScene(true);

            killChildren();

            SetTitle();

            SwitchHierarchyView(HierarchyState.Scenes);
        }

        private void removeSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.RemoveScene(Engine.eSettings.currentScene);
        }
        #endregion

        #region "Game Toolstrip Stuff"
        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.scenes[Engine.eSettings.currentScene].hierarchy.AddFolder();

            UpdateHierarchy();
        }

        private void addGameObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddGameObject();

                SwitchHierarchyView(HierarchyState.Objects);
            }
            catch (Exception exc)
            {
                Feed.GiveException("GameObject Creation", exc);
            }
        }

        private void addSoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddSound();

                SwitchHierarchyView(HierarchyState.Objects);
            }
            catch (Exception exc)
            {
                Feed.GiveException("Sound Creation", exc);
            }
        }


        private void addSpriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddSprite();

                SwitchHierarchyView(HierarchyState.Objects);
            }
            catch (Exception exc)
            {
                Feed.GiveException("Sprite Creation", exc);
            }
        }

        private void addScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddScript();

                SwitchHierarchyView(HierarchyState.Objects);
            }
            catch (Exception exc)
            {
                Feed.GiveException("Script Creation", exc);
            }
        }


        private void addColliderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddCollider();

                SwitchHierarchyView(HierarchyState.Objects);
            }
            catch (Exception exc)
            {
                Feed.GiveException("BoxCollider Creation", exc);
            }
        }

        private void addEmitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddEmitter();

                SwitchHierarchyView(HierarchyState.Objects);
            }
            catch (Exception exc)
            {
                Feed.GiveException("Emitter Creation", exc);
            }
        }

        private void addTilemapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddTilemap();

                SwitchHierarchyView(HierarchyState.Objects);
            }
            catch (Exception exc)
            {
                Feed.GiveException("Tilemap Creation", exc);
            }
        }

        private List<int> AddGameObject()
        {
            int sprite = Engine.GetEngineObjects().Length;
            int go = Engine.GetEngineObjects().Length + 1;
            int[] empty = Engine.GetEmptyEnginePositions();

            if (empty.Length > 1)
            {
                sprite = empty[0];
                go = empty[1];
            }

            string spriteCode = "let Sprite" + sprite + " = new lx.Sprite('res/lynx2d/sprite.png');";
            string goCode = "let GameObject" + go + " = new lx.GameObject(Sprite" + sprite + ", 0, 0, 64, 64);";

            int spriteR = Engine.AddEngineObject(EngineObjectType.Sprite, spriteCode, -1, go);
            int goR = Engine.AddEngineObject(EngineObjectType.GameObject, goCode, sprite, -1);

            if (spriteR != sprite)
                MessageBox.Show("Invalid id used while creating a Sprite.", "Lynx2D Engine - Warning");
            if (goR != go)
                MessageBox.Show("Invalid id used while creating a GameObject.", "Lynx2D Engine - Warning");

            UpdateHierarchy();

            List<int> result = new List<int>
            {
                go,
                sprite
            };

            return result;
        }

        private int AddSprite()
        {
            int sprite = Engine.GetEngineObjects().Length;
            int[] empty = Engine.GetEmptyEnginePositions();

            if (empty.Length > 0) sprite = empty[0];

            string code = "let Sprite" + sprite + " = new lx.Sprite('res/lynx2d/sprite.png');";

            int spriteR = Engine.AddEngineObject(EngineObjectType.Sprite, code, -1, -1);

            if (spriteR != sprite)
                MessageBox.Show("Invalid id used while creating a Sprite.", "Lynx2D Engine - Warning");

            UpdateHierarchy();

            return sprite;
        }

        private int AddSound()
        {
            int sound = Engine.GetEngineObjects().Length;
            int[] empty = Engine.GetEmptyEnginePositions();

            if (empty.Length > 0) sound = empty[0];

            string code = "let Sound" + sound + " = new lx.Sound('');";

            int soundR = Engine.AddEngineObject(EngineObjectType.Sound, code, -1, -1);

            if (soundR != sound)
                MessageBox.Show("Invalid id used while creating a Sound.", "Lynx2D Engine - Warning");

            UpdateHierarchy();

            return sound;
        }

        private List<int> AddCollider()
        {
            int script = Engine.GetEngineObjects().Length;
            int coll = Engine.GetEngineObjects().Length + 1;
            int[] empty = Engine.GetEmptyEnginePositions();

            if (empty.Length > 1)
            {
                script = empty[0];
                coll = empty[1];
            }

            string scriptCode = "";
            string collCode = "let BoxCollider" + coll + " = new lx.BoxCollider(0, 0, 64, 64, false, function(data) { " + scriptCode + " });";

            int scriptR = Engine.AddEngineObject(EngineObjectType.Script, scriptCode, -1, coll);
            int collR = Engine.AddEngineObject(EngineObjectType.BoxCollider, collCode, script, -1);

            if (scriptR != script)
                MessageBox.Show("Invalid id used while creating a Script.", "Lynx2D Engine - Warning");
            if (collR != coll)
                MessageBox.Show("Invalid id used while creating a BoxCollider.", "Lynx2D Engine - Warning");

            UpdateHierarchy();

            List<int> result = new List<int>
            {
                coll,
                script
            };

            return result;
        }

        private List<int> AddEmitter()
        {
            int sprite = Engine.GetEngineObjects().Length;
            int em = Engine.GetEngineObjects().Length + 1;
            int[] empty = Engine.GetEmptyEnginePositions();

            if (empty.Length > 1)
            {
                sprite = empty[0];
                em = empty[1];
            }

            string spriteCode = "let Sprite" + sprite + " = new lx.Sprite('res/lynx2d/particle.png');";
            int spriteR = Engine.AddEngineObject(EngineObjectType.Sprite, spriteCode, -1, em);

            EngineObject spriteEO = Engine.GetEngineObject(spriteR);
            string emCode =
                "let Emitter" + em + " = new lx.Emitter(Sprite" + sprite + ", 0, 0, " + spriteEO.amount + ", " + spriteEO.duration + ")" +
                ".Speed(" + spriteEO.speed + ")" +
                ".Setup(" + spriteEO.minvx + ", " + spriteEO.maxvx + ", " + spriteEO.minvy + ", " + spriteEO.maxvy + ", " + spriteEO.minSize + ", " + spriteEO.maxSize + ");";
            int emR = Engine.AddEngineObject(EngineObjectType.Emitter, emCode, sprite, -1);

            Engine.SetEngineObjectSource(spriteR, "res/lynx2d/particle.png");

            if (spriteR != sprite)
                MessageBox.Show("Invalid id used while creating a Sprite.", "Lynx2D Engine - Warning");
            if (emR != em)
                MessageBox.Show("Invalid id used while creating a Emitter.", "Lynx2D Engine - Warning");

            UpdateHierarchy();

            List<int> result = new List<int>
            {
                em,
                sprite
            };

            return result;
        }

        private int AddScript()
        {
            string code = "//JavaScript code";

            int result = Engine.AddEngineObject(EngineObjectType.Script, code, -1, -1);

            UpdateHierarchy();

            return result;
        }

        private int AddTilemap()
        {
            int result = Engine.AddEngineObject(EngineObjectType.Tilemap, "//Tilemap code", -1, -1);

            UpdateHierarchy();

            return result;
        }
        #endregion

        #region "Build Toolstrip Stuff"
        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshBrowser();
        }
        #endregion

        #region "Export Toolstrip Stuff"
        private void exportProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Project.Export();
        }

        private void exportSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportSettingsForm temp = new ExportSettingsForm();
            temp.Show();
        }
        #endregion

        #region "Settings Toolstrip Stuff"
        private void cameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.eSettings.camera = cameraToolStripMenuItem.Checked;

            RefreshBrowser();
        }

        private void checkCameraInjection(object sender, EventArgs e)
        {
            if (Engine.eSettings.camera)
                Camera.Inject();
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.eSettings.grid = gridToolStripMenuItem.Checked;

            checkGridInjection(sender, e);
        }

        private void checkGridInjection(object sender, EventArgs e)
        {
            if (Engine.eSettings.grid) Grid.Inject();
            else Grid.Remove();
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridForm gridForm = new GridForm();

            gridForm.Show();
        }

        private void imageSmoothingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.eSettings.imageSmoothing = imageSmoothingToolStripMenuItem.Checked;

            Engine.ExecuteScript("lx.Smoothing(" + Engine.eSettings.imageSmoothing.ToString().ToLower() + ");");
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.eSettings.debug = debugToolStripMenuItem.Checked;

            Engine.ExecuteScript("lx.GAME.DEBUG=" + debugToolStripMenuItem.Checked.ToString().ToLower() + ";");
        }

        private void drawCollidersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.eSettings.drawColliders = drawCollidersToolStripMenuItem.Checked;

            Engine.ExecuteScript("lx.GAME.DRAW_COLLIDERS=" + drawCollidersToolStripMenuItem.Checked.ToString().ToLower() + ";");
        }

        private void scriptBuildOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region "Help Toolstrip Stuff"
        private void viewDocumentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://www.lynx2d.com/documentation");
            }
            catch (Exception exc)
            {
                Feed.GiveException("Documentation", exc);
            }
        }

        private void showDevToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browser.ShowDevTools();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Feed.CheckVersion(true);
        }

        private void reloadFrameworkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Input.YesNo("Do you want to download and (re)install the latest version of the Lynx2D framework to your project?", "Lynx2D Engine - Question")) return;

            Project.DownloadFramework(true);
        }

        private void reloadStandardResourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Input.YesNo("Do you want to reinstall the standard Lynx2D image resources into your project?", "Lynx2D Engine - Question")) return;

            Project.InstallResources(true);
        }

        private void showChangelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Feed.ShowChangelog(false);
        }

        private void restoreBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists("projects/" + Project.Name() + "/backup/"))
                {
                    MessageBox.Show("No backups could be found to restore.", "Lynx2D Engine - Message");
                    return;
                }

                string[] backups = Manager.GetFilesFrom("projects/" + Project.Name() + "/backup/", new string[] { "bin" }, false);
                if (backups.Length == 0)
                {
                    MessageBox.Show("No backups could be found to restore.", "Lynx2D Engine - Message");
                    return;
                }

                for (int i = 0; i < backups.Length; i++)
                    backups[i] = backups[i].Substring(17 + Project.Name().Length, backups[i].Length - (21 + Project.Name().Length));

                string backup = Input.Selection("Load an existing backup for '" + Project.Name() + "'", "Load Backup", backups);
                if (backup == "HAS_BEEN_CLOSED")
                    return;
                
                File.Copy("projects/" + Project.Name() + "/backup/" + backup + ".bin", "projects/" + Project.Name() + "/state.bin", true);

                Engine.LoadEngineState();

                SetStatus("Backup '" + backup + "' has been restored.", StatusType.Message);
            }
            catch (Exception exc)
            {
                Feed.GiveException("Backup Restore", exc);
            }
        }
        #endregion

        #region "Browser Stuff"
        public void LoadEngineSettings()
        {
            cameraToolStripMenuItem.Checked = Engine.eSettings.camera;
            debugToolStripMenuItem.Checked = Engine.eSettings.debug;
            drawCollidersToolStripMenuItem.Checked = Engine.eSettings.drawColliders;
            imageSmoothingToolStripMenuItem.Checked = Engine.eSettings.imageSmoothing;
            gridToolStripMenuItem.Checked = Engine.eSettings.grid;
        }

        public void RefreshBrowser()
        {
            if (browser == null || !Cef.IsInitialized)
                return;

            try
            {
                Camera.Remove();
                Marker.Remove();
                Grid.Remove();
                Obfuscator.Remove();
                Tilemapper.StopEditing();
                Tilemapper.ResetInjections();

                registeredConsoleOutput = new Dictionary<string, bool>();
                
                browser.Load(Project.WorkDirectory() + "/engine.html");

                canViewObjects = true;
            }
            catch (Exception exc)
            {
                Feed.GiveException("Game Refresh", exc);
            }
        }

        public void CreateBrowser()
        {
            if (browser != null || !Cef.IsInitialized)
                return;

            browser = new ChromiumWebBrowser("about:blank");

            browser.DragHandler = new CustomDragHandler();

            browser.LoadingStateChanged += OnBrowserLoadingStateChanged;
            browser.ConsoleMessage += OnBrowserConsoleMessage;
            browser.MenuHandler = new CustomMenuHandler();

            browserContainer.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
        }

        private void OnBrowserLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            if (!args.IsLoading && Project.Name() != string.Empty)
            {
                if (!canViewObjects)
                {
                    canViewObjects = true;

                    UpdateHierarchy();
                }

                Project.Build();

                checkCameraInjection(sender, args);
                checkGridInjection(sender, args);

                debugToolStripMenuItem_Click(sender, args);
                drawCollidersToolStripMenuItem_Click(sender, args);
                imageSmoothingToolStripMenuItem_Click(sender, args);

                if (Engine.bSettings.obfuscates)
                    Obfuscator.Inject();

                Tilemapper.InjectAll();
            }
        }

        private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
        {
            if (args.Message.Contains("Lynx2D"))
                return;
            else if (args.Message.Contains("ENGINE_INTERACTION"))
            {
                string msg = args.Message.Replace("ENGINE_INTERACTION_", "");

                //Post msg to all console message handlers
                Tilemapper.HandleConsoleInteraction(msg);
                Engine.HandleConsoleInteraction(msg);

                return;
            }

            AddToConsole(args.Message);
        }

        delegate void AddToConsoleCallback(string msg);

        public void AddToConsole(string msg)
        {
            if (console.InvokeRequired)
            {
                AddToConsoleCallback d = new AddToConsoleCallback(AddToConsole);
                Invoke(d, new object[] { msg });
            }
            else
            {
                if (registeredConsoleOutput.ContainsKey(msg) && registeredConsoleOutput[msg]) return;

                CheckConsoleVisibility();

                console.AddOutput(msg);

                registeredConsoleOutput.Add(msg, true);
            }
        }

        private void CheckConsoleVisibility()
        {
            if (!consoleVisible)
            {
                registeredConsoleOutput = new Dictionary<string, bool>();
                consoleVisible = true;

                console = new ConsoleForm();
                console.Show();

                console.FormClosing += new FormClosingEventHandler(ConsoleFormClosing);
            }
        }
        private void ConsoleFormClosing(object sender, FormClosingEventArgs e)
        {
            consoleVisible = false;
        }
        #endregion

        #region "Misc Stuff"
        private void removeMarkerInjection(object sender, FormClosedEventArgs e)
        {
            Marker.Remove();
        }

        public void killChildren()
        {
            try
            {
                FormCollection fc = Application.OpenForms;

                foreach (Form frm in fc.Cast<Form>().ToList())
                    if (!(frm is Main)) frm.Close();
            }
            catch (Exception e)
            {
                SetStatus("Could not close all children.", StatusType.Warning);
                MessageBox.Show(e.Message);
            }
        }

        public Form checkChild(string text)
        {
            try
            {
                FormCollection fc = Application.OpenForms;

                foreach (Form frm in fc.Cast<Form>().ToList())
                    if (frm.Text == text) return frm;
            }
            catch (Exception e)
            {
                SetStatus("Could not check engine child.", StatusType.Warning);
                MessageBox.Show(e.Message);
            }

            return null;
        }

        public void LoadTheme(Theme t)
        {
            killChildren();
            
            Engine.ePreferences.theme = t;

            if (t == Theme.Light)
            {
                darkToolStripMenuItem.Checked = false;
                lightToolStripMenuItem.Checked = true;

                MenuStrip.Renderer = new ToolStripProfessionalRenderer();
                MenuStrip.BackColor = LightTheme.menuBackground;
                MenuStrip.ForeColor = LightTheme.font;
 
                statusLabel.BackColor = LightTheme.menuBackground;

                panel1.BackColor = LightTheme.mainBackground;
                hierarchy.BackColor = LightTheme.mainBackground;
                hierarchy.ForeColor = LightTheme.font;

                if (hierarchyView == HierarchyState.Objects)
                    hierarchyObjects.BackColor = LightTheme.background;
                else
                    hierarchyScenes.BackColor = LightTheme.background;

                BackColor = LightTheme.mainBackground;
                ForeColor = LightTheme.font;
            }
            else if (t == Theme.Dark)
            {
                darkToolStripMenuItem.Checked = true;
                lightToolStripMenuItem.Checked = false;

                MenuStrip.Renderer = new ToolStripProfessionalRenderer(new DarkThemeColorTable());
                MenuStrip.BackColor = DarkTheme.menuBackground;
                MenuStrip.ForeColor = DarkTheme.font;

                statusLabel.BackColor = DarkTheme.menuBackground;

                panel1.BackColor = DarkTheme.mainBackground;
                hierarchy.BackColor = DarkTheme.mainBackground;
                hierarchy.ForeColor = DarkTheme.font;

                if (hierarchyView == HierarchyState.Objects)
                {
                    hierarchyObjects.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
                    hierarchyScenes.BackColor = DarkTheme.background;
                }
                else
                {
                    hierarchyObjects.BackColor = DarkTheme.background;
                    hierarchyScenes.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
                }

                BackColor = DarkTheme.mainBackground;
                ForeColor = DarkTheme.font;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            VersionTimer.Enabled = false;

            Feed.CheckVersion(false);
        }
        #endregion
    }
}

public enum HierarchyState
{
    Objects = 0,
    Scenes
}

public class CustomMenuHandler : IContextMenuHandler
{
    public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
    {
        model.Clear();
    }

    public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
    {

        return false;
    }

    public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
    {

    }

    public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
    {
        return false;
    }
}

public class CustomDragHandler : IDragHandler
{
    public bool OnDragEnter(IWebBrowser chromiumWebBrowser, IBrowser browser, IDragData dragData, DragOperationsMask mask)
    {
        return true;
    }

    public void OnDraggableRegionsChanged(IWebBrowser chromiumWebBrowser, IBrowser browser, IList<DraggableRegion> regions)
    {
        //...
    }
}
