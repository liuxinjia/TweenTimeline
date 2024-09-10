
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RigidbodyTweeen
{
    [Serializable]
    public  class Rigidbody_RotationControlBehaviour : BaseControlBehaviour<UnityEngine.Rigidbody, Quaternion>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Rigidbody target, double duration, Quaternion startValue)
        {
            return PrimeTween.Tween.RigidbodyMoveRotation(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Rigidbody target)
        {
            return target.rotation;
        }

        protected override void OnSet(UnityEngine.Rigidbody target, Quaternion updateValue)
        {
            target.MoveRotation((updateValue)) ;
        }
    }
}
