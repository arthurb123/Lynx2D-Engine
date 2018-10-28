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

            Engine.ExecuteScript("lx.CreateController();" +
                                "var engineCamera = new lx.GameObject(undefined, 0, 0, 1, 1).Show(0).Focus();" +
                                "lx.OnKey('W', function() { engineCamera.Focus(); engineCamera.Position().Y-=3; });" +
                                "lx.OnKey('A', function() { engineCamera.Focus(); engineCamera.Position().X-=3; });" +
                                "lx.OnKey('S', function() { engineCamera.Focus(); engineCamera.Position().Y+=3; });" +
                                "lx.OnKey('D', function() { engineCamera.Focus(); engineCamera.Position().X+=3; });");

            injected = true;
        }

        public static void Remove()
        {
            injected = false;
        }
    }
}
