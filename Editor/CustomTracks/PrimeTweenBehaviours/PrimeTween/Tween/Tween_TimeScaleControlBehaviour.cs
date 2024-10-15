
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TweenTween
{
    [Serializable]
    public  class Tween_TimeScaleControlBehaviour : BaseControlBehaviour<PrimeTween.Tween, float>
    {
        protected override PrimeTween.Tween OnCreateTween(PrimeTween.Tween target, double duration, float startValue)
        {
            return PrimeTween.Tween.TweenTimeScale(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration);
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
