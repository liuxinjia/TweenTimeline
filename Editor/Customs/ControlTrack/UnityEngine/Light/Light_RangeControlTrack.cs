
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.LightTweeen
{
    [TrackClipType(typeof(Light_RangeControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Light))]
    public class Light_RangeControlTrack : TrackAsset,IBaseTrack
    {

    }
}
