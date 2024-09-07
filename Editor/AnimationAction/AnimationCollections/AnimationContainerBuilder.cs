using System.Text.RegularExpressions;

namespace Cr7Sund.TweenTimeLine
{
    public partial class AnimationContainerBuilder
    {
        public static string GetTweenMethodName<T>()
        {
            var typeName = typeof(T).Name;

            // Define the pattern to match the type name and exclude the "ControlBehaviour" suffix
            var pattern = @"^(?<name>.*?)(ControlBehaviour)?$";
            var match = Regex.Match(typeName, pattern);

            if (match.Success)
            {
                return match.Groups["name"].Value;
            }

            // Default return value
            return string.Empty;
        }
    }
}