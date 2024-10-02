
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
    [TrackColor(0.51f, 0.074f, 0.393f)]
    public class ScrollRect_GetNormalizedPositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
