
using System;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public  class AudioSourceVolumeControlBehaviour : BaseControlBehaviour<AudioSource, float>
    {
        protected override PrimeTween.Tween OnCreateTween(AudioSource target, double duration, float startValue)
        {
            return PrimeTween.Tween.AudioVolume(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }

        protected override object OnGet(AudioSource target)
        {
            return target.volume;
        }

        protected override void OnSet(AudioSource target, float updateValue)
        {
            target.volume = updateValue;
        }
    }
}
