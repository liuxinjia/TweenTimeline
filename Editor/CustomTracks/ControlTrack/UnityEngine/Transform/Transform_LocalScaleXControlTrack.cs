
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTween
{
    [TrackClipType(typeof(Transform_LocalScaleXControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    [TrackColor(0.678f, 0.595f, 0.104f)]
    public class Transform_LocalScaleXControlTrack : TrackAsset,IBaseTrack
    {

    }
}
