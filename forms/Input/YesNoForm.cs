using System;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    public partial class YesNoForm : Form
    {
        public YesNoForm()
        {
            InitializeComponent();
        }

        public void SetTitle(string text)
        {
            label1.Text = text;
        }

        public void SetCaption(string text)
        {
            Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void YesNoForm_Load(object sender, EventArgs e)
        {
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

                button2.BackColor = classes.DarkTheme.background;
                button2.FlatStyle = FlatStyle.Flat;
                button2.FlatAppearance.BorderColor = classes.DarkTheme.border;
            }
        }
    }
}
