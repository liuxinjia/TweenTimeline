
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
    [TrackColor(0.636f, 0.019f, 0.162f)]
    public class RectMask2D_PaddingControlTrack : TrackAsset,IBaseTrack
    {

    }
}
