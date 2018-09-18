using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return Directory.GetCurrentDirectory();
        }
    }
}
