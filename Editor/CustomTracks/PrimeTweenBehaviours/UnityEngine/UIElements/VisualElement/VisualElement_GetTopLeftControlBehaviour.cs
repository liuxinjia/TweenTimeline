
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UIElements;
namespace Cr7Sund.VisualElementTween
{
    [Serializable]
    public  class VisualElement_GetTopLeftControlBehaviour : BaseControlBehaviour<UnityEngine.UIElements.VisualElement, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UIElements.VisualElement target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.VisualElementTopLeft(target, startValue: startValue,
                  ease: PrimEase, endValue: (Vector2)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UIElements.VisualElement target)
        {
            return target.GetTopLeft();
        }

        protected override void OnSet(UnityEngine.UIElements.VisualElement target, Vector2 updateValue)
        {
           target.SetTopLeft((updateValue)) ;
        }
    }
}
