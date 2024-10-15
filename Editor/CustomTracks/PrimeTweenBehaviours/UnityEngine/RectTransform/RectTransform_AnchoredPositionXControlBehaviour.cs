
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [Serializable]
    public  class RectTransform_AnchoredPositionXControlBehaviour : BaseControlBehaviour<UnityEngine.RectTransform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.RectTransform target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIAnchoredPositionX(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.RectTransform target)
        {
            return target.anchoredPosition.x;
        }

        protected override void OnSet(UnityEngine.RectTransform target, float updateValue)
        {
           target.anchoredPosition = target.anchoredPosition.WithComponent(0, (updateValue)) ;
        }
    }
}
