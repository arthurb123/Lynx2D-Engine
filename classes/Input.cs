using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            temp.SetTitle(text);
            temp.SetCaption(caption);

            DialogResult result = temp.ShowDialog();
            if (result == DialogResult.Yes) return true;

            return false;
        }
    }
}
