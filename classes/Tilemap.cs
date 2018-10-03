﻿using System;
using System.Drawing;
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

        public void SetTile(int x, int y, Tile t)
        {
            try
            {
                if (x >= map.GetLength(0))
                    Resize(x+1, map.GetLength(1));
                if (y >= map.GetLength(1))
                    Resize(map.GetLength(0), y+1);

                t.build = true;
                map[x, y] = t;

                Tilemapper.ConvertAndSetMap(this);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
            }
        }

        public void RemoveTile(int x, int y)
        {
            try
            {
                map[x, y] = new Tile();
            }
            catch (Exception e)
            {
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
        public int cX;
        public int cY;
        public int cW;
        public int cH;
    }
}