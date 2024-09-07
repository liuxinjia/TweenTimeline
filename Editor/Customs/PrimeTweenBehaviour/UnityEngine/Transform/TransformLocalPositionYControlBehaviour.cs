
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class TransformLocalPositionYControlBehaviour : BaseControlBehaviour<Transform, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Transform target, double duration, float startValue)
        {
            return PrimeTween.Tween.LocalPositionY(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Transform target)
        {
            return target.localPosition.y;
        }

        protected override void OnSet(Transform target, float updateValue)
        {
            target.localPosition = target.localPosition.WithComponent(1, (updateValue)) ;
        }
    }
}
