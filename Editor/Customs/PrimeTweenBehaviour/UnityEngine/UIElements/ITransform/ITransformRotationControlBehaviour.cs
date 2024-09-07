
using System;
using UnityEngine.UIElements;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class ITransformRotationControlBehaviour : BaseControlBehaviour<ITransform, Quaternion>
    {
        protected override PrimeTween.Tween OnCreateTween(ITransform target, double duration, Quaternion startValue)
        {
            return PrimeTween.Tween.Rotation(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(ITransform target)
        {
            return target.rotation;
        }

        protected override void OnSet(ITransform target, Quaternion updateValue)
        {
            target.rotation = updateValue;
        }
    }
}
