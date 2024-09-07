
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class Rigidbody2DPositionControlBehaviour : BaseControlBehaviour<Rigidbody2D, Vector2>
    {
        protected override PrimeTween.Tween OnCreateTween(Rigidbody2D target, double duration, Vector2 startValue)
        {
            return PrimeTween.Tween.RigidbodyMovePosition(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Rigidbody2D target)
        {
            return target.position;
        }

        protected override void OnSet(Rigidbody2D target, Vector2 updateValue)
        {
            target.MovePosition((updateValue)) ;
        }
    }
}
