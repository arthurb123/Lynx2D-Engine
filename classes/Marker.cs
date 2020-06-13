namespace Lynx2DEngine
{
    public static class Marker
    {
        private static bool injected;

        /// <summary>
        /// Injects the marker.
        /// </summary>
        /// <param name="target">The object target.</param>
        public static void Inject(string target)
        {
            if (injected)
                Remove();

            Engine.ExecuteScript(
                "var engineMarker = new lx.GameObject(new lx.Sprite('res/lynx2d/pointer.png'), 0, 0, 12, 12).Show(lx.GAME.BUFFER.length+1);" +
                "var engineMarkerText = new lx.UIText('', 0, -4, 11).Follows(engineMarker).Show();" +

                "if (" + target + ".SIZE == undefined || " + target + ".SIZE.W == 0 && " + target + ".SIZE.H == 0) {" +
                    "engineMarker.SPRITE.Source('res/lynx2d/location.png');" +
                    "engineMarkerText.Offset(8, 0);" +
                "}" +
                "else {" +
                    "engineMarker.SPRITE.Source('res/lynx2d/pointer.png');" +
                    "engineMarkerText.Offset(0, -4);" +
                "}" +

                "engineMarker.Loops(function() { " +
                    "if (" + target + " == undefined) return; " +

                    "let tX = " + target + ".Position().X; " +
                    "let tY = " + target + ".Position().Y; " +

                    "if (" + target + ".SIZE == undefined || " + target + ".SIZE.W == 0 && " + target + ".SIZE.H == 0) {" +
                        "engineMarker.Position(tX-6, tY-6);" +
                    "}" +
                    "else {" +
                        "engineMarker.Position(tX, tY);" +
                    "}" +

                    "engineMarkerText.Text(Math.round(tX) + ' , ' + Math.round(tY)); " +
                "});"
            );

            injected = true;
        }

        /// <summary>
        /// Removes the marker.
        /// </summary>
        public static void Remove()
        {
            if (!injected) return;

            Engine.ExecuteScript(
                "engineMarker.Hide();" +
                "engineMarkerText.Hide();" +
                "delete engineMarker;" +
                "delete engineMarkerText;" +
                "engineMarker = undefined;" +
                "engineMarkerText = undefined;" +
                "engineMarkerDown = undefined;"
            );

            injected = false;
        }
    }
}
