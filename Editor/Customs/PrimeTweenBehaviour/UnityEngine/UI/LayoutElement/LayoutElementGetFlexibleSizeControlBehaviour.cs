
using System;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class LayoutElementGetFlexibleSizeControlBehaviour : BaseControlBehaviour<LayoutElement, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(LayoutElement target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.UIFlexibleSize(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(LayoutElement target)
        {
            return target.GetFlexibleSize();
        }

        protected override void OnSet(LayoutElement target, Vector2 updateValue)
        {
            target.SetFlexibleSize((updateValue)) ;
        }
    }
}
