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
            outputText.Text += TimeFormat() + msg + "\n";
        }
    }
}
