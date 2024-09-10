
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTweeen
{
    [Serializable]
    public  class Transform_PositionZControlBehaviour : BaseControlBehaviour<UnityEngine.Transform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Transform target, double duration, float startValue)
        {
            return PrimeTween.Tween.PositionZ(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Transform target)
        {
            return target.position.z;
        }

        protected override void OnSet(UnityEngine.Transform target, float updateValue)
        {
            target.position = target.position.WithComponent(2, (updateValue)) ;
        }
    }
}
