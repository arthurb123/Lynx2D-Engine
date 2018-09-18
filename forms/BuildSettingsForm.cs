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
    public partial class BuildSettingsForm : Form
    {
        public BuildSettingsForm()
        {
            InitializeComponent();
        }

        private void BuildSettingsForm_Load(object sender, EventArgs e)
        {
            FormClosing += BuildSettingsForm_Closing;

            hasIcon.Checked = Engine.settings.hasIcon;
            hasIcon_CheckedChanged(sender, e);

            lineBreaks.Value = Engine.settings.lineBreaks;
            obfuscates.Checked = Engine.settings.obfuscates;
        }

        private void hasIcon_CheckedChanged(object sender, EventArgs e)
        {
            iconLocation.Enabled = hasIcon.Checked;

            Engine.settings.hasIcon = hasIcon.Checked;

            if (!hasIcon.Checked) iconLocation.Text = "";
            else iconLocation.Text = Engine.settings.iconLocation;
        }

        private void iconLocation_TextChanged(object sender, EventArgs e)
        {
            Engine.settings.iconLocation = iconLocation.Text;
        }

        private void lineBreaks_ValueChanged(object sender, EventArgs e)
        {
            Engine.settings.lineBreaks = (int)lineBreaks.Value;
        }

        private void obfuscates_CheckedChanged(object sender, EventArgs e)
        {
            Engine.settings.obfuscates = obfuscates.Checked;
        }

        private void BuildSettingsForm_Closing(object sender, EventArgs e)
        {
            Project.Save();
        }
    }
}
