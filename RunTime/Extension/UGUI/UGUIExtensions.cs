using UnityEngine;

namespace Cr7Sund
{
    public static class UGUIExtensions
    {
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

    }
}
