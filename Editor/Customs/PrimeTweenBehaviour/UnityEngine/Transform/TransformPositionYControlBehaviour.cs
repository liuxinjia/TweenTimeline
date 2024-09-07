
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class TransformPositionYControlBehaviour : BaseControlBehaviour<Transform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Transform target, double duration, float startValue)
        {
            return PrimeTween.Tween.PositionY(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Transform target)
        {
            return target.position.y;
        }

        protected override void OnSet(Transform target, float updateValue)
        {
            target.position = target.position.WithComponent(1, (updateValue)) ;
        }
    }
}
