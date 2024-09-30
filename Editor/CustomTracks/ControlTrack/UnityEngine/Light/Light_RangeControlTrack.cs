
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.LightTween
{
    [TrackClipType(typeof(Light_RangeControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Light))]
    [TrackColor(0.261f, 0.177f, 0.919f)]
    public class Light_RangeControlTrack : TrackAsset,IBaseTrack
    {

    }
}
