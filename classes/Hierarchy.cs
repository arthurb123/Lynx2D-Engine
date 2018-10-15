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
            folders.Add(new HierarchyFolder("New Folder"));
        }

        public void RemoveItem(int engineId)
        {
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
                foreach (HierarchyItem hi in folders[id].content)
                {
                    Engine.RemoveEngineObject(hi.engineId, false);
                    RemoveItem(hi.engineId);
                }

                Engine.form.refreshBrowser();
            }
            else
                foreach (HierarchyItem hi in folders[id].content)
                {
                    int i = hi.engineId;

                    RemoveItem(hi.engineId);
                    AddItem(i);
                }

            folders.RemoveAt(id);

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

        public HierarchyFolder(string name)
        {
            Rename(name);
        }

        public void AddItem(HierarchyItem item)
        {
            content.Add(item);
        }

        public void RemoveItem(int engineId)
        {
            for (int i = 0; i < content.Count; i++)
                if (content[i].engineId == engineId)
                {
                    content.RemoveAt(i);

                    break;
                }
        }

        public void Rename(string name)
        {
            this.name = name;
        }
    }
}
