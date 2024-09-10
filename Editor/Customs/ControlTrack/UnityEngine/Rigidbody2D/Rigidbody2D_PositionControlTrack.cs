
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.Rigidbody2DTweeen
{
    [TrackClipType(typeof(Rigidbody2D_PositionControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Rigidbody2D))]
    public class Rigidbody2D_PositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
