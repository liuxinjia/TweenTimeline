
using System;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class ShadowEffectColorAControlBehaviour : BaseControlBehaviour<Shadow, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Shadow target, double duration, float startValue)
        {
            return PrimeTween.Tween.Alpha(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Shadow target)
        {
            return target.effectColor.a;
        }

        protected override void OnSet(Shadow target, float updateValue)
        {
            target.effectColor = target.effectColor.WithAlpha((updateValue)) ;
        }
    }
}
