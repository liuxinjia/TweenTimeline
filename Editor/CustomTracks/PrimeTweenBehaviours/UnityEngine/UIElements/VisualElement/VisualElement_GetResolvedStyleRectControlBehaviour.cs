
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UIElements;
namespace Cr7Sund.VisualElementTween
{
    [Serializable]
    public  class VisualElement_GetResolvedStyleRectControlBehaviour : BaseControlBehaviour<UnityEngine.UIElements.VisualElement, Rect>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UIElements.VisualElement target, double duration, Rect startValue)
        {
            return PrimeTween.Tween.VisualElementLayout(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UIElements.VisualElement target)
        {
            return target.GetResolvedStyleRect();
        }

        protected override void OnSet(UnityEngine.UIElements.VisualElement target, Rect updateValue)
        {
           target.SetStyleRect((updateValue)) ;
        }
    }
}
