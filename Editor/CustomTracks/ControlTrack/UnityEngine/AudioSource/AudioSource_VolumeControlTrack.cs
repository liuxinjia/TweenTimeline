
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.AudioSourceTween
{
    [TrackClipType(typeof(AudioSource_VolumeControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.AudioSource))]
    [TrackColor(0.579f, 0.978f, 0.649f)]
    public class AudioSource_VolumeControlTrack : TrackAsset,IBaseTrack
    {

    }
}
