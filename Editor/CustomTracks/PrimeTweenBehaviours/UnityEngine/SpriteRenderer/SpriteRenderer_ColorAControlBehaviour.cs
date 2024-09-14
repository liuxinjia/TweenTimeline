
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.SpriteRendererTween
{
    [Serializable]
    public  class SpriteRenderer_ColorAControlBehaviour : BaseControlBehaviour<UnityEngine.SpriteRenderer, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.SpriteRenderer target, double duration, float startValue)
        {
            return PrimeTween.Tween.Alpha(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.SpriteRenderer target)
        {
            return target.color.a;
        }

        protected override void OnSet(UnityEngine.SpriteRenderer target, float updateValue)
        {
           target.color = target.color.WithAlpha((updateValue)) ;
        }
    }
}
