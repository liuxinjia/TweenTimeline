
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.Rigidbody2DTween
{
    [TrackClipType(typeof(Rigidbody2D_PositionControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Rigidbody2D))]
    [TrackColor(0.957f, 0.741f, 0.447f)]
    public class Rigidbody2D_PositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
