
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CanvasGroupTween
{
    [TrackClipType(typeof(CanvasGroup_AlphaControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.CanvasGroup))]
    [TrackColor(0.745f, 0.893f, 0.591f)]
    public class CanvasGroup_AlphaControlTrack : TrackAsset,IBaseTrack
    {

    }
}
