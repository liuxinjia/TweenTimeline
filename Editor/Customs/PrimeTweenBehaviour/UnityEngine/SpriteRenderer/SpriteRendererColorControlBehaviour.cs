
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class SpriteRendererColorControlBehaviour : BaseControlBehaviour<SpriteRenderer, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(SpriteRenderer target, double duration, Color startValue)
        {
            return PrimeTween.Tween.Color(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(SpriteRenderer target)
        {
            return target.color;
        }

        protected override void OnSet(SpriteRenderer target, Color updateValue)
        {
            target.color = updateValue;
        }
    }
}
