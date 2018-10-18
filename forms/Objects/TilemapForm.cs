using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lynx2DEngine.forms
{
    public partial class TilemapForm : Form
    {
        private Tilemap tm = null;
        private Image cur = null;

        private int engineId;
        private EngineObject obj;
        private bool canDetect = false;

        private Point tilesetOffset = new Point(0, 0);
        private bool dragging = false;
        private Point oldMouse = default(Point);
        private Point selected = default(Point);

        public TilemapForm()
        {
            InitializeComponent();
        }

        public void Initialize(int engineId)
        {
            this.engineId = engineId;

            UpdateTitle();
            tm = Tilemapper.maps[obj.tileMap];

            tilesize.Value = tm.tilesize;
            layer.Value = tm.layer;
            x.Value = tm.x;
            y.Value = tm.y;
            updateSpriteSelection();

            if (tm.curSprite != string.Empty)
                loadSelectedSprite(false);

            OnResize(null);

            System.Threading.Thread.Sleep(10);

            canDetect = true;
        }
        
        private void UpdateTitle()
        {
            obj = Engine.GetEngineObjects()[engineId];
            Text = obj.Variable();
        }

        private void sprite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!canDetect) return;

            if (tm == null) return;

            loadSelectedSprite(true);
        }

        private void loadSelectedSprite(bool setsSprite)
        {
            try
            {
                if (setsSprite) tm.curSprite = sprite.Text;

                cur = Image.FromFile(Manager.Root() + "/projects/" + Project.Name() + "/" + Engine.GetEngineObjectWithVarName(tm.curSprite).source);

                drawTimer.Enabled = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Lynx2D Engine - Exception");
            }
        }

        private void TilemapForm_Load(object sender, EventArgs e)
        {
            x.Maximum = Decimal.MaxValue;
            y.Maximum = Decimal.MaxValue;
            x.Minimum = -Decimal.MaxValue;
            y.Minimum = -Decimal.MaxValue;

            tileSelection.Paint += new PaintEventHandler(DrawTileMap);
            tileSelection.MouseDown += new MouseEventHandler(tileSelection_MouseDown);
            tileSelection.MouseDown += new MouseEventHandler(SelectTile);
            tileSelection.MouseMove += new MouseEventHandler(tileSelection_MouseMove);
            tileSelection.MouseUp += new MouseEventHandler(tileSelection_MouseUp);
            Resize += new EventHandler(ResizeTileMap);
            FormClosing += new FormClosingEventHandler(TilemapForm_Closing);
        }

        private void TilemapForm_Closing(object sender, FormClosingEventArgs e)
        {
            Tilemapper.StopEditing();
        }

        private void updateSpriteSelection()
        {
            if (sprite.Items.Count == 0)
                foreach (EngineObject o in Engine.GetEngineObjectsWithType(EngineObjectType.Sprite))
                {
                    if (o == null)
                        continue;

                    sprite.Items.Add(o);
                }

            sprite.Text = tm.curSprite;
            if (tm.curSprite != string.Empty) drawTimer.Enabled = true;
        }

        private void drawTimer_Tick(object sender, EventArgs e)
        {
            tileSelection.Invalidate();
        }

        private void DrawTileMap(object sender, PaintEventArgs e)
        {
            if (cur == null || tm.curSprite == string.Empty)
                return;

            try
            {
                Graphics g = e.Graphics;

                g.Clear(Color.Gray);

                if (cur != null)
                    g.DrawImage(cur, tilesetOffset.X, tilesetOffset.Y, cur.Size.Width, cur.Size.Height);

                for (int y = 0; y < (tileSelection.Height - tilesetOffset.Y) / tm.tilesize; y++)
                    for (int x = 0; x < (tileSelection.Width - tilesetOffset.X) / tm.tilesize; x++)
                    {
                        g.DrawRectangle(Pens.Black, new Rectangle(x * tm.tilesize + tilesetOffset.X, y * tm.tilesize + tilesetOffset.Y, tm.tilesize, tm.tilesize));
                    }

                Point cursor = GetCursorTile();
                g.DrawRectangle(Pens.Silver, new Rectangle(cursor.X * tm.tilesize + tilesetOffset.X, cursor.Y * tm.tilesize + tilesetOffset.Y, tm.tilesize, tm.tilesize));

                if (selected != default(Point))
                    g.DrawRectangle(Pens.WhiteSmoke, new Rectangle(selected.X * tm.tilesize + tilesetOffset.X, selected.Y * tm.tilesize + tilesetOffset.Y, tm.tilesize, tm.tilesize));
            }
            catch (Exception ex)
            {
                drawTimer.Enabled = false;
                MessageBox.Show(ex.Message, "Lynx2D Engine - Exception");
            }
        }

        private Point GetCursorTile()
        {
            Point map = tileSelection.PointToClient(MousePosition);

            return new Point((map.X - tilesetOffset.X) / tm.tilesize, (map.Y - tilesetOffset.Y) / tm.tilesize);
        }

        private void SelectTile(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || selected == GetCursorTile() || selected.X < 0 || selected.Y < 0) return;

            try
            {
                selected = GetCursorTile();

                Tilemapper.SelectTile(tm.id, selected.X * tm.tilesize, selected.Y * tm.tilesize, tm.tilesize, tm.tilesize);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lynx2D Engine - Exception");
            }
        }

        private void ResizeTileMap(object sender, EventArgs e)
        {
            tileSelection.Width = Width + (int)tilesize.Value;
            tileSelection.Height = Height + (int)tilesize.Value;
        }

        private void tilesize_ValueChanged(object sender, EventArgs e)
        {
            if (!canDetect) return;

            tm.tilesize = (int)tilesize.Value;

            Tilemapper.RefreshEditingEvent();
            Tilemapper.SetCurrentTile();
        }

        private void layer_ValueChanged(object sender, EventArgs e)
        {
            if (!canDetect) return;

            Tilemapper.AdjustLayer(tm.id, (int)layer.Value);
            Tilemapper.ConvertAndSetMap(tm);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Engine.RemoveEngineObject(engineId, true);
            Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Engine.RenameEngineObject(engineId, Input.Prompt("Enter the new name", "Rename " + obj.Variable()));

            UpdateTitle();
        }

        private void x_ValueChanged(object sender, EventArgs e)
        {
            if (!canDetect) return;

            tm.x = (int)x.Value;

            Tilemapper.ConvertAndSetMap(tm);
            Tilemapper.SetCurrentTile();
        }

        private void y_ValueChanged(object sender, EventArgs e)
        {
            if (!canDetect) return;

            tm.y = (int)y.Value;

            Tilemapper.ConvertAndSetMap(tm);
            Tilemapper.SetCurrentTile();
        }

        private void tileSelection_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle || e.Button == MouseButtons.Right)
            {
                oldMouse = tileSelection.PointToClient(MousePosition);
                dragging = true;
            }
        }

        private void tileSelection_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                tilesetOffset.X += (e.X - oldMouse.X);
                tilesetOffset.Y += (e.Y - oldMouse.Y);

                oldMouse = new Point(e.X, e.Y);
            }
        }

        private void tileSelection_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
