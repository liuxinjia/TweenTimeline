
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [TrackClipType(typeof(Transform_LocalPositionZControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    [TrackColor(0.811f, 0.49f, 0.877f)]
    public class Transform_LocalPositionZControlTrack : TrackAsset,IBaseTrack
    {

    }
}
