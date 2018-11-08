using System;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    [Serializable]
    public class Tilemap
    {
        public Tile[,] map;
        public int layer = 0;
        public int id = 0;
        public int tilesize = 64;
        public int x = 0,
                   y = 0;
        public int scale = 1;
        public bool collides = false;
        public bool[,] colliders = new bool[0, 0];

        public string curSprite = string.Empty;

        public Tilemap(int width, int height)
        {
            map = new Tile[height, width];

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j] = new Tile();
        }

        public void Resize(int width, int height)
        {
            Tile[,] newArray = new Tile[width, height];

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    newArray[i, j] = map[i, j];

            map = newArray;
            Tilemapper.SetCurrentTile();
        }

        public void SetTile(int x, int y, Tile t, bool converts)
        {
            x -= this.x;
            y -= this.y;

            try
            {
                if (x >= map.GetLength(0))
                    Resize(x+1, map.GetLength(1));
                if (y >= map.GetLength(1))
                    Resize(map.GetLength(0), y+1);

                if (map[x, y] != null &&
                    map[x, y].build &&
                    map[x, y].cX == Tilemapper.selected.cX &&
                    map[x, y].cY == Tilemapper.selected.cY &&
                    map[x, y].r == Tilemapper.selected.r)
                    return;

                if (t.cW == tilesize && t.cH == tilesize)
                {
                    t.build = true;
                    map[x, y] = t;
                }
                else
                    for (int yy = 0; yy < t.cH / tilesize; yy++)
                        for (int xx = 0; xx < t.cW / tilesize; xx++)
                        {
                            Tile tt = new Tile()
                            {
                                sprite = t.sprite,
                                cX = t.cX + xx * tilesize,
                                cY = t.cY + yy * tilesize,
                                cW = tilesize,
                                cH = tilesize,
                                build = true
                            };

                            SetTile(x + xx, y + yy, tt, false);
                        }

                if (converts) Tilemapper.ConvertAndSetMap(this);
            }
            catch (Exception e)
            {
                Engine.ExecuteScript("lx.StopMouse(0);");

                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
            }
        }

        public void RemoveTile(int x, int y)
        {
            x -= this.x;
            y -= this.y;

            try
            {
                if (map[x, y] != null && !map[x, y].build)
                    return;

                map[x, y] = new Tile();

                Tilemapper.ConvertAndSetMap(this);
            }
            catch (Exception e)
            {
                Engine.ExecuteScript("lx.StopMouse(2);");

                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
            }
        }
    }

    [Serializable]
    public class Tile
    {
        public Tile()
        {
            //...
        }

        public Tile(string sprite, int cX, int cY, int cW, int cH)
        {
            this.sprite = sprite;
            this.cX = cX;
            this.cY = cY;
            this.cW = cW;
            this.cH = cH;
        }

        public bool build = false;

        public string sprite;
        public int cX = -1;
        public int cY = -1;
        public int cW;
        public int cH;
        public float r = 0;
    }
}
