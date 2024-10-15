
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [Serializable]
    public  class RectTransform_PivotControlBehaviour : BaseControlBehaviour<UnityEngine.RectTransform, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.RectTransform target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.UIPivot(target, startValue: startValue,
                  ease: PrimEase, endValue: (Vector2)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.RectTransform target)
        {
            return target.pivot;
        }

        protected override void OnSet(UnityEngine.RectTransform target, Vector2 updateValue)
        {
           target.pivot = updateValue;
        }
    }
}
