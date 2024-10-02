
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [TrackClipType(typeof(RectTransform_SizeDeltaControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.RectTransform))]
    [TrackColor(0.141f, 0.069f, 0.573f)]
    public class RectTransform_SizeDeltaControlTrack : TrackAsset,IBaseTrack
    {

    }
}
