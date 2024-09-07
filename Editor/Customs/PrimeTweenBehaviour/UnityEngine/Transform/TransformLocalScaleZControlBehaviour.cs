
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class TransformLocalScaleZControlBehaviour : BaseControlBehaviour<Transform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Transform target, double duration, float startValue)
        {
            return PrimeTween.Tween.ScaleZ(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Transform target)
        {
            return target.localScale.z;
        }

        protected override void OnSet(Transform target, float updateValue)
        {
            target.localScale = target.localScale.WithComponent(2, (updateValue)) ;
        }
    }
}
