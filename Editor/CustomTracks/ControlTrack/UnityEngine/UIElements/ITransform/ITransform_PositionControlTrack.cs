
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
    [TrackColor(0.574f, 0.646f, 0.383f)]
    public class ITransform_PositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
