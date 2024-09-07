
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class TransformLocalRotationControlBehaviour : BaseControlBehaviour<Transform, Quaternion>
    {
        protected override PrimeTween.Tween OnCreateTween(Transform target, double duration, Quaternion startValue)
        {
            return PrimeTween.Tween.LocalRotation(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Transform target)
        {
            return target.localRotation;
        }

        protected override void OnSet(Transform target, Quaternion updateValue)
        {
            target.localRotation = updateValue;
        }
    }
}
