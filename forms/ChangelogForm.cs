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

        public void Initialize(bool welcomes)
        {
            try
            {
                if (welcomes)
                    version.Text = "Welcome to " + Feed.Version();
                else
                    version.Text = "Version " + Feed.Version();

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

        private void ChangelogForm_Load(object sender, EventArgs e)
        {
            CheckTheme();
        }

        private void CheckTheme()
        {
            if (Engine.ePreferences.theme == classes.Theme.Dark)
            {
                BackColor = classes.DarkTheme.mainBackground;
                ForeColor = classes.DarkTheme.font;

                content.BackColor = classes.DarkTheme.background;
                content.ForeColor = classes.DarkTheme.font;

                exit.BackColor = classes.DarkTheme.background;
                exit.FlatStyle = FlatStyle.Flat;
                exit.FlatAppearance.BorderColor = classes.DarkTheme.border;
            }
        }

    }
}
