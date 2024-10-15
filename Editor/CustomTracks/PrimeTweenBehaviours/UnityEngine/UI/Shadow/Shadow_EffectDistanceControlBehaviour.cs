
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ShadowTween
{
    [Serializable]
    public  class Shadow_EffectDistanceControlBehaviour : BaseControlBehaviour<UnityEngine.UI.Shadow, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.Shadow target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.UIEffectDistance(target, startValue: startValue,
                  ease: PrimEase, endValue: (Vector2)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.Shadow target)
        {
            return target.effectDistance;
        }

        protected override void OnSet(UnityEngine.UI.Shadow target, Vector2 updateValue)
        {
           target.effectDistance = updateValue;
        }
    }
}
