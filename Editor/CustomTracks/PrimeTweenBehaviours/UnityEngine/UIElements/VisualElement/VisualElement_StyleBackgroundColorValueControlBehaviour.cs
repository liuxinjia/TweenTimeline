
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UIElements;
namespace Cr7Sund.VisualElementTween
{
    [Serializable]
    public  class VisualElement_StyleBackgroundColorValueControlBehaviour : BaseControlBehaviour<UnityEngine.UIElements.VisualElement, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UIElements.VisualElement target, double duration, Color startValue)
        {
            return PrimeTween.Tween.VisualElementBackgroundColor(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UIElements.VisualElement target)
        {
            return target.style.backgroundColor.value;
        }

        protected override void OnSet(UnityEngine.UIElements.VisualElement target, Color updateValue)
        {
           target.style.backgroundColor = updateValue;
        }
    }
}
