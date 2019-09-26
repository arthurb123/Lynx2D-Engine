using System.Threading.Tasks;

namespace Lynx2DEngine
{
    class Obfuscator
    {
        private static bool injected = false;

        public static async Task<string> Encode(string content)
        {
            if (!injected)
                throw new System.Exception();

            string r = await Engine.ExecuteScriptWithResult("JavaScriptObfuscator.obfuscate(`(function() {" + content + " })();`, {" +
                                                                "compact: true," +
                                                                "controlFlowFlattening: true" +
                                                            "}).getObfuscatedCode();");
            return r;
        }

        public static void Inject()
        {
            if (injected)
                return;

            Engine.ExecuteScript("let OBFUSCATE_SCRIPT = document.createElement('script');" +
                                 "OBFUSCATE_SCRIPT.src = 'https://cdn.jsdelivr.net/npm/javascript-obfuscator/dist/index.browser.js';" +
                                 "OBFUSCATE_SCRIPT.id = 'obfuscationScript';" +
                                 "document.body.appendChild(OBFUSCATE_SCRIPT);");

            injected = true;
        }

        public static void Remove()
        {
            if (!injected)
                return;

            Engine.ExecuteScript("let OBFUSCATE_EL = document.getElementById('obfuscationScript');" +
                                 "if (typeof(OBFUSCATE_EL) != 'undefined' && el != undefined)" +
                                     "document.body.removeChild(OBFUSCATE_EL);");

            injected = false;
        }
    }
}
