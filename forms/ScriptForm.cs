using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lynx2DEngine.forms
{
    public partial class ScriptForm : Form
    {
        private EngineObject obj;

        public int id;
        public int engineId;

        public ScriptForm()
        {
            InitializeComponent();

            scriptCode.TextChanged += new EventHandler(updateNumberLabel);
            scriptCode.SizeChanged += new EventHandler(updateNumberLabel);

            scriptCode.VScroll += new EventHandler(scriptCode_VScroll);
            scriptCode.KeyDown += new KeyEventHandler(checkKeys);

            Resize += new EventHandler(updateNumberLabel);
        }

        private void checkKeys(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                updateNumberLabel(sender, e);
            
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {

            }
        }

        public void Initialize(int engineId)
        {
            this.engineId = engineId;
            UpdateTitle();

            id = obj.id;

            scriptCode.Text = obj.code;
        }

        private void UpdateTitle()
        {
            obj = Engine.GetEngineObjects()[engineId];

            Text = "Script (" + obj.Variable() + ")";
        }

        private void ScriptForm_Load(object sender, EventArgs e)
        {
            FormClosing += ScriptForm_Close;
        }

        private void ScriptForm_Close(object sender, EventArgs e)
        {
            Engine.SetEngineObjectScript(engineId, scriptCode.Text);
        }

        private void updateNumberLabel(object sender, EventArgs e)
        {
            //we get index of first visible char and 
            //number of first visible line
            Point pos = new Point(0, 0);
            int firstIndex = scriptCode.GetCharIndexFromPosition(pos);
            int firstLine = scriptCode.GetLineFromCharIndex(firstIndex);

            //now we get index of last visible char 
            //and number of last visible line
            pos.X = ClientRectangle.Width;
            pos.Y = ClientRectangle.Height;
            int lastIndex = scriptCode.GetCharIndexFromPosition(pos);
            int lastLine = scriptCode.GetLineFromCharIndex(lastIndex);

            //this is point position of last visible char, we'll 
            //use its Y value for calculating numberLabel size
            pos = scriptCode.GetPositionFromCharIndex(lastIndex);

            //finally, renumber label
            numberLabel.Text = "";
            int largestSize = 0;
            for (int i = firstLine; i <= lastLine; i++)
            {
                int id = i + 1;

                if (id.ToString().Length > largestSize) largestSize = id.ToString().Length;
                numberLabel.Text += id + ".\n";
            }

            numberLabel.Size = new Size(28 + (largestSize - 1) * 10, Size.Height);
        }

        private void scriptCode_VScroll(object sender, EventArgs e)
        {
            //move location of numberLabel for amount 
            //of pixels caused by scrollbar
            int d = scriptCode.GetPositionFromCharIndex(0).Y %
                                      (scriptCode.Font.Height + 1);
            numberLabel.Location = new Point(0, d);

            updateNumberLabel(sender, e);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Engine.RemoveEngineObject(engineId, true);
            Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Engine.RenameEngineObject(engineId, Input.Prompt("Enter the new name", "Rename " + obj.Variable()));

            UpdateTitle();
        }
        
    }
}
