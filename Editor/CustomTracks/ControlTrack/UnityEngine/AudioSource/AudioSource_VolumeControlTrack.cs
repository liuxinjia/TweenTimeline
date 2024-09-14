
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.AudioSourceTween
{
    [TrackClipType(typeof(AudioSource_VolumeControlAsset))]
    [TrackBindingType(typeof(UnityEngine.AudioSource))]
    public class AudioSource_VolumeControlTrack : TrackAsset,IBaseTrack
    {

    }
}
