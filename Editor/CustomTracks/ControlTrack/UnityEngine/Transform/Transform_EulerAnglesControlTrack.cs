
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [TrackClipType(typeof(Transform_EulerAnglesControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    [TrackColor(0.98f, 0.879f, 0.199f)]
    public class Transform_EulerAnglesControlTrack : TrackAsset,IBaseTrack
    {

    }
}
