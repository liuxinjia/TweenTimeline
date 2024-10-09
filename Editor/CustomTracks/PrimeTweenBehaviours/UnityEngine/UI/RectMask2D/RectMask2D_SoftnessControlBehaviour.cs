
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.RectMask2DTween
{
    [Serializable]
    public  class RectMask2D_SoftnessControlBehaviour : BaseControlBehaviour<UnityEngine.UI.RectMask2D, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.RectMask2D target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.Custom(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration, 
                  onValueChange: (target, updateValue) => target.softness = new Vector2Int((int)updateValue.x, (int)updateValue.y));
        }

        protected override object OnGet(UnityEngine.UI.RectMask2D target)
        {
            return target.softness;
        }

        protected override void OnSet(UnityEngine.UI.RectMask2D target, Vector2 updateValue)
        {
           target.softness = new Vector2Int((int)updateValue.x, (int)updateValue.y);
        }
    }
}
