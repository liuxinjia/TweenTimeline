
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.LayoutElementTween
{
    [TrackClipType(typeof(LayoutElement_MinWidthControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.LayoutElement))]
    [TrackColor(0.271f, 0.1f, 0.988f)]
    public class LayoutElement_MinWidthControlTrack : TrackAsset,IBaseTrack
    {

    }
}
