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

namespace Lynx2DEngine.forms
{
    public partial class BuildSettingsForm : Form
    {
        public BuildSettingsForm()
        {
            InitializeComponent();
        }

        private void BuildSettingsForm_Load(object sender, EventArgs e)
        {
            FormClosing += BuildSettingsForm_Closing;

            hasIcon.Checked = Engine.bSettings.hasIcon;
            hasIcon_CheckedChanged(sender, e);

            lineBreaks.Value = Engine.bSettings.lineBreaks;
            obfuscates.Checked = Engine.bSettings.obfuscates;

            ReloadIconImage();
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

        private void BuildSettingsForm_Closing(object sender, EventArgs e)
        {
            if (iconLocation.Text.Length > 0 && !iconLocation.Text.Contains(".ico"))
                MessageBox.Show("Not a valid icon (.ico file) specified, the specified icon will probably not display upon build.", "Lynx2D Engine - Warning");

            Project.Save();
        }
    }
}
