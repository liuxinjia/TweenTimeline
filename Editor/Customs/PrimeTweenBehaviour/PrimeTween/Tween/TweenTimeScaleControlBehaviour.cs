
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class TweenTimeScaleControlBehaviour : BaseControlBehaviour<Tween, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Tween target, double duration, float startValue)
        {
            return PrimeTween.Tween.TweenTimeScale(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Tween target)
        {
            return target.timeScale;
        }

        protected override void OnSet(Tween target, float updateValue)
        {
            target.timeScale = updateValue;
        }
    }
}
