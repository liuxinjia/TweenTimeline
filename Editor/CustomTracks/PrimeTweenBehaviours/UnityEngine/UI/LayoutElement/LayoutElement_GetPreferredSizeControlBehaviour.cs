
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.LayoutElementTween
{
    [Serializable]
    public  class LayoutElement_GetPreferredSizeControlBehaviour : BaseControlBehaviour<UnityEngine.UI.LayoutElement, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.LayoutElement target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.UIPreferredSize(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.LayoutElement target)
        {
            return target.GetPreferredSize();
        }

        protected override void OnSet(UnityEngine.UI.LayoutElement target, Vector2 updateValue)
        {
           target.SetPreferredSize((updateValue)) ;
        }
    }
}
