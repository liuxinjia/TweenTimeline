
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
    [TrackColor(0.541f, 0.265f, 0.327f)]
    public class RectMask2D_PaddingControlTrack : TrackAsset,IBaseTrack
    {

    }
}
