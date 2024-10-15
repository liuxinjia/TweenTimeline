
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ShadowTween
{
    [Serializable]
    public  class Shadow_EffectColorControlBehaviour : BaseControlBehaviour<UnityEngine.UI.Shadow, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.Shadow target, double duration, Color startValue)
        {
            return PrimeTween.Tween.Color(target, startValue: startValue,
                  ease: PrimEase, endValue: (Color)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.Shadow target)
        {
            return target.effectColor;
        }

        protected override void OnSet(UnityEngine.UI.Shadow target, Color updateValue)
        {
           target.effectColor = updateValue;
        }
    }
}
