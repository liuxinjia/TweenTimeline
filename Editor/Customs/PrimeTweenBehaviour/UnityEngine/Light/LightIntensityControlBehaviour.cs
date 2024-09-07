
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class LightIntensityControlBehaviour : BaseControlBehaviour<Light, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Light target, double duration, float startValue)
        {
            return PrimeTween.Tween.LightIntensity(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Light target)
        {
            return target.intensity;
        }

        protected override void OnSet(Light target, float updateValue)
        {
            target.intensity = updateValue;
        }
    }
}
