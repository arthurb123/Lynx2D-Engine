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
