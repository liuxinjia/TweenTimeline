
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [TrackClipType(typeof(Transform_LocalEulerAnglesControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    [TrackColor(0.954f, 0.182f, 0.349f)]
    public class Transform_LocalEulerAnglesControlTrack : TrackAsset,IBaseTrack
    {

    }
}
