﻿using System;
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
        private bool saved = true;
        private bool startup = true;

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
            if (!saved)
                Engine.SetEngineObjectScript(engineId, scriptCode.Text);
        }

        private void updateNumberLabel(object sender, EventArgs e)
        {
            Point pos = new Point(0, 0);
            int firstIndex = scriptCode.GetCharIndexFromPosition(pos);
            int firstLine = scriptCode.GetLineFromCharIndex(firstIndex);
            
            pos.X = ClientRectangle.Width;
            pos.Y = ClientRectangle.Height;
            int lastIndex = scriptCode.GetCharIndexFromPosition(pos);
            int lastLine = scriptCode.GetLineFromCharIndex(lastIndex);
            
            pos = scriptCode.GetPositionFromCharIndex(lastIndex);
            
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

        private void highlightScript()
        {
            MatchCollection keywordMatches = Syntax.Match(scriptCode.Text, SyntaxMatch.Keywords);
            MatchCollection methodMatches = Syntax.Match(scriptCode.Text, SyntaxMatch.Methods);
            MatchCollection typeMatches = Syntax.Match(scriptCode.Text, SyntaxMatch.Types);
            MatchCollection commentMatches = Syntax.Match(scriptCode.Text, SyntaxMatch.Comments);
            MatchCollection stringMatches = Syntax.Match(scriptCode.Text, SyntaxMatch.Strings);

            int originalIndex = scriptCode.SelectionStart;
            int originalLength = scriptCode.SelectionLength;
            Color originalColor = Color.Black;

            //This is unfortunately necessary - to avoid blinking
            numberLabel.Focus();

            scriptCode.SelectionStart = 0;
            scriptCode.SelectionLength = scriptCode.Text.Length;
            scriptCode.SelectionColor = originalColor;

            foreach (Match m in keywordMatches)
            {
                scriptCode.SelectionStart = m.Index;
                scriptCode.SelectionLength = m.Length;
                scriptCode.SelectionColor = Color.Blue;
            }

            foreach (Match m in methodMatches)
            {
                scriptCode.SelectionStart = m.Index;
                scriptCode.SelectionLength = m.Length;
                scriptCode.SelectionColor = Color.DarkViolet;
            }

            foreach (Match m in typeMatches)
            {
                scriptCode.SelectionStart = m.Index;
                scriptCode.SelectionLength = m.Length;
                scriptCode.SelectionColor = Color.DarkCyan;
            }

            foreach (Match m in commentMatches)
            {
                scriptCode.SelectionStart = m.Index;
                scriptCode.SelectionLength = m.Length;
                scriptCode.SelectionColor = Color.Green;
            }

            foreach (Match m in stringMatches)
            {
                scriptCode.SelectionStart = m.Index;
                scriptCode.SelectionLength = m.Length;
                scriptCode.SelectionColor = Color.Brown;
            }

            scriptCode.SelectionStart = originalIndex;
            scriptCode.SelectionLength = originalLength;
            scriptCode.SelectionColor = originalColor;

            //and here we focus the script again
            scriptCode.Focus();
        }

        private void scriptCode_TextChanged(object sender, EventArgs e)
        {
            if (!startup) setSaved(false);
            else startup = false;

            highlightScript();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.SetEngineObjectScript(engineId, scriptCode.Text);

            setSaved(true);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void setSaved(bool saved)
        {
            this.saved = saved;
            saveToolStripMenuItem.Enabled = !saved;
        }
    }
}
