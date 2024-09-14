
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [TrackClipType(typeof(Transform_PositionZControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    public class Transform_PositionZControlTrack : TrackAsset,IBaseTrack
    {

    }
}
