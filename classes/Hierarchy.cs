using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lynx2DEngine
{
    [Serializable]
    public class Hierarchy
    {
        public List<HierarchyItem> items = new List<HierarchyItem>();
        public List<HierarchyFolder> folders = new List<HierarchyFolder>();

        public void AddItem(int engineId)
        {
            items.Add(new HierarchyItem(engineId));
        }

        public void AddFolder()
        {
            folders.Add(new HierarchyFolder(folders.Count, "New Folder"));
        }

        public void CopyFolder(int prevScene, HierarchyFolder f)
        {
            HierarchyFolder cf = new HierarchyFolder(folders.Count, f.name);

            foreach (HierarchyItem i in f.content)
            {
                EngineObject temp = Engine.scenes[prevScene].objects[i.engineId].Clone();
                if (temp.parent != -1)
                    continue;

                EngineObject tempChild = null;

                if (temp.child != -1)
                    tempChild = Engine.scenes[prevScene].objects[temp.child].Clone();

                string copies = "";
                int amount = 0;
                while (Engine.GetEngineObjectWithVarName(temp.Variable() + copies) != null)
                {
                    amount++;

                    copies = "_" + amount;
                }

                if (copies != "")
                {
                    temp.Rename(temp.Variable() + copies);
                    if (tempChild != null) tempChild.Rename(tempChild.Variable() + copies);
                }

                int result = 0;

                if (temp.child == -1) result = Engine.AddExistingEngineObject(temp);
                else
                {
                    Point r = Engine.AddExistingEngineObjectWithChild(temp, tempChild);

                    Engine.scenes[Engine.eSettings.currentScene].objects[r.X].child = r.Y;
                    Engine.scenes[Engine.eSettings.currentScene].objects[r.Y].parent = r.X;

                    result = r.X;
                }

                if (temp.type == EngineObjectType.Tilemap)
                    temp.tileMap = Tilemapper.AddMap(Engine.scenes[prevScene].tilemaps[temp.tileMap]);

                cf.AddItem(new HierarchyItem(result));
            }

            folders.Add(cf);
        }

        public void RemoveItem(int engineId, bool checkFolders)
        {
            bool removed = false;

            if (checkFolders)
            for (int i = 0; i < folders.Count; i++)
                if (folders[i].RemoveItem(engineId))
                {
                    removed = true;

                    break;
                }

            if (!removed || !checkFolders)
            for (int i = 0; i < items.Count; i++)
                if (items[i].engineId == engineId)
                {
                    items.RemoveAt(i);

                    break;
                }
        }

        public void RemoveFolderWithIdentifier(int id)
        {
            if (Input.YesNo("Do you also want to delete all the contained objects of this folder?", "Lynx2D Engine - Question"))
            {
                HierarchyItem[] items = folders[id].content.ToArray();

                foreach (HierarchyItem i in items)
                    Engine.RemoveEngineObject(i.engineId, false, false);

                folders.RemoveAt(id);

                Engine.form.refreshBrowser();
            }
            else
            {
                HierarchyItem[] items = folders[id].content.ToArray();

                foreach (HierarchyItem hi in items)
                {
                    int i = hi.engineId;

                    RemoveItem(hi.engineId, true);
                    AddItem(i);
                }

                folders.RemoveAt(id);
            }

            Engine.form.UpdateHierarchy();
        }

        public int GetFolderIdentifierWithName(string name)
        {
            int id = 0;

            for (int i = 0; i < folders.Count; i++)
            {
                if (folders[i].name == name)
                {
                    id = i;
                    break;
                }
            }

            return id;
        }

        public Point GetItemIdentifierWithEngineId(int engineId)
        {
            Point id = new Point(-1, -1);

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].engineId == engineId)
                {
                    id.Y = i;
                    break;
                }
            }

            if (id.Y == -1)
                for (int i = 0; i < folders.Count; i++)
                {
                    int result = GetItemIdentifierInFolderWithEngineId(engineId, i);

                    if (result != -1)
                    {
                        id.X = i;
                        id.Y = result;
                        break;
                    }
                }

            return id;
        }

        public int GetItemIdentifierInFolderWithEngineId(int engineId, int folder)
        {
            int id = -1;

            for (int i = 0; i < folders[folder].content.Count; i++)
                if (folders[folder].content[i].engineId == engineId)
                {
                    id = i;
                    break;
                }

            return id;
        }
    }

    [Serializable]
    public class HierarchyItem
    {
        public int engineId;

        public HierarchyItem(int engineId)
        {
            this.engineId = engineId;
        }

    }

    [Serializable]
    public class HierarchyFolder
    {
        public List<HierarchyItem> content = new List<HierarchyItem>();
        public string name;
        public int identifier;

        public HierarchyFolder(int id, string name)
        {
            identifier = id;
            Rename(name);
        }

        public void AddItem(HierarchyItem item)
        {
            content.Add(item);
        }

        public bool RemoveItem(int engineId)
        {
            for (int i = 0; i < content.Count; i++)
                if (content[i].engineId == engineId)
                {
                    content.RemoveAt(i);

                    return true;
                }

            return false;
        }

        public void Rename(string name)
        {
            this.name = name;
        }
    }
}
