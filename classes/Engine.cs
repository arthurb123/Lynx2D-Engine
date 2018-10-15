using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    class Engine
    {
        public static Main form;

        public static Scene[] scenes = new Scene[0];
        public static BuildSettings bSettings = new BuildSettings();
        public static EngineSettings eSettings = new EngineSettings();

        public static void LoadScene(int id)
        {
            if (id == -1 || eSettings.currentScene == -1)
                CreateScene(false);
            else
            {
                eSettings.currentScene = id;
                Tilemapper.LoadFromScene(eSettings.currentScene);

                form.refreshBrowser();
            }

            //We want to make sure a hierarchy exists (migrating from 0.4.0-beta)
            if (scenes[eSettings.currentScene].hierarchy == null)
            {
                scenes[eSettings.currentScene].hierarchy = new Hierarchy();

                for (int i = 0; i < scenes[eSettings.currentScene].objects.Length; i++)
                {
                    if (scenes[eSettings.currentScene].objects[i] != null)
                        scenes[eSettings.currentScene].hierarchy.AddItem(i);
                }
            }

            form.UpdateHierarchy();
        }

        public static void RemoveScene()
        {
            if (!Input.YesNo("Are you sure you want to delete the scene '" + scenes[eSettings.currentScene].Variable() + "'?", "Lynx2D Engine - Question"))
                return;

            if (eSettings.currentScene == bSettings.standardScene)
            {
                MessageBox.Show("Could not remove '" + scenes[eSettings.currentScene].Variable() + "', as this is the standard scene of the project.", "Lynx2D Engine - Exception");
                return;
            }

            scenes[eSettings.currentScene] = null;
            
            for (int i = 0; i < scenes.Length; i++)
            {
                if (scenes[i] != null)
                {
                    LoadScene(i);

                    return;
                }
            }

            LoadScene(-1);
        }

        public static void CreateScene(bool loads)
        {
            for (int i = 0; i < scenes.Length + 1; i++)
            {
                if (i == scenes.Length) Array.Resize(ref scenes, scenes.Length + 1);

                if (scenes[i] == null)
                {
                    scenes[i] = new Scene(i);

                    if (loads)
                        LoadScene(i);
                    else
                        eSettings.currentScene = i;

                    break;
                }
            }
        }

        public static void RenameScene(int id, string name)
        {
            if (scenes[id] == null) return;

            scenes[id].Rename(name);

            form.refreshBrowser();
        }

        public static int AddEngineObject(EngineObjectType type, string code, int child, int parent)
        {
            for (int i = 0; i < scenes[eSettings.currentScene].objects.Length + 1; i++)
            {
                if (i == scenes[eSettings.currentScene].objects.Length) Array.Resize(ref scenes[eSettings.currentScene].objects, scenes[eSettings.currentScene].objects.Length + 1);

                if (scenes[eSettings.currentScene].objects[i] == null || i == scenes[eSettings.currentScene].objects.Length)
                {
                    //Add new engine object
                    scenes[eSettings.currentScene].objects[i] = new EngineObject(i, type, code, child, parent);

                    //Add new hierarchy entry
                    scenes[eSettings.currentScene].hierarchy.AddItem(i);

                    return i;
                }
            }

            return -1;
        }

        public static void RemoveEngineObject(int id, bool refreshes)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            if (scenes[eSettings.currentScene].objects[id].child != -1) RemoveEngineObject(scenes[eSettings.currentScene].objects[id].child, false);
            if (scenes[eSettings.currentScene].objects[id].parent != -1) scenes[eSettings.currentScene].objects[scenes[eSettings.currentScene].objects[id].parent].child = -1;

            if (scenes[eSettings.currentScene].objects[id].type == EngineObjectType.Tilemap)
                Tilemapper.RemoveMap(scenes[eSettings.currentScene].objects[id].tileMap);

            scenes[eSettings.currentScene].objects[id] = null;
            scenes[eSettings.currentScene].hierarchy.RemoveItem(id);

            if (refreshes)
            {
                form.UpdateHierarchy();

                form.refreshBrowser();
            }
        }

        public static int[] GetEmptyEnginePositions()
        {
            int[] temp = new int[0];

            for (int i = 0; i < scenes[eSettings.currentScene].objects.Length; i++)
            {
                if (scenes[eSettings.currentScene].objects[i] == null)
                {
                    Array.Resize(ref temp, temp.Length + 1);
                    temp[temp.Length - 1] = i;
                }
            }

            Array.Resize(ref temp, temp.Length + 1);
            temp[temp.Length - 1] = scenes[eSettings.currentScene].objects.Length;

            return temp;
        }

        private static void GenerateEngineObjectCode(int scene, int id)
        {
            string lineBreaks = new string('\n', bSettings.lineBreaks);
            if (bSettings.obfuscates)
                lineBreaks = "";

            string variable = lineBreaks + scenes[scene].objects[id].Variable();

            form.SetStatus("Building '" + scenes[scene].objects[id].Variable() + "'", Main.StatusType.Message);

            if (scenes[scene].objects[id].type == EngineObjectType.GameObject)
            {
                scenes[scene].objects[id].buildCode = lineBreaks + "var " + scenes[scene].objects[id].Variable() + " = new lx.GameObject(" + scenes[scene].objects[id].sprite + ", " + scenes[scene].objects[id].x + ", " + scenes[scene].objects[id].y + ", " + scenes[scene].objects[id].w + ", " + scenes[scene].objects[id].h + "); ";

                if (scenes[scene].objects[id].collider != string.Empty)
                    scenes[scene].objects[id].buildCode += variable + ".ApplyCollider(" + scenes[scene].objects[id].collider + "); ";

                if (scenes[scene].objects[id].visible)
                    scenes[scene].objects[id].buildCode += variable + ".Show(" + scenes[scene].objects[id].layer + "); ";
            }
            else if (scenes[scene].objects[id].type == EngineObjectType.Sprite)
            {
                scenes[scene].objects[id].buildCode = lineBreaks + "var " + scenes[scene].objects[id].Variable() + " = new lx.Sprite('" + scenes[scene].objects[id].source + "'); ";

                if (scenes[scene].objects[id].rotation > 0 && scenes[scene].objects[id].rotation < 360)
                    scenes[scene].objects[id].buildCode += variable + ".Rotation(" + (scenes[scene].objects[id].rotation * Math.PI / 180) + "); ";

                if (scenes[scene].objects[id].clipped)
                    scenes[scene].objects[id].buildCode += variable + ".Clip(" + scenes[scene].objects[id].cx + ", " + scenes[scene].objects[id].cy + ", " + scenes[scene].objects[id].cw + ", " + scenes[scene].objects[id].ch + "); ";
            }
            else if (scenes[scene].objects[id].type == EngineObjectType.Collider)
            {
                string callback = "";
                if (scenes[scene].objects[id].child != -1) callback = ", function(data) {" + scenes[scene].objects[scenes[scene].objects[id].child].code + "}";

                scenes[scene].objects[id].buildCode = lineBreaks + "var " + scenes[scene].objects[id].Variable() + " = new lx.Collider(" + scenes[scene].objects[id].x + ", " + scenes[scene].objects[id].y + ", " + scenes[scene].objects[id].w + ", " + scenes[scene].objects[id].h + ", " + scenes[scene].objects[id].isStatic.ToString().ToLower() + callback + ");";
                scenes[scene].objects[id].buildCode += variable + ".Solid(" + scenes[scene].objects[id].isSolid.ToString().ToLower() + "); ";

                if (scenes[scene].objects[id].visible)
                    scenes[scene].objects[id].buildCode += variable + ".Enable(); ";
                else
                    scenes[scene].objects[id].buildCode += variable + ".Disable(); ";
            }
            else if (scenes[scene].objects[id].type == EngineObjectType.Emitter)
            {
                scenes[scene].objects[id].buildCode = lineBreaks + "var " + scenes[scene].objects[id].Variable() + " = new lx.Emitter(" + scenes[scene].objects[id].sprite + ", " + scenes[scene].objects[id].x + ", " + scenes[scene].objects[id].y + ", " + scenes[scene].objects[id].amount + ", " + scenes[scene].objects[id].duration + "); ";
                scenes[scene].objects[id].buildCode += variable + ".Setup(" + scenes[scene].objects[id].minvx + ", " + scenes[scene].objects[id].maxvx + ", " + scenes[scene].objects[id].minvy + ", " + scenes[scene].objects[id].maxvy + ", " + scenes[scene].objects[id].minSize + ", " + scenes[scene].objects[id].maxSize + "); ";
                scenes[scene].objects[id].buildCode += variable + ".Speed(" + scenes[scene].objects[id].speed + "); ";

                if (scenes[scene].objects[id].visible)
                    scenes[scene].objects[id].buildCode += variable + ".Show(" + scenes[scene].objects[id].layer + "); ";
                else
                    scenes[scene].objects[id].buildCode += variable + ".Hide(); ";
            }
            else if (scenes[scene].objects[id].type == EngineObjectType.Tilemap)
            {
                if (scene != eSettings.currentScene)
                    scenes[scene].objects[id].buildCode = lineBreaks + Tilemapper.ToBuildCode(scenes[scene].objects[id].Variable(), scenes[scene].tilemaps[scenes[scene].objects[id].tileMap]);
                else
                    scenes[scene].objects[id].buildCode = lineBreaks + Tilemapper.ToBuildCode(scenes[scene].objects[id].Variable(), Tilemapper.maps[scenes[scene].objects[id].tileMap]);
            }
            else if (scenes[scene].objects[id].type == EngineObjectType.Script)
                scenes[scene].objects[id].buildCode = lineBreaks + scenes[scene].objects[id].code + "\n";
        }

        public static EngineObject GetEngineObject(int id)
        {
            return scenes[eSettings.currentScene].objects[id];
        }

        public static EngineObject GetEngineObjectWithVarName(string variableName)
        {
            foreach (EngineObject obj in scenes[eSettings.currentScene].objects)
                if (obj != null && obj.Variable() == variableName) return obj;

            return null;
        }

        public static EngineObject[] GetEngineObjects()
        {
            return scenes[eSettings.currentScene].objects;
        }

        public static EngineObject[] GetEngineObjectsWithType(EngineObjectType type)
        {
            List<EngineObject> results = new List<EngineObject>();

            foreach (EngineObject obj in scenes[eSettings.currentScene].objects)
                if (obj != null && obj.type == type) results.Add(obj);

            return results.ToArray();
        }

        public static void SaveEngineState()
        {
            try
            {
                Stream stream = File.Open("projects/" + Project.Name() + "/state.bin", FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(stream, new EngineState(scenes, bSettings, eSettings));
                stream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
                form.SetStatus("Exception occurred while saving engine state.", Main.StatusType.Warning);
            }
        }

        public static string BuildEngineCode(bool stacks)
        {
            try
            {
                string buildSettings = "";
                string buildScenes = "";
                string standardScene = "";

                string currentScene = "";

                //Build engine scenes
                for (int i = 0; i < scenes.Length; i++)
                    if (scenes[i] != null) {
                        if (!stacks && i == eSettings.currentScene)
                            currentScene = BuildEngineScene(i, true);

                        buildScenes += BuildEngineScene(i, false);
                    }

                //Check if build or export
                if (!stacks)
                    return buildScenes + currentScene;

                //Load build settings
                if (bSettings.hasIcon)
                    buildSettings += "document.getElementById('icon').href='" + bSettings.iconLocation + "';";

                standardScene = "lx.LoadScene(" + scenes[bSettings.standardScene].Variable() + ");";

                Project.AddGameCode(buildSettings + buildScenes + standardScene);

                form.SetStatus("'" + Project.cur + "' has been build.", Main.StatusType.Alert);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
                form.SetStatus("Could not build project code.", Main.StatusType.Alert);
            }

            return string.Empty;
        }

        public static string BuildEngineScene(int id, bool globalScope)
        {
            if (scenes[id] == null)
                return "";

            string scripts = "";
            string colliders = "";
            string emitters = "";
            string gameobjects = "";
            string sprites = "";
            string tilemaps = "";
            
            for (int i = 0; i < scenes[id].objects.Length; i++)
            {
                if (scenes[id].objects[i] == null) continue;

                GenerateEngineObjectCode(id, i);

                if (scenes[id].objects[i].type == EngineObjectType.Sprite) sprites += scenes[id].objects[i].buildCode;
                else if (scenes[id].objects[i].type == EngineObjectType.GameObject) gameobjects += scenes[id].objects[i].buildCode;
                else if (scenes[id].objects[i].type == EngineObjectType.Script && scenes[id].objects[i].parent == -1) scripts += scenes[id].objects[i].buildCode;
                else if (scenes[id].objects[i].type == EngineObjectType.Collider) colliders += scenes[id].objects[i].buildCode;
                else if (scenes[id].objects[i].type == EngineObjectType.Emitter) emitters += scenes[id].objects[i].buildCode;
                else if (scenes[id].objects[i].type == EngineObjectType.Tilemap) tilemaps += scenes[id].objects[i].buildCode;

                scenes[id].objects[i].buildCode = "";
            }

            if (!globalScope)
                return ("var " + scenes[id].Variable() + " = new lx.Scene(function() {" + sprites + tilemaps + colliders + gameobjects + emitters + scripts + "});");
            else
                return (sprites + tilemaps + colliders + gameobjects + emitters + scripts);
        }

        public static bool LoadEngineState()
        {
            scenes = new Scene[0];
            bSettings = new BuildSettings();
            eSettings = new EngineSettings();

            try
            {
                if (!File.Exists("projects/" + Project.Name() + "/state.bin"))
                    return false;

                Stream stream = File.Open("projects/" + Project.Name() + "/state.bin", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();

                EngineState temp = ((EngineState)bf.Deserialize(stream));

                if (temp != null)
                {
                    if (temp.scenes != null)
                        scenes = temp.scenes;
                    else
                    {
                        //Running pre-scene projects (running v0.3.3-alpha or earlier)
                        MessageBox.Show("This project does not support scenes. Please open this project using version 0.3.3-alpha or earlier.", "Lynx2D Engine - Incompatible");

                        return false;
                    }
                    
                    if (temp.bSettings != null)
                        bSettings = temp.bSettings;

                    if (temp.eSettings != null)
                    {
                        eSettings = temp.eSettings;

                        LoadScene(eSettings.currentScene);
                    }
                }
                else
                {
                    MessageBox.Show("Project could not be loaded, the engine state has been corrupted.", "Lynx2D Engine - Error");
                    return false;
                }

                stream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
                form.SetStatus("Exception occurred while loading engine state.", Main.StatusType.Warning);
            }

            return true;
        }

        public static void ClearEngine()
        {
            scenes = new Scene[0];
            bSettings = new BuildSettings();
            eSettings = new EngineSettings();
            Tilemapper.Clear();
        }

        public static async void ExecuteScript(string script)
        {
            JavascriptResponse response = await form.browser.EvaluateScriptAsync(script);

            /*if (response.Message != null && response.Message != string.Empty)
            {
                form.AddToConsole(response.Message);
            }*/
        }

        public static async Task<string> ExecuteScriptWithResult(string script)
        {
            JavascriptResponse response = await form.browser.EvaluateScriptAsync(script);

            /*if (response.Message != null && response.Message != string.Empty)
            {
                form.AddToConsole(response.Message);
            }*/

            return response.Result.ToString();
        }

        #region "Engine Object Interaction"
        public static void SetEngineObject(int id, EngineObject obj)
        {
            obj.id = id;
            scenes[eSettings.currentScene].objects[id] = obj;
        }

        public static void SetEngineObjectPosition(int id, int x, int y)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].x = x;
            scenes[eSettings.currentScene].objects[id].y = y;
        }

        public static void SetEngineObjectSize(int id, int x, int y)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].w = x;
            scenes[eSettings.currentScene].objects[id].h = y;
        }

        public static void SetEngineObjectLayer(int id, int layer)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].layer = layer;
        }

        public static void SetEngineObjectSource(int id, string src)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].source = src;
        }

        public static void SetEngineObjectVisible(int id, bool visible)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].visible = visible;
        }

        public static void SetEngineObjectClipped(int id, bool clipped)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].clipped = clipped;
        }

        public static void SetEngineObjectClip(int id, int cx, int cy, int cw, int ch)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].cx = cx;
            scenes[eSettings.currentScene].objects[id].cy = cy;
            scenes[eSettings.currentScene].objects[id].cw = cw;
            scenes[eSettings.currentScene].objects[id].ch = ch;
        }

        public static void SetEngineObjectRotation(int id, int angle)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].rotation = angle;
        }

        public static void SetEngineObjectScript(int id, string script)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].code = script;

            form.refreshBrowser();
        }

        public static void SetEngineObjectSprite(int id, string sprite)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            if (sprite == string.Empty)
            {
                EngineObject[] filler = GetEngineObjectsWithType(EngineObjectType.Sprite);

                if (filler.Length == 0)
                {
                    MessageBox.Show(scenes[eSettings.currentScene].objects[id].Variable() + " could not be assigned a existing sprite. Please refer to a existing sprite.", "Lynx2D Engine - Exception");
                    return;
                }

                sprite = filler[0].Variable();
            }

            scenes[eSettings.currentScene].objects[id].sprite = sprite;
        }

        public static void SetEngineObjectCollider(int id, string collider)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            if (collider == string.Empty)
            {
                EngineObject[] filler = GetEngineObjectsWithType(EngineObjectType.Collider);

                if (filler.Length == 0)
                {
                    MessageBox.Show(scenes[eSettings.currentScene].objects[id].Variable() + " could not be assigned a existing collider. Please refer to a existing collider.", "Lynx2D Engine - Exception");
                    return;
                }

                collider = filler[0].Variable();
            }

            scenes[eSettings.currentScene].objects[GetEngineObjectWithVarName(collider).id].applied = true;
            scenes[eSettings.currentScene].objects[GetEngineObjectWithVarName(collider).id].visible = true;

            scenes[eSettings.currentScene].objects[id].collider = collider;
        }

        public static void RemoveEngineObjectCollider(int id)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;
            
            scenes[eSettings.currentScene].objects[GetEngineObjectWithVarName(scenes[eSettings.currentScene].objects[id].collider).id].applied = false;

            scenes[eSettings.currentScene].objects[id].collider = string.Empty;
        }

        public static void SetEngineObjectStatic(int id, bool isStatic)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].isStatic = isStatic;
        }

        public static void SetEngineObjectSolid(int id, bool isSolid)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].isSolid = isSolid;
        }

        public static void SetEngineObjectSetup(int id, float minX, float maxX, float minY, float maxY, int minS, int maxS)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].minvx = minX;
            scenes[eSettings.currentScene].objects[id].maxvx = maxX;
            scenes[eSettings.currentScene].objects[id].minvy = minY;
            scenes[eSettings.currentScene].objects[id].maxvy = maxY;
            scenes[eSettings.currentScene].objects[id].minSize = minS;
            scenes[eSettings.currentScene].objects[id].maxSize = maxS;
        }

        public static void SetEngineObjectAmount(int id, int amount)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].amount = amount;
        }

        public static void SetEngineObjectDuration(int id, int duration)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].duration = duration;
        }

        public static void SetEngineObjectSpeed(int id, float speed)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            scenes[eSettings.currentScene].objects[id].speed = speed;
        }

        public static void RenameEngineObject(int id, string name)
        {
            if (scenes[eSettings.currentScene].objects[id] == null || name == "HAS_BEEN_CLOSED") return;

            scenes[eSettings.currentScene].objects[id].Rename(name);
            form.UpdateHierarchy();

            form.refreshBrowser();
        }
        #endregion
    }

    [Serializable]
    class EngineState
    {
        public EngineState(Scene[] scenes, BuildSettings bSettings, EngineSettings eSettings)
        {
            this.scenes = scenes;
            this.bSettings = bSettings;
            this.eSettings = eSettings;
        }

        public Scene[] scenes;
        public BuildSettings bSettings;
        public EngineSettings eSettings;
    }

    [Serializable]
    public class EngineObject
    {
        public EngineObject(int id, EngineObjectType type, string code, int child, int parent)
        {
            this.id = id;
            this.type = type;

            if (child != -1) this.child = child;
            if (parent != -1) this.parent = parent;

            if (type != EngineObjectType.Script) Engine.ExecuteScript(code);

            switch (type)
            {
                case EngineObjectType.GameObject:
                    name = "GameObject";
                    sprite = "Sprite" + this.child;
                    Engine.ExecuteScript(Variable() + ".Show(0);");

                    break;
                case EngineObjectType.Sprite:
                    name = "Sprite";
                    source = "res/lynx2d/sprite.png";

                    break;
                case EngineObjectType.Script:
                    name = "Script";
                    this.code = code;

                    break;
                case EngineObjectType.Collider:
                    name = "Collider";
                    visible = false;
                    Engine.ExecuteScript(Variable() + ".Disable();");

                    break;
                case EngineObjectType.Emitter:
                    name = "Emitter";
                    sprite = "Sprite" + this.child;
                    Engine.ExecuteScript(Variable() + ".Show(0);"); ;

                    break;
                case EngineObjectType.Tilemap:
                    name = "Tilemap";
                    tileMap = Tilemapper.AddMap(new Tilemap(10, 10));

                    break;
            }
        }

        public EngineObject(int id, EngineObjectType type, int child, int parent)
        {
            this.id = id;
            this.type = type;

            if (child != -1) this.child = child;
            if (parent != -1) this.parent = parent;

            switch (type)
            {
                case EngineObjectType.GameObject:
                    name = "GameObject";
                    sprite = "Sprite" + this.child;
                    Engine.ExecuteScript(Variable() + ".Show(0);");

                    break;
                case EngineObjectType.Sprite:
                    name = "Sprite";
                    source = "res/lynx2d/sprite.png";

                    break;
                case EngineObjectType.Script:
                    name = "Script";

                    break;
                case EngineObjectType.Collider:
                    name = "Collider";
                    visible = false;
                    Engine.ExecuteScript(Variable() + ".Disable();");

                    break;
                case EngineObjectType.Emitter:
                    name = "Emitter";
                    sprite = "Sprite" + this.child;
                    Engine.ExecuteScript(Variable() + ".Show(0);"); ;

                    break;
                case EngineObjectType.Tilemap:
                    name = "Tilemap";
                    tileMap = Tilemapper.AddMap(new Tilemap(10, 10));

                    break;
            }
        }

        public string Variable()
        {
            if (unique != string.Empty) return unique.Replace(' ', '_');

            return name + id;
        }

        public void Rename(string name)
        {
            if (name == string.Empty)
            {
                unique = string.Empty;
                return;
            }

            unique = name;
        }

        public override string ToString()
        {
            return Variable();
        }

        public EngineObject Clone()
        {
            EngineObject temp = new EngineObject(id, type, child, parent);

            temp.layer = layer;
            temp.visible = visible;

            temp.sprite = sprite;
            temp.x = x;
            temp.y = y;
            temp.w = w;
            temp.h = h;

            temp.minvx = minvx;
            temp.maxvx = maxvx;
            temp.minvy = minvy;
            temp.maxvy = maxvy;
            temp.minSize = minSize;
            temp.maxSize = maxSize;
            temp.speed = speed;
            temp.amount = amount;
            temp.duration = duration;

            temp.source = source;
            temp.cx = cx;
            temp.cy = cy;
            temp.cw = cw;
            temp.ch = ch;
            temp.clipped = clipped;
            temp.rotation = rotation;

            temp.unique = unique;

            return temp;
        }

        public int id;

        public EngineObjectType type;
        public string name;
        public string unique = string.Empty;
        public string code;
        public string buildCode;
        public int child = -1;
        public int parent = -1;

        public int x = 0;
        public int y = 0;
        public int w = 64;
        public int h = 64;

        public float minvx = -2f;
        public float maxvx = 2f;
        public float minvy = -2f;
        public float maxvy = 2f;
        public int minSize = 6;
        public int maxSize = 12;
        public float speed = 8;
        public int duration = 30;
        public int amount = 12;

        public int cx = 0;
        public int cy = 0;
        public int cw = 0;
        public int ch = 0;
        public bool clipped = false;

        public int rotation = 0;
        public string source;
        public string sprite;
        public string collider = string.Empty;

        public int layer = 0;
        public bool visible = true;
        public bool isStatic = false;
        public bool isSolid = true;
        public bool applied = false;

        public int tileMap = -1;
    }

    [Serializable]
    public class BuildSettings
    {
        public bool hasIcon = false;
        public string iconLocation = string.Empty;
        public int standardScene = 0;

        public int lineBreaks = 0;
        public bool obfuscates = false;
    }

    [Serializable]
    public class EngineSettings
    {
        public bool imageSmoothing = true;
        public bool camera = true;
        public bool debug = false;
        public bool drawColliders = false;

        public bool grid = true;
        public int gridLayer = 0;
        public int gridSize = 64;
        public int gridStrokeSize = 2;
        public int gridWidth = 16;
        public int gridHeight = 16;
        public int gridOpacity = 25;
        public int gridOffX = -8;
        public int gridOffY = -8;
        public string gridColor = "white";

        public int currentScene = -1;
    }

    [Serializable]
    public class Scene
    {
        public Scene(int id)
        {
            this.id = id;
            objects = new EngineObject[0];
            tilemaps = new Tilemap[0];
            hierarchy = new Hierarchy();
            name = "Scene";
        }

        public void Rename(string name)
        {
            if (string.IsNullOrEmpty(name) || name == "")
                unique = string.Empty;

            unique = name;
        }

        public string Variable()
        {
            if (unique == string.Empty)
                return name + id;

            return unique;
        }

        public override string ToString()
        {
            return Variable();
        }

        public EngineObject[] objects;
        public Hierarchy hierarchy;
        public Tilemap[] tilemaps;
        public string name;
        public string unique = string.Empty;
        public int id;
    }

    public enum EngineObjectType
    {
        GameObject = 0,
        Sprite,
        Script,
        Collider,
        Emitter,
        Tilemap
    }
}
