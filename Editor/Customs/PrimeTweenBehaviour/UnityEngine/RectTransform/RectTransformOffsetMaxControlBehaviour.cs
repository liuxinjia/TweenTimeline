
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class RectTransformOffsetMaxControlBehaviour : BaseControlBehaviour<RectTransform, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(RectTransform target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.UIOffsetMax(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(RectTransform target)
        {
            return target.offsetMax;
        }

        protected override void OnSet(RectTransform target, Vector2 updateValue)
        {
            target.offsetMax = updateValue;
        }
    }
}
