
using System;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class SliderValueControlBehaviour : BaseControlBehaviour<Slider, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Slider target, double duration, float startValue)
        {
            return PrimeTween.Tween.UISliderValue(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Slider target)
        {
            return target.value;
        }

        protected override void OnSet(Slider target, float updateValue)
        {
            target.value = updateValue;
        }
    }
}
