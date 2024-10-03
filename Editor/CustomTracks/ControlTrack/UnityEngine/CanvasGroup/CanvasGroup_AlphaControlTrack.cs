
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CanvasGroupTween
{
    [TrackClipType(typeof(CanvasGroup_AlphaControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.CanvasGroup))]
    [TrackColor(0.461f, 0.608f, 0.424f)]
    public class CanvasGroup_AlphaControlTrack : TrackAsset,IBaseTrack
    {

    }
}
