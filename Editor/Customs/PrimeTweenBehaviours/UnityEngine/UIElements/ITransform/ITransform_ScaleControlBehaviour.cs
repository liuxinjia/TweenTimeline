
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UIElements;
namespace Cr7Sund.ITransformTweeen
{
    [Serializable]
    public  class ITransform_ScaleControlBehaviour : BaseControlBehaviour<UnityEngine.UIElements.ITransform, Vector3>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.UIElements.ITransform target, double duration, Vector3 startValue)
        {
            return PrimeTween.Tween.Scale(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.UIElements.ITransform target)
        {
            return target.scale;
        }

        protected override void OnSet(UnityEngine.UIElements.ITransform target, Vector3 updateValue)
        {
            target.scale = updateValue;
        }
    }
}
