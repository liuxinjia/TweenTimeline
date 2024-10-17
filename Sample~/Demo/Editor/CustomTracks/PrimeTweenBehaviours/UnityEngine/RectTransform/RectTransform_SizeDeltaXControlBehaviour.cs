
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [Serializable]
    public  class RectTransform_SizeDeltaXControlBehaviour : BaseControlBehaviour<UnityEngine.RectTransform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.RectTransform target, double duration, float startValue)
        {
            return PrimeTween.Tween.Custom(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration, 
                  onValueChange: (target, updateValue) => target.sizeDelta = new Vector2(updateValue, target.sizeDelta.y));
        }

        protected override object OnGet(UnityEngine.RectTransform target)
        {
            return target.sizeDelta.x;
        }

        protected override void OnSet(UnityEngine.RectTransform target, float updateValue)
        {
           target.sizeDelta = new Vector2(updateValue, target.sizeDelta.y);
        }
    }
}
