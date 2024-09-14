
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ScrollRectTween
{
    [Serializable]
    public  class ScrollRect_VerticalNormalizedPositionControlBehaviour : BaseControlBehaviour<UnityEngine.UI.ScrollRect, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.ScrollRect target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIVerticalNormalizedPosition(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.ScrollRect target)
        {
            return target.verticalNormalizedPosition;
        }

        protected override void OnSet(UnityEngine.UI.ScrollRect target, float updateValue)
        {
           target.verticalNormalizedPosition = updateValue;
        }
    }
}
