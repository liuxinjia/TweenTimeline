
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class RigidbodyRotationControlBehaviour : BaseControlBehaviour<Rigidbody, Quaternion>
    {
        protected override PrimeTween.Tween OnCreateTween(Rigidbody target, double duration, Quaternion startValue)
        {
            return PrimeTween.Tween.RigidbodyMoveRotation(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Rigidbody target)
        {
            return target.rotation;
        }

        protected override void OnSet(Rigidbody target, Quaternion updateValue)
        {
            target.MoveRotation((updateValue)) ;
        }
    }
}
