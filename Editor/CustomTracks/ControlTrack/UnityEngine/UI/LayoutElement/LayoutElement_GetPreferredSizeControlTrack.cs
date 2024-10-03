
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.LayoutElementTween
{
    [TrackClipType(typeof(LayoutElement_GetPreferredSizeControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.LayoutElement))]
    [TrackColor(0.706f, 0.392f, 0.103f)]
    public class LayoutElement_GetPreferredSizeControlTrack : TrackAsset,IBaseTrack
    {

    }
}
