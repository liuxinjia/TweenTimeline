
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class RigidbodyPositionControlBehaviour : BaseControlBehaviour<Rigidbody, Vector3>
    {
        protected override PrimeTween.Tween OnCreateTween(Rigidbody target, double duration, Vector3 startValue)
        {
            return PrimeTween.Tween.RigidbodyMovePosition(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Rigidbody target)
        {
            return target.position;
        }

        protected override void OnSet(Rigidbody target, Vector3 updateValue)
        {
            target.MovePosition((updateValue)) ;
        }
    }
}
