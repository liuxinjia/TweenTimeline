
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.RectMask2DTween
{
    [TrackClipType(typeof(RectMask2D_SoftnessControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.RectMask2D))]
    [TrackColor(0.498f, 0.392f, 0.662f)]
    public class RectMask2D_SoftnessControlTrack : TrackAsset,IBaseTrack
    {

    }
}
