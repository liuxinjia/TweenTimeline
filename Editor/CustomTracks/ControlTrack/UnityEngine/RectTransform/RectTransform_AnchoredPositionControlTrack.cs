
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [TrackClipType(typeof(RectTransform_AnchoredPositionControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.RectTransform))]
    [TrackColor(0.003f, 0.512f, 0.835f)]
    public class RectTransform_AnchoredPositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
