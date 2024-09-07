
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class RectTransformAnchoredPosition3DZControlBehaviour : BaseControlBehaviour<RectTransform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(RectTransform target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIAnchoredPosition3DZ(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(RectTransform target)
        {
            return target.anchoredPosition3D.z;
        }

        protected override void OnSet(RectTransform target, float updateValue)
        {
            target.anchoredPosition3D = target.anchoredPosition3D.WithComponent(2, (updateValue)) ;
        }
    }
}
