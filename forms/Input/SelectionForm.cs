using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    public partial class SelectionForm : Form
    {
        public SelectionForm()
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

        public void SetSelectionContent(string[] content)
        {
            selection.Items.AddRange(content);

            selection.Text = content[0];
        }

        public string Value()
        {
            return selection.Text;
        }

        private void SelectionForm_Load(object sender, EventArgs e)
        {
            AcceptButton = button1;

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
            }
        }

        private void selection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selection.Text != string.Empty)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }
    }
}
