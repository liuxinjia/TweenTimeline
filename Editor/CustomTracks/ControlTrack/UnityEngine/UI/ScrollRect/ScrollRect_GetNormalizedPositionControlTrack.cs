
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ScrollRectTween
{
    [TrackClipType(typeof(ScrollRect_GetNormalizedPositionControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.ScrollRect))]
    [TrackColor(0.707f, 0.565f, 0.921f)]
    public class ScrollRect_GetNormalizedPositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
