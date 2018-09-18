using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lynx2DEngine
{
    class Pointer
    {
        private static bool injected;

        public static void Inject(string target)
        {
            if (injected)
                Remove();

            Engine.ExecuteScript("var enginePointer = new lx.GameObject(new lx.Sprite('res/lynx2d/pointer.png'), 0, 0, 12, 12).Show(lx.GAME.BUFFER.length);");
            Engine.ExecuteScript("var enginePointerText = new lx.UIText('', 0, -4, 11).Follows(enginePointer).Show();");
            Engine.ExecuteScript("var enginePointerLoopID = lx.Loops(function() { if (" + target + " == undefined) return; var tX = " + target + ".Position().X; var tY = " + target + ".Position().Y; enginePointer.Position(tX, tY); enginePointerText.Text(Math.round(tX) + ' , ' + Math.round(tY)); }).GAME.LOOPS.length-1;");

            injected = true;
        }

        public static void Remove()
        {
            if (!injected) return;

            Engine.ExecuteScript("enginePointer.Hide(); enginePointerText.Hide(); lx.ClearLoop(enginePointerLoopID); enginePointer = undefined; enginePointerText = undefined; enginePointerLoopID = undefined;");

            injected = false;
        }
    }
}
