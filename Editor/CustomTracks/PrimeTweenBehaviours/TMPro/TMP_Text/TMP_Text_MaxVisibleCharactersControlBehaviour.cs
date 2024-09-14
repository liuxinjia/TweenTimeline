
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using TMPro;
namespace Cr7Sund.TMP_TextTween
{
    [Serializable]
    public  class TMP_Text_MaxVisibleCharactersControlBehaviour : BaseControlBehaviour<TMPro.TMP_Text, int>
    {
        protected override PrimeTween.Tween OnCreateTween(TMPro.TMP_Text target, double duration, int startValue)
        {
            return PrimeTween.Tween.TextMaxVisibleCharacters(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(TMPro.TMP_Text target)
        {
            return target.maxVisibleCharacters;
        }

        protected override void OnSet(TMPro.TMP_Text target, int updateValue)
        {
           target.maxVisibleCharacters = updateValue;
        }
    }
}
