
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTweeen
{
    [Serializable]
    public  class Transform_PositionYControlBehaviour : BaseControlBehaviour<UnityEngine.Transform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Transform target, double duration, float startValue)
        {
            return PrimeTween.Tween.PositionY(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Transform target)
        {
            return target.position.y;
        }

        protected override void OnSet(UnityEngine.Transform target, float updateValue)
        {
            target.position = target.position.WithComponent(1, (updateValue)) ;
        }
    }
}
