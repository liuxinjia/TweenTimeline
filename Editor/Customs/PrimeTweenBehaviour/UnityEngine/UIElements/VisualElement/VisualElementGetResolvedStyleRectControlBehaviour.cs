
using System;
using UnityEngine.UIElements;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class VisualElementGetResolvedStyleRectControlBehaviour : BaseControlBehaviour<VisualElement, Rect>
    {
        protected override PrimeTween.Tween OnCreateTween(VisualElement target, double duration, Rect startValue)
        {
            return PrimeTween.Tween.VisualElementLayout(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(VisualElement target)
        {
            return target.GetResolvedStyleRect();
        }

        protected override void OnSet(VisualElement target, Rect updateValue)
        {
            target.SetStyleRect((updateValue)) ;
        }
    }
}
