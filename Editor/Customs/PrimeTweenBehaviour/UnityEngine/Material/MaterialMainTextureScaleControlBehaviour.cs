
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class MaterialMainTextureScaleControlBehaviour : BaseControlBehaviour<Material, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(Material target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.MaterialMainTextureScale(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Material target)
        {
            return target.mainTextureScale;
        }

        protected override void OnSet(Material target, Vector2 updateValue)
        {
            target.mainTextureScale = updateValue;
        }
    }
}
