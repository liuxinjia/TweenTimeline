
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UIElements;
namespace Cr7Sund.ITransformTween
{
    [TrackClipType(typeof(ITransform_ScaleControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UIElements.ITransform))]
    public class ITransform_ScaleControlTrack : TrackAsset,IBaseTrack
    {

    }
}
