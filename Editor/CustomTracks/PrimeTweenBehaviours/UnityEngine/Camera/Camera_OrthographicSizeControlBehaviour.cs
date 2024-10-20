
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [Serializable]
    public  class Camera_OrthographicSizeControlBehaviour : BaseControlBehaviour<UnityEngine.Camera, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.Camera target, double duration, float startValue)
        {
            return PrimeTween.Tween.CameraOrthographicSize(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.Camera target)
        {
            return target.orthographicSize;
        }

        protected override void OnSet(UnityEngine.Camera target, float updateValue)
        {
           target.orthographicSize = updateValue;
        }
    }
}
