
using System;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class ImageFillAmountControlBehaviour : BaseControlBehaviour<Image, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Image target, double duration, float startValue)
        {
            return PrimeTween.Tween.UIFillAmount(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Image target)
        {
            return target.fillAmount;
        }

        protected override void OnSet(Image target, float updateValue)
        {
            target.fillAmount = updateValue;
        }
    }
}
