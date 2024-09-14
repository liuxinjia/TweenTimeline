
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [Serializable]
    public  class Transform_RotationControlBehaviour : BaseControlBehaviour<UnityEngine.Transform, Quaternion>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Transform target, double duration, Quaternion startValue)
        {
            return PrimeTween.Tween.Rotation(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Transform target)
        {
            return target.rotation;
        }

        protected override void OnSet(UnityEngine.Transform target, Quaternion updateValue)
        {
           target.rotation = updateValue;
        }
    }
}
