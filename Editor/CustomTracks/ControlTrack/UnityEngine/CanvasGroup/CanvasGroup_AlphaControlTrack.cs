
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CanvasGroupTween
{
    [TrackClipType(typeof(CanvasGroup_AlphaControlAsset))]
    [TrackBindingType(typeof(UnityEngine.CanvasGroup))]
    public class CanvasGroup_AlphaControlTrack : TrackAsset,IBaseTrack
    {

    }
}
