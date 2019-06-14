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

            Engine.ExecuteScript("let script = document.createElement('script');" +
                                 "script.src = 'https://cdn.jsdelivr.net/npm/javascript-obfuscator/dist/index.browser.js';" +
                                 "script.id = 'obfuscationScript';" +
                                 "document.body.appendChild(script);");

            injected = true;
        }

        public static void Remove()
        {
            if (!injected)
                return;

            Engine.ExecuteScript("let el = document.getElementById('obfuscationScript');" +
                                 "if (typeof(el) != 'undefined' && el != undefined)" +
                                     "document.body.removeChild(el);");

            injected = false;
        }
    }
}
