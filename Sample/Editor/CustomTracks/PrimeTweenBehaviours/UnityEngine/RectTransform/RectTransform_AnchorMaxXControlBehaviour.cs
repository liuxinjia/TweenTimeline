
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [Serializable]
    public  class RectTransform_AnchorMaxXControlBehaviour : BaseControlBehaviour<UnityEngine.RectTransform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.RectTransform target, double duration, float startValue)
        {
            return PrimeTween.Tween.Custom(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration, 
                  onValueChange: (target, updateValue) => target.anchorMax = new Vector2(updateValue, target.anchorMax.y));
        }

        protected override object OnGet(UnityEngine.RectTransform target)
        {
            return target.anchorMax.x;
        }

        protected override void OnSet(UnityEngine.RectTransform target, float updateValue)
        {
           target.anchorMax = new Vector2(updateValue, target.anchorMax.y);
        }
    }
}
