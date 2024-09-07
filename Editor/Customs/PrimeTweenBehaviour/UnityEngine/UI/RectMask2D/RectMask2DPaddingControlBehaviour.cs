
using System;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public class RectMask2DPaddingControlBehaviour : BaseControlBehaviour<RectMask2D, Vector4>
    {
        protected override PrimeTween.Tween OnCreateTween(RectMask2D target, double duration, Vector4 startValue)
        {
            return PrimeTween.Tween.RectMaskPadding(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(RectMask2D target)
        {
            return target.padding;
        }

        protected override void OnSet(RectMask2D target, Vector4 updateValue)
        {
            target.padding = updateValue;
        }
    }
}
