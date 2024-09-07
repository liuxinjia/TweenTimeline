
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class TransformPositionXControlBehaviour : BaseControlBehaviour<Transform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Transform target, double duration, float startValue)
        {
            return PrimeTween.Tween.PositionX(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Transform target)
        {
            return target.position.x;
        }

        protected override void OnSet(Transform target, float updateValue)
        {
            target.position = target.position.WithComponent(0, (updateValue)) ;
        }
    }
}
