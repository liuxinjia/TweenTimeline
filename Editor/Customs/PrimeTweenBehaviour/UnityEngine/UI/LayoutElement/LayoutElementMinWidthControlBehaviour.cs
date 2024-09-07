
using System;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class LayoutElementMinWidthControlBehaviour : BaseControlBehaviour<LayoutElement, float>
    {
        protected override PrimeTween.Tween OnCreateTween(LayoutElement target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIMinWidth(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(LayoutElement target)
        {
            return target.minWidth;
        }

        protected override void OnSet(LayoutElement target, float updateValue)
        {
            target.minWidth = updateValue;
        }
    }
}
