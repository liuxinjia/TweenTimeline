using UnityEngine;
using UnityEngine.UI;

namespace Cr7Sund
{
    public static partial class GraphicExtension
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





    }
}