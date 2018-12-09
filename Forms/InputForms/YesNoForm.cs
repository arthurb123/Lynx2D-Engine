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

        public void SetText(string text)
        {
            label1.Text = text;

            System.Drawing.Size s = new System.Drawing.Size(Width, 90 + label1.Size.Height);

            MaximumSize = s;
            MinimumSize = s;
            Size = s;
        }

        public void SetCaption(string text)
        {
            Text = text;
        }

        public void DisableYes()
        {
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
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
