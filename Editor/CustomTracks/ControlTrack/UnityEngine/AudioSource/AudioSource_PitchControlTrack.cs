
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.AudioSourceTween
{
    [TrackClipType(typeof(AudioSource_PitchControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.AudioSource))]
    [TrackColor(0.043f, 0.422f, 0.382f)]
    public class AudioSource_PitchControlTrack : TrackAsset,IBaseTrack
    {

    }
}
