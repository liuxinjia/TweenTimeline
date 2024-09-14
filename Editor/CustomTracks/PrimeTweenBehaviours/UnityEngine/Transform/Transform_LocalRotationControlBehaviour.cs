
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [Serializable]
    public  class Transform_LocalRotationControlBehaviour : BaseControlBehaviour<UnityEngine.Transform, Quaternion>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Transform target, double duration, Quaternion startValue)
        {
            return PrimeTween.Tween.LocalRotation(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Transform target)
        {
            return target.localRotation;
        }

        protected override void OnSet(UnityEngine.Transform target, Quaternion updateValue)
        {
           target.localRotation = updateValue;
        }
    }
}
