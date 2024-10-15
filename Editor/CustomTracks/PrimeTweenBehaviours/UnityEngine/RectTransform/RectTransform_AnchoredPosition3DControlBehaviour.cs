
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [Serializable]
    public  class RectTransform_AnchoredPosition3DControlBehaviour : BaseControlBehaviour<UnityEngine.RectTransform, Vector3>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.RectTransform target, double duration, Vector3 startValue)
        {
            return PrimeTween.Tween.UIAnchoredPosition3D(target, startValue: startValue,
                  ease: PrimEase, endValue: (Vector3)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.RectTransform target)
        {
            return target.anchoredPosition3D;
        }

        protected override void OnSet(UnityEngine.RectTransform target, Vector3 updateValue)
        {
           target.anchoredPosition3D = updateValue;
        }
    }
}
