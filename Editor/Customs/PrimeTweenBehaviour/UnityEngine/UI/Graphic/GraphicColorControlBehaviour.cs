
using System;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class GraphicColorControlBehaviour : BaseControlBehaviour<Graphic, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(Graphic target, double duration, Color startValue)
        {
            return PrimeTween.Tween.Color(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Graphic target)
        {
            return target.color;
        }

        protected override void OnSet(Graphic target, Color updateValue)
        {
            target.color = updateValue;
        }
    }
}
