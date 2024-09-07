
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class AudioSourcePitchControlBehaviour : BaseControlBehaviour<AudioSource, float>
    {
        protected override PrimeTween.Tween OnCreateTween(AudioSource target, double duration, float startValue)
        {
            return PrimeTween.Tween.AudioPitch(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(AudioSource target)
        {
            return target.pitch;
        }

        protected override void OnSet(AudioSource target, float updateValue)
        {
            target.pitch = updateValue;
        }
    }
}
