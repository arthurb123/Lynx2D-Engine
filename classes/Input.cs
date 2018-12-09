using Lynx2DEngine.InputForms;
using System.Drawing;
using System.Windows.Forms;

namespace Lynx2DEngine
{
    class Input
    {
        public static string Prompt(string text, string caption)
        {
            PromptForm temp = new PromptForm();
            temp.SetTitle(text);
            temp.SetCaption(caption);

            return temp.ShowDialog() == DialogResult.OK ? temp.Value() : "HAS_BEEN_CLOSED";
        }

        public static bool YesNo(string text, string caption)
        {
            YesNoForm temp = new YesNoForm();
            temp.SetText(text);
            temp.SetCaption(caption);

            if (temp.ShowDialog() == DialogResult.Yes) return true;

            return false;
        }

        public static void No(string text, string caption)
        {
            YesNoForm temp = new YesNoForm();
            temp.SetText(text);
            temp.SetCaption(caption);
            temp.DisableYes();

            temp.ShowDialog();
        }

        public static string Selection(string text, string caption, string[] content)
        {
            SelectionForm temp = new SelectionForm();
            temp.SetTitle(text);
            temp.SetCaption(caption);
            temp.SetSelectionContent(content);

            return temp.ShowDialog() == DialogResult.OK ? temp.Value() : "HAS_BEEN_CLOSED";
        }

        public static Point HierarchySelection(string text, string caption)
        {
            HierarchySelectionForm temp = new HierarchySelectionForm();
            temp.SetTitle(text);
            temp.SetCaption(caption);

            return temp.ShowDialog() == DialogResult.OK ? temp.Value() : new Point(-1, -1);
        }
    }
}
