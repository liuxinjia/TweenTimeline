
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTweeen
{
    [Serializable]
    public  class RectTransform_AnchorMinControlBehaviour : BaseControlBehaviour<UnityEngine.RectTransform, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.RectTransform target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.UIAnchorMin(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.RectTransform target)
        {
            return target.anchorMin;
        }

        protected override void OnSet(UnityEngine.RectTransform target, Vector2 updateValue)
        {
            target.anchorMin = updateValue;
        }
    }
}
