
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.LightTween
{
    [TrackClipType(typeof(Light_IntensityControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Light))]
    [TrackColor(0.44f, 0.114f, 0.159f)]
    public class Light_IntensityControlTrack : TrackAsset,IBaseTrack
    {

    }
}
