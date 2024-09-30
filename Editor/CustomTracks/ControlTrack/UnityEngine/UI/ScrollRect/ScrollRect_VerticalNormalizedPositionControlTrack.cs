
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ScrollRectTween
{
    [TrackClipType(typeof(ScrollRect_VerticalNormalizedPositionControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.ScrollRect))]
    [TrackColor(0.442f, 0.642f, 0.058f)]
    public class ScrollRect_VerticalNormalizedPositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
