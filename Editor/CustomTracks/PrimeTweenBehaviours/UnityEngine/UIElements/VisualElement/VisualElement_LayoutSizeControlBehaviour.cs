
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UIElements;
namespace Cr7Sund.VisualElementTween
{
    [Serializable]
    public  class VisualElement_LayoutSizeControlBehaviour : BaseControlBehaviour<UnityEngine.UIElements.VisualElement, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UIElements.VisualElement target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.VisualElementSize(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UIElements.VisualElement target)
        {
            return target.layout.size;
        }

        protected override void OnSet(UnityEngine.UIElements.VisualElement target, Vector2 updateValue)
        {
           target.style.width = updateValue.x;             target.style.height = updateValue.y;
        }
    }
}
