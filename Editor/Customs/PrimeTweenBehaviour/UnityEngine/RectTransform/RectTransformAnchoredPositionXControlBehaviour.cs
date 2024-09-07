
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public class RectTransformAnchoredPositionXControlBehaviour : BaseControlBehaviour<RectTransform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(RectTransform target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIAnchoredPositionX(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(RectTransform target)
        {
            return target.anchoredPosition.x;
        }

        protected override void OnSet(RectTransform target, float updateValue)
        {
            target.anchoredPosition = target.anchoredPosition.WithComponent(0, (updateValue));
        }
    }
}
