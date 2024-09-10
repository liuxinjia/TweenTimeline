
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.LightTweeen
{
    [Serializable]
    public  class Light_ShadowStrengthControlBehaviour : BaseControlBehaviour<UnityEngine.Light, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Light target, double duration, float startValue)
        {
            return PrimeTween.Tween.LightShadowStrength(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Light target)
        {
            return target.shadowStrength;
        }

        protected override void OnSet(UnityEngine.Light target, float updateValue)
        {
            target.shadowStrength = updateValue;
        }
    }
}
