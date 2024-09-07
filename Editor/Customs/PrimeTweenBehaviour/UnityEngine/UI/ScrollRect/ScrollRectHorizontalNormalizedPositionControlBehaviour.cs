
using System;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class ScrollRectHorizontalNormalizedPositionControlBehaviour : BaseControlBehaviour<ScrollRect, float>
    {
        protected override PrimeTween.Tween OnCreateTween(ScrollRect target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIHorizontalNormalizedPosition(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(ScrollRect target)
        {
            return target.horizontalNormalizedPosition;
        }

        protected override void OnSet(ScrollRect target, float updateValue)
        {
            target.horizontalNormalizedPosition = updateValue;
        }
    }
}
