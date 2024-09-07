
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class TransformLocalScaleControlBehaviour : BaseControlBehaviour<Transform, Vector3>
    {
        protected override PrimeTween.Tween OnCreateTween(Transform target, double duration, Vector3 startValue)
        {
            return PrimeTween.Tween.Scale(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Transform target)
        {
            return target.localScale;
        }

        protected override void OnSet(Transform target, Vector3 updateValue)
        {
            target.localScale = updateValue;
        }
    }
}
