
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.MaterialTweeen
{
    [Serializable]
    public  class Material_ColorControlBehaviour : BaseControlBehaviour<UnityEngine.Material, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Material target, double duration, Color startValue)
        {
            return PrimeTween.Tween.MaterialColor(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Material target)
        {
            return target.color;
        }

        protected override void OnSet(UnityEngine.Material target, Color updateValue)
        {
            target.color = updateValue;
        }
    }
}
