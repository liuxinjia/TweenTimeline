
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.AudioSourceTween
{
    [Serializable]
    public  class AudioSource_PitchControlBehaviour : BaseControlBehaviour<UnityEngine.AudioSource, float>
    {
        protected override PrimeTween.Tween OnCreateTween(UnityEngine.AudioSource target, double duration, float startValue)
        {
            return PrimeTween.Tween.AudioPitch(target, startValue: startValue,
                  ease: PrimEase, endValue: (float)EndPos, duration: (float)duration);
        }

        protected override object OnGet(UnityEngine.AudioSource target)
        {
            return target.pitch;
        }

        protected override void OnSet(UnityEngine.AudioSource target, float updateValue)
        {
           target.pitch = updateValue;
        }
    }
}
