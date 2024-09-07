
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class Rigidbody2DRotationControlBehaviour : BaseControlBehaviour<Rigidbody2D, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Rigidbody2D target, double duration, float startValue)
        {
            return PrimeTween.Tween.RigidbodyMoveRotation(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Rigidbody2D target)
        {
            return target.rotation;
        }

        protected override void OnSet(Rigidbody2D target, float updateValue)
        {
            target.MoveRotation((updateValue)) ;
        }
    }
}
