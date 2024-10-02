
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using TMPro;
namespace Cr7Sund.TMP_TextTween
{
    [Serializable]
    public  class TMP_Text_FontSizeControlBehaviour : BaseControlBehaviour<TMPro.TMP_Text, float>
    {
        protected override PrimeTween.Tween OnCreateTween(TMPro.TMP_Text target, double duration, float startValue)
        {
            return PrimeTween.Tween.Custom(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration, 
                  onValueChange: (target, updateValue) => target.fontSize = updateValue);
        }

        protected override object OnGet(TMPro.TMP_Text target)
        {
            return target.fontSize;
        }

        protected override void OnSet(TMPro.TMP_Text target, float updateValue)
        {
           target.fontSize = updateValue;
        }
    }
}
