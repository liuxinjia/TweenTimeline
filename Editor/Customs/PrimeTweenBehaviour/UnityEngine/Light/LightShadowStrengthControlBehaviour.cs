
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class LightShadowStrengthControlBehaviour : BaseControlBehaviour<Light, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Light target, double duration, float startValue)
        {
            return PrimeTween.Tween.LightShadowStrength(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Light target)
        {
            return target.shadowStrength;
        }

        protected override void OnSet(Light target, float updateValue)
        {
            target.shadowStrength = updateValue;
        }
    }
}
