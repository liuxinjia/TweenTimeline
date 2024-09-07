
using System;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class ShadowEffectDistanceControlBehaviour : BaseControlBehaviour<Shadow, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(Shadow target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.UIEffectDistance(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Shadow target)
        {
            return target.effectDistance;
        }

        protected override void OnSet(Shadow target, Vector2 updateValue)
        {
            target.effectDistance = updateValue;
        }
    }
}
