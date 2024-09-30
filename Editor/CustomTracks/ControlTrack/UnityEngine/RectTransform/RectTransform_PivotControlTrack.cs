
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [TrackClipType(typeof(RectTransform_PivotControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.RectTransform))]
    [TrackColor(0.014f, 0.299f, 0.254f)]
    public class RectTransform_PivotControlTrack : TrackAsset,IBaseTrack
    {

    }
}
