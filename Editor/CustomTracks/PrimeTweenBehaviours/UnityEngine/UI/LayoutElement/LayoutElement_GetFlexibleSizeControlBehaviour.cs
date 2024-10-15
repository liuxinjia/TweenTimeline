
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.LayoutElementTween
{
    [Serializable]
    public  class LayoutElement_GetFlexibleSizeControlBehaviour : BaseControlBehaviour<UnityEngine.UI.LayoutElement, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.LayoutElement target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.UIFlexibleSize(target, startValue: startValue,
                  ease: PrimEase, endValue: (Vector2)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.LayoutElement target)
        {
            return target.GetFlexibleSize();
        }

        protected override void OnSet(UnityEngine.UI.LayoutElement target, Vector2 updateValue)
        {
           target.SetFlexibleSize((updateValue)) ;
        }
    }
}
