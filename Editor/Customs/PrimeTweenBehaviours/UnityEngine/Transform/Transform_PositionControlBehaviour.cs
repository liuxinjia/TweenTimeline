
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTweeen
{
    [Serializable]
    public  class Transform_PositionControlBehaviour : BaseControlBehaviour<UnityEngine.Transform, Vector3>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Transform target, double duration, Vector3 startValue)
        {
            return PrimeTween.Tween.Position(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Transform target)
        {
            return target.position;
        }

        protected override void OnSet(UnityEngine.Transform target, Vector3 updateValue)
        {
            target.position = updateValue;
        }
    }
}
