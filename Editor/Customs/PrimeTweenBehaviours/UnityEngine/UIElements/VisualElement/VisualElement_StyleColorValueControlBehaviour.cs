
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UIElements;
namespace Cr7Sund.VisualElementTweeen
{
    [Serializable]
    public  class VisualElement_StyleColorValueControlBehaviour : BaseControlBehaviour<UnityEngine.UIElements.VisualElement, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UIElements.VisualElement target, double duration, Color startValue)
        {
            return PrimeTween.Tween.VisualElementColor(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UIElements.VisualElement target)
        {
            return target.style.color.value;
        }

        protected override void OnSet(UnityEngine.UIElements.VisualElement target, Color updateValue)
        {
            target.style.color = updateValue;
        }
    }
}
