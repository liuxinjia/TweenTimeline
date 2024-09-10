
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTweeen
{
    [Serializable]
    public  class RectTransform_OffsetMaxYControlBehaviour : BaseControlBehaviour<UnityEngine.RectTransform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.RectTransform target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIOffsetMaxY(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.RectTransform target)
        {
            return target.offsetMax.y;
        }

        protected override void OnSet(UnityEngine.RectTransform target, float updateValue)
        {
            target.offsetMax = target.offsetMax.WithComponent(1, (updateValue)) ;
        }
    }
}
