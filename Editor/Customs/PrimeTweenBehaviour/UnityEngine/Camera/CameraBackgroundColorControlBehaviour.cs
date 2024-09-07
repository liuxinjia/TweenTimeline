
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class CameraBackgroundColorControlBehaviour : BaseControlBehaviour<Camera, Color>
    {
        protected override PrimeTween.Tween OnCreateTween(Camera target, double duration, Color startValue)
        {
            return PrimeTween.Tween.CameraBackgroundColor(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Camera target)
        {
            return target.backgroundColor;
        }

        protected override void OnSet(Camera target, Color updateValue)
        {
            target.backgroundColor = updateValue;
        }
    }
}
