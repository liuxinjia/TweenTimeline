
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.MaterialTween
{
    [Serializable]
    public  class Material_ColorAControlBehaviour : BaseControlBehaviour<UnityEngine.Material, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Material target, double duration, float startValue)
        {
            return PrimeTween.Tween.MaterialAlpha(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Material target)
        {
            return target.color.a;
        }

        protected override void OnSet(UnityEngine.Material target, float updateValue)
        {
           target.color = target.color.WithAlpha((updateValue)) ;
        }
    }
}
