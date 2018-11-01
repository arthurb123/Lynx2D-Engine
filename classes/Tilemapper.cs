using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    public static class Tilemapper
    {
        private static bool[] injected = new bool[0];
        public static Tilemap[] maps = new Tilemap[0];
        public static Tile selected = null;
        public static int editing = -1;
        private static int selectedLayer = 0;
        public static Main form;
        private static Dictionary<int, List<string>> existingColliders = new Dictionary<int, List<string>>();

        public static void LoadFromScene(int id)
        {
            Clear();

            maps = Engine.scenes[id].tilemaps;
            injected = new bool[maps.Length];

            for (int i = 0; i < maps.Length; i++)
            {
                injected[i] = false;
                existingColliders.Add(i, new List<string>());
            }
        }

        public static void SaveMapsToCurrentScene()
        {
            if (Engine.eSettings.currentScene == -1)
                return;

            Engine.scenes[Engine.eSettings.currentScene].tilemaps = maps;
        }

        public static string ToBuildCode(string var, Tilemap tm)
        {
            tm.colliders = new bool[tm.map.GetLength(0), tm.map.GetLength(1)];

            string r = "var " + var + " = lx.GAME.ADD_LAYER_DRAW_EVENT(" + tm.layer + ", function(gfx) {",
                   c = "\n";

            for (int i = 0; i < tm.map.GetLength(0); i++)
                for (int j = 0; j < tm.map.GetLength(1); j++)
                {
                    Tile el = tm.map[i, j];

                    if (el != null && el.build)
                    {
                        r += BuildTile(i, j, tm, el);

                        if (tm.collides && !tm.colliders[i, j])
                            c += GenerateCollider(tm, i, j);
                    }
                }

            r += "});";

            return r + c + (c.Length != 0 ? "\n" : "");
        }

        public static void Clear()
        {
            StopEditing();

            injected = new bool[0];
            maps = new Tilemap[0];
            selected = null;
        }

        public static void ResetInjections()
        {
            for (int i = 0; i < injected.Length; i++)
                injected[i] = false;
        }

        public static void InjectAll()
        {
            for (int i = 0; i < maps.Length; i++)
                if (maps[i] != null) InjectMap(i);
        }

        public static int AddMap(Tilemap tm)
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
                    injected[i] = false;

                    SaveMapsToCurrentScene();

                    InjectMap(i);

                    return i;
                }
            }

            return -1;
        }

        public static void InjectMap(int map)
        {
            if (injected[map]) return;

            maps[map].colliders = new bool[maps[map].map.GetLength(0), maps[map].map.GetLength(1)];
            Engine.ExecuteScript("if (lx.GAME.LAYER_DRAW_EVENTS[" + maps[map].layer + "] == undefined) lx.GAME.LAYER_DRAW_EVENTS[" + maps[map].layer + "] = [];" +
                                 "var engineTileMap" + map + "RenderID = lx.GAME.ADD_LAYER_DRAW_EVENT(" + maps[map].layer + ", function(gfx) {});");
            injected[map] = true;

            ConvertAndSetMap(maps[map]);
        }

        public static void AdjustLayer(int map, int layer)
        {
            if (!injected[map]) return;

            Engine.ExecuteScript("lx.GAME.LAYER_DRAW_EVENTS[" + maps[map].layer + "][engineTileMap" + map + "RenderID] = undefined;" +
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

            tm.colliders = new bool[tm.map.GetLength(0), tm.map.GetLength(1)];

            string r = "",
                   c = "";

            foreach (string ec in existingColliders[tm.id])
            {
                c += "if (window['" + ec + "'] != undefined) {" +
                        "window['" + ec + "'].Disable();" +
                        "window['" + ec + "'] = undefined;" +
                     "}";
            }

            existingColliders[tm.id] = new List<string>();

            for (int i = 0; i < tm.map.GetLength(0); i++)
                for (int j = 0; j < tm.map.GetLength(1); j++)
                {
                    Tile el = tm.map[i, j];

                    if (el != null && el.build)
                    {
                        r += BuildTile(i, j, tm, el);

                        if (tm.collides && !tm.colliders[i, j])
                        {
                            string tileColl = "engineTileMap" + tm.id + "TileCollider" + (j * tm.map.GetLength(1) + i);
                            existingColliders[tm.id].Add(tileColl);

                            c += "var " + tileColl + " = " + GenerateCollider(tm, i, j);
                        }
                    }
                }

            string total = "lx.GAME.LAYER_DRAW_EVENTS[" + tm.layer + "][engineTileMap" + tm.id + "RenderID] = function(gfx){ " +
                                    r +
                           "};" + (tm.collides ? c : "");

            Engine.ExecuteScript(total);
        }

        public static string BuildTile(int i, int j, Tilemap tm, Tile el)
        {
            if (tm.scale == 0)
                tm.scale = 1;

            return "lx.DrawSprite(" + el.sprite + ".Rotation(" + el.r + ").Clip(" + el.cX + ", " + el.cY + ", " + el.cW + ", " + el.cH + ")," +
                                (i + tm.x) * tm.scale * tm.tilesize + ", " + (j + tm.y) * tm.scale * tm.tilesize + ", " + (el.cW * tm.scale) + ", " + (el.cH * tm.scale) + ");";
        }

        public static void RemoveMap(int map)
        {
            maps[map] = null;

            SaveMapsToCurrentScene();

            //Project.Save();

            //Engine.LoadEngineState();
        }

        public static void BeginEditing(int map)
        {
            if (editing != -1) return;

            if (!injected[map])
                InjectMap(map);

            selectedLayer = maps[map].layer + 1;

            Engine.ExecuteScript("var engineTileMapperRenderID = lx.GAME.ADD_LAYER_DRAW_EVENT(" + selectedLayer + ", function(gfx) {});" +
                                 "var engineTileMapperTileSize = " + maps[map].tilesize*maps[map].scale + ";" +
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
                                    "console.log('ENGINE_INTERACTION_' + key + '(X' + (Math.floor((center.X-lx.GetDimensions().width/2-engineTileMapperTileSize/2)/engineTileMapperTileSize) + Math.ceil(lx.CONTEXT.CONTROLLER.MOUSE.POS.X/engineTileMapperTileSize)) + 'Y' + (Math.floor((center.Y-lx.GetDimensions().height/2-engineTileMapperTileSize/2)/engineTileMapperTileSize)+Math.ceil(lx.CONTEXT.CONTROLLER.MOUSE.POS.Y/engineTileMapperTileSize)) + rotation +')');" +
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
                                    "var tPos = { X: " + (maps[editing].x * maps[editing].tilesize * maps[editing].scale) + ", Y: " + (maps[editing].y * maps[editing].tilesize * maps[editing].scale) + " };" +
                                    "if (lx.GAME.FOCUS != undefined) " +
                                        "tPos = lx.GAME.TRANSLATE_FROM_FOCUS(tPos);" +
                                    "gfx.strokeRect(tPos.X, tPos.Y, " + (maps[editing].map.GetLength(0)*maps[editing].tilesize*maps[editing].scale) + ", " + (maps[editing].map.GetLength(1) * maps[editing].tilesize * maps[editing].scale) + ");" +
                                    "tPos = { X: lx.GetDimensions().width/2, Y: lx.GetDimensions().height/2 };" +
                                    "if (lx.GAME.FOCUS != undefined) tPos = lx.GAME.FOCUS.POS;" +
                                    "tPos = {" +
                                        "X: Math.floor((tPos.X - lx.GetDimensions().width / 2-" + maps[editing].tilesize * maps[editing].scale + "/2) / " + maps[editing].tilesize * maps[editing].scale + ") * " + maps[editing].tilesize * maps[editing].scale + "+Math.ceil(lx.CONTEXT.CONTROLLER.MOUSE.POS.X / " + maps[editing].tilesize * maps[editing].scale + ") * " + maps[editing].tilesize * maps[editing].scale + ", " +
                                        "Y: Math.floor((tPos.Y-lx.GetDimensions().height/2-" + maps[editing].tilesize * maps[editing].scale + "/2)/" + maps[editing].tilesize * maps[editing].scale + ")*" + maps[editing].tilesize * maps[editing].scale + "+Math.ceil(lx.CONTEXT.CONTROLLER.MOUSE.POS.Y/" + maps[editing].tilesize * maps[editing].scale + ")*" + maps[editing].tilesize * maps[editing].scale +
                                    "};" +
                                    "lx.DrawSprite(" + selected.sprite + ".Rotation(engineTileSelectionRotation*Math.PI/180).Clip(" +
                                        selected.cX + ", " +
                                        selected.cY + ", " +
                                        selected.cW + ", " +
                                        selected.cH + "), " +
                                        "tPos.X, " +
                                        "tPos.Y, " +
                                        selected.cW * maps[editing].scale + ", " +
                                        selected.cH * maps[editing].scale + ");" +
                                  "gfx.strokeStyle = 'ghostwhite';" +
                                  "if (lx.GAME.FOCUS != undefined) " +
                                        "tPos = lx.GAME.TRANSLATE_FROM_FOCUS(tPos);" +
                                  "gfx.strokeRect(tPos.X, tPos.Y, " + selected.cW * maps[editing].scale + ", " + selected.cH * maps[editing].scale + ");" +
                                  "gfx.restore();" +
                                "};");
        }

        public static void PlaceTile(int x, int y, float r)
        {
            if (selected == null)
                return;

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
                int.TryParse(msg.Substring(msg.IndexOf('X') + 1, msg.IndexOf('Y') - msg.IndexOf('X') - 1), out int x);
                int.TryParse(msg.Substring(msg.IndexOf('Y') + 1, msg.IndexOf('R') - msg.IndexOf('Y') - 1), out int y);
                float.TryParse(msg.Substring(msg.IndexOf('R') + 1, msg.IndexOf(')') - msg.IndexOf('R') - 1), out float r);

                PlaceTile(x, y, r);
            }
            if (msg.Contains("REMOVE_TILE"))
            {
                int.TryParse(msg.Substring(msg.IndexOf('X') + 1, msg.IndexOf('Y') - msg.IndexOf('X') - 1), out int x);
                int.TryParse(msg.Substring(msg.IndexOf('Y') + 1, msg.IndexOf(')') - msg.IndexOf('Y') - 1), out int y);

                RemoveTile(x, y);
            }
        }

        public static void RenameSpriteInTiles(string oldSprite, string newSprite)
        {
            foreach (Tilemap tm in maps)
                if (tm != null)
                    foreach (Tile t in tm.map)
                        if (t != null && t.build && t.sprite == oldSprite)
                            t.sprite = newSprite;
        }

        public static string GenerateCollider(Tilemap tm, int y, int x)
        {
            int w = 1, h = 1;

            tm.colliders[y, x] = true;

            for (int i = 0; i < tm.map.GetLength(0) - y; i++)
            {
                if (tm.map[y + i, x] != null && tm.map[y + i, x].build && !tm.colliders[y + i, x])
                {
                    w++;

                    tm.colliders[y + i, x] = true;
                }
                else if (i != 0)
                    break;
            }

            int fh = 1;

            for (int yy = y; yy < y + w; yy++)
            {
                h = 1;

                for (int xx = x+1; xx <= tm.map.GetLength(1) - x - 1; xx++)
                {
                    if (tm.map[yy, xx] != null && tm.map[yy, xx].build && !tm.colliders[yy, xx])
                    {
                        if (yy != y && h >= fh)
                            break;

                        h++;

                        tm.colliders[yy, xx] = true;
                    }
                    else break;
                }

                if (yy == y)
                    fh = h;

                if (h < fh)
                {
                    w = (yy - y);

                    break;
                }
            }

            return "new lx.Collider(" + (y + tm.x) * tm.tilesize * tm.scale + ", " + (x + tm.y) * tm.tilesize * tm.scale + ", " + w * tm.tilesize * tm.scale + ", " + fh * tm.tilesize * tm.scale + ", true);";
        }
    }
}
