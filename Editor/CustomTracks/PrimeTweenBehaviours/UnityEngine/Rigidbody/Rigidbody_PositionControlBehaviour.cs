
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RigidbodyTween
{
    [Serializable]
    public  class Rigidbody_PositionControlBehaviour : BaseControlBehaviour<UnityEngine.Rigidbody, Vector3>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Rigidbody target, double duration, Vector3 startValue)
        {
            return PrimeTween.Tween.RigidbodyMovePosition(target, startValue: startValue,
                  ease: PrimEase, endValue: (Vector3)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Rigidbody target)
        {
            return target.position;
        }

        protected override void OnSet(UnityEngine.Rigidbody target, Vector3 updateValue)
        {
           target.MovePosition((updateValue)) ;
        }
    }
}
