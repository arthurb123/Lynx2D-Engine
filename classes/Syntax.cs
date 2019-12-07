using System.Text.RegularExpressions;

namespace Lynx2DEngine
{
    class Syntax
    {
        private static string keywords = @"\b(this|function|var|for|while|let|const|else|if|new|in|end|return|break|true|false|" +
                                         @"undefined|null|NaN|Infinity|eval|uneval|isFinity|isNaN|parseFloat|parseInt)\b";
        private static string methods = @"\b(.Initialize|.Start|.Smoothing|.Framerate|.GetDimensions|.Background|.OnKey|.OnMouse|.StopKey|.StopMouse|.MouseMove|" +
                                        @".ClearMouse|.RemoveMouse|.ParticleLimit|.ClearLoops|.ClearLayerDraw|.ResetCentering|.ResetLayerDraw|.ChannelVolume|" +
                                        @".FindGameObjectWithIdentifier|.FindGameObjectsWithIdentifier|.FindGameObjectWithCollider|" +
                                        @".CreateHorizontalTileSheet|.CreateVerticalTileSheet|.Direction|.Cast|.CastPoint|.CastPoints|.CastPointRadially|.CastPointsRadially|" +
                                        @".CastPointRadiallyMultiple|.DrawSprite|.OnLayerDraw|.Loops|.CreateCollider|.Show|.Hide|.Focus|.Setup|.Text|.Color|" +
                                        @".Position|.Size|.Draws|.Rotation|.Clip|.MaxVelocity|.AddVelocity|.Alignment|.Movement|.ShowAnimation|.ClearAnimation|" +
                                        @".MovementDecelerates|.SetTopDownController|.SetSideWaysController|.ApplyCollider|.ClearCollider|" +
                                        @".Follows|.StopFollowing|.Emit|.Speed|.Solid|.Static|.Enable|.Disable|.Identifier|.Play|.PlaySpatial|.LoadScene|.Save|.Restore|" +
                                        @".OnClick|.ShowAmount|.Range)\b";
        private static string lxTypes = @"\b(lx.GameObject|lx.Sprite|lx.BoxCollider|lx.CircleCollider|lx.Collider|lx.Emitter|lx.Animation|lx.Scene|lx.Audio|lx.UIText|lx.UIMultiText|lx.UITexture|lx.UIButton|lx.Ray)\b";
        private static string types = @"\b(Math|Array|Object|Date|Function|Boolean|Symbol|JSON|String|lx)\b";
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
                case SyntaxMatch.LxTypes:
                    return Regex.Matches(text, lxTypes);
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
        LxTypes,
        Types,
        Comments,
        Strings,
        Numbers
    }
}
