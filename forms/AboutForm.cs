using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Lynx2DEngine.forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(linkLabel1.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
            }
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            versionText.Text = "version " + Feed.Version();

            CheckTheme();
        }

        private void CheckTheme()
        {
            if (Engine.ePreferences.theme == classes.Theme.Dark)
            {
                BackColor = classes.DarkTheme.background;
                ForeColor = classes.DarkTheme.font;
            }
        }
    }
}
