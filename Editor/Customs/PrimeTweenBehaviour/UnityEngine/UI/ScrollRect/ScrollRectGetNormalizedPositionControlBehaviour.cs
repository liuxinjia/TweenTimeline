
using System;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class ScrollRectGetNormalizedPositionControlBehaviour : BaseControlBehaviour<ScrollRect, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(ScrollRect target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.UINormalizedPosition(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(ScrollRect target)
        {
            return target.GetNormalizedPosition();
        }

        protected override void OnSet(ScrollRect target, Vector2 updateValue)
        {
            target.SetNormalizedPosition((updateValue)) ;
        }
    }
}
