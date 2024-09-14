
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UIElements;
namespace Cr7Sund.ITransformTween
{
    [Serializable]
    public  class ITransform_RotationControlBehaviour : BaseControlBehaviour<UnityEngine.UIElements.ITransform, Quaternion>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UIElements.ITransform target, double duration, Quaternion startValue)
        {
            return PrimeTween.Tween.Rotation(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UIElements.ITransform target)
        {
            return target.rotation;
        }

        protected override void OnSet(UnityEngine.UIElements.ITransform target, Quaternion updateValue)
        {
           target.rotation = updateValue;
        }
    }
}
