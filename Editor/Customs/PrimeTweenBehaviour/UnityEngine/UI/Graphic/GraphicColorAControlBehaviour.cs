
using System;
using UnityEngine.UI;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class GraphicColorAControlBehaviour : BaseControlBehaviour<Graphic, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Graphic target, double duration, float startValue)
        {
            return  PrimeTween.Tween.Alpha(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Graphic target)
        {
            return target.color.a;
        }

        protected override void OnSet(Graphic target, float updateValue)
        {
            target.color = target.color.WithAlpha((updateValue)) ;
        }
    }
}
