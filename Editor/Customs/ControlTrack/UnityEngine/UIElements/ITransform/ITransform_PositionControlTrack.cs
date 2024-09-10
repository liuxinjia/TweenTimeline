
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UIElements;
namespace Cr7Sund.ITransformTweeen
{
    [TrackClipType(typeof(ITransform_PositionControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UIElements.ITransform))]
    public class ITransform_PositionControlTrack : TrackAsset,IBaseTrack
    {

    }
}
