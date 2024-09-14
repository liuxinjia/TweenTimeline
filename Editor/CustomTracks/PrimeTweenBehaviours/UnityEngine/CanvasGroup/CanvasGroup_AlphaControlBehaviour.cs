
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CanvasGroupTween
{
    [Serializable]
    public  class CanvasGroup_AlphaControlBehaviour : BaseControlBehaviour<UnityEngine.CanvasGroup, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.CanvasGroup target, double duration, float startValue)
        {
            return PrimeTween.Tween.Alpha(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.CanvasGroup target)
        {
            return target.alpha;
        }

        protected override void OnSet(UnityEngine.CanvasGroup target, float updateValue)
        {
           target.alpha = updateValue;
        }
    }
}
