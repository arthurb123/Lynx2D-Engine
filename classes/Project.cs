﻿using System;
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
        private static string cur = string.Empty;
        public static Main form;
        private static string gameCode;

        public static void Load(bool needsName)
        {
            string old_cur = cur;

            if (!Manager.CheckDirectory("projects", false))
            {
                MessageBox.Show("No projects folder could be found. Please create a project first.", "Lynx2D Engine - Error");
                return;
            }

            if (needsName) cur = Input.Prompt("Enter the name of the existing project", "Lynx2D - Load Project");
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
                gameCode = "lx.Initialize('" + cur + "'); lx.Smoothing(false); lx.Start(60);";

                form.createBrowser();

                Engine.LoadEngineState();
                form.UpdateHierarchy();

                form.Text = "Lynx2D Engine - " + cur;
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
            CheckProjectsExistence();

            cur = Input.Prompt("Enter the name of the new project", "Lynx2D - New Project");
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

        public static void Save()
        {
            if (cur == string.Empty || cur == "HAS_BEEN_CLOSED") return;

            Engine.BuildEngineCode();

            Engine.SaveEngineState();

            using (FileStream fs = new FileStream("projects/" + cur + "/data/game.js", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.Write(gameCode);

                    w.Dispose();
                    fs.Dispose();
                }
            }

            gameCode = "lx.Initialize('" + cur + "'); lx.Smoothing(false); lx.Start(60);";
            form.SetStatus("'" + cur + "' has been saved.", Main.StatusType.Message);
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
                        w.Write("<html><head><meta charset='utf-8'/></head><body><script type='text/javascript' src='data/lynx2d.js'></script><script type='text/javascript' src='data/game.js'></script></body></html>");
                        w.Dispose();
                        fs.Dispose();
                    }
                }

                using (FileStream fs = new FileStream("projects/" + cur + "/data/lynx2d.js", FileMode.Create))
                {
                    using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                    {
                        WebClient client = new WebClient();

                        w.Write(client.DownloadString(new Uri("http://www.lythumn.com/lynx2d/res/lynx2d.js")));

                        client.Dispose();
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

                File.Copy(@"resources/sprite.png", "projects/" + cur + "/res/lynx2d/sprite.png");

                Load(false);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
                form.SetStatus("Exception occurred while creating project canon.", Main.StatusType.Warning);
            }
        }
    }
}
