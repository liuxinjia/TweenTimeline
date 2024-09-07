
using System;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class ShadowEffectColorControlBehaviour : BaseControlBehaviour<Shadow, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(Shadow target, double duration, Color startValue)
        {
            return PrimeTween.Tween.Color(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Shadow target)
        {
            return target.effectColor;
        }

        protected override void OnSet(Shadow target, Color updateValue)
        {
            target.effectColor = updateValue;
        }
    }
}
