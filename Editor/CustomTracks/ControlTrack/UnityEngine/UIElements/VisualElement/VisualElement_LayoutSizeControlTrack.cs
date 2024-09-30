
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UIElements;
namespace Cr7Sund.VisualElementTween
{
    [TrackClipType(typeof(VisualElement_LayoutSizeControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UIElements.VisualElement))]
    [TrackColor(0.256f, 0.408f, 0.483f)]
    public class VisualElement_LayoutSizeControlTrack : TrackAsset,IBaseTrack
    {

    }
}
