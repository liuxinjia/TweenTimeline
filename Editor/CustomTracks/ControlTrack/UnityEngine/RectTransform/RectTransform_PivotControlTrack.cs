
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [TrackClipType(typeof(RectTransform_PivotControlAsset))]
    [TrackBindingType(typeof(UnityEngine.RectTransform))]
    public class RectTransform_PivotControlTrack : TrackAsset,IBaseTrack
    {

    }
}
