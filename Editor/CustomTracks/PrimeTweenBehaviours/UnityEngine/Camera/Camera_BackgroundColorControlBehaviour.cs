
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [Serializable]
    public  class Camera_BackgroundColorControlBehaviour : BaseControlBehaviour<UnityEngine.Camera, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Camera target, double duration, Color startValue)
        {
            return PrimeTween.Tween.CameraBackgroundColor(target, startValue: startValue,
                  ease: PrimEase, endValue: (Color)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Camera target)
        {
            return target.backgroundColor;
        }

        protected override void OnSet(UnityEngine.Camera target, Color updateValue)
        {
           target.backgroundColor = updateValue;
        }
    }
}
