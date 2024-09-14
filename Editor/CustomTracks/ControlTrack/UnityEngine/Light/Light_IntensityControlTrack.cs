
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.LightTween
{
    [TrackClipType(typeof(Light_IntensityControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Light))]
    public class Light_IntensityControlTrack : TrackAsset,IBaseTrack
    {

    }
}
