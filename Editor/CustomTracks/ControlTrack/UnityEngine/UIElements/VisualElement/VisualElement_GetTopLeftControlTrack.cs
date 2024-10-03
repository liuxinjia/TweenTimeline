
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UIElements;
namespace Cr7Sund.VisualElementTween
{
    [TrackClipType(typeof(VisualElement_GetTopLeftControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UIElements.VisualElement))]
    [TrackColor(0.383f, 0.715f, 0.473f)]
    public class VisualElement_GetTopLeftControlTrack : TrackAsset,IBaseTrack
    {

    }
}
