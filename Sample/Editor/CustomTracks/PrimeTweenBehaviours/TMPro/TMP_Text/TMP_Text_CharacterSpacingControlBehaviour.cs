
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using TMPro;
namespace Cr7Sund.TMP_TextTween
{
    [Serializable]
    public  class TMP_Text_CharacterSpacingControlBehaviour : BaseControlBehaviour<TMPro.TMP_Text, float>
    {
        protected override PrimeTween.Tween OnCreateTween(TMPro.TMP_Text target, double duration, float startValue)
        {
            return PrimeTween.Tween.Custom(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration, 
                  onValueChange: (target, updateValue) => target.characterSpacing = updateValue);
        }

        protected override object OnGet(TMPro.TMP_Text target)
        {
            return target.characterSpacing;
        }

        protected override void OnSet(TMPro.TMP_Text target, float updateValue)
        {
           target.characterSpacing = updateValue;
        }
    }
}
