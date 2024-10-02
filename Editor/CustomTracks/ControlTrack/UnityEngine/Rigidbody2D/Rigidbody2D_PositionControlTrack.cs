
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.Rigidbody2DTween
{
    [TrackClipType(typeof(Rigidbody2D_PositionControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Rigidbody2D))]
    [TrackColor(0.456f, 0.925f, 0.805f)]
    public class Rigidbody2D_PositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
