using System.Text.RegularExpressions;

namespace Lynx2DEngine
{
    class Syntax
    {
        private static string keywords = @"\b(function|var|for|while|let|const|else|if|new|in|end|return|break|true|false|undefined|null)\b";
        private static string methods = @"\b(Initialize|Start|Smoothing|Framerate|GetDimensions|OnKey|OnMouse|StopKey|StopMouse|MouseMove
                                             FindGameObjectWithIdentifier|FindGameObjectsWithIdentifier|FindGameObjectWithCollider|
                                             DrawSprite|OnLayerDraw|Loops|CreateCollider|Show|Hide|
                                             Position|Size|Draws|Rotation|Clip|MaxVelocity|AddVelocity|Movement|
                                             MovementDecelerates)\b";
        private static string types = @"\b(GameObject|Sprite|Collider|Emitter|UIText|UIRichText|Math|Array|Object)\b";
        private static string comments = @"(\/\/.+?$|\/\*.+?\*\/)";
        private static string strings = "\".+?\"|'.+?'";

        public static MatchCollection Match(string text, SyntaxMatch match)
        {
            switch (match)
            {
                case SyntaxMatch.Keywords:
                    return Regex.Matches(text, keywords);
                case SyntaxMatch.Methods:
                    return Regex.Matches(text, methods);
                case SyntaxMatch.Types:
                    return Regex.Matches(text, types);
                case SyntaxMatch.Comments:
                    return Regex.Matches(text, comments, RegexOptions.Multiline);
                case SyntaxMatch.Strings:
                    return Regex.Matches(text, strings);
            }

            return null;
        }
    }

    public enum SyntaxMatch
    {
        Keywords = 0,
        Methods,
        Types,
        Comments,
        Strings
    }
}
