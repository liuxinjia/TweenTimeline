
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.LightTween
{
    [Serializable]
    public  class Light_ColorControlBehaviour : BaseControlBehaviour<UnityEngine.Light, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Light target, double duration, Color startValue)
        {
            return PrimeTween.Tween.LightColor(target, startValue: startValue,
                  ease: PrimEase, endValue: (Color)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Light target)
        {
            return target.color;
        }

        protected override void OnSet(UnityEngine.Light target, Color updateValue)
        {
           target.color = updateValue;
        }
    }
}
