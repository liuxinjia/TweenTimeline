
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class RectTransformOffsetMaxYControlBehaviour : BaseControlBehaviour<RectTransform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(RectTransform target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIOffsetMaxY(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(RectTransform target)
        {
            return target.offsetMax.y;
        }

        protected override void OnSet(RectTransform target, float updateValue)
        {
            target.offsetMax = target.offsetMax.WithComponent(1, (updateValue)) ;
        }
    }
}
