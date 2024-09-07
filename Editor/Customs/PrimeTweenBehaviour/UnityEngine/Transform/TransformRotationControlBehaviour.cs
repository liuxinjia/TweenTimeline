
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class TransformRotationControlBehaviour : BaseControlBehaviour<Transform, Vector3>
    {
        protected override PrimeTween.Tween OnCreateTween(Transform target, double duration, Vector3 startValue)
        {
            return PrimeTween.Tween.Rotation(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Transform target)
        {
            return target.rotation;
        }

        protected override void OnSet(Transform target, Vector3 updateValue)
        {
            target.rotation = Quaternion.Euler(updateValue);
        }
    }
}
