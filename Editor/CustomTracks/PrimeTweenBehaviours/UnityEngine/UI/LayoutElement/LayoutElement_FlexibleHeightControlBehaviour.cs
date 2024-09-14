
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.LayoutElementTween
{
    [Serializable]
    public  class LayoutElement_FlexibleHeightControlBehaviour : BaseControlBehaviour<UnityEngine.UI.LayoutElement, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.LayoutElement target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIFlexibleHeight(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.LayoutElement target)
        {
            return target.flexibleHeight;
        }

        protected override void OnSet(UnityEngine.UI.LayoutElement target, float updateValue)
        {
           target.flexibleHeight = updateValue;
        }
    }
}
