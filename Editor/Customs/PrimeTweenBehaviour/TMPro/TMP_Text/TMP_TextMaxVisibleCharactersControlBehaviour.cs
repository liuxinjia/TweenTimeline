
using System;
using TMPro;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class TMP_TextMaxVisibleCharactersControlBehaviour : BaseControlBehaviour<TMP_Text, int>
    {
        protected override PrimeTween.Tween OnCreateTween(TMP_Text target, double duration, int startValue)
        {
            return PrimeTween.Tween.TextMaxVisibleCharacters(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(TMP_Text target)
        {
            return target.maxVisibleCharacters;
        }

        protected override void OnSet(TMP_Text target, int updateValue)
        {
            target.maxVisibleCharacters = updateValue;
        }
    }
}
