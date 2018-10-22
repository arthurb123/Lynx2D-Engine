﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lynx2DEngine.forms
{
    public partial class ExportSettingsForm : Form
    {
        public ExportSettingsForm()
        {
            InitializeComponent();
        }

        private void ExportSettingsForm_Load(object sender, EventArgs e)
        {
            FormClosing += ExportSettingsForm_Closing;

            hasIcon.Checked = Engine.bSettings.hasIcon;
            hasIcon_CheckedChanged(sender, e);

            lineBreaks.Value = Engine.bSettings.lineBreaks;
            obfuscates.Checked = Engine.bSettings.obfuscates;

            ReloadIconImage();
            UpdateScenesCollection();
        }

        private void UpdateScenesCollection()
        {
            if (standardScene.Items.Count == 0)
            {
                foreach (Scene s in Engine.scenes)
                {
                    if (s == null)
                        continue;

                    standardScene.Items.Add(s);
                }
            }

            standardScene.Text = Engine.scenes[Engine.bSettings.standardScene].Variable();
        }

        private void hasIcon_CheckedChanged(object sender, EventArgs e)
        {
            iconLocation.Enabled = hasIcon.Checked;

            Engine.bSettings.hasIcon = hasIcon.Checked;

            if (!hasIcon.Checked) iconLocation.Text = "";
            else iconLocation.Text = Engine.bSettings.iconLocation;
        }

        private void iconLocation_TextChanged(object sender, EventArgs e)
        {
            Engine.bSettings.iconLocation = iconLocation.Text;

            ReloadIconImage();
        }

        private void ReloadIconImage()
        {
            try
            {
                iconImage.ImageLocation = Project.WorkDirectory() + iconLocation.Text;
            }
            catch (Exception e)
            {
                iconMessage.Text = e.Message;
                iconMessage.Visible = true;
            }

            iconMessage.Visible = false;
        }

        private void lineBreaks_ValueChanged(object sender, EventArgs e)
        {
            if (!lineBreaks.Enabled) return;

            Engine.bSettings.lineBreaks = (int)lineBreaks.Value;
        }

        private void obfuscates_CheckedChanged(object sender, EventArgs e)
        {
            Engine.bSettings.obfuscates = obfuscates.Checked;

            if (obfuscates.Checked)
            {
                Obfuscater.Inject();

                lineBreaks.Enabled = false;
                lineBreaks.Value = 0;
            }
            else
            {
                Obfuscater.Remove();

                lineBreaks.Value = Engine.bSettings.lineBreaks;
                lineBreaks.Enabled = true;
            }
        }

        private void ExportSettingsForm_Closing(object sender, EventArgs e)
        {
            if (iconLocation.Text.Length > 0 && !iconLocation.Text.Contains(".ico"))
                MessageBox.Show("Not a valid icon (.ico file) specified, the specified icon will probably not display upon build.", "Lynx2D Engine - Warning");

            Project.Save();
        }

        private void standardScene_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Engine.scenes.Length; i++)
            {
                if (Engine.scenes[i] != null && Engine.scenes[i].Variable() == standardScene.Text)
                {
                    Engine.bSettings.standardScene = i;
                    break;
                }
            }
        }
    }
}