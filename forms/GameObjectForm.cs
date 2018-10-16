using System;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    public partial class GameObjectForm : Form
    {
        private bool canDetect = false;

        public int id;
        public int engineId;

        private EngineObject obj;

        public GameObjectForm()
        {
            InitializeComponent();
        }

        public void Initialize(int engineId)
        {
            this.engineId = engineId;
            UpdateTitle();

            id = obj.id;

            x.Value = obj.x;
            y.Value = obj.y;

            w.Value = obj.w;
            h.Value = obj.h;

            updateSpriteSelection();
            updateColliderSelection();

            layer.Value = obj.layer;
            visible.Checked = obj.visible;

            System.Threading.Thread.Sleep(10);

            canDetect = true;
        }

        private void updateSpriteSelection()
        {
            if (sprite.Items.Count == 0)
                foreach (EngineObject o in Engine.GetEngineObjectsWithType(EngineObjectType.Sprite))
                {
                    if (o == null)
                        continue;

                    sprite.Items.Add(o);
                }

            sprite.Text = obj.sprite;
        }

        private void updateColliderSelection()
        {
            if (collider.Items.Count == 0)
            {
                collider.Items.Add("<None>");

                foreach (EngineObject o in Engine.GetEngineObjectsWithType(EngineObjectType.Collider))
                {
                    if (o == null)
                        continue;

                    collider.Items.Add(o);
                }
            }

            if (obj.collider == string.Empty)
                collider.Text = "<None>";
            else
                collider.Text = obj.collider;
        }

        private void UpdateTitle()
        {
            obj = Engine.GetEngineObjects()[engineId];

            Text = "GO (" + obj.Variable() + ")";
        }

        private void x_ValueChanged(object sender, EventArgs e)
        {
            SetPosition();
        }

        private void y_ValueChanged(object sender, EventArgs e)
        {
            SetPosition();
        }

        private void w_ValueChanged(object sender, EventArgs e)
        {
            SetSize();
        }

        private void h_ValueChanged(object sender, EventArgs e)
        {
            SetSize();
        }

        private void layer_ValueChanged(object sender, EventArgs e)
        {
            if (!canDetect) return;

            Engine.SetEngineObjectLayer(engineId, (int)layer.Value);

            Engine.ExecuteScript(obj.Variable() + ".Hide().Show(" + layer.Value + ");");
        }

        private void visible_CheckedChanged(object sender, EventArgs e)
        {
            if (!canDetect) return;

            Engine.SetEngineObjectVisible(engineId, visible.Checked);

            if (!visible.Checked) Engine.ExecuteScript(obj.Variable() + ".Hide();");
            else Engine.ExecuteScript(obj.Variable() + ".Show(" + obj.layer + ");");
        }

        private void SetPosition()
        {
            if (!canDetect) return;

            try
            {
                Engine.SetEngineObjectPosition(engineId, (int)x.Value, (int)y.Value);

                Engine.ExecuteScript(obj.Variable() + ".Position(" + x.Value + ", " + y.Value + ");");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
            }
        }

        private void SetSize()
        {
            if (!canDetect) return;

            try
            {
                Engine.SetEngineObjectSize(engineId, (int)w.Value, (int)h.Value);

                Engine.ExecuteScript(obj.Variable() + ".Size(" + w.Value + ", " + h.Value + ");");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
            }
        }

        private void GameObjectForm_Load(object sender, EventArgs e)
        {
            x.Maximum = Decimal.MaxValue;
            y.Maximum = Decimal.MaxValue;
            x.Minimum = -Decimal.MaxValue;
            y.Minimum = -Decimal.MaxValue;

            w.Maximum = Decimal.MaxValue;
            h.Maximum = Decimal.MaxValue;
            w.Minimum = -Decimal.MaxValue;
            h.Minimum = -Decimal.MaxValue;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Engine.RemoveEngineObject(engineId, true);

            Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Engine.RenameEngineObject(engineId, Input.Prompt("Enter the new name", "Rename " + obj.Variable()));

            UpdateTitle();
        }

        private void sprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!canDetect) return;

            Engine.SetEngineObjectSprite(engineId, sprite.Text);
            Engine.ExecuteScript(obj.Variable() + ".SPRITE = " + sprite.Text + ";");
        }

        private void collider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!canDetect) return;

            string r = collider.Text;
            removeCollider();

            if (r == "<None>")
                r = string.Empty;
            else
                Engine.ExecuteScript(obj.Variable() + ".ApplyCollider(" + collider.Text + ");");

            Engine.SetEngineObjectCollider(engineId, r);
        }

        private void removeCollider()
        {
            if (!canDetect) return;

            EngineObject coll = Engine.GetEngineObjectWithVarName(Engine.GetEngineObject(engineId).collider);
            if (coll == null)
                return;

            Engine.ExecuteScript(obj.Variable() + ".COLLIDER = undefined; " + Engine.GetEngineObject(engineId).collider + ".Position(" + coll.x + ", " + coll.y + ");");

            Engine.RemoveEngineObjectCollider(engineId);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

