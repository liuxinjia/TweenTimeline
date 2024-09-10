
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ScrollRectTweeen
{
    [TrackClipType(typeof(ScrollRect_GetNormalizedPositionControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.ScrollRect))]
    public class ScrollRect_GetNormalizedPositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
