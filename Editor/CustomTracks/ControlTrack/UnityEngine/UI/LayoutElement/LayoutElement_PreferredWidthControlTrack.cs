
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.LayoutElementTween
{
    [TrackClipType(typeof(LayoutElement_PreferredWidthControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.LayoutElement))]
    [TrackColor(0.522f, 0.684f, 0.278f)]
    public class LayoutElement_PreferredWidthControlTrack : TrackAsset,IBaseTrack
    {

    }
}
