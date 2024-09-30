
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.LightTween
{
    [TrackClipType(typeof(Light_ColorControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Light))]
    [TrackColor(0.536f, 0.212f, 0.161f)]
    public class Light_ColorControlTrack : TrackAsset,IBaseTrack
    {

    }
}
