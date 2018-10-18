using System;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    public partial class ConsoleForm : Form
    {
        public ConsoleForm()
        {
            InitializeComponent();
        }

        private void ConsoleForm_Load(object sender, EventArgs e)
        {
            outputText.Text = "";
        }

        private string TimeFormat()
        {
            return "[" + DateTime.Now.ToString("HH:mm:ss") + "] ";
        }

        public void AddOutput(string msg)
        {
            try
            {
                outputText.Text += TimeFormat() + msg + "\n";
            }
            catch (Exception e)
            {
                //Do something with this exception
            }
        }
    }
}
