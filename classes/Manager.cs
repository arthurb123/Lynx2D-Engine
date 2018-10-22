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
    }
}
