
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [TrackClipType(typeof(Transform_LocalRotationEulerAnglesControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    [TrackColor(0.266f, 0.263f, 0.048f)]
    public class Transform_LocalRotationEulerAnglesControlTrack : TrackAsset,IBaseTrack
    {

    }
}
