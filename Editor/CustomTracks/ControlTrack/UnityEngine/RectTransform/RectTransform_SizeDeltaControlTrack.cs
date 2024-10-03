
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [TrackClipType(typeof(RectTransform_SizeDeltaControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.RectTransform))]
    [TrackColor(0.736f, 0.301f, 0.085f)]
    public class RectTransform_SizeDeltaControlTrack : TrackAsset,IBaseTrack
    {

    }
}
