namespace Lynx2DEngine
{
    class Camera
    {
        private static bool injected;

        public static void Inject()
        {
            if (injected)
            {
                Engine.ExecuteScript("if (window['engineCamera'] != undefined) engineCamera.Focus();");
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
