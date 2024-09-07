
using System;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    [TrackClipType(typeof(ITransformRotationControlAsset))]
    [TrackBindingType(typeof(ITransform))]
    public class ITransformRotationControlTrack : TrackAsset
    {

    }
}
