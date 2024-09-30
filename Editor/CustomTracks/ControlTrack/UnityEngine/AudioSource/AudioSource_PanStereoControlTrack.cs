
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.AudioSourceTween
{
    [TrackClipType(typeof(AudioSource_PanStereoControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.AudioSource))]
    [TrackColor(0.883f, 0.785f, 0.767f)]
    public class AudioSource_PanStereoControlTrack : TrackAsset,IBaseTrack
    {

    }
}
