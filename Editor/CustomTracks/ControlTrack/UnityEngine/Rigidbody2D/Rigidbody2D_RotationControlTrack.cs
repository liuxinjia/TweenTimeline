
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.Rigidbody2DTween
{
    [TrackClipType(typeof(Rigidbody2D_RotationControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Rigidbody2D))]
    [TrackColor(0.085f, 0.726f, 0.73f)]
    public class Rigidbody2D_RotationControlTrack : TrackAsset,IBaseTrack
    {

    }
}
