
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RigidbodyTweeen
{
    [TrackClipType(typeof(Rigidbody_PositionControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Rigidbody))]
    public class Rigidbody_PositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
