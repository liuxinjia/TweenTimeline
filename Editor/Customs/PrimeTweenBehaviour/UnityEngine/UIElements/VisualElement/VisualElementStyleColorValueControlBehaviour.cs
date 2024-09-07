
using System;
using UnityEngine.UIElements;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class VisualElementStyleColorValueControlBehaviour : BaseControlBehaviour<VisualElement, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(VisualElement target, double duration, Color startValue)
        {
            return PrimeTween.Tween.VisualElementColor(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(VisualElement target)
        {
            return target.style.color.value;
        }

        protected override void OnSet(VisualElement target, Color updateValue)
        {
            target.style.color = updateValue;
        }
    }
}
