
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.LayoutElementTweeen
{
    [TrackClipType(typeof(LayoutElement_GetPreferredSizeControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.LayoutElement))]
    public class LayoutElement_GetPreferredSizeControlTrack : TrackAsset,IBaseTrack
    {

    }
}
