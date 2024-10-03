
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [TrackClipType(typeof(Transform_LocalPositionXControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    [TrackColor(0.501f, 0.773f, 0.232f)]
    public class Transform_LocalPositionXControlTrack : TrackAsset,IBaseTrack
    {

    }
}
