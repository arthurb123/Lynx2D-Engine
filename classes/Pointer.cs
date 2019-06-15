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

            Engine.ExecuteScript("var enginePointer = new lx.GameObject(new lx.Sprite('res/lynx2d/pointer.png'), 0, 0, 12, 12).Show(lx.GAME.BUFFER.length+1);" +
                                 "var enginePointerText = new lx.UIText('', 0, -4, 11).Follows(enginePointer).Show();" +
                                 "var enginePointerLoopID = lx.Loops(function() { " +
                                    "if (" + target + " == undefined) return; " +
                                    "var tX = " + target + ".Position().X; " +
                                    "var tY = " + target + ".Position().Y; " +
                                    "if (" + target + ".SIZE == undefined || " + target + ".SIZE.W == 0 && " + target + ".SIZE.H == 0) {" +
                                        "enginePointer.SPRITE.Source('res/lynx2d/location.png');" +
                                        "enginePointer.Position(tX-6, tY-6);" +
                                        "enginePointerText.Position(8, 0);" +
                                    "}" +
                                    "else {" +
                                        "enginePointer.SPRITE.Source('res/lynx2d/pointer.png');" +
                                        "enginePointer.Position(tX, tY);" +
                                        "enginePointerText.Position(0, -4);" +
                                    "}" +
                                    "enginePointerText.Text(Math.round(tX) + ' , ' + Math.round(tY)); " +
                                 "}).GAME.LOOPS.length-1;");

            injected = true;
        }

        public static void Remove()
        {
            if (!injected) return;

            Engine.ExecuteScript("enginePointer.Hide(); enginePointerText.Hide(); lx.GAME.LOOPS[enginePointerLoopID] = undefined; enginePointer = undefined; enginePointerText = undefined; enginePointerLoopID = undefined;");

            injected = false;
        }
    }
}
