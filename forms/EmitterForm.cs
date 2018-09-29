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

        private void SetSetup()
        {
            if (!canDetect) return;

            try
            {
                Engine.SetEngineObjectSetup(engineId, (float)minX.Value, (float)maxX.Value, (float)minY.Value, (float)maxY.Value, (int)minSize.Value, (int)maxSize.Value);

                Engine.ExecuteScript(obj.Variable() + ".Setup(" + minX.Value + ", " + maxX.Value + ", " + minY.Value + ", " + maxY.Value + ", " + minSize.Value + ", " + maxSize.Value + ");");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
            }
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
            try
            {
                Engine.SetEngineObjectAmount(engineId, (int)amount.Value);

                Engine.ExecuteScript(obj.Variable() + ".AMOUNT = " + amount.Value + ";");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lynx2D Engine - Exception");
            }
        }

        private void duration_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Engine.SetEngineObjectDuration(engineId, (int)duration.Value);

                Engine.ExecuteScript(obj.Variable() + ".DURATION = " + duration.Value + ";");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lynx2D Engine - Exception");
            }
        }
    }
}
