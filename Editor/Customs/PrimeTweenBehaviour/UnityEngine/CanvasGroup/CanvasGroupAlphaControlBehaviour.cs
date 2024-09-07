
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class CanvasGroupAlphaControlBehaviour : BaseControlBehaviour<CanvasGroup, float>
    {
        protected override PrimeTween.Tween OnCreateTween(CanvasGroup target, double duration, float startValue)
        {
            return PrimeTween.Tween.Alpha(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(CanvasGroup target)
        {
            return target.alpha;
        }

        protected override void OnSet(CanvasGroup target, float updateValue)
        {
            target.alpha = updateValue;
        }
    }
}
