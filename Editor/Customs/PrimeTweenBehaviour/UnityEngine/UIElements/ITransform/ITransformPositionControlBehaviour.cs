
using System;
using UnityEngine.UIElements;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public class ITransformPositionControlBehaviour : BaseControlBehaviour<ITransform, Vector3>
    {
        protected override PrimeTween.Tween OnCreateTween(ITransform target, double duration, Vector3 startValue)
        {
            return PrimeTween.Tween.Position(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(ITransform target)
        {
            return target.position;
        }

        protected override void OnSet(ITransform target, Vector3 updateValue)
        {
            target.position = updateValue;
        }
    }
}
