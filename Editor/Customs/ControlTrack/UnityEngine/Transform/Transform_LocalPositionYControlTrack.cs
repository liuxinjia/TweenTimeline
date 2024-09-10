
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTweeen
{
    [TrackClipType(typeof(Transform_LocalPositionYControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    public class Transform_LocalPositionYControlTrack : TrackAsset,IBaseTrack
    {

    }
}
