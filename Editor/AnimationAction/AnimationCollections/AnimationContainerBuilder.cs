using System.Text.RegularExpressions;

namespace Cr7Sund.TweenTimeLine
{
    public partial class AnimationContainerBuilder
    {
        public static string GetTweenMethodName<T>()
        {
            var typeName = typeof(T).Name;

            // Default return value
            return typeName;
        }
    }
}