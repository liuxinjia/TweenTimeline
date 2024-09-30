
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RigidbodyTween
{
    [TrackClipType(typeof(Rigidbody_RotationControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Rigidbody))]
    [TrackColor(0.79f, 0.567f, 0.089f)]
    public class Rigidbody_RotationControlTrack : TrackAsset,IBaseTrack
    {

    }
}
