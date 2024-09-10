
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.TransformTweeen
{
    [TrackClipType(typeof(Transform_LocalScaleXControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Transform))]
    public class Transform_LocalScaleXControlTrack : TrackAsset,IBaseTrack
    {

    }
}
