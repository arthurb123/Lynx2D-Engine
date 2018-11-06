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
    public partial class SoundForm : Form
    {
        public int id;
        public int engineId;

        private EngineObject obj;

        public SoundForm()
        {
            InitializeComponent();
        }

        public void Initialize(int engineId)
        {
            this.engineId = engineId;
            UpdateTitle();

            id = obj.id;
            UpdateSoundSelection();

            x.Value = obj.x;
            y.Value = obj.y;

            channel.Value = obj.layer;
        }

        private void UpdateTitle()
        {
            obj = Engine.GetEngineObject(engineId);
            Text = obj.Variable();
        }

        private void SoundForm_Load(object sender, EventArgs e)
        {
            x.Maximum = Decimal.MaxValue;
            y.Maximum = Decimal.MaxValue;
            x.Minimum = -Decimal.MaxValue;
            y.Minimum = -Decimal.MaxValue;

            CheckTheme();
        }

        private void CheckTheme()
        {
            if (Engine.ePreferences.theme == classes.Theme.Dark)
            {
                BackColor = classes.DarkTheme.mainBackground;
                ForeColor = classes.DarkTheme.font;

                pointer.BackColor = classes.DarkTheme.background;
                pointer.FlatAppearance.BorderColor = classes.DarkTheme.border;
            }
        }

        private void x_ValueChanged(object sender, EventArgs e)
        {
            SetPosition();
        }

        private void y_ValueChanged(object sender, EventArgs e)
        {
            SetPosition();
        }

        private void channel_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Engine.SetEngineObjectLayer(engineId, (int)channel.Value);

                Engine.ExecuteScript(obj.Variable() + ".CHANNEL = " + channel.Value + ";");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Engine.RenameEngineObject(engineId, Input.Prompt("Enter the new name", "Rename " + obj.Variable()), true);

            UpdateTitle();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Engine.RemoveEngineObject(engineId, true, true);

            Close();
        }

        private void SetPosition()
        {
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

        private void pointer_Click(object sender, EventArgs e)
        {
            if (pointer.BackgroundImage == null)
            {
                pointer.BackgroundImage = Properties.Resources.location;

                Pointer.Inject(obj.Variable());
            }
            else
            {
                pointer.BackgroundImage = null;

                Pointer.Remove();
            }
        }

        private void UpdateSoundSelection()
        {
            string[] filters = new string[] { "mp3", "wav", "ogg" };
            string[] sounds = Manager.GetFilesFrom("projects/" + Project.Name() + "/res/", filters, true);

            for (int i = 0; i < sounds.Length; i++)
                sounds[i] = sounds[i].Substring(10 + Project.Name().Length, sounds[i].Length - (10 + Project.Name().Length));

            source.Items.Clear();
            source.Items.AddRange(sounds);

            source.Text = obj.source;
        }

        private void source_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (source.Text == string.Empty) return;

            try
            {
                Engine.SetEngineObjectSource(engineId, source.Text);

                Engine.ExecuteScript(obj.Variable() + ".SRC = '" + source.Text + "';");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
            }
        }
    }
}
