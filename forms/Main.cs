using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Lynx2DEngine.forms;

namespace Lynx2DEngine
{
    public partial class Main : Form
    {
        public ChromiumWebBrowser browser = null;

        public ConsoleForm console;
        private bool consoleVisible;
        private HierarchyState hierarchyView = HierarchyState.Objects;

        private EngineObject copied = null;

        #region "Main Stuff"
        public Main()
        {
            InitializeComponent();

            //Create console
            console = new ConsoleForm();

            //Set events
            FormClosing += Form1_FormClosing;
            hierarchy.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(hierarchy_MouseDoubleClick);
            hierarchy.KeyDown += new KeyEventHandler(hierarchy_KeyDown);
            hierarchy.AfterLabelEdit += new NodeLabelEditEventHandler(hierarchy_LabelEdit);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Project.form = this;
            Engine.form = this;
            Feed.form = this;

            Feed.CheckVersion(false);

            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
        }

        private void Form1_FormClosing(object sender, CancelEventArgs e)
        {
            Project.RequestSave();

            Cef.Shutdown();
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
            sceneToolStripMenuItem.Enabled = available;
            exportToolStripMenuItem.Enabled = available;

            hierarchyObjects.Visible = available;
            hierarchyScenes.Visible = available;
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
                        statusLabel.ForeColor = Color.Black;
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

                timer1.Enabled = true;
            }
            catch (Exception e)
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
            timer1.Enabled = false;
            statusLabel.Visible = false;
        }
        #endregion

        #region "Hierarchy Stuff"
        public void SwitchHierarchyView(HierarchyState state)
        {
            if (hierarchyView == state)
                return;

            switch (state)
            {
                case HierarchyState.Objects:
                    hierarchyObjects.BackColor = Color.FromKnownColor(KnownColor.MenuBar);
                    hierarchyScenes.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
                    break;
                case HierarchyState.Scenes:
                    hierarchyObjects.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
                    hierarchyScenes.BackColor = Color.FromKnownColor(KnownColor.MenuBar);
                    break;
            }

            hierarchyView = state;

            UpdateHierarchy();
        }

        public void UpdateHierarchy()
        {
            try
            {
                hierarchy.Nodes.Clear();

                switch (hierarchyView)
                {
                    case HierarchyState.Objects:
                        UpdateHierarchyWithObjects();
                        break;
                    case HierarchyState.Scenes:
                        UpdateHierarchyWithScenes();
                        break;
                }

                hierarchy.Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
                SetStatus("Exception occurred while updating hierarchy.", Main.StatusType.Warning);
            }
        }

