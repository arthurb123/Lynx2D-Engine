using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx2DEngine
{
    class Grid
    {
        private static bool injected;

        public static void Inject()
        {
            if (injected)
                Remove();

            Engine.ExecuteScript("var engineGridRenderID = lx.GAME.ADD_LAYER_DRAW_EVENT(" + Engine.eSettings.gridLayer + ", function(gfx) { " +
                "gfx.save();" +
                "gfx.lineWidth = " + Engine.eSettings.gridStrokeSize + ";" +
                "gfx.strokeStyle = '" + Engine.eSettings.gridColor + "';" +
                "gfx.globalAlpha = " + Engine.eSettings.gridOpacity + "/100;" +
                "var x = 0, y = 0; " +
                "if (lx.GAME.FOCUS != undefined) { " +
                    "var tPos = lx.GAME.TRANSLATE_FROM_FOCUS({ X: x, Y: y });" +
                    "x = tPos.X;" +
                    "y = tPos.Y;" +
                "} " +
                "for (var yy = 0; yy < " + Engine.eSettings.gridHeight + "; yy++) {" +
                    "for (var xx = 0; xx < " + Engine.eSettings.gridWidth + "; xx++) {" +
                        "gfx.strokeRect(x + (xx+" + Engine.eSettings.gridOffX  + ")* " + Engine.eSettings.gridSize + ", y + (yy+" + Engine.eSettings.gridOffY  + ")* " + Engine.eSettings.gridSize + ", " + Engine.eSettings.gridSize + ", " + Engine.eSettings.gridSize + ");" +
                    "}" +
                "}" +
                "gfx.restore();" +
            "});");

            injected = true;
        }

        public static void Remove()
        {
            if (!injected) return;

            Engine.ExecuteScript("lx.GAME.LAYER_DRAW_EVENTS[" + Engine.eSettings.gridLayer + "][engineGridRenderID] = undefined; engineGridRenderID = undefined;");

            injected = false;
        }
    }
}
