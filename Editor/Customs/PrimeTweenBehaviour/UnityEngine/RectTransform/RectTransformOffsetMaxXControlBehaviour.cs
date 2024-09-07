
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class RectTransformOffsetMaxXControlBehaviour : BaseControlBehaviour<RectTransform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(RectTransform target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIOffsetMaxX(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(RectTransform target)
        {
            return target.offsetMax.x;
        }

        protected override void OnSet(RectTransform target, float updateValue)
        {
            target.offsetMax = target.offsetMax.WithComponent(0, (updateValue)) ;
        }
    }
}
