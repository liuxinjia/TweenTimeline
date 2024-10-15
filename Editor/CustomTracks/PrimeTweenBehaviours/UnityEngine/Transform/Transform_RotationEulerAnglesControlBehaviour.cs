
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [Serializable]
    public  class Transform_RotationEulerAnglesControlBehaviour : BaseControlBehaviour<UnityEngine.Transform, Vector3>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Transform target, double duration, Vector3 startValue)
        {
            return PrimeTween.Tween.Rotation(target, startValue: startValue,
                  ease: PrimEase, endValue: (Vector3)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Transform target)
        {
            return target.rotation.eulerAngles;
        }

        protected override void OnSet(UnityEngine.Transform target, Vector3 updateValue)
        {
           target.rotation = Quaternion.Euler(updateValue);
        }
    }
}
