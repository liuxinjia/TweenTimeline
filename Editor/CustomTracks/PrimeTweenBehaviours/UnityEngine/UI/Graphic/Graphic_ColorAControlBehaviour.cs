
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.GraphicTween
{
    [Serializable]
    public  class Graphic_ColorAControlBehaviour : BaseControlBehaviour<UnityEngine.UI.Graphic, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UI.Graphic target, double duration, float startValue)
        {
            return PrimeTween.Tween.Alpha(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UI.Graphic target)
        {
            return target.color.a;
        }

        protected override void OnSet(UnityEngine.UI.Graphic target, float updateValue)
        {
           target.color = target.color.WithAlpha((updateValue)) ;
        }
    }
}
