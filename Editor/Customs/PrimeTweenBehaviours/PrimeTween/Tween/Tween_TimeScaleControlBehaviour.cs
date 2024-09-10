
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TweenTweeen
{
    [Serializable]
    public  class Tween_TimeScaleControlBehaviour : BaseControlBehaviour<PrimeTween.Tween, float>
    {
        protected override PrimeTween.Tween OnCreateTween(PrimeTween.Tween target, double duration, float startValue)
        {
            return PrimeTween.Tween.TweenTimeScale(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(PrimeTween.Tween target)
        {
            return target.timeScale;
        }

        protected override void OnSet(PrimeTween.Tween target, float updateValue)
        {
            target.timeScale = updateValue;
        }
    }
}
