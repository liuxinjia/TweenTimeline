
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.RectMask2DTween
{
    [Serializable]
    public  class RectMask2D_PaddingControlBehaviour : BaseControlBehaviour<UnityEngine.UI.RectMask2D, Vector4>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.RectMask2D target, double duration, Vector4 startValue)
        {
            return PrimeTween.Tween.Custom(target, startValue: startValue,
                  ease: PrimEase, endValue: (Vector4)EndPos, duration: (float)duration, 
                  onValueChange: (target, updateValue) => target.padding = updateValue);
        }

        protected override object OnGet(UnityEngine.UI.RectMask2D target)
        {
            return target.padding;
        }

        protected override void OnSet(UnityEngine.UI.RectMask2D target, Vector4 updateValue)
        {
           target.padding = updateValue;
        }
    }
}
