using System;
using System.Windows.Forms;

namespace Lynx2DEngine.forms
{
    public partial class ColliderForm : Form
    {
        private bool canDetect = false;

        public int id;
        public int engineId;

        private EngineObject obj;

        public ColliderForm()
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

            if (obj.applied) label2.Text = "Offset";

            w.Value = obj.w;
            h.Value = obj.h;
 
            visible.Checked = obj.visible;
            isStatic.Checked = obj.isStatic;
            solid.Checked = obj.isSolid;

            if (obj.child == -1)
                button1.Enabled = false;

            System.Threading.Thread.Sleep(10);

            canDetect = true;
        }

        private void UpdateTitle()
        {
            obj = Engine.GetEngineObjects()[engineId];

            Text = obj.Variable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (obj.child == -1)
                return;

            ScriptForm script = new ScriptForm();

            script.Show();
            script.Initialize(obj.child);
        }

        private void visible_CheckedChanged(object sender, EventArgs e)
        {
            if (!canDetect) return;

            Engine.SetEngineObjectVisible(engineId, visible.Checked);

            if (!visible.Checked) Engine.ExecuteScript(obj.Variable() + ".Disable();");
            else Engine.ExecuteScript(obj.Variable() + ".Enable();");
        }

        private void isStatic_CheckedChanged(object sender, EventArgs e)
        {
            if (!canDetect) return;

            Engine.SetEngineObjectStatic(engineId, isStatic.Checked);

            Engine.ExecuteScript(obj.Variable() + ".STATIC = " + isStatic.Checked.ToString().ToLower() + ";");
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

        private void SetPosition()
        {
            if (!canDetect) return;

            try
            {
                Engine.SetEngineObjectPosition(engineId, (int)x.Value, (int)y.Value);

                if (!obj.applied)
                    Engine.ExecuteScript(obj.Variable() + ".Position(" + x.Value + ", " + y.Value + ");");
                else
                    Engine.ExecuteScript(obj.Variable() + ".OFFSET = { X: " + x.Value + ", Y: " + y.Value + " };");
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

        private void ColliderForm_Load(object sender, EventArgs e)
        {
            x.Maximum = Decimal.MaxValue;
            y.Maximum = Decimal.MaxValue;
            x.Minimum = -Decimal.MaxValue;
            y.Minimum = -Decimal.MaxValue;

            w.Maximum = Decimal.MaxValue;
            h.Maximum = Decimal.MaxValue;
            w.Minimum = -Decimal.MaxValue;
            h.Minimum = -Decimal.MaxValue;

            CheckTheme();
        }

        private void CheckTheme()
        {
            if (Engine.ePreferences.theme == classes.Theme.Dark)
            {
                BackColor = classes.DarkTheme.mainBackground;
                ForeColor = classes.DarkTheme.font;

                button1.BackColor = classes.DarkTheme.background;
                button1.FlatStyle = FlatStyle.Flat;
                button1.FlatAppearance.BorderColor = classes.DarkTheme.border;

                pointer.BackColor = classes.DarkTheme.background;
                pointer.FlatAppearance.BorderColor = classes.DarkTheme.border;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Engine.RemoveEngineObject(engineId, true, true);
            Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Engine.RenameEngineObject(engineId, Input.Prompt("Enter the new name", "Rename " + obj.Variable()), true);

            UpdateTitle();
        }

        private void solid_CheckedChanged(object sender, EventArgs e)
        {
            if (!canDetect) return;

            Engine.SetEngineObjectSolid(engineId, solid.Checked);

            Engine.ExecuteScript(obj.Variable() + ".SOLID = " + solid.Checked.ToString().ToLower() + ";");
        }

        private void pointer_Click(object sender, EventArgs e)
        {
            if (pointer.BackgroundImage == null)
            {
                pointer.BackgroundImage = Properties.Resources.location;

                Marker.Inject(obj.Variable());
            }
            else
            {
                pointer.BackgroundImage = null;

                Marker.Remove();
            }
        }
    }
}
