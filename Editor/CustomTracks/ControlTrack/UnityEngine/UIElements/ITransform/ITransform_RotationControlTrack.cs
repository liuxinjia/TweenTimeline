
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UIElements;
namespace Cr7Sund.ITransformTween
{
    [TrackClipType(typeof(ITransform_RotationControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UIElements.ITransform))]
    [TrackColor(0.091f, 0.72f, 0.384f)]
    public class ITransform_RotationControlTrack : TrackAsset,IBaseTrack
    {

    }
}
