
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.LightTween
{
    [TrackClipType(typeof(Light_ShadowStrengthControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Light))]
    [TrackColor(0.238f, 0.038f, 0.98f)]
    public class Light_ShadowStrengthControlTrack : TrackAsset,IBaseTrack
    {

    }
}
