using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    class Project
    {
        public static string cur = string.Empty;
        public static Main form;
        private static string gameCode;

        public static void Load(bool needsName)
        {
            form.killChildren();
            string old_cur = cur;

            if (!Manager.CheckDirectory("projects", false) || Directory.GetDirectories("projects/").Length == 0)
            {
                MessageBox.Show("No projects could be found. Please create a project first.", "Lynx2D Engine - Error");
                return;
            }

            if (needsName && cur != string.Empty)
                RequestSave();

            string[] projects = Directory.GetDirectories("projects/");
            for (int i = 0; i < projects.Length; i++)
                projects[i] = projects[i].Substring(9, projects[i].Length-9);

            if (needsName) cur = Input.Selection("Choose an existing project", "Load Project", projects);

            if (cur == "HAS_BEEN_CLOSED")
            {
                cur = old_cur;

                return;
            }

            if (!Manager.CheckDirectory("projects/" + cur, false))
            {
                cur = old_cur;

                Load(needsName);
                return;
            }

            try
            {
                gameCode = "lx.Initialize('" + cur + "'); lx.Smoothing(true); lx.Start(60);";

                if (!Engine.LoadEngineState())
                {
                    cur = string.Empty;
                    return;
                }

                form.createBrowser();

                form.LoadEngineSettings();

                form.SetTitle();
                form.SetStatus("'" + cur + "' has been loaded.", Main.StatusType.Message);

                form.SetGameAvailability(true);
                form.refreshBrowser();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
                form.SetStatus("Exception occured while loading '" + cur + "'", Main.StatusType.Warning);
            }
        }

        public static void Create()
        {
            if (!Feed.CheckOnline())
            {
                MessageBox.Show("Creating a project requires a valid internet connection. The Lynx2D framework could not be downloaded.", "Lynx2D Engine - Exception");
                return;
            }

            CheckProjectsExistence();

            cur = Input.Prompt("Enter the name of the new project", "Create New Project");
            if (cur == "HAS_BEEN_CLOSED") return;

            if (Manager.CheckDirectory("projects/" + cur, false))
            {
                Create();
                return;
            }

            MakeCanon();
        }

        private static void CheckProjectsExistence()
        {
            if (!Manager.CheckDirectory("projects", false))
            {
                if (Input.YesNo("The projects folder does not exist yet, do you want to create this folder?", "Lynx2D Engine - Question"))
                    Manager.CheckDirectory("projects", true);
            }
        }

        public static void RequestSave()
        {
            if (cur != string.Empty &&
                cur != "HAS_BEEN_CLOSED" &&
                Input.YesNo("Do you want to save the current project?", "Lynx2D Engine - Question"))
            Save();
        }

        public static void Save()
        {
            if (cur == string.Empty || cur == "HAS_BEEN_CLOSED") return;

            Tilemapper.SaveMapsToCurrentScene();

            Engine.SaveEngineState();

            form.SetStatus("'" + cur + "' has been saved.", Main.StatusType.Message);
        }

        public static async void Export()
        {
            if (cur == string.Empty || cur == "HAS_BEEN_CLOSED") return;

            bool obfuscated = false;
            form.SetStatus("Started exporting project.", Main.StatusType.Message);

            Save();

            gameCode = "lx.Initialize('" + cur + "'); lx.Smoothing(" + Engine.bSettings.imageSmoothing.ToString().ToLower() + "); lx.Start(" + Engine.bSettings.initialFramerate + ");";
            Engine.BuildEngineCode(true);

            using (FileStream fs = new FileStream("projects/" + cur + "/data/game.js", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    if (Engine.bSettings.obfuscates)
                    {
                        if (!Feed.CheckOnline())
                            form.SetStatus("Game obfuscation requires a internet connection.", Main.StatusType.Warning);
                        else
                        {
                            form.SetStatus("Obfuscating game code.", Main.StatusType.Message);

                            try
                            {
                                string r = await Obfuscater.Encode(gameCode);
                                gameCode = r;

                                obfuscated = true;
                            }
                            catch (Exception e)
                            {
                                form.SetStatus("Could not obfuscate game code.", Main.StatusType.Warning);
                            }
                        }
                    }

                    w.Write(gameCode);

                    w.Dispose();
                    fs.Dispose();
                }
            }

            gameCode = "lx.Initialize('" + cur + "'); lx.Smoothing(true); lx.Start(60);";

            if (obfuscated || !Engine.bSettings.obfuscates)
            {
                form.SetStatus("'" + cur + "' has been exported.", Main.StatusType.Message);

                form.showProjectToolStripMenuItem_Click(form, new EventArgs());
            }
        }

        public static void Build()
        {
            if (cur == string.Empty || cur == "HAS_BEEN_CLOSED") return;

            Engine.ExecuteScript(gameCode + Engine.BuildEngineCode(false));

            form.SetStatus("'" + cur + "' has been build.", Main.StatusType.Message);
        }

        public static string WorkDirectory()
        {
            return String.Format("file:///{0}/projects/{1}/", Directory.GetCurrentDirectory(), Name());
        }

        public static string Name()
        {
            return cur;
        }

        public static void AddGameCode(string code)
        {
            gameCode += code;
        }

        private static void MakeCanon()
        {
            try
            {
                Manager.CheckDirectory("projects/" + cur, true);
                Manager.CheckDirectory("projects/" + cur + "/data", true);
                Manager.CheckDirectory("projects/" + cur + "/res", true);
                Manager.CheckDirectory("projects/" + cur + "/res/lynx2d", true);

                using (FileStream fs = new FileStream("projects/" + cur + "/index.html", FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        w.Write("<html>\n<head>\n  <link id='icon' type='image / ico' rel='shortcut icon'/>\n  <meta charset='utf-8'/>\n</head>\n<body bgcolor='black'>\n  <script type='text/javascript' src='data/lynx2d.js'></script>\n  <script type='text/javascript' src='data/game.js'></script>\n</body>\n</html>");
                        w.Dispose();
                        fs.Dispose();
                    }
                }
                
                using (FileStream fs = new FileStream("projects/" + cur + "/engine.html", FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        w.Write("<html>\n<head>\n  <meta charset='utf-8'/>\n</head>\n<body bgcolor='#282828'>\n  <script type='text/javascript' src='data/lynx2d.js'></script>\n  </body>\n</html>");
                        w.Dispose();
                        fs.Dispose();
                    }
                }

                using (FileStream fs = new FileStream("projects/" + cur + "/data/game.js", FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        w.Write("lx.Initialize('" + cur + "'); lx.Smoothing(false); lx.Start(60);");
                        w.Dispose();
                        fs.Dispose();
                    }
                }

                DownloadFramework(false);
                InstallResources(false);

                Engine.ClearEngine();
                Engine.SaveEngineState();

                Load(false);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
                form.SetStatus("Exception occurred while creating project canon.", Main.StatusType.Warning);
            }
        }

        public static void DownloadFramework(bool setsStatus)
        {
            if (!Feed.CheckOnline())
            {
                MessageBox.Show("Reloading the framework requires a valid internet connection. The Lynx2D framework could not be downloaded.", "Lynx2D Engine - Exception");
                return;
            }

            try
            {
                using (FileStream fs = new FileStream("projects/" + cur + "/data/lynx2d.js", FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        WebClient client = new WebClient();

                        w.Write(client.DownloadString(new Uri("http://www.lynx2d.com/res/lynx2d.js")));

                        client.Dispose();
                        w.Dispose();
                        fs.Dispose();

                        if (setsStatus) form.SetStatus("The Lynx2D framework has been reloaded.", Main.StatusType.Message);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
                form.SetStatus("Exception occurred while reloading framework.", Main.StatusType.Warning);
            }
        }

        public static void InstallResources(bool setsStatus)
        {
            try
            {
                int installed = 0;

                if (!File.Exists("projects/" + cur + "/res/lynx2d/sprite.png"))
                {
                    File.Copy(@"resources/sprite.png", "projects/" + cur + "/res/lynx2d/sprite.png");
                    installed++;
                }
                if (!File.Exists("projects/" + cur + "/res/lynx2d/pointer.png"))
                {
                    File.Copy(@"resources/pointer.png", "projects/" + cur + "/res/lynx2d/pointer.png");
                    installed++;
                }
                if (!File.Exists("projects/" + cur + "/res/lynx2d/particle.png"))
                {
                    File.Copy(@"resources/particle.png", "projects/" + cur + "/res/lynx2d/particle.png");
                    installed++;
                }
                if (!File.Exists("projects/" + cur + "/res/lynx2d/location.png"))
                {
                    File.Copy(@"resources/location.png", "projects/" + cur + "/res/lynx2d/location.png");
                    installed++;
                }

                if (setsStatus) form.SetStatus(installed + " Lynx2D resource(s) reloaded.", Main.StatusType.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
                form.SetStatus("Exception occurred while reloading resources", Main.StatusType.Warning);
            }
        }
    }
}
