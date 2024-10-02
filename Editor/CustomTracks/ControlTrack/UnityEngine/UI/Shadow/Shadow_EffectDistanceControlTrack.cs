
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ShadowTween
{
    [TrackClipType(typeof(Shadow_EffectDistanceControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.Shadow))]
    [TrackColor(0.81f, 0.273f, 0.447f)]
    public class Shadow_EffectDistanceControlTrack : TrackAsset,IBaseTrack
    {

    }
}
