
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ShadowTween
{
    [Serializable]
    public  class Shadow_EffectColorAControlBehaviour : BaseControlBehaviour<UnityEngine.UI.Shadow, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.Shadow target, double duration, float startValue)
        {
            return PrimeTween.Tween.Alpha(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.Shadow target)
        {
            return target.effectColor.a;
        }

        protected override void OnSet(UnityEngine.UI.Shadow target, float updateValue)
        {
           target.effectColor = target.effectColor.WithAlpha((updateValue)) ;
        }
    }
}
