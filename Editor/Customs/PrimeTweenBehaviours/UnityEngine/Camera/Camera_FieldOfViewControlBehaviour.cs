
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTweeen
{
    [Serializable]
    public  class Camera_FieldOfViewControlBehaviour : BaseControlBehaviour<UnityEngine.Camera, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Camera target, double duration, float startValue)
        {
            return PrimeTween.Tween.CameraFieldOfView(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Camera target)
        {
            return target.fieldOfView;
        }

        protected override void OnSet(UnityEngine.Camera target, float updateValue)
        {
            target.fieldOfView = updateValue;
        }
    }
}
