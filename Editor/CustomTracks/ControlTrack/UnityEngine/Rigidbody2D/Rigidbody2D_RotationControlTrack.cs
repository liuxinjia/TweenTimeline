
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.Rigidbody2DTween
{
    [TrackClipType(typeof(Rigidbody2D_RotationControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Rigidbody2D))]
    [TrackColor(0.716f, 0.231f, 0.588f)]
    public class Rigidbody2D_RotationControlTrack : TrackAsset,IBaseTrack
    {

    }
}
