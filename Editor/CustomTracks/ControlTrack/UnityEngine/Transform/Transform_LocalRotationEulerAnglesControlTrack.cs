
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [TrackClipType(typeof(Transform_LocalRotationEulerAnglesControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    [TrackColor(0.424f, 0.917f, 0.843f)]
    public class Transform_LocalRotationEulerAnglesControlTrack : TrackAsset,IBaseTrack
    {

    }
}