        private void UpdateHierarchyWithObjects()
        {
            EngineObject[] objects = Engine.GetEngineObjects();

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] == null) continue;

                if (objects[i].parent == -1)
                {
                    TreeNode[] children = new TreeNode[0];

                    if (objects[i].child != -1)
                    {
                        Array.Resize(ref children, 1);
                        children[0] = new TreeNode(objects[objects[i].child].Variable());
                        children[0].Tag = objects[i].child;
                    }

                    TreeNode node = new TreeNode(objects[i].Variable(), children);
                    node.Tag = i;

                    hierarchy.Nodes.Add(node);
                }
            }
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

                hierarchy.Nodes.Add(node);
            }
        }

        private void hierarchy_MouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (hierarchyView == HierarchyState.Scenes)
            {
                Engine.LoadScene((int)e.Node.Tag);

                return;
            }

            try
            {
                EngineObject obj = Engine.GetEngineObjects()[(int)e.Node.Tag];

                switch (obj.type)
                {
                    case EngineObjectType.GameObject:
                        GameObjectForm go = new GameObjectForm();

                        go.FormClosed += new FormClosedEventHandler(checkCameraInjection);
                        go.FormClosed += new FormClosedEventHandler(removePointerInjection);

                        go.Show();
                        go.Initialize(obj.id);

                        Engine.ExecuteScript(obj.Variable() + ".Focus();");
                        Pointer.Inject(obj.Variable());

                        break;
                    case EngineObjectType.Sprite:
                        SpriteForm sprite = new SpriteForm();

                        sprite.Show();
                        sprite.Initialize(obj.id);

                        break;
                    case EngineObjectType.Script:
                        ScriptForm script = new ScriptForm();

                        script.Show();
                        script.Initialize(obj.id);

                        break;
                    case EngineObjectType.Collider:
                        ColliderForm coll = new ColliderForm();

                        Engine.ExecuteScript("lx.GAME.DRAW_COLLIDERS=true;");
                        coll.FormClosed += new FormClosedEventHandler(drawCollidersToolStripMenuItem_Click);

                        coll.Show();
                        coll.Initialize(obj.id);

                        break;
                    case EngineObjectType.Emitter:
                        EmitterForm emit = new EmitterForm();

                        emit.Show();
                        emit.Initialize(obj.id);

                        break;
                    case EngineObjectType.Tilemap:
                        TilemapForm tilemap = new TilemapForm();

                        tilemap.Show();
                        tilemap.Initialize(obj.id);
                        
                        break;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                SetStatus("Exception occurred while opening engine object.", Main.StatusType.Warning);
            }
        }

        private void hierarchy_KeyDown(object sender, KeyEventArgs e)
        {
            if (hierarchyView == HierarchyState.Scenes)
                return;

            if (e.Control && e.KeyCode == Keys.C) copied = Engine.GetEngineObjects()[(int)hierarchy.SelectedNode.Tag];

            if (e.Control && e.KeyCode == Keys.V)
            {
                e.Handled = true;
                if (copied == null) return;

                List<int> r;
                int result = -1;
                int child = copied.child;

                try
                {
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
                        case EngineObjectType.Collider:
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
                    }

                    if (result == -1) return;

                    EngineObject temp = copied.Clone();
                    temp.id = result;
                    temp.child = child;

                    Engine.SetEngineObject(result, temp);
                    Engine.RenameEngineObject(result, copied.Variable() + "Copy");

                    copied = null;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                    SetStatus("Exception occurred while adding GameObject", StatusType.Warning);
                }
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
                    Engine.RenameEngineObject((int)e.Node.Tag, e.Label);
                else if (hierarchyView == HierarchyState.Scenes)
                    Engine.RenameScene((int)e.Node.Tag, e.Label);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                SetStatus("Exception occurred while renaming item", StatusType.Warning);
            }

            UpdateHierarchy();
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

        #region "File Toolstrip Stuff"
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


        private void showProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@Project.WorkDirectory());
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                SetStatus("Exception occurred trying to open project.", StatusType.Warning);
            }
        }
        #endregion

        #region "Scene Toolstrip Stuff"
        private void addSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.CreateScene(true);

            SwitchHierarchyView(HierarchyState.Scenes);
        }

        private void removeSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.RemoveScene();
        }
        #endregion

        #region "Game Toolstrip Stuff"
        private void addGameObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddGameObject();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                SetStatus("Exception occurred while adding GameObject", StatusType.Warning);
            }
        }


        private void addSpriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddSprite();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                SetStatus("Exception occurred while adding Sprite", Main.StatusType.Warning);
            }
        }

        private void addScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddScript();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                SetStatus("Exception occurred while adding Sprite", Main.StatusType.Warning);
            }
        }


        private void addColliderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddCollider();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                SetStatus("Exception occurred while adding Collider", Main.StatusType.Warning);
            }
        }

        private void addEmitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddEmitter();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                SetStatus("Exception occurred while adding Emitter", Main.StatusType.Warning);
            }
        }

        private void addTilemapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AddTilemap();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                SetStatus("Exception occurred while adding Tilemap", Main.StatusType.Warning);
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

            string spriteCode = "var Sprite" + sprite + " = new lx.Sprite('res/lynx2d/sprite.png');";
            string goCode = "var GameObject" + go + " = new lx.GameObject(Sprite" + sprite + ", 0, 0, 64, 64);";

            int spriteR = Engine.AddEngineObject(EngineObjectType.Sprite, spriteCode, -1, go);
            int goR = Engine.AddEngineObject(EngineObjectType.GameObject, goCode, sprite, -1);

            if (spriteR != sprite)
                MessageBox.Show("Invalid id used while creating a Sprite.", "Lynx2D Engine - Warning");
            if (goR != go)
                MessageBox.Show("Invalid id used while creating a GameObject.", "Lynx2D Engine - Warning");

            UpdateHierarchy();

            List<int> result = new List<int>();
            result.Add(go);
            result.Add(sprite);

            return result;
        }

        private int AddSprite()
        {
            int sprite = Engine.GetEngineObjects().Length;
            int[] empty = Engine.GetEmptyEnginePositions();

            if (empty.Length > 0) sprite = empty[0];

            string code = "var Sprite" + sprite + " = new lx.Sprite('res/lynx2d/sprite.png');";

            int spriteR = Engine.AddEngineObject(EngineObjectType.Sprite, code, -1, -1);

            if (spriteR != sprite)
                MessageBox.Show("Invalid id used while creating a Sprite.", "Lynx2D Engine - Warning");

            UpdateHierarchy();

            return sprite;
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
            string collCode = "var Collider" + coll + " = new lx.Collider(0, 0, 64, 64, false, function(data) { " + scriptCode + " });";

            int scriptR = Engine.AddEngineObject(EngineObjectType.Script, scriptCode, -1, coll);
            int collR = Engine.AddEngineObject(EngineObjectType.Collider, collCode, script, -1);

            if (scriptR != script)
                MessageBox.Show("Invalid id used while creating a Script.", "Lynx2D Engine - Warning");
            if (collR != coll)
                MessageBox.Show("Invalid id used while creating a Collider.", "Lynx2D Engine - Warning");

            UpdateHierarchy();

            List<int> result = new List<int>();
            result.Add(coll);
            result.Add(script);

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

            string spriteCode = "var Sprite" + sprite + " = new lx.Sprite('res/lynx2d/particle.png');";
            int spriteR = Engine.AddEngineObject(EngineObjectType.Sprite, spriteCode, -1, em);

            EngineObject spriteEO = Engine.GetEngineObject(spriteR);
            string emCode =
                "var Emitter" + em + " = new lx.Emitter(Sprite" + sprite + ", 0, 0, " + spriteEO.amount + ", " + spriteEO.duration + ")" +
                ".Speed(" + spriteEO.speed + ")" +
                ".Setup(" + spriteEO.minvx + ", " + spriteEO.maxvx + ", " + spriteEO.minvy + ", " + spriteEO.maxvy + ", " + spriteEO.minSize + ", " + spriteEO.maxSize + ");";
            int emR = Engine.AddEngineObject(EngineObjectType.Emitter, emCode, sprite, -1);

            Engine.SetEngineObjectSource(spriteR, "res/lynx2d/particle.png");

            if (spriteR != sprite)
                MessageBox.Show("Invalid id used while creating a Sprite.", "Lynx2D Engine - Warning");
            if (emR != em)
                MessageBox.Show("Invalid id used while creating a Emitter.", "Lynx2D Engine - Warning");

            UpdateHierarchy();

            List<int> result = new List<int>();
            result.Add(em);
            result.Add(sprite);

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
            refreshBrowser();
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

            refreshBrowser();
        }

        private void checkCameraInjection(object sender, EventArgs e)
        {
            if (cameraToolStripMenuItem.Checked) Camera.Inject();
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

            Engine.ExecuteScript("lx.Smoothing(" + imageSmoothingToolStripMenuItem.Checked.ToString().ToLower() + ");");
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
        #endregion

        #region "Help Toolstrip Stuff"
        private void viewDocumentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://www.lythumn.com/lynx2d/documentation");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                SetStatus("Exception occurred while opening documentation", StatusType.Warning);
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

        public void refreshBrowser()
        {
            try
            {
                Camera.Remove();
                Pointer.Remove();
                Grid.Remove();
                Obfuscater.Remove();
                Tilemapper.RemoveAll();

                browser.Load(Project.WorkDirectory() + "/engine.html");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
                SetStatus("Exception occurred during game refresh.", Main.StatusType.Warning);
            }
        }

        public void createBrowser()
        {
            if (browser != null || !Cef.IsInitialized)
                return;

            browser = new ChromiumWebBrowser("about:blank");
            browser.BrowserSettings = new BrowserSettings()
            {
                BackgroundColor = Cef.ColorSetARGB(0, 255, 255, 255)
            };

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
                Project.Build();

                checkCameraInjection(sender, args);
                checkGridInjection(sender, args);

                debugToolStripMenuItem_Click(sender, args);
                drawCollidersToolStripMenuItem_Click(sender, args);
                imageSmoothingToolStripMenuItem_Click(sender, args);

                if (Engine.bSettings.obfuscates)
                    Obfuscater.Inject();

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

                return;
            }

            AddToConsole(args.Message);
        }

        public void AddToConsole(string msg)
        {
            CheckConsoleVisibility();
            
            console.AddOutput(msg);
        }

        private void CheckConsoleVisibility()
        {
            if (!consoleVisible)
            {
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
        private void removePointerInjection(object sender, FormClosedEventArgs e)
        {
            Pointer.Remove();
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
        #endregion
    }
}

public enum HierarchyState
{
    Objects = 0,
    Scenes
}

public class CustomMenuHandler : CefSharp.IContextMenuHandler
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
