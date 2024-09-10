
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTweeen
{
    [Serializable]
    public  class RectTransform_OffsetMinYControlBehaviour : BaseControlBehaviour<UnityEngine.RectTransform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.RectTransform target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIOffsetMinY(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.RectTransform target)
        {
            return target.offsetMin.y;
        }

        protected override void OnSet(UnityEngine.RectTransform target, float updateValue)
        {
            target.offsetMin = target.offsetMin.WithComponent(1, (updateValue)) ;
        }
    }
}
