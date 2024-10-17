using UnityEngine;

namespace Cr7Sund
{
    public static class UIToolkitExtension
    {
        public static Vector2 GetTopLeft(this UnityEngine.UIElements.VisualElement e)
        {
            var resolvedStyle = e.resolvedStyle;
            return new Vector2(resolvedStyle.left, resolvedStyle.top);
        }
        public static void SetTopLeft(this UnityEngine.UIElements.VisualElement e, Vector2 c)
        {
            var style = e.style;
            style.left = c.x;
            style.top = c.y;
        }
        public static Rect GetResolvedStyleRect(this UnityEngine.UIElements.VisualElement e)
        {
            var resolvedStyle = e.resolvedStyle;
            return new Rect(
                resolvedStyle.left,
                resolvedStyle.top,
                resolvedStyle.width,
                resolvedStyle.height
            );
        }
        public static void SetStyleRect(this UnityEngine.UIElements.VisualElement e, Rect c)
        {
            var style = e.style;
            style.left = c.x;
            style.top = c.y;
            style.width = c.width;
            style.height = c.height;

        }
    }
}