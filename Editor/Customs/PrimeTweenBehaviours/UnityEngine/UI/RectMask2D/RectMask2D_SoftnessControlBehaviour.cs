
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.RectMask2DTweeen
{
    [Serializable]
    public  class RectMask2D_SoftnessControlBehaviour : BaseControlBehaviour<UnityEngine.UI.RectMask2D, Vector2Int>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.RectMask2D target, double duration, Vector2Int startValue)
        {
            return PrimeTween.Tween.RectMaskSoftness(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.RectMask2D target)
        {
            return target.softness;
        }

        protected override void OnSet(UnityEngine.UI.RectMask2D target, Vector2Int updateValue)
        {
            target.softness = updateValue;
        }
    }
}
