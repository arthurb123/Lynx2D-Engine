using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lynx2DEngine.forms
{
    public partial class ScriptForm : Form
    {
        private EngineObject obj;
        private bool saved = true;
        private bool startup = true;

        private int prevLine = -1;
        private int prevStart = -1;
        private int prevLength = -1;
        private Color prevForeColor;

        public int id;
        public int engineId;

        public ScriptForm()
        {
            InitializeComponent();

            scriptCode.TextChanged += new EventHandler(updateNumberLabel);
            scriptCode.SizeChanged += new EventHandler(updateNumberLabel);

            scriptCode.VScroll += new EventHandler(scriptCode_VScroll);
            scriptCode.KeyDown += new KeyEventHandler(checkKeys);

            scriptCode.Click += new EventHandler((object sender, EventArgs e) =>
            {
                if (searchPanel.Visible)
                    ClearPrevHighlight(true);

                searchPanel.Visible = false;
            });

            search.KeyDown += new KeyEventHandler(search_KeyDown);

            Resize += new EventHandler(updateNumberLabel);
        }

        private void checkKeys(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                updateNumberLabel(sender, e);

            if (ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.S && !saved)
                saveToolStripMenuItem_Click(sender, e);

            if (ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.Z)
                scriptCode.Undo();

            if (ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.F)
            {
                searchPanel.Visible = true;
                search.Focus();
            }
        }

        public void Initialize(int engineId)
        {
            this.engineId = engineId;
            UpdateTitle();

            id = obj.id;

            CheckExternalEditing();

            scriptCode.Text = obj.code;
        }

        private void UpdateTitle()
        {
            obj = Engine.GetEngineObjects()[engineId];

            Text = obj.Variable();
        }

        private void ScriptForm_Load(object sender, EventArgs e)
        {
            FormClosing += ScriptForm_Close;

            CheckTheme();
        }

        private void CheckTheme()
        {
            if (Engine.ePreferences.theme == classes.Theme.Dark)
            {
                BackColor = classes.DarkTheme.mainBackground;
                ForeColor = classes.DarkTheme.font;

                numberLabel.BackColor = classes.DarkTheme.scriptNumbersBackground;
                scriptCode.BackColor = classes.DarkTheme.scriptBackground;

                menuStrip1.Renderer = new ToolStripProfessionalRenderer(new classes.DarkThemeColorTable());
                menuStrip1.BackColor = classes.DarkTheme.menuBackground;
                menuStrip1.ForeColor = classes.DarkTheme.font;

                searchPanel.BackColor = classes.DarkTheme.mainBackground;
            }
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
                string id = (i+1).ToString();

                if (id.Length > largestSize) largestSize = id.Length;
                numberLabel.Text += id + ".\n";
            }

            numberLabel.Size = new Size(28 + (largestSize - 1) * 10, Size.Height-menuStrip1.Size.Height);
        }

        private void scriptCode_VScroll(object sender, EventArgs e)
        {
            int d = scriptCode.GetPositionFromCharIndex(0).Y %
                                      (scriptCode.Font.Height + 1);
            if (d < 0) d = 0;

            numberLabel.Location = new Point(0, d+menuStrip1.Size.Height);

            updateNumberLabel(sender, e);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Engine.RemoveEngineObject(engineId, true, true);
            Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Engine.RenameEngineObject(engineId, Input.Prompt("Enter the new name", "Rename " + obj.Variable()), true);

            UpdateTitle();
        }

        private void highlightScript()
        {
            MatchCollection keywordMatches = Syntax.Match(scriptCode.Text, SyntaxMatch.Keywords);
            MatchCollection methodMatches = Syntax.Match(scriptCode.Text, SyntaxMatch.Methods);
            MatchCollection lxTypeMatches = Syntax.Match(scriptCode.Text, SyntaxMatch.LxTypes);
            MatchCollection typeMatches = Syntax.Match(scriptCode.Text, SyntaxMatch.Types);
            MatchCollection commentMatches = Syntax.Match(scriptCode.Text, SyntaxMatch.Comments);
            MatchCollection stringMatches = Syntax.Match(scriptCode.Text, SyntaxMatch.Strings);
            MatchCollection numberMatches = Syntax.Match(scriptCode.Text, SyntaxMatch.Numbers);

            int originalIndex = scriptCode.SelectionStart;
            int originalLength = scriptCode.SelectionLength;
            Color originalColor = Color.Black;

            if (Engine.ePreferences.theme == classes.Theme.Dark)
                originalColor = classes.DarkTheme.font;

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
                if (Engine.ePreferences.theme == classes.Theme.Dark)
                    scriptCode.SelectionColor = Color.RoyalBlue;
            }

            foreach (Match m in methodMatches)
            {
                scriptCode.SelectionStart = m.Index+1;
                scriptCode.SelectionLength = m.Length-1;
                scriptCode.SelectionColor = Color.DarkViolet;
            }

            foreach (Match m in lxTypeMatches)
            {
                scriptCode.SelectionStart = m.Index+3;
                scriptCode.SelectionLength = m.Length-3;
                scriptCode.SelectionColor = Color.DarkCyan;
            }

            foreach (Match m in typeMatches)
            {
                scriptCode.SelectionStart = m.Index;
                scriptCode.SelectionLength = m.Length;
                scriptCode.SelectionColor = Color.DarkSlateBlue;
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

            foreach (Match m in numberMatches)
            {
                scriptCode.SelectionStart = m.Index;
                scriptCode.SelectionLength = m.Length;
                scriptCode.SelectionColor = Color.DarkOliveGreen;
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

            Engine.form.RefreshBrowser();

            setSaved(true);
        }

        private void setSaved(bool saved)
        {
            this.saved = saved;
            saveToolStripMenuItem.Enabled = !saved;
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            SearchText(false);
        }

        private void search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchText(true);
        }

        private void SearchText(bool skipPrev)
        {
            int originalIndex = scriptCode.SelectionStart;
            int originalLength = scriptCode.SelectionLength;
            Color originalColor = Color.Black;

            if (Engine.ePreferences.theme == classes.Theme.Dark)
                originalColor = classes.DarkTheme.font;
           
            for (int i = 0; i < scriptCode.Lines.Length; i++)
            {
                if (skipPrev && i <= prevLine) continue;

                if (scriptCode.Lines[i].Contains(search.Text))
                {
                    ClearPrevHighlight(false);

                    numberLabel.Focus();
                    
                    prevStart = scriptCode.SelectionStart = scriptCode.Find(scriptCode.Lines[i]) + scriptCode.Lines[i].IndexOf(search.Text);
                    prevLength = scriptCode.SelectionLength = search.Text.Length;
                    prevForeColor = scriptCode.SelectionColor;

                    scriptCode.SelectionColor = Color.Black;
                    scriptCode.SelectionBackColor = Color.LightYellow;

                    scriptCode.SelectionStart = originalIndex;
                    scriptCode.SelectionLength = originalLength;
                    scriptCode.SelectionColor = originalColor;

                    if (prevLine != i)
                        scriptCode.ScrollToCaret();

                    prevLine = i;

                    search.Focus();

                    break;
                }
            }
        }

        private void ClearPrevHighlight(bool whole)
        {
            int originalIndex = scriptCode.SelectionStart;
            int originalLength = scriptCode.SelectionLength;
            Color originalColor = Color.Black;

            if (Engine.ePreferences.theme == classes.Theme.Dark)
                originalColor = classes.DarkTheme.font;

            if (!whole && prevStart != -1 && prevLength != -1)
            {
                scriptCode.SelectionStart = prevStart;
                scriptCode.SelectionLength = prevLength;
                scriptCode.SelectionBackColor = scriptCode.BackColor;
                scriptCode.SelectionColor = prevForeColor;
            }
            else if (prevStart != -1)
            {
                scriptCode.SelectionStart = prevStart;
                scriptCode.SelectionLength = search.Text.Length;
                scriptCode.SelectionBackColor = scriptCode.BackColor;
                scriptCode.SelectionColor = prevForeColor;
            }

            scriptCode.SelectionStart = originalIndex;
            scriptCode.SelectionLength = originalLength;
            scriptCode.SelectionColor = originalColor;

            if (!whole) search.Focus();
            else scriptCode.Focus();
        }

        private void CheckExternalEditing()
        {
            try
            {
                string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/lynx2d/",
                           appdataFile = appdataPath + Path.DirectorySeparatorChar + obj.Variable() + ".js";

                if (File.Exists(appdataFile))
                    File.Delete(appdataFile);
            }
            catch
            {
                //...
            }
        }

        private void openExternallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/lynx2d/",
                       appdataFile = appdataPath + "/" + obj.Variable() + ".js";

                Directory.CreateDirectory(appdataPath);

                if (!File.Exists(appdataFile))
                    using (Stream stream = File.Open(appdataFile, FileMode.Create))
                    {
                        StreamWriter sw = new StreamWriter(stream);
                        sw.Write(obj.code);

                        sw.Close();

                        System.Diagnostics.Process.Start(appdataFile);

                        Engine.CreateSpecificFileWatcher(appdataPath, obj.Variable());

                        Close();
                    }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
            }
        }
    }
}
