
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [TrackClipType(typeof(Transform_RotationEulerAnglesControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    [TrackColor(0.281f, 0.672f, 0.224f)]
    public class Transform_RotationEulerAnglesControlTrack : TrackAsset,IBaseTrack
    {

    }
}
