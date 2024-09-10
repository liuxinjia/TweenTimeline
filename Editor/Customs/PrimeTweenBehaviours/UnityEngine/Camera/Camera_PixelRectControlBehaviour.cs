
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTweeen
{
    [Serializable]
    public  class Camera_PixelRectControlBehaviour : BaseControlBehaviour<UnityEngine.Camera, Rect>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Camera target, double duration, Rect startValue)
        {
            return PrimeTween.Tween.CameraPixelRect(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Camera target)
        {
            return target.pixelRect;
        }

        protected override void OnSet(UnityEngine.Camera target, Rect updateValue)
        {
            target.pixelRect = updateValue;
        }
    }
}
