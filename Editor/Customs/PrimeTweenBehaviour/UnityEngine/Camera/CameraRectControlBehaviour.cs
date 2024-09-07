
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class CameraRectControlBehaviour : BaseControlBehaviour<Camera, Rect>
    {
        protected override PrimeTween.Tween OnCreateTween(Camera target, double duration, Rect startValue)
        {
            return PrimeTween.Tween.CameraRect(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(Camera target)
        {
            return target.rect;
        }

        protected override void OnSet(Camera target, Rect updateValue)
        {
            target.rect = updateValue;
        }
    }
}
