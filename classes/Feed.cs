using Lynx2DEngine.forms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    public static class Feed
    {
        public static Main form;
        public static bool wantsToExtract = false;

        private static readonly string version = "1.0.3";
        private static readonly string stage = "official";
        private static bool extract = true;

        /// <summary>
        /// Check if there is an internet connection available.
        /// </summary>
        /// <returns>Boolean indicating if online.</returns>
        public static bool CheckOnline()
        {
            try
            {
                WebRequest  webRequest = WebRequest.Create(@"http://www.google.com/");
                WebResponse response   = webRequest.GetResponse();

                response.Close();

                return true;
            }
            catch
            {
                form.SetStatus("Not connected to the internet.", Main.StatusType.Warning);
            }

            return false;
        }

        /// <summary>
        /// Check this version against the latest version available online.
        /// </summary>
        /// <param name="setsStatus">
        /// Bool indicating if the status should reflect
        /// this process.
        /// </param>
        public static void CheckVersion(bool setsStatus)
        {
            string onlineVersion = string.Empty;

            try
            {
                WebRequest webRequest = WebRequest.Create(@"http://www.lynx2d.com/engine/version.txt");

                using (WebResponse response = webRequest.GetResponse())
                using (Stream content = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(content))
                    onlineVersion = reader.ReadToEnd();
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

        /// <summary>
        /// Compares the date of the latest online available framework
        /// with the current framework in the project.
        /// </summary>
        public static void CheckFrameworkDate()
        {
            if (!CheckOnline())
                return;

            HttpWebRequest frameworkFile = (HttpWebRequest)WebRequest.Create(@"http://www.lynx2d.com/res/lynx2d-min.js");
            HttpWebResponse frameworkFileResponse = (HttpWebResponse)frameworkFile.GetResponse();

            DateTime localFileModifiedTime = File.GetLastWriteTime("projects/" + Project.Name() + "/data/lynx2d.js");
            DateTime onlineFileModifiedTime = frameworkFileResponse.LastModified;

            if (localFileModifiedTime < onlineFileModifiedTime)
            {
                if (Input.YesNo("This project is using an outdated version of the Lynx2D framework. Do you want to install the newer version?", "Lynx2D Engine - Question"))
                    Project.DownloadFramework(true);
            }
        }

        /// <summary>
        /// Evaluate the startup.
        /// </summary>
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

        /// <summary>
        /// Show the changelog.
        /// </summary>
        /// <param name="welcomes"></param>
        public static void ShowChangelog(bool welcomes)
        {
            ChangelogForm changelog = new ChangelogForm();

            changelog.Initialize(welcomes);

            changelog.ShowDialog();
        }

        /// <summary>
        /// Update to the specified engine version.
        /// </summary>
        /// <param name="version">The requested version.</param>
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

        /// <summary>
        /// Get the current version.
        /// </summary>
        /// <returns>The current version.</returns>
        public static string Version()
        {
            return version + "-" + stage;
        }

        #region "Exception Handling"

        private delegate void GiveExceptionCallback(string type, string msg, string stack);

        /// <summary>
        /// Give an exception, allowing for possible exception submission.
        /// </summary>
        /// <param name="type">The type of exception.</param>
        /// <param name="msg">The exception message.</param>
        /// <param name="stack">The exception stack.</param>
        public static void GiveException(string type, string msg, string stack)
        {
            if (form.InvokeRequired)
                form.Invoke(new GiveExceptionCallback(GiveException), new object[] { type, msg, stack });
            else
            {
                form.SetStatus(type + " Exception occured.", Main.StatusType.Warning);

                if (Engine.ePreferences != null && Engine.ePreferences.suppressExceptions)
                    return;

                bool r = false;

                if (type != "Submit")
                    r = Input.YesNo(msg + "\n\nDo you want to submit this exception?", type + " Exception");
                else
                    Input.No(msg + "\n\nDo you want to submit this exception?", type + " Exception");

                if (r)
                {
                    try
                    {
                        using (WebClient client = new WebClient())
                        {
                            string s = client.DownloadString("http://www.lynx2d.com:5318/?title=[Lynx2D Engine Feedback] " + type + " Exception&body=" + (msg + " \n(stack:" + stack + ")"));

                            if (s == string.Empty || s == "wrong usage")
                                form.SetStatus("Could not submit the exception online.", Main.StatusType.Message);
                            else if (s == "success")
                                form.SetStatus("The exception has been submitted.", Main.StatusType.Message);
                        }
                    }
                    catch (Exception exc)
                    {
                        GiveException("Submit", exc);
                    }
                }
            }
        }

        /// <summary>
        /// Give an exception, allowing for possible submission.
        /// </summary>
        /// <param name="type">The exception type.</param>
        /// <param name="exc">The exception object.</param>
        public static void GiveException(string type, Exception exc)
        {
            GiveException(type, exc.Message, exc.StackTrace);
        }

        #endregion

        #region "Private Methods"

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
                GiveException("Update Evaluation", exc);
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
                wantsToExtract = true;

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
                GiveException("Updater", exc);
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

        #endregion
    }
}
