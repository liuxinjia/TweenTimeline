
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [Serializable]
    public  class Transform_LocalScaleXControlBehaviour : BaseControlBehaviour<UnityEngine.Transform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Transform target, double duration, float startValue)
        {
            return PrimeTween.Tween.ScaleX(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Transform target)
        {
            return target.localScale.x;
        }

        protected override void OnSet(UnityEngine.Transform target, float updateValue)
        {
           target.localScale = target.localScale.WithComponent(0, (updateValue)) ;
        }
    }
}
