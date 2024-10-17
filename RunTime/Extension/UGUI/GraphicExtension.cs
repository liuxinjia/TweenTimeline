using UnityEngine;
using UnityEngine.UI;

namespace Cr7Sund
{
    public static class GraphicExtension
    {
        public static void Fade(this Graphic targetComponent, bool targetValue)
        {
            if (!targetValue)
            {
                var alphaColor = new Color(targetComponent.color.r, targetComponent.color.g,
                targetComponent.color.b, 0);
                targetComponent.color = alphaColor;
            }
            else
            {
                var alphaColor = new Color(targetComponent.color.r, targetComponent.color.g,
                                  targetComponent.color.b, 1);
                targetComponent.color = alphaColor;
            }
        }

        public static void Fade(this CanvasGroup targetComponent, bool targetValue)
        {
            targetComponent.alpha = targetValue ? 1 : 0;
        }


        public static Color WithAlpha(this Color c, float alpha)
        {
            c.a = alpha;
            return c;
        }

        public static Vector2 WithComponent(this Vector2 v, int index, float val)
        {
            v[index] = val;
            return v;
        }

        public static Vector3 WithComponent(this Vector3 v, int index, float val)
        {
            v[index] = val;
            return v;
        }

#if !UNITY_2019_1_OR_NEWER || UNITY_UGUI_INSTALLED
        public static Vector2 GetFlexibleSize(this UnityEngine.UI.LayoutElement target) => new Vector2(target.flexibleWidth, target.flexibleHeight);
        public static void SetFlexibleSize(this UnityEngine.UI.LayoutElement target, Vector2 vector2)
        {
            target.flexibleWidth = vector2.x;
            target.flexibleHeight = vector2.y;
        }

        public static Vector2 GetMinSize(this UnityEngine.UI.LayoutElement target) => new Vector2(target.minWidth, target.minHeight);
        public static void SetMinSize(this UnityEngine.UI.LayoutElement target, Vector2 vector2)
        {
            target.minWidth = vector2.x;
            target.minHeight = vector2.y;
        }

        public static Vector2 GetPreferredSize(this UnityEngine.UI.LayoutElement target) => new Vector2(target.preferredWidth, target.preferredHeight);
        public static void SetPreferredSize(this UnityEngine.UI.LayoutElement target, Vector2 vector2)
        {
            target.preferredWidth = vector2.x;
            target.preferredHeight = vector2.y;
        }

        public static Vector2 GetNormalizedPosition(this UnityEngine.UI.ScrollRect target) => new Vector2(target.horizontalNormalizedPosition, target.verticalNormalizedPosition);
        public static void SetNormalizedPosition(this UnityEngine.UI.ScrollRect target, Vector2 vector2)
        {
            target.horizontalNormalizedPosition = vector2.x;
            target.verticalNormalizedPosition = vector2.y;
        }
#endif

#if UI_ELEMENTS_MODULE_INSTALLED
        public static Vector2 GetTopLeft(this UnityEngine.UIElements.VisualElement e) {
            var resolvedStyle = e.resolvedStyle;
            return new Vector2(resolvedStyle.left, resolvedStyle.top);
        }
        public static void SetTopLeft(this UnityEngine.UIElements.VisualElement e, Vector2 c) {
            var style = e.style;
            style.left = c.x;
            style.top = c.y;
        }
        public static Rect GetResolvedStyleRect(this UnityEngine.UIElements.VisualElement e) {
            var resolvedStyle = e.resolvedStyle;
            return new Rect(
                resolvedStyle.left,
                resolvedStyle.top,
                resolvedStyle.width,
                resolvedStyle.height
            );
        }
        public static void SetStyleRect(this UnityEngine.UIElements.VisualElement e, Rect c) {
            var style = e.style;
            style.left = c.x;
            style.top = c.y;
            style.width = c.width;
            style.height = c.height;
        }


#endif
    }
}