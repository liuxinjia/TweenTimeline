
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RigidbodyTween
{
    [TrackClipType(typeof(Rigidbody_RotationControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Rigidbody))]
    public class Rigidbody_RotationControlTrack : TrackAsset,IBaseTrack
    {

    }
}
