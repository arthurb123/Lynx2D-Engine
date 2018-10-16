using System;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    public partial class PromptForm : Form
    {
        public PromptForm()
        {
            InitializeComponent();

            textBox1.TextChanged += textBox1_TextChanged;
        }

        public void SetTitle(string text)
        {
            label1.Text = text;
        }

        public void SetCaption(string text)
        {
            Text = text;
        }
        
        public string Value()
        {
            return textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PromptForm_Load(object sender, EventArgs e)
        {
            AcceptButton = button1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0) button1.Enabled = true;
            else button1.Enabled = false;
        }
    }
}
