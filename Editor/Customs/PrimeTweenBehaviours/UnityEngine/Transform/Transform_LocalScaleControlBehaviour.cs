
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTweeen
{
    [Serializable]
    public  class Transform_LocalScaleControlBehaviour : BaseControlBehaviour<UnityEngine.Transform, Vector3>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Transform target, double duration, Vector3 startValue)
        {
            return PrimeTween.Tween.Scale(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Transform target)
        {
            return target.localScale;
        }

        protected override void OnSet(UnityEngine.Transform target, Vector3 updateValue)
        {
            target.localScale = updateValue;
        }
    }
}
