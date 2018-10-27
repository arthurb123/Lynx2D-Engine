using System.Drawing;
using System.Windows.Forms;

namespace Lynx2DEngine.classes
{
    public static class LightTheme
    {
        public static Color menuBackground = Color.FromKnownColor(KnownColor.HighlightText);
        public static Color mainBackground = Color.FromKnownColor(KnownColor.ScrollBar);
        public static Color background = Color.FromKnownColor(KnownColor.Control);
        public static Color font = Color.Black;
    }

    public static class DarkTheme
    {
        public static Color menuBackground = ColorTranslator.FromHtml("#383838");
        public static Color mainBackground = ColorTranslator.FromHtml("#595959");
        public static Color scriptBackground = ColorTranslator.FromHtml("#232323");
        public static Color scriptNumbersBackground = ColorTranslator.FromHtml("#424242");
        public static Color background = ColorTranslator.FromHtml("#727272");
        public static Color font = Color.WhiteSmoke;
        public static Color border = Color.Silver;
    }

    public class DarkThemeColorTable : ProfessionalColorTable
    {
        public override Color MenuItemBorder
        {
            get { return DarkTheme.background; }
        }

        public override Color MenuItemSelected
        {
            get { return DarkTheme.border; }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get { return DarkTheme.background; }
        }

        public override Color MenuItemPressedGradientMiddle
        {
            get { return DarkTheme.background; }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get { return DarkTheme.background; }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get { return DarkTheme.background; }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get { return DarkTheme.background; }
        }

        public override Color MenuStripGradientBegin
        {
            get { return DarkTheme.menuBackground; }
        }

        public override Color MenuStripGradientEnd
        {
            get { return DarkTheme.menuBackground; }
        }
    }

    public enum Theme
    {
        Light = 0,
        Dark
    }
}
