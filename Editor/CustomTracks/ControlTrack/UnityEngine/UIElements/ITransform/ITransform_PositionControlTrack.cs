
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UIElements;
namespace Cr7Sund.ITransformTween
{
    [TrackClipType(typeof(ITransform_PositionControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UIElements.ITransform))]
    [TrackColor(0.027f, 0.273f, 0.968f)]
    public class ITransform_PositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
