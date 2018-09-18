using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    class Camera
    {
        private static bool injected;

        public static void Inject()
        {
            if (injected)
            {
                Engine.ExecuteScript("engineCamera.Focus();");
                return;
            }

            Engine.ExecuteScript("var engineCamera = new lx.GameObject(undefined, 0, 0, 1, 1).Show(0).Focus();");

            Engine.ExecuteScript("lx.OnKey('W', function() { engineCamera.Focus(); engineCamera.Position().Y-=3; });");
            Engine.ExecuteScript("lx.OnKey('A', function() { engineCamera.Focus(); engineCamera.Position().X-=3; });");
            Engine.ExecuteScript("lx.OnKey('S', function() { engineCamera.Focus(); engineCamera.Position().Y+=3; });");
            Engine.ExecuteScript("lx.OnKey('D', function() { engineCamera.Focus(); engineCamera.Position().X+=3; });");

            injected = true;
        }

        public static void Remove()
        {
            injected = false;
        }
    }
}
