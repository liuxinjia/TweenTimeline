
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [Serializable]
    public  class RectTransform_AnchoredPositionYControlBehaviour : BaseControlBehaviour<UnityEngine.RectTransform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.RectTransform target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIAnchoredPositionY(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.RectTransform target)
        {
            return target.anchoredPosition.y;
        }

        protected override void OnSet(UnityEngine.RectTransform target, float updateValue)
        {
           target.anchoredPosition = target.anchoredPosition.WithComponent(1, (updateValue)) ;
        }
    }
}
