
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.Rigidbody2DTween
{
    [Serializable]
    public  class Rigidbody2D_RotationControlBehaviour : BaseControlBehaviour<UnityEngine.Rigidbody2D, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Rigidbody2D target, double duration, float startValue)
        {
            return PrimeTween.Tween.RigidbodyMoveRotation(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Rigidbody2D target)
        {
            return target.rotation;
        }

        protected override void OnSet(UnityEngine.Rigidbody2D target, float updateValue)
        {
           target.MoveRotation((updateValue)) ;
        }
    }
}
