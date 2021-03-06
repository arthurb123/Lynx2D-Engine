﻿using Lynx2DEngine.Classes;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    public static class Project
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

            if (cur != string.Empty)
            {
                RemoveEngineHTML();

                if (needsName) RequestSave();
            }

            string[] projects = Directory.GetDirectories("projects/");
            for (int i = 0; i < projects.Length; i++)
                projects[i] = projects[i].Substring(9, projects[i].Length-9);

            if (needsName) 
                cur = Input.Selection("Choose an existing project", "Load Project", projects);

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
                InstallEngineHTML();

                if (!Engine.LoadEngineState())
                {
                    cur = string.Empty;

                    RemoveEngineHTML();

                    return;
                }

                gameCode = "lx.Initialize('" + cur + "'); lx.Smoothing(true); lx.Start(60);";

                Manager.ClearAppData();
                Backup.Disable();
                Backup.Enable();

                form.CreateBrowser();

                form.LoadEngineSettings();

                form.SetTitle();
                form.SetStatus("'" + cur + "' has been loaded.", Main.StatusType.Message);

                form.SetGameAvailability(true);
                form.RefreshBrowser();

                Feed.CheckFrameworkDate();
            }
            catch (Exception exc)
            {
                Feed.GiveException("Project Load", exc);
            }
        }

        public static void Create()
        {
            if (!Feed.CheckOnline())
            {
                MessageBox.Show("Creating a project requires a valid internet connection. The Lynx2D framework could not be downloaded.", "Lynx2D Engine - Message");
                return;
            }

            CheckProjectsExistence();

            cur = Input.Prompt("Enter the name of the new project", "Create New Project");
            if (cur == "HAS_BEEN_CLOSED") return;

            if (Manager.CheckDirectory("projects/" + cur, false))
            {
                MessageBox.Show("Project '" + cur + "' already exists.", "Lynx2D Engine - Message");

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
            ExportHTML();

            gameCode = (Engine.bSettings.mergeFramework ? File.ReadAllText("projects/" + cur + "/data/lynx2d.js") : "") + "lx.Initialize('" + cur + "'); lx.Smoothing(" + Engine.bSettings.imageSmoothing.ToString().ToLower() + ");";
            Engine.BuildEngineCode(true);

            gameCode += "lx.Start(" + Engine.bSettings.initialFramerate + ")";

            using (FileStream fs = new FileStream("projects/" + cur + "/data/game.js", FileMode.Create))
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
                            string r = await Obfuscator.Encode(gameCode);
                            gameCode = r;

                            obfuscated = true;
                        }
                        catch
                        {
                            form.SetStatus("Could not obfuscate game code.", Main.StatusType.Warning);
                        }
                    }
                }

                w.Write(gameCode);

                w.Close();
                fs.Close();
            }

            gameCode = "lx.Initialize('" + cur + "'); lx.Smoothing(true); lx.Start(60);";

            if (obfuscated || !Engine.bSettings.obfuscates)
            {
                form.SetStatus("'" + cur + "' has been exported.", Main.StatusType.Message);

                Manager.OpenDirectory(@WorkDirectory());
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
            return string.Format("file:///{0}/projects/{1}/", Directory.GetCurrentDirectory(), Name());
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

                ExportHTML();

                using (FileStream fs = new FileStream("projects/" + cur + "/data/game.js", FileMode.Create))
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.Write("lx.Initialize('" + cur + "'); lx.Smoothing(false); lx.Start(60);");

                    w.Close();
                    fs.Close();
                }

                DownloadFramework(false);
                InstallResources(false);

                Engine.ClearEngine();
                Engine.SaveEngineState();
            }
            catch (Exception exc)
            {
                Feed.GiveException("Canon Creation", exc);
            }
            finally
            {
                Load(false);
            }
        }

        private static void ExportHTML()
        {
            string html = "<!DOCTYPE HTML>\n" +
                          "<html>\n" +
                          "<head>\n  " +
                          "  <link id='icon' type='image / ico' rel='shortcut icon'/>\n" +
                          "  <title>" + cur + "</title>\n" +
                          "  <meta charset='utf-8'/>\n" +
                          "</head>\n" +
                          "<body bgcolor='black'>\n";

            if (Engine.bSettings.mergeFramework)
                html += "  <script type='text/javascript' src='data/game.js'></script>\n";
            else
                html += "  <script type='text/javascript' src='data/lynx2d.js'></script>\n" +
                        "  <script type='text/javascript' src='data/game.js'></script>\n";

            html += "</body>\n" +
                    "</html>";

            File.WriteAllText("projects/" + cur + "/index.html", html);
        }

        public static void DownloadFramework(bool setsStatus)
        {
            if (!Feed.CheckOnline())
            {
                MessageBox.Show("Reloading the framework requires a valid internet connection. The Lynx2D framework could not be downloaded.", "Lynx2D Engine - Message");
                return;
            }

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFileCompleted += (object s, AsyncCompletedEventArgs e) =>
                    {
                        if (setsStatus)
                            form.SetStatus("Downloaded Lynx2D framework.", Main.StatusType.Message);

                        form.RefreshBrowser();

                        client.Dispose();
                    };

                    client.DownloadFile(new Uri("http://www.lynx2d.com/res/lynx2d-min.js"), "projects/" + cur + "/data/lynx2d.js");
                }
            }
            catch (Exception exc)
            {
                Feed.GiveException("Framework Reload", exc);
            }
        }

        public static void InstallResources(bool setsStatus)
        {
            try
            {
                int installed = 0;

                if (!File.Exists("projects/" + cur + "/res/lynx2d/sprite.png"))
                {
                    Properties.Resources.sprite.Save("projects/" + cur + "/res/lynx2d/sprite.png");
                    installed++;
                }
                if (!File.Exists("projects/" + cur + "/res/lynx2d/pointer.png"))
                {
                    Properties.Resources.pointer.Save("projects/" + cur + "/res/lynx2d/pointer.png");
                    installed++;
                }
                if (!File.Exists("projects/" + cur + "/res/lynx2d/particle.png"))
                {
                    Properties.Resources.particle.Save("projects/" + cur + "/res/lynx2d/particle.png");
                    installed++;
                }
                if (!File.Exists("projects/" + cur + "/res/lynx2d/location.png"))
                {
                    Properties.Resources.location.Save("projects/" + cur + "/res/lynx2d/location.png");
                    installed++;
                }

                if (setsStatus)
                {
                    if (installed != 0)
                        form.SetStatus(installed + " Lynx2D resource(s) reloaded.", Main.StatusType.Message);
                    else
                        form.SetStatus("No Lynx2D resources are missing.", Main.StatusType.Message);
                }
            }
            catch (Exception exc)
            {
                Feed.GiveException("Resources Reload", exc);
            }
        }

        public static void InstallEngineHTML()
        {
            if (File.Exists("projects/" + cur + "/engine.html"))
                return;

            using (FileStream fs = new FileStream("projects/" + cur + "/engine.html", FileMode.Create))
            using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
            {
                w.Write("<html>\n<head>\n  <meta charset='utf-8'/>\n</head>\n<body bgcolor='#282828'>\n  <script type='text/javascript' src='data/lynx2d.js'></script>\n  </body>\n</html>");

                w.Close();
                fs.Close();
            }
        }
        
        public static void RemoveEngineHTML()
        {
            if (cur == string.Empty ||
                cur == "HAS_BEEN_CLOSED" || 
                !File.Exists("projects/" + cur + "/engine.html"))
                return;

            File.Delete("projects/" + cur + "/engine.html");
        }
    }
}
