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

        }
    }
}
