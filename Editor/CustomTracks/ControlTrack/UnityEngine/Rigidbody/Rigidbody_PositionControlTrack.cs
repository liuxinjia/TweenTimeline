
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RigidbodyTween
{
    [TrackClipType(typeof(Rigidbody_PositionControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Rigidbody))]
    [TrackColor(0.38f, 0.151f, 0.008f)]
    public class Rigidbody_PositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
