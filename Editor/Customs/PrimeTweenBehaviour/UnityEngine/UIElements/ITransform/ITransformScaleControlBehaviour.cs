
using System;
using UnityEngine.UIElements;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class ITransformScaleControlBehaviour : BaseControlBehaviour<ITransform, Vector3>
    {
        protected override PrimeTween.Tween OnCreateTween(ITransform target, double duration, Vector3 startValue)
        {
            return PrimeTween.Tween.Scale(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(ITransform target)
        {
            return target.scale;
        }

        protected override void OnSet(ITransform target, Vector3 updateValue)
        {
            target.scale = updateValue;
        }
    }
}
