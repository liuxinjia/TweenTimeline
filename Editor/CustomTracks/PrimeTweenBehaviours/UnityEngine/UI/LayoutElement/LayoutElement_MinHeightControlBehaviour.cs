
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.LayoutElementTween
{
    [Serializable]
    public  class LayoutElement_MinHeightControlBehaviour : BaseControlBehaviour<UnityEngine.UI.LayoutElement, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.LayoutElement target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIMinHeight(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.LayoutElement target)
        {
            return target.minHeight;
        }

        protected override void OnSet(UnityEngine.UI.LayoutElement target, float updateValue)
        {
           target.minHeight = updateValue;
        }
    }
}
