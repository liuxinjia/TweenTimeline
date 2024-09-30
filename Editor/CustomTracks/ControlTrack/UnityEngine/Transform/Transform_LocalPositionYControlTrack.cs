
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [TrackClipType(typeof(Transform_LocalPositionYControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    [TrackColor(0.54f, 0.472f, 0.016f)]
    public class Transform_LocalPositionYControlTrack : TrackAsset,IBaseTrack
    {

    }
}
