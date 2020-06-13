namespace Lynx2DEngine
{
    public static class Camera
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
                                "let engineCamera = new lx.GameObject(undefined, 0, 0, 1, 1).Show(0).Focus();" +
                                "let engineCameraSpeed = 4;" +
                                "lx.OnKey(16, function() { engineCameraSpeed = 6; });" +
                                "lx.OnKey('W', function() { engineCamera.Focus(); engineCamera.Position().Y-=engineCameraSpeed; engineCameraSpeed = 4; });" +
                                "lx.OnKey('A', function() { engineCamera.Focus(); engineCamera.Position().X-=engineCameraSpeed; engineCameraSpeed = 4; });" +
                                "lx.OnKey('S', function() { engineCamera.Focus(); engineCamera.Position().Y+=engineCameraSpeed; engineCameraSpeed = 4; });" +
                                "lx.OnKey('D', function() { engineCamera.Focus(); engineCamera.Position().X+=engineCameraSpeed; engineCameraSpeed = 4; });");

            injected = true;
        }

        public static void Remove()
        {
            injected = false;
        }
    }
}
