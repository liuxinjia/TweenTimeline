
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.GraphicTween
{
    [Serializable]
    public  class Graphic_ColorControlBehaviour : BaseControlBehaviour<UnityEngine.UI.Graphic, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.Graphic target, double duration, Color startValue)
        {
            return PrimeTween.Tween.Color(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.Graphic target)
        {
            return target.color;
        }

        protected override void OnSet(UnityEngine.UI.Graphic target, Color updateValue)
        {
           target.color = updateValue;
        }
    }
}
