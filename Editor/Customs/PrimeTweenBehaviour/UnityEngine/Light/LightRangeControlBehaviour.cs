
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class LightRangeControlBehaviour : BaseControlBehaviour<Light, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Light target, double duration, float startValue)
        {
            return PrimeTween.Tween.LightRange(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Light target)
        {
            return target.range;
        }

        protected override void OnSet(Light target, float updateValue)
        {
            target.range = updateValue;
        }
    }
}
