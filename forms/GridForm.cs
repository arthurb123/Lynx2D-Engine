using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lynx2DEngine.forms
{
    public partial class GridForm : Form
    {
        public GridForm()
        {
            InitializeComponent();
        }

        private void GridForm_Load(object sender, EventArgs e)
        {
            FormClosing += GridForm_Closing;

            gridSize.Maximum = Decimal.MaxValue;
            gridWidth.Maximum = Decimal.MaxValue;
            gridHeight.Maximum = Decimal.MaxValue;
            gridSize.Minimum = -Decimal.MaxValue;
            gridWidth.Minimum = -Decimal.MaxValue;
            gridHeight.Minimum = -Decimal.MaxValue;

            gridSize.Value = Engine.eSettings.gridSize;
            gridWidth.Value = Engine.eSettings.gridWidth;
            gridHeight.Value = Engine.eSettings.gridHeight;
            gridStrokeSize.Value = Engine.eSettings.gridStrokeSize;

            LoadColor();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ColorDialog c = new ColorDialog();

            if (c.ShowDialog() == DialogResult.OK)
            {
                Engine.eSettings.gridColor = HexConverter(c.Color);

                LoadColor();
            }

            Grid.Inject();
        }

        private void gridSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.eSettings.gridSize = (int)gridSize.Value;

            Grid.Inject();
        }

        private void gridWidth_ValueChanged(object sender, EventArgs e)
        {
            Engine.eSettings.gridWidth = (int)gridWidth.Value;

            Grid.Inject();
        }

        private void gridHeight_ValueChanged(object sender, EventArgs e)
        {
            Engine.eSettings.gridHeight = (int)gridHeight.Value;

            Grid.Inject();
        }

        private void gridStrokeSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.eSettings.gridStrokeSize = (int)gridStrokeSize.Value;

            Grid.Inject();
        }

        private void LoadColor()
        {
            gridColor.BackColor = ColorTranslator.FromHtml(Engine.eSettings.gridColor);
        }

        private void GridForm_Closing(object sender, EventArgs e)
        {
            Project.Save();
        }

        private String HexConverter(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
    }
}
