
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class TransformPositionZControlBehaviour : BaseControlBehaviour<Transform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Transform target, double duration, float startValue)
        {
            return PrimeTween.Tween.PositionZ(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Transform target)
        {
            return target.position.z;
        }

        protected override void OnSet(Transform target, float updateValue)
        {
            target.position = target.position.WithComponent(2, (updateValue)) ;
        }
    }
}
