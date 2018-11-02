using System;
using System.Collections.Generic;
using System.IO;

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
    }
}
