
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.RectMask2DTween
{
    [TrackClipType(typeof(RectMask2D_PaddingControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.RectMask2D))]
    [TrackColor(0.699f, 0.632f, 0.533f)]
    public class RectMask2D_PaddingControlTrack : TrackAsset,IBaseTrack
    {

    }
}
