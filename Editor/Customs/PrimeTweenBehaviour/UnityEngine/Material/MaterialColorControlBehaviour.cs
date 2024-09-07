
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class MaterialColorControlBehaviour : BaseControlBehaviour<Material, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(Material target, double duration, Color startValue)
        {
            return PrimeTween.Tween.MaterialColor(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Material target)
        {
            return target.color;
        }

        protected override void OnSet(Material target, Color updateValue)
        {
            target.color = updateValue;
        }
    }
}
