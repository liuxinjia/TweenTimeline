
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [TrackClipType(typeof(RectTransform_PivotYControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.RectTransform))]
    [TrackColor(0.197f, 0.359f, 0.438f)]
    public class RectTransform_PivotYControlTrack : TrackAsset,IBaseTrack
    {

    }
}
