
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class RectTransformAnchoredPosition3DControlBehaviour : BaseControlBehaviour<RectTransform, Vector3>
    {
        protected override PrimeTween.Tween OnCreateTween(RectTransform target, double duration, Vector3 startValue)
        {
            return PrimeTween.Tween.UIAnchoredPosition3D(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(RectTransform target)
        {
            return target.anchoredPosition3D;
        }

        protected override void OnSet(RectTransform target, Vector3 updateValue)
        {
            target.anchoredPosition3D = updateValue;
        }
    }
}
