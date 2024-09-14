
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [TrackClipType(typeof(Transform_PositionXControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    public class Transform_PositionXControlTrack : TrackAsset,IBaseTrack
    {

    }
}
