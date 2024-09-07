
using System;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public class RectMask2DSoftnessControlBehaviour : BaseControlBehaviour<RectMask2D, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(RectMask2D target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.RectMaskSoftness(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(RectMask2D target)
        {
            return new Vector2(target.softness.x, target.softness.y);
        }

        protected override void OnSet(RectMask2D target, Vector2 updateValue)
        {
            target.softness = new UnityEngine.Vector2Int((int)updateValue.x, (int)updateValue.y); ;
        }
    }
}
