
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.Rigidbody2DTweeen
{
    [Serializable]
    public  class Rigidbody2D_PositionControlBehaviour : BaseControlBehaviour<UnityEngine.Rigidbody2D, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Rigidbody2D target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.RigidbodyMovePosition(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Rigidbody2D target)
        {
            return target.position;
        }

        protected override void OnSet(UnityEngine.Rigidbody2D target, Vector2 updateValue)
        {
            target.MovePosition((updateValue)) ;
        }
    }
}
