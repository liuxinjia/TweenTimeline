
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [Serializable]
    public  class Camera_RectControlBehaviour : BaseControlBehaviour<UnityEngine.Camera, Rect>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Camera target, double duration, Rect startValue)
        {
            return PrimeTween.Tween.CameraRect(target, startValue: startValue,
                  ease: PrimEase, endValue: (Rect)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Camera target)
        {
            return target.rect;
        }

        protected override void OnSet(UnityEngine.Camera target, Rect updateValue)
        {
           target.rect = updateValue;
        }
    }
}
