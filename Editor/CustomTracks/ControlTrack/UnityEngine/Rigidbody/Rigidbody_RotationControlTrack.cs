
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RigidbodyTween
{
    [TrackClipType(typeof(Rigidbody_RotationControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Rigidbody))]
    [TrackColor(0.569f, 0.419f, 0.539f)]
    public class Rigidbody_RotationControlTrack : TrackAsset,IBaseTrack
    {

    }
}
