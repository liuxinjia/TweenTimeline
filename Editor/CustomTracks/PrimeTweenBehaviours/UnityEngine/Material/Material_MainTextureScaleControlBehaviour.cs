
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.MaterialTween
{
    [Serializable]
    public  class Material_MainTextureScaleControlBehaviour : BaseControlBehaviour<UnityEngine.Material, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Material target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.MaterialMainTextureScale(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Material target)
        {
            return target.mainTextureScale;
        }

        protected override void OnSet(UnityEngine.Material target, Vector2 updateValue)
        {
           target.mainTextureScale = updateValue;
        }
    }
}
