﻿using CefSharp;
using Lynx2DEngine.classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.Text;
using System.Linq;

namespace Lynx2DEngine
{
    public static class Engine
    {
        public static Main form;

        public static Scene[] scenes = new Scene[0];
        public static BuildSettings bSettings = new BuildSettings();
        public static EngineSettings eSettings = new EngineSettings();
        public static EnginePreferences ePreferences = new EnginePreferences();

        #region "Engine Preferences"
        public static bool EvaluateEnginePreferences()
        {
            try
            {
                if (File.Exists("preferences.bin"))
                {
                    Stream stream = File.Open("preferences.bin", FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();

                    ePreferences = ((EnginePreferences)bf.Deserialize(stream));
                    stream.Close();

                    return true;
                } else
                {
                    SaveEnginePreferences(false);

                    return true;
                }
            }   
            catch (Exception exc)
            {
                Feed.GiveException("Preferences Load", exc);
            }

            return false;
        }

        public static void SaveEnginePreferences(bool exists)
        {
            try
            {
                Stream stream = File.Open("preferences.bin", FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();

                EnginePreferences temp = new EnginePreferences();
                if (exists) temp = ePreferences;

                bf.Serialize(stream, temp);

                stream.Close();
            }
            catch (Exception exc)
            {
                Feed.GiveException("Preferences Save", exc);
            }
        }
        #endregion

        #region "Engine Scenes"
        public static void LoadScene(int id)
        {
            if (id == -1 || eSettings.currentScene == -1)
                CreateScene(false);
            else
            {
                eSettings.currentScene = id;
                Tilemapper.LoadFromScene(eSettings.currentScene);

                form.RefreshBrowser();
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

        public static void RemoveScene(int id)
        {
            if (!Input.YesNo("Are you sure you want to delete the scene '" + scenes[id].Variable() + "'?", "Lynx2D Engine - Question"))
                return;

            if (id == bSettings.standardScene)
            {
                MessageBox.Show("Could not remove '" + scenes[id].Variable() + "', as this is the standard scene of the project.", "Lynx2D Engine - Message");
                return;
            }

            scenes[id] = null;
            
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

            form.RefreshBrowser();
        }
        #endregion

        #region "Engine Object Management"
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

        public static int AddExistingEngineObject(EngineObject eo)
        {
            for (int i = 0; i < scenes[eSettings.currentScene].objects.Length + 1; i++)
            {
                if (i == scenes[eSettings.currentScene].objects.Length) Array.Resize(ref scenes[eSettings.currentScene].objects, scenes[eSettings.currentScene].objects.Length + 1);

                if (scenes[eSettings.currentScene].objects[i] == null || i == scenes[eSettings.currentScene].objects.Length)
                {
                    //Add existing engine object
                    scenes[eSettings.currentScene].objects[i] = eo;

                    return i;
                }
            }

            return -1;
        }

        public static Point AddExistingEngineObjectWithChild(EngineObject eo, EngineObject eoChild)
        {
            int parent = -1, child = -1;

            for (int i = 0; i < scenes[eSettings.currentScene].objects.Length + 2; i++)
            {
                if (i == scenes[eSettings.currentScene].objects.Length) Array.Resize(ref scenes[eSettings.currentScene].objects, scenes[eSettings.currentScene].objects.Length + 1);

                if (scenes[eSettings.currentScene].objects[i] == null || i == scenes[eSettings.currentScene].objects.Length)
                {
                    if (child == -1)
                    {
                        scenes[eSettings.currentScene].objects[i] = eoChild;

                        child = i;
                    }
                    else
                    {
                        scenes[eSettings.currentScene].objects[i] = eo;

                        parent = i;

                        break;
                    }
                }
            }

            return new Point(parent, child);
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

        public static void RemoveEngineObject(int id, bool refreshes, bool updates)
        {
            bool childRemoved = false;

            if (scenes[eSettings.currentScene].objects[id] == null) return;

            if (scenes[eSettings.currentScene].objects[id].child != -1)
            {
                RemoveEngineObject(scenes[eSettings.currentScene].objects[id].child, false, true);

                childRemoved = true;
            }
            if (scenes[eSettings.currentScene].objects[id].parent != -1)
            {
                scenes[eSettings.currentScene].objects[scenes[eSettings.currentScene].objects[id].parent].child = -1;

                if (scenes[eSettings.currentScene].objects[id].type == EngineObjectType.Sprite)
                    scenes[eSettings.currentScene].objects[scenes[eSettings.currentScene].objects[id].parent].sprite = "undefined";
            }

            if (scenes[eSettings.currentScene].objects[id].type == EngineObjectType.Tilemap)
                Tilemapper.RemoveMap(scenes[eSettings.currentScene].objects[id].tileMap);

            scenes[eSettings.currentScene].objects[id] = null;
            scenes[eSettings.currentScene].hierarchy.RemoveItem(id, true);

            if (refreshes)
                form.RefreshBrowser();

            if (updates || childRemoved)
                form.UpdateHierarchy();
        }
        #endregion

        #region "Engine Object Inspection"
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

        public static EngineObject GetEngineObjectWithVarNameInScene(int scene, string variableName)
        {
            foreach (EngineObject obj in scenes[scene].objects)
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
        #endregion

        #region "Engine File Management"
        public static bool LoadEngineState()
        {
            ClearEngine();

            try
            {
                if (!File.Exists("projects/" + Project.Name() + "/state.bin"))
                {
                    MessageBox.Show("The project savestate could not be found.", "Lynx2D Engine - Message");
                    return false;
                }

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
                    {
                        bSettings = temp.bSettings;

                        if (bSettings.initialFramerate == 0)
                        {
                            //Initial setup of graphics build settings (running v0.4.5-beta or earlier)
                            bSettings.initialFramerate = 60;
                            bSettings.imageSmoothing = true;
                        }
                    }

                    if (temp.eSettings != null)
                    {
                        eSettings = temp.eSettings;

                        LoadScene(eSettings.currentScene);
                    }
                }
                else
                {
                    MessageBox.Show("Project could not be loaded, the engine savestate has been corrupted. Try restoring a backup manually.", "Lynx2D Engine - Message");
                    return false;
                }

                stream.Close();
            }
            catch (Exception exc)
            {
                Feed.GiveException("State Load", exc);

                return false;
            }

            return true;
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
            catch (Exception exc)
            {
                Feed.GiveException("State Save", exc);
            }
        }

        public static void SaveEngineObject(int scene, int engineId)
        {
            List<EngineObject> eol = new List<EngineObject>() {
                scenes[scene].objects[engineId].Clone()
            };

            string tempEO = "projects/" + Project.Name() + "/item.eo",
                   tempTM = "projects/" + Project.Name() + "/item.tm";

            try
            {
                //Save Dialog
                SaveFileDialog sfd = new SaveFileDialog();

                if (eol[0].type == EngineObjectType.Script)
                    sfd.Filter = "JavaScript (*.js)|*.js|Lynx2D Item (*.lx2d)|*.lx2d";
                else 
                    sfd.Filter = "Lynx2D Item (*.lx2d)|*.lx2d";

                sfd.Title = "Export '" + eol[0].Variable() + "' as a Lynx2D item";
                sfd.ShowDialog();

                if (sfd.FileName == "")
                    return;
                else if (File.Exists(sfd.FileName))
                    File.Delete(sfd.FileName);

                //If script and needs to be javascript, export as javascript
                if (eol[0].type == EngineObjectType.Script)
                {
                    using (Stream stream = File.Open(sfd.FileName, FileMode.Create))
                    {
                        StreamWriter sw = new StreamWriter(stream);
                        sw.Write(eol[0].code);

                        sw.Close();
                        stream.Close();

                        form.SetStatus("'" + eol[0].Variable() + "' has been exported.", Main.StatusType.Message);
                    }

                    return;
                }

                //If tilemap, create tilemap object
                if (eol[0].type == EngineObjectType.Tilemap)
                    using (Stream stream = File.Open(tempTM, FileMode.Create))
                    {
                        Tilemap eoTM = scenes[scene].tilemaps[eol[0].tileMap];
                        BinaryFormatter bf = new BinaryFormatter();

                        bf.Serialize(stream, eoTM);

                        //Add used sprites
                        foreach (string sprite in eoTM.GetUsedSprites())
                            eol.Add(GetEngineObjectWithVarNameInScene(scene, sprite).Clone());

                        stream.Close();
                    }

                //If child available, add that child
                if (eol[0].child != -1)
                {
                    eol.Add(scenes[scene].objects[eol[0].child].Clone());

                    eol[0].child = 1;
                    eol[1].parent = 0;
                }

                //Add Engine Objects
                using (Stream stream = File.Open(tempEO, FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    bf.Serialize(stream, eol.ToArray());

                    stream.Close();
                }

                //Archive
                using (ZipArchive archive = ZipFile.Open(sfd.FileName, ZipArchiveMode.Create))
                {
                    //Add Engine Object
                    archive.CreateEntryFromFile(tempEO, Path.GetFileName(tempEO));
                    File.Delete(tempEO);

                    //If tilemap, add that to the archive
                    if (eol[0].type == EngineObjectType.Tilemap) {
                        archive.CreateEntryFromFile(tempTM, Path.GetFileName(tempTM));
                        File.Delete(tempTM);
                    }

                    //If source, add that to the archive
                    foreach (EngineObject eo in eol)
                        if (eo != null && eo.source != null && eo.source != string.Empty && eo.source.Length > 0)
                            archive.CreateEntryFromFile(Manager.Root() + "projects/" + Project.Name() + Path.DirectorySeparatorChar + eo.source, Path.GetFileName(Manager.Root() + "projects/" + Project.Name() + Path.DirectorySeparatorChar + eo.source));

                    form.SetStatus("'" + eol[0].Variable() + "' has been exported.", Main.StatusType.Message);
                }
            }
            catch (Exception exc)
            {
                if (File.Exists(tempEO))
                    File.Delete(tempEO);
                if (File.Exists(tempTM))
                    File.Delete(tempTM);

                Feed.GiveException("Item Save", exc);
            }
        }

        public static void ImportEngineObject(string source)
        {
            string extractDest = "projects/" + Project.Name() + "/temp";

            try
            {
                if (!File.Exists(source))
                    return;

                //Extract data
                using (ZipArchive archive = ZipFile.Open(source, ZipArchiveMode.Read))
                {
                    Manager.CheckDirectory(extractDest, true);
                    archive.ExtractToDirectory(extractDest);
                }

                //Get Engine Objects
                EngineObject[] eoa;

                using (Stream stream = new FileStream(extractDest + "/item.eo", FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    eoa = (EngineObject[])bf.Deserialize(stream);

                    stream.Close();
                }

                //Copy files
                string[] genericFiles = Manager.GetFilesFrom(extractDest, new string[] { "png", "jpg", "bmp", "jpeg", "mp3", "wav", "ogg" }, false);

                foreach (string file in genericFiles)
                {
                    string ext = file.Substring(file.IndexOf('.'), file.Length - file.IndexOf('.')).ToLower(),
                           name = file.Substring(file.LastIndexOf('\\') + 1, file.Length - ext.Length - file.LastIndexOf('\\') - 1);

                    Manager.CopyFile(file, "projects/" + Project.Name() + "/res/" + name + ext);
                }

                //Create all Engine Objects
                foreach (EngineObject eo in eoa)
                {
                    if (eo == null || eo.parent != -1 || GetEngineObjectWithVarName(eo.Variable()) != null)
                        continue;

                    Point r = new Point(-1, -1);
                    int result = -1;

                    if (eo.child != -1)
                    {
                        r = AddExistingEngineObjectWithChild(eo, eoa[eo.child]);

                        scenes[eSettings.currentScene].objects[r.X].child = r.Y;
                        scenes[eSettings.currentScene].objects[r.Y].parent = r.X;

                        scenes[eSettings.currentScene].objects[r.X].id = r.X;
                        scenes[eSettings.currentScene].objects[r.Y].id = r.Y;

                        result = r.X;
                    }
                    else
                    {
                        result = AddExistingEngineObject(eo);

                        scenes[eSettings.currentScene].objects[result].id = result;
                    }

                    //If this is a tilemap, import this tilemap
                    if (eo.type == EngineObjectType.Tilemap && File.Exists(extractDest + "/item.tm"))
                    {
                        using (Stream stream = new FileStream(extractDest + "/item.tm", FileMode.Open))
                        {
                            BinaryFormatter bf = new BinaryFormatter();

                            scenes[eSettings.currentScene].objects[result].tileMap = Tilemapper.AddMap((Tilemap)bf.Deserialize(stream));

                            stream.Close();
                        }
                    }
                    
                    scenes[eSettings.currentScene].hierarchy.AddItem(result);
                }
            }
            catch (Exception exc)
            {
                Feed.GiveException("Item Import", exc);
            }

            if (Directory.Exists(extractDest))
                Directory.Delete(extractDest, true);
        }

        public static void CreateSpecificFileWatcher(string path, string fileName)
        {
            FileSystemWatcher watcher = new FileSystemWatcher()
            {
                Path = path,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = fileName + ".js"
            };

            watcher.Changed += (object o, FileSystemEventArgs e) =>
            {
                string name = e.Name.Substring(0, e.Name.IndexOf('.'));

                try
                {
                    EngineObject eo = GetEngineObjectWithVarName(name);

                    using (FileStream fs = File.Open(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                    {
                        eo.code = sr.ReadToEnd();

                        form.RefreshBrowser();
                    }
                }
                catch (Exception exc)
                {
                    Feed.GiveException("External Change", exc);
                }
            };

            watcher.Deleted += (object o, FileSystemEventArgs e) =>
            {
                watcher.Dispose();
            };

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }
        #endregion

        #region "Engine JavaScript IO"
        private static void GenerateEngineObjectCode(int scene, int id, bool globalScope)
        {
            string lineBreaks = new string('\n', bSettings.lineBreaks);
            if (bSettings.obfuscates)
                lineBreaks = "";

            string variable = lineBreaks + scenes[scene].objects[id].Variable();

            form.SetStatus("Building '" + scenes[scene].objects[id].Variable() + "'", Main.StatusType.Message);

            switch (scenes[scene].objects[id].type)
            {
                case EngineObjectType.GameObject:
                    scenes[scene].objects[id].buildCode = lineBreaks + "let " + scenes[scene].objects[id].Variable() + " = new lx.GameObject(" + scenes[scene].objects[id].sprite + ", " + scenes[scene].objects[id].x + ", " + scenes[scene].objects[id].y + ", " + scenes[scene].objects[id].w + ", " + scenes[scene].objects[id].h + "); ";

                    if (scenes[scene].objects[id].collider != string.Empty)
                        scenes[scene].objects[id].buildCode += variable + ".ApplyCollider(" + scenes[scene].objects[id].collider + "); ";

                    if (scenes[scene].objects[id].visible)
                        scenes[scene].objects[id].buildCode += variable + ".Show(" + scenes[scene].objects[id].layer + "); ";
                    break;

                case EngineObjectType.Sprite:
                    scenes[scene].objects[id].buildCode = lineBreaks + "let " + scenes[scene].objects[id].Variable() + " = new lx.Sprite('" + scenes[scene].objects[id].source + "'" + (!globalScope ? ", ON_SPRITE_LOAD" : "") + "); ";

                    if (scenes[scene].objects[id].rotation > 0 && scenes[scene].objects[id].rotation < 360)
                        scenes[scene].objects[id].buildCode += variable + ".Rotation(" + (scenes[scene].objects[id].rotation * Math.PI / 180) + "); ";

                    if (scenes[scene].objects[id].clipped)
                        scenes[scene].objects[id].buildCode += variable + ".Clip(" + scenes[scene].objects[id].cx + ", " + scenes[scene].objects[id].cy + ", " + scenes[scene].objects[id].cw + ", " + scenes[scene].objects[id].ch + "); ";
                    break;

                case EngineObjectType.BoxCollider:
                    string callback = "";
                    if (scenes[scene].objects[id].child != -1) callback = ", function(data) {" + scenes[scene].objects[scenes[scene].objects[id].child].code + "}";

                    scenes[scene].objects[id].buildCode = lineBreaks + "let " + scenes[scene].objects[id].Variable() + " = new lx.BoxCollider(" + scenes[scene].objects[id].x + ", " + scenes[scene].objects[id].y + ", " + scenes[scene].objects[id].w + ", " + scenes[scene].objects[id].h + ", " + scenes[scene].objects[id].isStatic.ToString().ToLower() + callback + ");";
                    scenes[scene].objects[id].buildCode += variable + ".Solid(" + scenes[scene].objects[id].isSolid.ToString().ToLower() + "); ";

                    if (scenes[scene].objects[id].visible)
                        scenes[scene].objects[id].buildCode += variable + ".Enable(); ";
                    else
                        scenes[scene].objects[id].buildCode += variable + ".Disable(); ";
                    break;

                case EngineObjectType.Emitter:
                    scenes[scene].objects[id].buildCode = lineBreaks + "let " + scenes[scene].objects[id].Variable() + " = new lx.Emitter(" + scenes[scene].objects[id].sprite + ", " + scenes[scene].objects[id].x + ", " + scenes[scene].objects[id].y + ", " + scenes[scene].objects[id].amount + ", " + scenes[scene].objects[id].duration + "); ";
                    scenes[scene].objects[id].buildCode += variable + ".Setup(" + scenes[scene].objects[id].minvx + ", " + scenes[scene].objects[id].maxvx + ", " + scenes[scene].objects[id].minvy + ", " + scenes[scene].objects[id].maxvy + ", " + scenes[scene].objects[id].minSize + ", " + scenes[scene].objects[id].maxSize + "); ";
                    scenes[scene].objects[id].buildCode += variable + ".Speed(" + scenes[scene].objects[id].speed + "); ";

                    if (scenes[scene].objects[id].visible)
                        scenes[scene].objects[id].buildCode += variable + ".Show(" + scenes[scene].objects[id].layer + "); ";
                    else
                        scenes[scene].objects[id].buildCode += variable + ".Hide(); ";
                    break;

                case EngineObjectType.Tilemap:
                    if (scene != eSettings.currentScene)
                        scenes[scene].objects[id].buildCode = 
                            lineBreaks + Tilemapper.ToBuildCode(scenes[scene].objects[id].Variable(), scenes[scene].tilemaps[scenes[scene].objects[id].tileMap]);
                    else
                        scenes[scene].objects[id].buildCode = 
                            lineBreaks + Tilemapper.ToBuildCode(scenes[scene].objects[id].Variable(), Tilemapper.maps[scenes[scene].objects[id].tileMap]);
                    break;

                case EngineObjectType.Sound:
                    scenes[scene].objects[id].buildCode = lineBreaks + "let " + scenes[scene].objects[id].Variable() + " = new lx.Sound('" + scenes[scene].objects[id].source + "', " + scenes[scene].objects[id].layer + "); ";
                    scenes[scene].objects[id].buildCode += variable + ".Position(" + scenes[scene].objects[id].x + ", " + scenes[scene].objects[id].y + ");";
                    break;

                case EngineObjectType.Script:
                    scenes[scene].objects[id].buildCode = lineBreaks + scenes[scene].objects[id].code + "\n";
                    break;
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

                        if (!stacks)
                            buildScenes += scenes[i].Variable() + ".ENGINE_ID=" + i + ";";
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
            catch (Exception exc)
            {
                Feed.GiveException("Project Build", exc);
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
            string sounds = "";

            int amountOfSprites = 0;

            //Handle all scene objects without scripts

            List<EngineObject> scriptList = new List<EngineObject>();

            for (int i = 0; i < scenes[id].objects.Length; i++)
            {
                if (scenes[id].objects[i] == null) 
                    continue;

                EngineObject eo = scenes[id].objects[i];

                //Check if script, if so
                //add to list and skip

                if (eo.type == EngineObjectType.Script)
                {
                    scriptList.Add(eo);
                    continue;
                }

                //Generate the engine object code

                GenerateEngineObjectCode(id, i, globalScope);

                //Add to correct type of string chunk

                switch (eo.type)
                {
                    case EngineObjectType.Sprite:
                        sprites += eo.buildCode;
                        amountOfSprites++;
                        break;
                    case EngineObjectType.GameObject:
                        gameobjects += eo.buildCode;
                        break;
                    case EngineObjectType.BoxCollider:
                        colliders += eo.buildCode;
                        break;
                    case EngineObjectType.Emitter:
                        emitters += eo.buildCode;
                        break;
                    case EngineObjectType.Tilemap:
                        if (!globalScope)
                            tilemaps += eo.buildCode;
                        break;
                    case EngineObjectType.Sound:
                        sounds += eo.buildCode;
                        break;
                };

                scenes[id].objects[i].buildCode = "";
            }

            //Handle all scripts based on the
            //specified build order

            EngineObject[] sorted = scriptList.OrderBy(s => s.buildOrder).ToArray();
            for (int i = 0; i < sorted.Length; i++)
            {
                int eoId = sorted[i].id;

                GenerateEngineObjectCode(id, eoId, globalScope);

                if (scriptList[i].parent == -1)
                    scripts += sorted[i].buildCode;

                scenes[id].objects[eoId].buildCode = "";
            }

            //Add sprite initializition check

            string spritesInit = "let AMOUNT_OF_SPRITES = " + amountOfSprites + "; let CUR_SPRITES = 0;\n",
                   spritesInv = "function ON_SPRITE_LOAD() { " +
                                    "CUR_SPRITES++;" +
                                    "if (CUR_SPRITES === AMOUNT_OF_SPRITES) {";

            //If there are no sprite objects available,
            //remove the initialization check as it is
            //unnecessary

            if (amountOfSprites == 0)
                spritesInit = spritesInv = string.Empty;

            //Compile code based on if in global scope,
            //or not

            if (!globalScope)
                return "const " + scenes[id].Variable() + " = new lx.Scene(function() {\n" +
                    spritesInit +
                    spritesInv +
                (spritesInit == string.Empty ? sprites : "") +
                    tilemaps +
                    colliders +
                    gameobjects +
                    sounds +
                    emitters +
                    scripts +
                 (spritesInit != string.Empty ? "}};" : "") +
                 (spritesInit != string.Empty ? sprites : "") +
                "});";
            else
                return
                    sprites +
                    tilemaps +
                    colliders +
                    gameobjects +
                    sounds +
                    emitters +
                    scripts;
        }

        public static async void ExecuteScript(string script)
        {
            await form.browser.EvaluateScriptAsync(script);
        }

        public static async Task<string> ExecuteScriptWithResult(string script)
        {
            JavascriptResponse response = await form.browser.EvaluateScriptAsync(script);

            return response.Result.ToString();
        }

        public static void HandleConsoleInteraction(string msg)
        {
            if (msg.Contains("LOAD_SCENE"))
            {
                int.TryParse(msg.Substring(11, 1), out int sceneEngineID);

                if (sceneEngineID != eSettings.currentScene) form.canViewObjects = false;
                else form.canViewObjects = true;

                form.UpdateHierarchy();
            }
        }
        #endregion

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
        }

        public static void SetEngineObjectSprite(int id, string sprite)
        {
            if (scenes[eSettings.currentScene].objects[id] == null) return;

            if (sprite == string.Empty)
            {
                EngineObject[] filler = GetEngineObjectsWithType(EngineObjectType.Sprite);

                if (filler.Length == 0)
                {
                    MessageBox.Show(scenes[eSettings.currentScene].objects[id].Variable() + " could not be assigned a existing sprite. Please refer to a existing sprite.", "Lynx2D Engine - Message");
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
                EngineObject[] filler = GetEngineObjectsWithType(EngineObjectType.BoxCollider);

                if (filler.Length == 0)
                {
                    MessageBox.Show(scenes[eSettings.currentScene].objects[id].Variable() + " could not be assigned a existing collider. Please refer to a existing collider.", "Lynx2D Engine - Message");
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

        public static void RenameEngineObject(int id, string name, bool updates)
        {
            if (scenes[eSettings.currentScene].objects[id] == null || name == "HAS_BEEN_CLOSED") return;

            if (scenes[eSettings.currentScene].objects[id].type == EngineObjectType.Sprite)
            {
                //Rename sprite usage in engine objects
                for (int i = 0; i < scenes[eSettings.currentScene].objects.Length; i++)
                    if (i != id && scenes[eSettings.currentScene].objects[i].sprite == scenes[eSettings.currentScene].objects[id].Variable())
                        scenes[eSettings.currentScene].objects[i].sprite = name;

                //Rename sprite usage in tilemapper maps
                Tilemapper.RenameSpriteInTiles(scenes[eSettings.currentScene].objects[id].Variable(), name);
            }

            if (scenes[eSettings.currentScene].objects[id].type == EngineObjectType.BoxCollider)
            {
                //Rename collider usage in engine objects
                for (int i = 0; i < scenes[eSettings.currentScene].objects.Length; i++)
                    if (i != id && scenes[eSettings.currentScene].objects[i].collider == scenes[eSettings.currentScene].objects[id].Variable())
                        scenes[eSettings.currentScene].objects[i].collider = name;
            }

            scenes[eSettings.currentScene].objects[id].Rename(name);

            if (updates)
                form.UpdateHierarchy();

            form.RefreshBrowser();
        }
        #endregion

        public static void ClearEngine()
        {
            scenes = new Scene[0];
            bSettings = new BuildSettings();
            eSettings = new EngineSettings();

            Tilemapper.Clear();
        }
    }

    [Serializable]
    public class EnginePreferences
    {
        public Theme theme = Theme.Light;
        public bool suppressExceptions = false;
    }

    [Serializable]
    public class EngineState
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
                case EngineObjectType.BoxCollider:
                    name = "BoxCollider";
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
                case EngineObjectType.Sound:
                    name = "Sound";

                    break;
            }
        }

        public EngineObject(int id, EngineObjectType type, int child, int parent)
        {
            this.id = id;
            this.type = type;

            if (child != -1) this.child = child;
            if (parent != -1) this.parent = parent;
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
            EngineObject temp = new EngineObject(id, type, child, parent)
            {
                layer = layer,
                visible = visible,
                name = name,

                sprite = sprite,
                x = x,
                y = y,
                w = w,
                h = h,

                minvx = minvx,
                maxvx = maxvx,
                minvy = minvy,
                maxvy = maxvy,
                minSize = minSize,
                maxSize = maxSize,
                speed = speed,
                amount = amount,
                duration = duration,

                source = source,
                cx = cx,
                cy = cy,
                cw = cw,
                ch = ch,
                clipped = clipped,
                rotation = rotation,

                code = code,
                buildOrder = buildOrder,

                tileMap = tileMap,

                unique = unique
            };

            return temp;
        }
        
        public int id;

        public EngineObjectType type;
        public string name;
        public string unique = string.Empty;
        public string code;
        public string buildCode;
        public int buildOrder = 0;

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

        public int initialFramerate = 60;
        public bool imageSmoothing = true;

        public int lineBreaks = 0;
        public bool obfuscates = false;
        public bool mergeFramework = false;
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
        BoxCollider,
        Emitter,
        Tilemap,
        Sound
    }
}
