
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CanvasGroupTween
{
    [TrackClipType(typeof(CanvasGroup_AlphaControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.CanvasGroup))]
    [TrackColor(0.987f, 0.612f, 0.677f)]
    public class CanvasGroup_AlphaControlTrack : TrackAsset,IBaseTrack
    {

    }
}
