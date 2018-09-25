﻿using System.Text.RegularExpressions;

namespace Lynx2DEngine
{
    class Syntax
    {
        private static string keywords = @"\b(function|var|for|while|let|const|else|if|new|in|end|return|break|true|false|undefined|null)\b";
        private static string methods = @"\b(Initialize|Start|Smoothing|Framerate|GetDimensions|OnKey|OnMouse|StopKey|StopMouse|MouseMove|" +
                                             @"ParticleLimit|ClearLoops|ClearLayerDraw|ResetCentering|ResetLayerDraw|ChannelVolume" +
                                             @"FindGameObjectWithIdentifier|FindGameObjectsWithIdentifier|FindGameObjectWithCollider|" +
                                             @"DrawSprite|OnLayerDraw|Loops|CreateCollider|Show|Hide|Focus|Setup|Text|Color|" +
                                             @"Position|Size|Draws|Rotation|Clip|MaxVelocity|AddVelocity|Movement|Alignment|" +
                                             @"MovementDecelerates|SetTopDownController|SetSideWaysController|ApplyCollider|" +
                                             @"Follows|StopFollowing|Emit|Speed|Solid|Static|Enable|Disable|Identifier|Play)\b";
        private static string types = @"\b(GameObject|Sprite|Collider|Emitter|Animation|Scene|Audio|UIText|UIRichText|UITexture|Math|Array|Object)\b";
        private static string comments = @"(\/\/.+?$|\/\*.+?\*\/)";
        private static string strings = "\".+?\"|'.+?'";
        private static string numbers = @"\d+";

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
                case SyntaxMatch.Numbers:
                    return Regex.Matches(text, numbers);
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
        Strings,
        Numbers
    }
}
