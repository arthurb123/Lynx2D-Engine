using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx2DEngine
{
    class Grid
    {
        public static int gridSize = 64;
        public static int gridWidth = 16;
        public static int gridHeight = 16;
        public static int gridStrokeSize = 2;

        private static bool injected;

        public static void Inject()
        {
            if (injected)
                Remove();

            Engine.ExecuteScript("var engineGridRenderID = lx.GAME.ENGINE_RENDER.length; lx.GAME.ENGINE_RENDER[engineGridRenderID] = function(gfx) { " +
                "gfx.save();" +
                "gfx.strokeWidth = " + gridStrokeSize + ";" +
                "var x = 0, y = 0; " +
                "if (lx.GAME.FOCUS != undefined) { " +
                    "var tPos = lx.GAME.TRANSLATE_FROM_FOCUS({ X: x, Y: y });" +
                    "x = tPos.X;" +
                    "y = tPos.Y;" +
                "} " +
                "for (var yy = 0; yy < " + gridHeight + "; yy++) {" +
                    "for (var xx = 0; xx < " + gridWidth + "; xx++) {" +
                        "gfx.strokeRect(x + xx*" + gridSize + ", y + yy*" + gridSize + ", " + gridSize + ", " + gridSize + ");" +
                    "}" +
                "}" +
                "gfx.restore();" +
            "};");

            injected = true;
        }

        public static void Remove()
        {
            if (!injected) return;

            Engine.ExecuteScript("lx.GAME.ENGINE_RENDER.splice(engineGridRenderID, 1); engineGridRenderID = undefined;");

            injected = false;
        }
    }
}
