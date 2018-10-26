using System;
using System.IO;
using System.Windows.Forms;

namespace Lynx2DEngine.forms
{
    public partial class ChangelogForm : Form
    {
        public ChangelogForm()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            try
            {
                version.Text = "Version [" + Feed.Version() + "]";

                if (File.Exists("changelog.txt"))
                    content.Text = File.ReadAllText("changelog.txt");
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not load changelog: " + e.Message, "Lynx2D Engine - Exception");
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
