
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RigidbodyTween
{
    [TrackClipType(typeof(Rigidbody_PositionControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Rigidbody))]
    [TrackColor(0.44f, 0.87f, 0.388f)]
    public class Rigidbody_PositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
