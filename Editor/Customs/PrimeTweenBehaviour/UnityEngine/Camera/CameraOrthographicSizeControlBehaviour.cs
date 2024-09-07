
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class CameraOrthographicSizeControlBehaviour : BaseControlBehaviour<Camera, float>
    {
        protected override PrimeTween.Tween OnCreateTween(Camera target, double duration, float startValue)
        {
            return PrimeTween.Tween.CameraOrthographicSize(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Camera target)
        {
            return target.orthographicSize;
        }

        protected override void OnSet(Camera target, float updateValue)
        {
            target.orthographicSize = updateValue;
        }
    }
}
