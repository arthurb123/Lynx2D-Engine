using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    public static class Tilemapper
    {
        private static bool[] injected = new bool[0];
        public static Tilemap[] maps = new Tilemap[0];
        public static Tile selected = null;
        private static int editing = -1;
        private static int selectedLayer = 0;

        public static void LoadFromScene(int id)
        {
            Clear();

            maps = Engine.scenes[id].tilemaps;
            injected = new bool[maps.Length];

            for (int i = 0; i < injected.Length; i++)
                injected[i] = false;
        }

        public static void SaveMapsToCurrentScene()
        {
            if (Engine.eSettings.currentScene == -1)
                return;

            Engine.scenes[Engine.eSettings.currentScene].tilemaps = maps;
        }

        public static string ToBuildCode(string var, Tilemap tm)
        {
            string r = "var " + var + " = lx.GAME.ADD_LAYER_DRAW_EVENT(" + tm.layer + ", function(gfx) {";

            for (int i = 0; i < tm.map.GetLength(0); i++)
                for (int j = 0; j < tm.map.GetLength(1); j++)
                {
                    Tile el = tm.map[i, j];

                    if (el != null && el.build)
                        r += BuildTile(i, j, tm, el);
                }

            r += "});\n";

            return r;
        }

        public static void Clear()
        {
            StopEditing();

            injected = new bool[0];
            maps = new Tilemap[0];
            selected = null;
        }

        public static void InjectAll()
        {
            for (int i = 0; i < maps.Length; i++)
                if (maps[i] != null) InjectMap(i);
        }

        public static int AddMap(Tilemap tm, bool injects)
        {
            for (int i = 0; i < maps.Length + 1; i++)
            {
                if (i == maps.Length)
                {
                    Array.Resize(ref maps, maps.Length + 1);
                    Array.Resize(ref injected, maps.Length);
                }

                if (maps[i] == null || i == maps.Length)
                {
                    maps[i] = tm;
                    maps[i].id = i;

                    if (injects)
                        InjectMap(i);

                    SaveMapsToCurrentScene();

                    return i;
                }
            }

            return -1;
        }

        public static void InjectMap(int map)
        {
            if (injected[map]) return;

            Engine.ExecuteScript("var engineTileMap" + map + "RenderID;");
            injected[map] = true;

            AdjustLayer(map, maps[map].layer);
            ConvertAndSetMap(maps[map]);
        }

        public static void AdjustLayer(int map, int layer)
        {
            if (!injected[map]) return;

            Engine.ExecuteScript("if (lx.GAME.LAYER_DRAW_EVENTS[" + maps[map].layer + "] == undefined) lx.GAME.LAYER_DRAW_EVENTS[" + maps[map].layer + "] = [];" +
                                 "lx.GAME.LAYER_DRAW_EVENTS[" + maps[map].layer + "][engineTileMap" + map + "RenderID] = undefined;" +
                                 "engineTileMap" + map + "RenderID = lx.GAME.ADD_LAYER_DRAW_EVENT(" + layer +", function(gfx) {});");

            maps[map].layer = layer;
        }

        public static void ConvertAndSetMap(Tilemap tm)
        {
            if (!injected[tm.id])
            {
                InjectMap(tm.id);
                return;
            }

            string r = "";


            for (int i = 0; i < tm.map.GetLength(0); i++)
                for (int j = 0; j < tm.map.GetLength(1); j++)
                {
                    Tile el = tm.map[i, j];

                    if (el != null && el.build)
                        r += BuildTile(i, j, tm, el);
                }

            Engine.ExecuteScript("lx.GAME.LAYER_DRAW_EVENTS[" + tm.layer + "][engineTileMap" + tm.id + "RenderID] = function(gfx){ " +
                                    r +
                                "};");
        }

        public static string BuildTile(int i, int j, Tilemap tm, Tile el)
        {
            return "lx.DrawSprite(" + el.sprite + ".Rotation(" + el.r + ").Clip(" + el.cX + ", " + el.cY + ", " + el.cW + ", " + el.cH + ")," +
                                (i + tm.x) * tm.tilesize + ", " + (j + tm.y) * tm.tilesize + ", " + el.cW + ", " + el.cH + ");";
        }

        public static void RemoveMap(int map)
        {
            //Engine.ExecuteScript("lx.GAME.LAYER_DRAW_EVENTS[" + tm.layer + "][engineTileMap" + tm.id + "RenderID] = undefined; engineTileMap" + tm.id + "RenderID = undefined;");
            maps[map] = null;
            Project.Save();

            Engine.LoadEngineState();
        }

        public static void BeginEditing(int map)
        {
            if (editing != -1) return;

            selectedLayer = maps[map].layer + 1;

            Engine.ExecuteScript("var engineTileMapperRenderID = lx.GAME.ADD_LAYER_DRAW_EVENT(" + selectedLayer + ", function(gfx) {});" +
                                 "var engineTileMapperTileSize = " + maps[map].tilesize + ";" +
                                 "var engineTileSelectionRotation = 0; " +
                                 "var engineTileSelectionRotationEvent = lx.GAME.ADD_EVENT('mousebutton', 1, function() { " +
                                    "engineTileSelectionRotation += 90; " +
                                    "if (engineTileSelectionRotation >= 360) " +
                                        "engineTileSelectionRotation = 0; " +
                                    "lx.StopMouse(1); " +
                                 "});" +
                                 "var engineTileMapperPostMouse = function(key) { " +
                                    "var center = { X: lx.GetDimensions().width/2, Y: lx.GetDimensions().height/2 };" +
                                    "var rotation = '';" +
                                    "if (lx.GAME.FOCUS != undefined) center = lx.GAME.FOCUS.POS;" +
                                    "if (key == 'PLACE_TILE') rotation = 'R' + (engineTileSelectionRotation*Math.PI/180);" +
                                    "console.log('ENGINE_INTERACTION_' + key + '(X' + (Math.floor((center.X-lx.GetDimensions().width/2)/engineTileMapperTileSize) + Math.ceil(lx.CONTEXT.CONTROLLER.MOUSE.POS.X/engineTileMapperTileSize)) + 'Y' + (Math.floor((center.Y-lx.GetDimensions().height/2)/engineTileMapperTileSize)+Math.ceil(lx.CONTEXT.CONTROLLER.MOUSE.POS.Y/engineTileMapperTileSize)) + rotation +')');" +
                                 "};" +
                                 "var engineTileMapperEventIDL = lx.GAME.ADD_EVENT('mousebutton', 0, function() {" +
                                    "engineTileMapperPostMouse('PLACE_TILE');" +
                                 "});" +
                                 "var engineTileMapperEventIDR = lx.GAME.ADD_EVENT('mousebutton', 2, function() {" +
                                    "engineTileMapperPostMouse('REMOVE_TILE');" +
                                 "});");

            editing = map;
            
            if (selected != null) SetCurrentTile();
        }

        public static void RefreshEditingEvent()
        {
            if (editing == -1) return;

            Engine.ExecuteScript("engineTileMapperTileSize = " + maps[editing].tilesize + ";");
        }

        public static void StopEditing()
        {
            if (editing == -1) return;

            Engine.ExecuteScript("lx.GAME.LAYER_DRAW_EVENTS[" + selectedLayer + "][engineTileMapperRenderID] = undefined; " +
                "engineTileMapperRenderID = undefined; " +
                "lx.GAME.EVENTS[engineTileMapperEventIDL] = undefined; " +
                "engineTileMapperEventIDL = undefined; " +
                "lx.GAME.EVENTS[engineTileMapperEventIDR] = undefined; " +
                "engineTileMapperEventIDR = undefined; " +
                "lx.GAME.EVENTS[engineTileSelectionRotationEvent] = undefined;" +
                "engineTileSelectionRotationEvent = undefined" +
                "engineTileMapperPostMouse = undefined; " +
                "engineTileMapperTileSize = undefined;" +
                "engineTileSelectionRotation = undefined;");
            selected = null;

            editing = -1;
        }

        public static void SetCurrentTile()
        {
            if (editing == -1) return;

            Engine.ExecuteScript("lx.GAME.LAYER_DRAW_EVENTS[" + selectedLayer + "][engineTileMapperRenderID] = function(gfx) {" +
                                    "gfx.save();" +
                                    "gfx.imageSmoothing = lx.GAME.SETTINGS.AA;" +
                                    "gfx.lineWidth = 2;" +
                                    "gfx.strokeStyle = 'purple';" +
                                    "var tPos = { X: " + (maps[editing].x * maps[editing].tilesize) + ", Y: " + (maps[editing].y * maps[editing].tilesize) + " };" +
                                    "if (lx.GAME.FOCUS != undefined) " +
                                        "tPos = lx.GAME.TRANSLATE_FROM_FOCUS(tPos);" +
                                    "gfx.strokeRect(tPos.X, tPos.Y, " + (maps[editing].map.GetLength(0)*maps[editing].tilesize) + ", " + (maps[editing].map.GetLength(1) * maps[editing].tilesize) + ");" +
                                    "tPos = { X: lx.GetDimensions().width/2, Y: lx.GetDimensions().height/2 };" +
                                    "if (lx.GAME.FOCUS != undefined) tPos = lx.GAME.FOCUS.POS;" +
                                    "tPos = {" +
                                        "X: Math.floor((tPos.X - lx.GetDimensions().width / 2) / " + maps[editing].tilesize + ") * " + maps[editing].tilesize + "+Math.ceil(lx.CONTEXT.CONTROLLER.MOUSE.POS.X / " + maps[editing].tilesize + ") * " + maps[editing].tilesize + ", " +
                                        "Y: Math.floor((tPos.Y-lx.GetDimensions().height/2)/" + maps[editing].tilesize + ")*" + maps[editing].tilesize + "+Math.ceil(lx.CONTEXT.CONTROLLER.MOUSE.POS.Y/" + maps[editing].tilesize + ")*" + maps[editing].tilesize +
                                    "};" +
                                    "lx.DrawSprite(" + selected.sprite + ".Rotation(engineTileSelectionRotation*Math.PI/180).Clip(" +
                                        selected.cX + ", " +
                                        selected.cY + ", " +
                                        selected.cW + ", " +
                                        selected.cH + "), " +
                                        "tPos.X, " +
                                        "tPos.Y, " +
                                        selected.cW + ", " +
                                        selected.cH + ");" +
                                  "gfx.strokeStyle = 'ghostwhite';" +
                                  "if (lx.GAME.FOCUS != undefined) " +
                                        "tPos = lx.GAME.TRANSLATE_FROM_FOCUS(tPos);" +
                                  "gfx.strokeRect(tPos.X, tPos.Y, " + selected.cW + ", " + selected.cH + ");" +
                                  "gfx.restore();" +
                                "};");
        }

        public static void PlaceTile(int x, int y, float r)
        {
            if (editing == -1) return;
            if (x < maps[editing].x || y < maps[editing].y)
            {
                Engine.ExecuteScript("lx.StopMouse(0);");

                MessageBox.Show("Tilemaps do not support negative values (yet). Try changing the position of the tilemap.", "Lynx2D Engine - Exception");
                return;
            }

            selected.r = r;

            maps[editing].SetTile(x, y, selected);
        }

        public static void RemoveTile(int x, int y)
        {
            if (editing == -1) return;
            if (x < maps[editing].x || y < maps[editing].y)
            {
                Engine.ExecuteScript("lx.StopMouse(2);");

                MessageBox.Show("Tilemaps do not support negative values (yet). Try changing the position of the tilemap.", "Lynx2D Engine - Exception");
                return;
            }

            maps[editing].RemoveTile(x, y);
            ConvertAndSetMap(maps[editing]);
        }

        public static void SelectTile(int map, int cX, int cY, int cW, int cH)
        {
            selected = new Tile(maps[map].curSprite, cX, cY, cW, cH);

            if (editing == -1)
                BeginEditing(map);
            else
                SetCurrentTile();
        }

        public static void HandleConsoleInteraction(string msg)
        {
            if (msg.Contains("PLACE_TILE"))
            {
                int x = 0, y = 0;
                float r = 0;

                int.TryParse(msg.Substring(msg.IndexOf('X') + 1, msg.IndexOf('Y') - msg.IndexOf('X') - 1), out x);
                int.TryParse(msg.Substring(msg.IndexOf('Y') + 1, msg.IndexOf('R') - msg.IndexOf('Y') - 1), out y);
                float.TryParse(msg.Substring(msg.IndexOf('R') + 1, msg.IndexOf(')') - msg.IndexOf('R') - 1), out r);

                PlaceTile(x, y, r);
            }
            if (msg.Contains("REMOVE_TILE"))
            {
                int x = 0, y = 0;

                int.TryParse(msg.Substring(msg.IndexOf('X') + 1, msg.IndexOf('Y') - msg.IndexOf('X') - 1), out x);
                int.TryParse(msg.Substring(msg.IndexOf('Y') + 1, msg.IndexOf(')') - msg.IndexOf('Y') - 1), out y);

                RemoveTile(x, y);
            }
        }
    }
}
