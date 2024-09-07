
using System;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class LayoutElementFlexibleWidthControlBehaviour : BaseControlBehaviour<LayoutElement, float>
    {
        protected override PrimeTween.Tween OnCreateTween(LayoutElement target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIFlexibleWidth(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(LayoutElement target)
        {
            return target.flexibleWidth;
        }

        protected override void OnSet(LayoutElement target, float updateValue)
        {
            target.flexibleWidth = updateValue;
        }
    }
}
