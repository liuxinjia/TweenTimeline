
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ScrollRectTweeen
{
    [Serializable]
    public  class ScrollRect_HorizontalNormalizedPositionControlBehaviour : BaseControlBehaviour<UnityEngine.UI.ScrollRect, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.ScrollRect target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIHorizontalNormalizedPosition(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.ScrollRect target)
        {
            return target.horizontalNormalizedPosition;
        }

        protected override void OnSet(UnityEngine.UI.ScrollRect target, float updateValue)
        {
            target.horizontalNormalizedPosition = updateValue;
        }
    }
}
