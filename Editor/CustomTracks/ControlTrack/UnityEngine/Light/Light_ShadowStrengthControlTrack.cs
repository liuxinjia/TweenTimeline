
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.LightTween
{
    [TrackClipType(typeof(Light_ShadowStrengthControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Light))]
    [TrackColor(0.982f, 0.25f, 0.786f)]
    public class Light_ShadowStrengthControlTrack : TrackAsset,IBaseTrack
    {

    }
}
