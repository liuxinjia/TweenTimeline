
using System;
using UnityEngine.UIElements;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class VisualElementLayoutSizeControlBehaviour : BaseControlBehaviour<VisualElement, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(VisualElement target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.VisualElementSize(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(VisualElement target)
        {
            return target.layout.size;
        }

        protected override void OnSet(VisualElement target, Vector2 updateValue)
        {
            target.style.width = updateValue.x;             target.style.height = updateValue.y;
        }
    }
}
