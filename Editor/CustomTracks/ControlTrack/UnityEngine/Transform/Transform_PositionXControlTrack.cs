
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [TrackClipType(typeof(Transform_PositionXControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    [TrackColor(0.234f, 0.849f, 0.154f)]
    public class Transform_PositionXControlTrack : TrackAsset,IBaseTrack
    {

    }
}
