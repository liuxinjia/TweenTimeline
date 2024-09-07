
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class MaterialColorAControlBehaviour : BaseControlBehaviour<Material, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Material target, double duration, float startValue)
        {
            return PrimeTween.Tween.MaterialAlpha(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Material target)
        {
            return target.color.a;
        }

        protected override void OnSet(Material target, float updateValue)
        {
            target.color = target.color.WithAlpha((updateValue)) ;
        }
    }
}
