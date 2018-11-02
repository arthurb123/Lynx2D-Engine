using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    public partial class SpriteForm : Form
    {
        private bool canDetect = false;

        public int id;
        public int engineId;

        private EngineObject obj;

        public SpriteForm()
        {
            InitializeComponent();
        }

        public void Initialize(int engineId)
        {
            this.engineId = engineId;
            UpdateTitle();

            id = obj.id;

            UpdateSpriteSelection();

            rotation.Value = obj.rotation;

            if (obj.clipped)
            {
                cX.Value = obj.cx;
                cY.Value = obj.cy;
                cW.Value = obj.cw;
                cH.Value = obj.ch;
            }
            clipped.Checked = obj.clipped;

            System.Threading.Thread.Sleep(10);

            GetSpriteSize();

            canDetect = true;
        }

        private void UpdateSpriteSelection()
        {
            string[] filters = new string[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
            string[] sprites = Manager.GetFilesFrom("projects/" + Project.Name() + "/res/", filters, true);

            for (int i = 0; i < sprites.Length; i++)
                sprites[i] = sprites[i].Substring(10+Project.Name().Length, sprites[i].Length - (10+Project.Name().Length));

            source.Items.Clear();
            source.Items.AddRange(sprites);

            source.Text = obj.source;
        }

        private void UpdateTitle()
        {
            obj = Engine.GetEngineObjects()[engineId];

            Text = obj.Variable();
        }

        private async void GetSpriteSize()
        {
            try
            {
                w.Value = int.Parse(await Engine.ExecuteScriptWithResult(obj.Variable() + ".Size().W;"));
                h.Value = int.Parse(await Engine.ExecuteScriptWithResult(obj.Variable() + ".Size().H;"));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
            }
        }

        private void SetClip()
        {
            if (!canDetect || !obj.clipped) return;

            try
            {
                Engine.SetEngineObjectClip(engineId, (int)cX.Value, (int)cY.Value, (int)cW.Value, (int)cH.Value);

                Engine.ExecuteScript(obj.Variable() + ".Clip(" + (int)cX.Value + ", " + (int)cY.Value + ", " + (int)cW.Value + ", " + (int)cH.Value + ");");

                GetSpriteSize();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
            }
        }

        private void SpriteForm_Load(object sender, EventArgs e)
        {
            w.Maximum = Decimal.MaxValue;
            h.Maximum = Decimal.MaxValue;
            w.Minimum = -Decimal.MaxValue;
            h.Minimum = -Decimal.MaxValue;
            cX.Maximum = Decimal.MaxValue;
            cY.Maximum = Decimal.MaxValue;
            cX.Minimum = -Decimal.MaxValue;
            cY.Minimum = -Decimal.MaxValue;
            cW.Maximum = Decimal.MaxValue;
            cH.Maximum = Decimal.MaxValue;
            cW.Minimum = -Decimal.MaxValue;
            cH.Minimum = -Decimal.MaxValue;

            CheckTheme();
        }

        private void CheckTheme()
        {
            if (Engine.ePreferences.theme == classes.Theme.Dark)
            {
                BackColor = classes.DarkTheme.mainBackground;
                ForeColor = classes.DarkTheme.font;
            }
        }

        private void cX_ValueChanged(object sender, EventArgs e)
        {
            SetClip();
        }

        private void cY_ValueChanged(object sender, EventArgs e)
        {
            SetClip();
        }

        private void cW_ValueChanged(object sender, EventArgs e)
        {
            SetClip();
        }

        private void cH_ValueChanged(object sender, EventArgs e)
        {
            SetClip();
        }

        private void clipped_CheckedChanged(object sender, EventArgs e)
        {
            if (!canDetect) return;

            try
            {
                Engine.SetEngineObjectClipped(engineId, clipped.Checked);

                if (!clipped.Checked) Engine.ExecuteScript(obj.Variable() + ".CLIP = undefined");
                else SetClip();

                GetSpriteSize();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
            }
        }

        private void rotation_ValueChanged(object sender, EventArgs e)
        {
            if (!canDetect) return;

            try
            {
                Engine.SetEngineObjectRotation(engineId, (int)rotation.Value);

                Engine.ExecuteScript(obj.Variable() + ".Rotation(" + ((double)rotation.Value * Math.PI / 180) + ");");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
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

        private void sprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!canDetect || source.Text == string.Empty) return;

            try
            {
                Engine.SetEngineObjectSource(engineId, source.Text);

                Engine.ExecuteScript(obj.Variable() + ".Source('" + source.Text + "');");

                GetSpriteSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lynx2D Engine - Exception");
            }
        }
    }
}
