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

        private static EngineObject[] objects = new EngineObject[0];
        public static BuildSettings bSettings = new BuildSettings();
        public static EngineSettings eSettings = new EngineSettings();

        public static int AddEngineObject(EngineObjectType type, string code, int child, int parent)
        {
            for (int i = 0; i < objects.Length + 1; i++)
            {
                if (i == objects.Length) Array.Resize(ref objects, objects.Length + 1);

                if (objects[i] == null || i == objects.Length)
                {
                    objects[i] = new EngineObject(i, type, code, child, parent);

                    return i;
                }
            }

            return -1;
        }

        public static void RemoveEngineObject(int id, bool refreshes)
        {
            if (objects[id] == null) return;

            if (objects[id].child != -1) RemoveEngineObject(objects[id].child, false);
            if (objects[id].parent != -1) objects[objects[id].parent].child = -1;

            objects[id] = null;

            if (refreshes)
            {
                Project.Build(true);

                form.UpdateHierarchy();
            }
        }

        public static int[] GetEmptyEnginePositions()
        {
            int[] temp = new int[0];

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] == null)
                {
                    Array.Resize(ref temp, temp.Length + 1);
                    temp[temp.Length - 1] = i;
                }
            }

            Array.Resize(ref temp, temp.Length + 1);
            temp[temp.Length - 1] = objects.Length;

            return temp;
        }

        private static void GenerateEngineObjectCode(int id)
        {
            string lineBreaks = new string('\n', bSettings.lineBreaks);
            string variable = lineBreaks + objects[id].Variable();

            form.SetStatus("Building '" + objects[id].Variable() + "'", Main.StatusType.Alert);

            if (objects[id].type == EngineObjectType.GameObject)
            {
                objects[id].buildCode = lineBreaks + "var " + objects[id].Variable() + " = new lx.GameObject(" + objects[id].sprite + ", " + objects[id].x + ", " + objects[id].y + ", " + objects[id].w + ", " + objects[id].h + ");";

                if (objects[id].collider != string.Empty)
                    objects[id].buildCode += variable + ".ApplyCollider(" + objects[id].collider + ");";
                
                if (objects[id].visible)
                    objects[id].buildCode += variable + ".Show(" + objects[id].layer + ");";
            }
            else if (objects[id].type == EngineObjectType.Sprite)
            {
                objects[id].buildCode = lineBreaks + "var " + objects[id].Variable() + " = new lx.Sprite('" + objects[id].source + "');";

                if (objects[id].rotation > 0 && objects[id].rotation < 360)
                    objects[id].buildCode += variable + ".Rotation(" + (objects[id].rotation * Math.PI / 180) + ");";

                if (objects[id].clipped)
                    objects[id].buildCode += variable + ".Clip(" + objects[id].cx + ", " + objects[id].cy + ", " + objects[id].cw + ", " + objects[id].ch + ");";
            }
            else if (objects[id].type == EngineObjectType.Collider)
            {
                string callback = "";
                if (objects[id].child != -1) callback = ", function(data) {" + objects[objects[id].child].code + "}";

                objects[id].buildCode = lineBreaks + "var " + objects[id].Variable() + " = new lx.Collider(" + objects[id].x + ", " + objects[id].y + ", " + objects[id].w + ", " + objects[id].h + ", " + objects[id].isStatic.ToString().ToLower() + callback + ");";
                objects[id].buildCode += variable + ".Solid(" + objects[id].isSolid.ToString().ToLower() + ");";

                if (objects[id].visible)
                    objects[id].buildCode += variable + ".Enable();";
                else
                    objects[id].buildCode += variable + ".Disable();";
            }
            else if (objects[id].type == EngineObjectType.Script) objects[id].buildCode = lineBreaks + objects[id].code;
        }

        public static EngineObject GetEngineObject(int id)
        {
            return objects[id];
        }

        public static EngineObject GetEngineObjectWithVarName(string variableName)
        {
            foreach (EngineObject obj in objects)
                if (obj != null && obj.Variable() == variableName) return obj;

            return null;
        }

        public static EngineObject[] GetEngineObjects()
        {
            return objects;
        }

        public static EngineObject[] GetEngineObjectsWithType(EngineObjectType type)
        {
            List<EngineObject> results = new List<EngineObject>();

            foreach (EngineObject obj in objects)
                if (obj != null && obj.type == type) results.Add(obj);

            return results.ToArray();
        }

        public static void SaveEngineState()
        {
            try
            {
                Stream stream = File.Open("projects/" + Project.Name() + "/state.bin", FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(stream, new EngineState(objects, bSettings, eSettings));
                stream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
                form.SetStatus("Exception occurred while saving engine state.", Main.StatusType.Warning);
            }
        }

        public static void BuildEngineCode()
        {
            try
            {
                string buildSettings = "";
                string scripts = "";
                string colliders = "";
                string gameobjects = "";
                string sprites = "";

                //Build engine objects
                for (int i = 0; i < objects.Length; i++)
                {
                    if (objects[i] == null) continue;

                    GenerateEngineObjectCode(i);

                    if (objects[i].type == EngineObjectType.Sprite) sprites += objects[i].buildCode;
                    else if (objects[i].type == EngineObjectType.GameObject) gameobjects += objects[i].buildCode;
                    else if (objects[i].type == EngineObjectType.Script && objects[i].parent == -1) scripts += objects[i].buildCode;
                    else if (objects[i].type == EngineObjectType.Collider) colliders += objects[i].buildCode;

                    objects[i].buildCode = "";
                }

                //Load build settings
                if (bSettings.hasIcon)
                    buildSettings += "document.getElementById('icon').href='" + bSettings.iconLocation + "';";

                Project.AddGameCode(buildSettings + sprites + colliders + gameobjects + scripts);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
                form.SetStatus("Could not build project.", Main.StatusType.Alert);
            }
        }

        public static void LoadEngineState()
        {
            objects = new EngineObject[0];
            bSettings = new BuildSettings();
            eSettings = new EngineSettings();

            try
            {
                if (!File.Exists("projects/" + Project.Name() + "/state.bin")) return;

                Stream stream = File.Open("projects/" + Project.Name() + "/state.bin", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();

                EngineState temp = ((EngineState)bf.Deserialize(stream));

                if (temp.objects != null) objects = temp.objects;
                if (temp.bSettings != null) bSettings = temp.bSettings;
                if (temp.eSettings != null) eSettings = temp.eSettings;

                stream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
                form.SetStatus("Exception occurred while loading engine state.", Main.StatusType.Warning);
            }
        }

        public static void ClearEngine()
        {
            objects = new EngineObject[0];
            bSettings = new BuildSettings();
            eSettings = new EngineSettings();
        }

        public static async void ExecuteScript(string script)
        {
            JavascriptResponse response = await form.browser.EvaluateScriptAsync(script);

            if (response.Message != null && response.Message != string.Empty)
            {
                form.AddToConsole(response.Message);
            }
        }

        public static async Task<string> ExecuteScriptWithResult(string script)
        {
            JavascriptResponse response = await form.browser.EvaluateScriptAsync(script);

            if (response.Message != null && response.Message != string.Empty)
            {
                form.AddToConsole(response.Message);
            }

            return response.Result.ToString();
        }

        #region "Engine Object Interaction"
        public static void SetEngineObject(int id, EngineObject obj)
        {
            obj.id = id;
            objects[id] = obj;
        }

        public static void SetEngineObjectPosition(int id, int x, int y)
        {
            if (objects[id] == null) return;

            objects[id].x = x;
            objects[id].y = y;
        }

        public static void SetEngineObjectSize(int id, int x, int y)
        {
            if (objects[id] == null) return;

            objects[id].w = x;
            objects[id].h = y;
        }

        public static void SetEngineObjectLayer(int id, int layer)
        {
            if (objects[id] == null) return;

            objects[id].layer = layer;
        }

        public static void SetEngineObjectSource(int id, string src)
        {
            if (objects[id] == null) return;

            objects[id].source = src;
        }

        public static void SetEngineObjectVisible(int id, bool visible)
        {
            if (objects[id] == null) return;

            objects[id].visible = visible;
        }

        public static void SetEngineObjectClipped(int id, bool clipped)
        {
            if (objects[id] == null) return;

            objects[id].clipped = clipped;
        }

        public static void SetEngineObjectClip(int id, int cx, int cy, int cw, int ch)
        {
            if (objects[id] == null) return;

            objects[id].cx = cx;
            objects[id].cy = cy;
            objects[id].cw = cw;
            objects[id].ch = ch;
        }

        public static void SetEngineObjectRotation(int id, int angle)
        {
            if (objects[id] == null) return;

            objects[id].rotation = angle;
        }

        public static void SetEngineObjectScript(int id, string script)
        {
            if (objects[id] == null) return;

            objects[id].code = script;

            Project.Build(true);

            form.refreshBrowser();
        }

        public static void SetEngineObjectSprite(int id, string sprite)
        {
            if (objects[id] == null) return;

            if (sprite == string.Empty)
            {
                EngineObject[] filler = GetEngineObjectsWithType(EngineObjectType.Sprite);

                if (filler.Length == 0)
                {
                    MessageBox.Show(objects[id].Variable() + " could not be assigned a existing sprite. Please refer to a existing sprite.", "Lynx2D Engine - Exception");
                    return;
                }

                sprite = filler[0].Variable();
            }

            objects[id].sprite = sprite;
        }

        public static void SetEngineObjectCollider(int id, string collider)
        {
            if (objects[id] == null) return;

            if (collider == string.Empty)
            {
                EngineObject[] filler = GetEngineObjectsWithType(EngineObjectType.Collider);

                if (filler.Length == 0)
                {
                    MessageBox.Show(objects[id].Variable() + " could not be assigned a existing collider. Please refer to a existing collider.", "Lynx2D Engine - Exception");
                    return;
                }

                collider = filler[0].Variable();
            }

            objects[GetEngineObjectWithVarName(collider).id].applied = true;
            objects[GetEngineObjectWithVarName(collider).id].visible = true;

            objects[id].collider = collider;
        }

        public static void RemoveEngineObjectCollider(int id)
        {
            if (objects[id] == null) return;
            
            objects[GetEngineObjectWithVarName(objects[id].collider).id].applied = false;

            objects[id].collider = string.Empty;
        }

        public static void SetEngineObjectStatic(int id, bool isStatic)
        {
            if (objects[id] == null) return;

            objects[id].isStatic = isStatic;
        }

        public static void SetEngineObjectSolid(int id, bool isSolid)
        {
            if (objects[id] == null) return;

            objects[id].isSolid = isSolid;
        }

        public static void RenameEngineObject(int id, string name)
        {
            if (objects[id] == null) return;

            objects[id].Rename(name);
            form.UpdateHierarchy();

            Project.Build(true);
        }
        #endregion
    }

    [Serializable]
    class EngineState
    {
        public EngineState (EngineObject[] objects, BuildSettings bSettings, EngineSettings eSettings)
        {
            this.objects = objects;
            this.bSettings = bSettings;
            this.eSettings = eSettings;
        }

        public EngineObject[] objects;
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
    }

    [Serializable]
    public class BuildSettings
    {
        public bool hasIcon;
        public string iconLocation;

        public int lineBreaks = 0;
        public bool obfuscates;
    }

    [Serializable]
    public class EngineSettings
    {
        public bool imageSmoothing = true;
        public bool camera = true;
        public bool debug = false;
        public bool drawColliders = false;

        public bool grid = true;
        public int gridSize = 64;
        public int gridStrokeSize = 2;
        public int gridWidth = 16;
        public int gridHeight = 16;
        public int gridOpacity = 25;
        public int gridOffX = -8;
        public int gridOffY = -8;
        public string gridColor = "white";
    }

    public enum EngineObjectType
    {
        GameObject = 0,
        Sprite,
        Script,
        Collider
    }
}
