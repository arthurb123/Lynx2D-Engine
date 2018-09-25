using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    class Feed
    {
        public static Main form;

        private static readonly string version = "0.2.1";
        private static readonly string stage = "alpha";

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
            catch (Exception e)
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
                WebRequest webRequest = WebRequest.Create(@"http://www.lythumn.com/lynx2d/engine/version.txt");

                using (WebResponse response = webRequest.GetResponse())
                    using (Stream content = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(content))
                        {
                            onlineVersion = reader.ReadToEnd();
                            reader.Dispose();
                            content.Dispose();
                        }
            }
            catch (Exception e)
            {
                if (setsStatus) form.SetStatus("Could not get online version.", Main.StatusType.Warning);
            }

            if (onlineVersion != string.Empty && onlineVersion != Version())
            {
                if (Input.YesNo("A new version (" + onlineVersion + ") is available. Do you want to download this update?", "Lynx2D Engine - Update"))
                {
                    UpdateVersion(onlineVersion);
                }
                else if (setsStatus) form.SetStatus("You are running version " + Version() + ".", Main.StatusType.Message);
            }
            else if (setsStatus) form.SetStatus("Running latest version (" + Version() + ").", Main.StatusType.Message);
        }

        public static void UpdateVersion(string version)
        {
            Manager.CheckDirectory("downloads", true);

            try
            {
                WebClient client = new WebClient();

                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(UpdateVersionProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(UpdateVersionCompleted);
                client.DownloadFileAsync(new Uri("http://www.lythumn.com/lynx2d/engine/" + version + ".zip"), "downloads/" + version + ".zip");
            }
            catch (Exception e)
            {
                form.SetStatus("Could not download update.", Main.StatusType.Message);
            }
        }

        private static void UpdateVersionProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            form.SetStatus("Downloading.. (" + e.ProgressPercentage + "%)", Main.StatusType.Message);
        }

        private static void UpdateVersionCompleted(object sender, AsyncCompletedEventArgs e)
        {
            form.SetStatus("Download complete!", Main.StatusType.Message);

            try
            {
                Process.Start(@"downloads");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                form.SetStatus("Exception occurred trying to open downloads.", Main.StatusType.Warning);
            }
        }

        public static string Version()
        {
            return version + "-" + stage;
        }
    }
}
