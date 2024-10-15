
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.SliderTween
{
    [Serializable]
    public  class Slider_ValueControlBehaviour : BaseControlBehaviour<UnityEngine.UI.Slider, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.Slider target, double duration, float startValue)
        {
            return PrimeTween.Tween.UISliderValue(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.Slider target)
        {
            return target.value;
        }

        protected override void OnSet(UnityEngine.UI.Slider target, float updateValue)
        {
           target.value = updateValue;
        }
    }
}
