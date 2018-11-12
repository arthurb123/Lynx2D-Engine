using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lynx2DEngine.Classes
{
    public static class Backup
    {
        public static float intervalMinutes = 5f;
        private static Timer interval = new Timer();

        public static void Enable()
        {
            if (interval.Enabled) return;

            interval = new Timer();

            interval.Interval = (int)(intervalMinutes * 60000);
            interval.Tick += new EventHandler(TimerDue);

            interval.Enabled = true;
        }

        public static void Disable()
        {
            if (!interval.Enabled) return;

            interval.Enabled = false;
        }

        public static void ChangeInterval(int minutes)
        {
            intervalMinutes = minutes;

            Disable();
            Enable();
        }

        private static void TimerDue(object sender, EventArgs e)
        {
            try
            {
                Manager.CheckDirectory("projects/" + Project.Name() + "/backup/", true);

                string time = DateTime.Now.ToString("dddd dd-HH.mm");
                DirectoryInfo dir = new DirectoryInfo("projects/" + Project.Name() + "/backup");

                if (dir.GetFiles().Length > 2)
                {
                    FileInfo oldestFile = dir.GetFiles()
                                 .OrderByDescending(f => f.LastWriteTime)
                                 .First();

                    File.Delete(oldestFile.FullName);
                }

                File.Copy("projects/" + Project.Name() + "/state.bin", "projects/" + Project.Name() + "/backup/state (" + time + ").bin");
            }
            catch
            {
                MessageBox.Show("An exception occurred while backing up the project state. Backup has been disabled.", "Lynx2D Engine - Exception");
                Disable();
            }
        }
    }
}
