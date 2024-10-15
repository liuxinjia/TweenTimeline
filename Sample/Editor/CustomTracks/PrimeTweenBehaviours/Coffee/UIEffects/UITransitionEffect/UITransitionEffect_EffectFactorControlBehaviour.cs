
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using Coffee.UIEffects;
namespace Cr7Sund.UITransitionEffectTween
{
    [Serializable]
    public  class UITransitionEffect_EffectFactorControlBehaviour : BaseControlBehaviour<Coffee.UIEffects.UITransitionEffect, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Coffee.UIEffects.UITransitionEffect target, double duration, float startValue)
        {
            return PrimeTween.Tween.Custom(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration, 
                  onValueChange: (target, updateValue) => target.effectFactor = updateValue);
        }

        protected override object OnGet(Coffee.UIEffects.UITransitionEffect target)
        {
            return target.effectFactor;
        }

        protected override void OnSet(Coffee.UIEffects.UITransitionEffect target, float updateValue)
        {
           target.effectFactor = updateValue;
        }
    }
}
