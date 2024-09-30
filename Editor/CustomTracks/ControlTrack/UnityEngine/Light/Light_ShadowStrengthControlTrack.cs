
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.LightTween
{
    [TrackClipType(typeof(Light_ShadowStrengthControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Light))]
    [TrackColor(0.969f, 0.029f, 0.597f)]
    public class Light_ShadowStrengthControlTrack : TrackAsset,IBaseTrack
    {

    }
}
