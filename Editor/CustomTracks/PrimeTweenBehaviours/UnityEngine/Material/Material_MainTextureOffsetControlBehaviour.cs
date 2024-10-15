
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.MaterialTween
{
    [Serializable]
    public  class Material_MainTextureOffsetControlBehaviour : BaseControlBehaviour<UnityEngine.Material, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Material target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.MaterialMainTextureOffset(target, startValue: startValue,
                  ease: PrimEase, endValue: (Vector2)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Material target)
        {
            return target.mainTextureOffset;
        }

        protected override void OnSet(UnityEngine.Material target, Vector2 updateValue)
        {
           target.mainTextureOffset = updateValue;
        }
    }
}
