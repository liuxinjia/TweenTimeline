
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class RectTransformSizeDeltaControlBehaviour : BaseControlBehaviour<RectTransform, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(RectTransform target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.UISizeDelta(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(RectTransform target)
        {
            return target.sizeDelta;
        }

        protected override void OnSet(RectTransform target, Vector2 updateValue)
        {
            target.sizeDelta = updateValue;
        }
    }
}
