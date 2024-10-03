
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.LayoutElementTween
{
    [TrackClipType(typeof(LayoutElement_MinHeightControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.LayoutElement))]
    [TrackColor(0.07f, 0.675f, 0.9f)]
    public class LayoutElement_MinHeightControlTrack : TrackAsset,IBaseTrack
    {

    }
}
