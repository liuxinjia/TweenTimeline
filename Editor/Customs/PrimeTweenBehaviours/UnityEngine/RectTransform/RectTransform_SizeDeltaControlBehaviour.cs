
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTweeen
{
    [Serializable]
    public  class RectTransform_SizeDeltaControlBehaviour : BaseControlBehaviour<UnityEngine.RectTransform, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.RectTransform target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.UISizeDelta(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.RectTransform target)
        {
            return target.sizeDelta;
        }

        protected override void OnSet(UnityEngine.RectTransform target, Vector2 updateValue)
        {
            target.sizeDelta = updateValue;
        }
    }
}
