
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.LayoutElementTween
{
    [TrackClipType(typeof(LayoutElement_MinHeightControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.LayoutElement))]
    public class LayoutElement_MinHeightControlTrack : TrackAsset,IBaseTrack
    {

    }
}
