
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [TrackClipType(typeof(RectTransform_OffsetMinControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.RectTransform))]
    [TrackColor(0.344f, 0f, 0.198f)]
    public class RectTransform_OffsetMinControlTrack : TrackAsset,IBaseTrack
    {

    }
}
