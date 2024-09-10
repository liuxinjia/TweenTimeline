
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ImageTweeen
{
    [Serializable]
    public  class Image_FillAmountControlBehaviour : BaseControlBehaviour<UnityEngine.UI.Image, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.Image target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIFillAmount(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.Image target)
        {
            return target.fillAmount;
        }

        protected override void OnSet(UnityEngine.UI.Image target, float updateValue)
        {
            target.fillAmount = updateValue;
        }
    }
}
