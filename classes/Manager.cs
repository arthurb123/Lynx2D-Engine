using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    class Manager
    {
        public static bool CheckDirectory(string path, bool creates)
        {
            bool exists = Directory.Exists(Root() + "/" + path);
            if (!exists && creates)
            {
                Directory.CreateDirectory(Root() + "/" + path);
                exists = true;
            }

            return exists;
        }

        public static string Root()
        {
            return Directory.GetCurrentDirectory() + "/";
        }

        public static String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }

        public static void OpenDirectory(string path)
        {
            try
            {
                Process.Start(path);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Lynx2D Engine - Exception");
                Engine.form.SetStatus("Exception occurred while opening directory.", Main.StatusType.Warning);
            }
        }

        public static bool CopyFile(string source, string dest)
        {
            try
            {
                if (File.Exists(dest))
                    return false;

                File.Copy(source, dest, true);

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
                Engine.form.SetStatus("Exception occurred while copying file.", Main.StatusType.Warning);
            }

            return false;
        }

        public static void ClearAppData()
        {
            string appdataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/lynx2d/";

            if (Directory.Exists(appdataDir))
                Directory.Delete(appdataDir, true);
        }
    }
}
