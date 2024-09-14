
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.Rigidbody2DTween
{
    [TrackClipType(typeof(Rigidbody2D_RotationControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Rigidbody2D))]
    public class Rigidbody2D_RotationControlTrack : TrackAsset,IBaseTrack
    {

    }
}
