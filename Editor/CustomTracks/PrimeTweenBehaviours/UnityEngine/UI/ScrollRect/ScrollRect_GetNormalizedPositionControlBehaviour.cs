
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ScrollRectTween
{
    [Serializable]
    public  class ScrollRect_GetNormalizedPositionControlBehaviour : BaseControlBehaviour<UnityEngine.UI.ScrollRect, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.ScrollRect target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.UINormalizedPosition(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.ScrollRect target)
        {
            return target.GetNormalizedPosition();
        }

        protected override void OnSet(UnityEngine.UI.ScrollRect target, Vector2 updateValue)
        {
           target.SetNormalizedPosition((updateValue)) ;
        }
    }
}
