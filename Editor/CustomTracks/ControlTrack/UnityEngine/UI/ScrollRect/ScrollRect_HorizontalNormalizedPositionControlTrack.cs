
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ScrollRectTween
{
    [TrackClipType(typeof(ScrollRect_HorizontalNormalizedPositionControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.ScrollRect))]
    [TrackColor(0.356f, 0.462f, 0.124f)]
    public class ScrollRect_HorizontalNormalizedPositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
