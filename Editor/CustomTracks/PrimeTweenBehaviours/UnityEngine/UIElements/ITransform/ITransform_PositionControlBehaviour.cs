
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UIElements;
namespace Cr7Sund.ITransformTween
{
    [Serializable]
    public  class ITransform_PositionControlBehaviour : BaseControlBehaviour<UnityEngine.UIElements.ITransform, Vector3>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UIElements.ITransform target, double duration, Vector3 startValue)
        {
            return PrimeTween.Tween.Position(target, startValue: startValue,
                  ease: PrimEase, endValue: (Vector3)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UIElements.ITransform target)
        {
            return target.position;
        }

        protected override void OnSet(UnityEngine.UIElements.ITransform target, Vector3 updateValue)
        {
           target.position = updateValue;
        }
    }
}
