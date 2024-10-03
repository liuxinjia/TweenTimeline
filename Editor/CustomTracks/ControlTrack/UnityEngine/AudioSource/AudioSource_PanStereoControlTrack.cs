
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.AudioSourceTween
{
    [TrackClipType(typeof(AudioSource_PanStereoControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.AudioSource))]
    [TrackColor(0.717f, 0.274f, 0.683f)]
    public class AudioSource_PanStereoControlTrack : TrackAsset,IBaseTrack
    {

    }
}
