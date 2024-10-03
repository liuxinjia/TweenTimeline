
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
    [TrackColor(0.183f, 0.08f, 0.791f)]
    public class LayoutElement_PreferredWidthControlTrack : TrackAsset,IBaseTrack
    {

    }
}
