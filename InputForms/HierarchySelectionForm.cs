using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lynx2DEngine.InputForms
{
    public partial class HierarchySelectionForm : Form
    {
        private int engineId = -1;
        private int scene = -1;

        public HierarchySelectionForm()
        {
            InitializeComponent();
        }

        private void HierarchySelectionForm_Load(object sender, EventArgs e)
        {
            hierarchy.ImageList = Engine.form.hierarchyList;
            hierarchy.NodeMouseDoubleClick += (object s, TreeNodeMouseClickEventArgs tea) =>
            {
                if (hierarchy.SelectedNode.ImageIndex == 8 || hierarchy.SelectedNode.ImageIndex == 1)
                    return;

                string tag = hierarchy.SelectedNode.Tag.ToString();

                int.TryParse(tag.Substring(0, tag.IndexOf("/")), out scene);
                int.TryParse(tag.Substring(tag.IndexOf("/")+1, tag.Length-tag.IndexOf("/")-1), out engineId);

                DialogResult = DialogResult.OK;

                Close();
            };

            CheckTheme();

            UpdateHierarchy();
        }

        private void CheckTheme()
        {
            if (Engine.ePreferences.theme == classes.Theme.Dark)
            {
                BackColor = classes.DarkTheme.mainBackground;
                ForeColor = classes.DarkTheme.font;

                hierarchy.BackColor = classes.DarkTheme.mainBackground;
                hierarchy.ForeColor = classes.DarkTheme.font;
            }
        }

        public void SetTitle(string title)
        {
            Text = title;
        }

        public void SetCaption(string caption)
        {
            this.caption.Text = caption;
        }

        public Point Value()
        {
            return new Point(scene, engineId);
        }

        private void UpdateHierarchy()
        {
            Scene[] scenes = Engine.scenes;
            List<TreeNode> nodes = new List<TreeNode>();

            for (int i = 0; i < scenes.Length; i++)
            {
                if (scenes[i] == null || i == Engine.eSettings.currentScene) continue;

                List<TreeNode> sceneObjects = new List<TreeNode>();

                //Folders
                for (int e = 0; e < scenes[i].hierarchy.folders.Count; e++)
                {
                    HierarchyFolder f = scenes[i].hierarchy.folders[e];
                    List<TreeNode> folderContent = new List<TreeNode>();

                    foreach (HierarchyItem item in f.content)
                    {
                        if (item.isLink)
                            continue;

                        EngineObject obj = Engine.scenes[i].objects[item.engineId];

                        if (obj != null && obj.parent == -1)
                        {
                            List<TreeNode> children = new List<TreeNode>();

                            if (obj.child != -1)
                            {
                                EngineObject childEO = Engine.scenes[i].objects[obj.child];

                                TreeNode child = new TreeNode(childEO.Variable())
                                {
                                    Tag = i + "/" + obj.child
                                };

                                if (childEO.type == EngineObjectType.Sprite)
                                    child.ImageIndex = 3;
                                if (childEO.type == EngineObjectType.Script)
                                    child.ImageIndex = 6;

                                child.SelectedImageIndex = child.ImageIndex;
                                children.Add(child);
                            }

                            TreeNode eo = new TreeNode(obj.Variable(), children.ToArray())
                            {
                                Tag = i + "/" + item.engineId
                            };

                            if (obj.type == EngineObjectType.GameObject)
                                eo.ImageIndex = 2;
                            if (obj.type == EngineObjectType.Sprite)
                                eo.ImageIndex = 3;
                            if (obj.type == EngineObjectType.Collider)
                                eo.ImageIndex = 4;
                            if (obj.type == EngineObjectType.Emitter)
                                eo.ImageIndex = 5;
                            if (obj.type == EngineObjectType.Script)
                                eo.ImageIndex = 6;
                            if (obj.type == EngineObjectType.Tilemap)
                                eo.ImageIndex = 7;
                            if (obj.type == EngineObjectType.Sound)
                                eo.ImageIndex = 9;

                            eo.SelectedImageIndex = eo.ImageIndex;
                            folderContent.Add(eo);
                        }
                    }

                    TreeNode ftn = new TreeNode(f.name, folderContent.ToArray())
                    {
                        ImageIndex = 1
                    };

                    ftn.SelectedImageIndex = ftn.ImageIndex;
                    sceneObjects.Add(ftn);
                }

                //Objects
                foreach (HierarchyItem item in scenes[i].hierarchy.items)
                {
                    if (item.isLink)
                        continue;

                    EngineObject obj = Engine.scenes[i].objects[item.engineId];

                    if (obj != null && obj.parent == -1)
                    {
                        List<TreeNode> children = new List<TreeNode>();

                        if (obj.child != -1)
                        {
                            EngineObject childEO = Engine.scenes[i].objects[obj.child];

                            TreeNode child = new TreeNode(childEO.Variable())
                            {
                                Tag = i + "/" + obj.child
                            };

                            if (childEO.type == EngineObjectType.Sprite)
                                child.ImageIndex = 3;
                            if (childEO.type == EngineObjectType.Script)
                                child.ImageIndex = 6;

                            child.SelectedImageIndex = child.ImageIndex;
                            children.Add(child);
                        }

                        TreeNode eo = new TreeNode(obj.Variable(), children.ToArray())
                        {
                            Tag = i + "/" + item.engineId
                        };

                        if (obj.type == EngineObjectType.GameObject)
                            eo.ImageIndex = 2;
                        if (obj.type == EngineObjectType.Sprite)
                            eo.ImageIndex = 3;
                        if (obj.type == EngineObjectType.Collider)
                            eo.ImageIndex = 4;
                        if (obj.type == EngineObjectType.Emitter)
                            eo.ImageIndex = 5;
                        if (obj.type == EngineObjectType.Script)
                            eo.ImageIndex = 6;
                        if (obj.type == EngineObjectType.Tilemap)
                            eo.ImageIndex = 7;
                        if (obj.type == EngineObjectType.Sound)
                            eo.ImageIndex = 9;

                        eo.SelectedImageIndex = eo.ImageIndex;
                        sceneObjects.Add(eo);
                    }
                }

                TreeNode node = new TreeNode(scenes[i].Variable(), sceneObjects.ToArray());

                node.ImageIndex = 8;
                node.SelectedImageIndex = node.ImageIndex;

                nodes.Add(node);
            }

            hierarchy.Nodes.AddRange(nodes.ToArray());
        }
    }
}
