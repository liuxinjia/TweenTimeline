
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.SpriteRendererTweeen
{
    [Serializable]
    public  class SpriteRenderer_ColorControlBehaviour : BaseControlBehaviour<UnityEngine.SpriteRenderer, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.SpriteRenderer target, double duration, Color startValue)
        {
            return PrimeTween.Tween.Color(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.SpriteRenderer target)
        {
            return target.color;
        }

        protected override void OnSet(UnityEngine.SpriteRenderer target, Color updateValue)
        {
            target.color = updateValue;
        }
    }
}
