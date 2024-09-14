
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [TrackClipType(typeof(Transform_PositionYControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    public class Transform_PositionYControlTrack : TrackAsset,IBaseTrack
    {

    }
}
