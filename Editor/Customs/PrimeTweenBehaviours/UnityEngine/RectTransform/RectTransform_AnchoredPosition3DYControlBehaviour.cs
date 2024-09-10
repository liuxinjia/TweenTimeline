
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTweeen
{
    [Serializable]
    public  class RectTransform_AnchoredPosition3DYControlBehaviour : BaseControlBehaviour<UnityEngine.RectTransform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.RectTransform target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIAnchoredPosition3DY(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.RectTransform target)
        {
            return target.anchoredPosition3D.y;
        }

        protected override void OnSet(UnityEngine.RectTransform target, float updateValue)
        {
            target.anchoredPosition3D = target.anchoredPosition3D.WithComponent(1, (updateValue)) ;
        }
    }
}
