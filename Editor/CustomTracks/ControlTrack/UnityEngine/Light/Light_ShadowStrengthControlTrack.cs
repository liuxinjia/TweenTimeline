
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.LightTween
{
    [TrackClipType(typeof(Light_ShadowStrengthControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Light))]
    public class Light_ShadowStrengthControlTrack : TrackAsset,IBaseTrack
    {

    }
}
