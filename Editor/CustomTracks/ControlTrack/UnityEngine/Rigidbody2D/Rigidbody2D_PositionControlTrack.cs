
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.Rigidbody2DTween
{
    [TrackClipType(typeof(Rigidbody2D_PositionControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Rigidbody2D))]
    [TrackColor(0.139f, 0.334f, 0.89f)]
    public class Rigidbody2D_PositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
