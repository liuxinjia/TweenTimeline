
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class LightColorControlBehaviour : BaseControlBehaviour<Light, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(Light target, double duration, Color startValue)
        {
            return PrimeTween.Tween.LightColor(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Light target)
        {
            return target.color;
        }

        protected override void OnSet(Light target, Color updateValue)
        {
            target.color = updateValue;
        }
    }
}
