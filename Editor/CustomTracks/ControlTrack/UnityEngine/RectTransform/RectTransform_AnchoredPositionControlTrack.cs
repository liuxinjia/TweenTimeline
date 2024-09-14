
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [TrackClipType(typeof(RectTransform_AnchoredPositionControlAsset))]
    [TrackBindingType(typeof(UnityEngine.RectTransform))]
    public class RectTransform_AnchoredPositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
