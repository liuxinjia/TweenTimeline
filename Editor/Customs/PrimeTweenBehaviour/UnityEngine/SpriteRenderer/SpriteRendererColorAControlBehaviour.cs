
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class SpriteRendererColorAControlBehaviour : BaseControlBehaviour<SpriteRenderer, float>
    {
        protected override PrimeTween.Tween OnCreateTween(SpriteRenderer target, double duration, float startValue)
        {
            return PrimeTween.Tween.Alpha(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(SpriteRenderer target)
        {
            return target.color.a;
        }

        protected override void OnSet(SpriteRenderer target, float updateValue)
        {
            target.color = target.color.WithAlpha((updateValue)) ;
        }
    }
}
