
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.LayoutElementTween
{
    [TrackClipType(typeof(LayoutElement_PreferredWidthControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.LayoutElement))]
    public class LayoutElement_PreferredWidthControlTrack : TrackAsset,IBaseTrack
    {

    }
}
