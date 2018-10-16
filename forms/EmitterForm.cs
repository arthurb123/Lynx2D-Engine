using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lynx2DEngine.forms
{
    public partial class EmitterForm : Form
    {
        private bool canDetect = false;

        public int id;
        public int engineId;

        private EngineObject obj;

        public EmitterForm()
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
            minX.Value = (decimal)obj.minvx;
            maxX.Value = (decimal)obj.maxvx;
            minY.Value = (decimal)obj.minvy;
            maxY.Value = (decimal)obj.maxvy;
            minSize.Value = obj.minSize;
            maxSize.Value = obj.maxSize;
            amount.Value = obj.amount;
            duration.Value = obj.duration;
            speed.Value = (decimal)obj.speed;
            layer.Value = obj.layer;
            visible.Checked = obj.visible;

            updateSpriteSelection();

            System.Threading.Thread.Sleep(10);

            canDetect = true;
        }

        private void UpdateTitle()
        {
            obj = Engine.GetEngineObjects()[engineId];

            Text = "Emitter (" + obj.Variable() + ")";
        }

        private void EmitterForm_Load(object sender, EventArgs e)
        {
            x.Maximum = Decimal.MaxValue;
            y.Maximum = Decimal.MaxValue;
            x.Minimum = -Decimal.MaxValue;
            y.Minimum = -Decimal.MaxValue;

            minX.Maximum = Decimal.MaxValue;
            maxX.Maximum = Decimal.MaxValue;
            minX.Minimum = -Decimal.MaxValue;
            maxX.Minimum = -Decimal.MaxValue;

            minY.Maximum = Decimal.MaxValue;
            maxY.Maximum = Decimal.MaxValue;
            minY.Minimum = -Decimal.MaxValue;
            maxY.Minimum = -Decimal.MaxValue;

            minSize.Maximum = Decimal.MaxValue;
            maxSize.Maximum = Decimal.MaxValue;
            minSize.Minimum = -Decimal.MaxValue;
            maxSize.Minimum = -Decimal.MaxValue;
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

        private void SetPosition()
        {
            if (!canDetect) return;

            Engine.SetEngineObjectPosition(engineId, (int)x.Value, (int)y.Value);

            Engine.ExecuteScript(obj.Variable() + ".Position(" + x.Value + ", " + y.Value + ");");
        }

        private void SetSetup()
        {
            if (!canDetect) return;

            Engine.SetEngineObjectSetup(engineId, (float)minX.Value, (float)maxX.Value, (float)minY.Value, (float)maxY.Value, (int)minSize.Value, (int)maxSize.Value);

            Engine.ExecuteScript(obj.Variable() + ".Setup(" + minX.Value + ", " + maxX.Value + ", " + minY.Value + ", " + maxY.Value + ", " + minSize.Value + ", " + maxSize.Value + ");");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Engine.RenameEngineObject(engineId, Input.Prompt("Enter the new name", "Rename " + obj.Variable()));

            UpdateTitle();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Engine.RemoveEngineObject(engineId, true);
            Close();
        }

        private void x_ValueChanged(object sender, EventArgs e)
        {
            SetPosition();
        }

        private void y_ValueChanged(object sender, EventArgs e)
        {
            SetPosition();
        }

        private void minX_ValueChanged(object sender, EventArgs e)
        {
            SetSetup();
        }

        private void maxX_ValueChanged(object sender, EventArgs e)
        {
            SetSetup();
        }

        private void minY_ValueChanged(object sender, EventArgs e)
        {
            SetSetup();
        }

        private void maxY_ValueChanged(object sender, EventArgs e)
        {
            SetSetup();
        }

        private void minSize_ValueChanged(object sender, EventArgs e)
        {
            SetSetup();
        }

        private void maxSize_ValueChanged(object sender, EventArgs e)
        {
            SetSetup();
        }

        private void amount_ValueChanged(object sender, EventArgs e)
        {
            Engine.SetEngineObjectAmount(engineId, (int)amount.Value);

            Engine.ExecuteScript(obj.Variable() + ".AMOUNT = " + amount.Value + ";");
        }

        private void duration_ValueChanged(object sender, EventArgs e)
        {
            Engine.SetEngineObjectDuration(engineId, (int)duration.Value);

            Engine.ExecuteScript(obj.Variable() + ".DURATION = " + duration.Value + ";");
        }

        private void visible_CheckedChanged(object sender, EventArgs e)
        {
            Engine.SetEngineObjectVisible(engineId, visible.Checked);

            if (!visible.Checked)
            {
                Engine.ExecuteScript(obj.Variable() + ".Hide()");
                layer.Enabled = false;
            }
            else
            {
                Engine.ExecuteScript(obj.Variable() + ".Show(" + layer.Value + ");");
                layer.Enabled = true;
            }
        }

        private void layer_ValueChanged(object sender, EventArgs e)
        {
            Engine.SetEngineObjectLayer(engineId, (int)layer.Value);

            Engine.ExecuteScript(obj.Variable() + ".Hide().Show(" + layer.Value + ");");
        }

        private void speed_ValueChanged(object sender, EventArgs e)
        {
            Engine.SetEngineObjectSpeed(engineId, (float)speed.Value);

            Engine.ExecuteScript(obj.Variable() + ".Speed(" + speed.Value + ");");
        }

        private void sprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.SetEngineObjectSprite(engineId, sprite.Text);

            Engine.ExecuteScript(obj.Variable() + ".SPRITE = " + sprite.Text + ";");
        }
    }
}
