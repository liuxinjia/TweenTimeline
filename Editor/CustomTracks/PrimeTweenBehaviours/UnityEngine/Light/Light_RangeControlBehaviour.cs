
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.LightTween
{
    [Serializable]
    public  class Light_RangeControlBehaviour : BaseControlBehaviour<UnityEngine.Light, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Light target, double duration, float startValue)
        {
            return PrimeTween.Tween.LightRange(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Light target)
        {
            return target.range;
        }

        protected override void OnSet(UnityEngine.Light target, float updateValue)
        {
           target.range = updateValue;
        }
    }
}
