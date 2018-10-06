using System.Threading.Tasks;

namespace Lynx2DEngine
{
    class Obfuscater
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

            Engine.ExecuteScript("var script = document.createElement('script');" +
                                 "script.src = 'https://cdn.jsdelivr.net/npm/javascript-obfuscator/dist/index.browser.js';" +
                                 "script.id = 'obfuscationScript';" +
                                 "document.body.appendChild(script);");

            injected = true;
        }

        public static void Remove()
        {
            if (!injected)
                return;

            Engine.ExecuteScript("document.body.removeChild(document.getElementById('obfuscationScript'));");

            injected = false;
        }
    }
}
