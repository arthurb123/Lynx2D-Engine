using Lynx2DEngine.forms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    class Feed
    {
        public static Main form;

        private static readonly string version = "0.5.2";
        private static readonly string stage = "beta";

        private static bool extract = true;

        public static bool CheckOnline()
        {
            try
            {
                WebRequest webRequest = WebRequest.Create(@"http://www.google.com/");

                WebResponse response = webRequest.GetResponse();

                response.Close();
                response.Dispose();

                return true;
            }
            catch
            {
                form.SetStatus("Not connected to the internet.", Main.StatusType.Warning);
            }

            return false;
        }

        public static void CheckVersion(bool setsStatus)
        {
            string onlineVersion = string.Empty;

            try
            {
                WebRequest webRequest = WebRequest.Create(@"http://www.lynx2d.com/engine/version.txt");

                using (WebResponse response = webRequest.GetResponse())
                    using (Stream content = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(content))
                        {
                            onlineVersion = reader.ReadToEnd();
                            reader.Dispose();
                            content.Dispose();
                        }
            }
            catch
            {
                if (setsStatus) form.SetStatus("Could not get online version.", Main.StatusType.Warning);
            }

            if (onlineVersion != string.Empty && !string.IsNullOrWhiteSpace(onlineVersion) && onlineVersion != Version())
            {
                if (Input.YesNo("A new version (" + onlineVersion + ") is available. Do you want to download this update?", "Lynx2D Engine - Update"))
                {
                    UpdateVersion(onlineVersion);
                }
                else if (setsStatus) form.SetStatus("You are running version " + Version() + ".", Main.StatusType.Message);
            }
            else if (setsStatus) form.SetStatus("Running latest version (" + Version() + ").", Main.StatusType.Message);
        }

        public static void CheckFrameworkDate()
        {
            if (!CheckOnline())
                return;

            HttpWebRequest frameworkFile = (HttpWebRequest)WebRequest.Create(@"http://www.lynx2d.com/res/lynx2d.js");
            HttpWebResponse frameworkFileResponse = (HttpWebResponse)frameworkFile.GetResponse();

            DateTime localFileModifiedTime = File.GetLastWriteTime("projects/" + Project.Name() + "/data/lynx2d.js");
            DateTime onlineFileModifiedTime = frameworkFileResponse.LastModified;

            if (localFileModifiedTime < onlineFileModifiedTime)
            {
                if (Input.YesNo("This project is using an outdated version of the Lynx2D framework. Do you want to install the newer version?", "Lynx2D Engine - Question"))
                    Project.DownloadFramework(true);
            }
        }

        public static void EvaluateStartup()
        {
            if (!Directory.Exists("blob_storage"))
                ShowChangelog(true);

            if (Directory.Exists("downloads"))
            {
                string[] files = Directory.GetFiles("downloads");

                if (files.Length == 0 || files.Length == 1 && files[0].Contains("Lynx2DEngineUpdater"))
                    Directory.Delete("downloads", true);
            }
        }

        public static void ShowChangelog(bool welcomes)
        {
            ChangelogForm changelog = new ChangelogForm();

            changelog.Initialize(welcomes);

            changelog.ShowDialog();
        }

        public static void UpdateVersion(string version)
        {
            Manager.CheckDirectory("downloads", true);

            if (File.Exists("downloads/" + version + ".zip"))
            {
                EvaluateUpdatedVersion();
                return;
            }

            try
            {
                WebClient client = new WebClient();

                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(UpdateVersionCompleted);
                client.DownloadFileAsync(new Uri("http://www.lynx2d.com/engine/res/" + version + ".zip"), "downloads/" + version + ".zip");
            }
            catch
            {
                form.SetStatus("Could not download update.", Main.StatusType.Message);
            }
        }

        private static void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            form.SetStatus("Downloading.. (" + e.ProgressPercentage + "%)", Main.StatusType.Message);
        }

        private static void UpdateVersionCompleted(object sender, AsyncCompletedEventArgs e)
        {
            form.SetStatus("Download complete!", Main.StatusType.Message);

            EvaluateUpdatedVersion();
        }

        private static void EvaluateUpdatedVersion()
        {
            try
            {
                extract = Input.YesNo("Do you want the engine to install the downloaded update?", "Lynx2D Engine - Question");

                if (!extract && Input.YesNo("Do you want to view the location of the downloaded update?", "Lynx2D Engine - Question")) 
                    Process.Start(@"downloads");
                else if (extract)
                    GetUpdater();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                form.SetStatus("Exception occurred trying to open downloads.", Main.StatusType.Warning);
            }
        }

        private static void GetUpdaterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            form.SetStatus("Opening Lynx2D updater..", Main.StatusType.Message);

            LaunchUpdater();
        }

        private static void LaunchUpdater()
        {
            try
            {
                ProcessStartInfo cmd = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "CMD.exe",
                    Arguments = "/C cd downloads&start Lynx2DEngineUpdater.exe"
                };
                Process.Start(cmd);

                Application.Exit();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                form.SetStatus("Exception occurred while starting updater.", Main.StatusType.Warning);
            }
        }

        private static void GetUpdater()
        {
            Manager.CheckDirectory("downloads", true);

            if (File.Exists(Manager.Root() + "downloads/Lynx2DEngineUpdater.exe"))
            {
                LaunchUpdater();
                return;
            }

            try
            {
                WebClient client = new WebClient();

                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(GetUpdaterCompleted);
                client.DownloadFileAsync(new Uri("http://www.lynx2d.com/engine/res/updater/Lynx2DEngineUpdater.exe"), "downloads/Lynx2DEngineUpdater.exe");
            }
            catch
            {
                form.SetStatus("Could not download updater.", Main.StatusType.Message);
            }
        }

        public static string Version()
        {
            return version + "-" + stage;
        }
    }
}
