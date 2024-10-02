
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ImageTween
{
    [TrackClipType(typeof(Image_FillAmountControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.Image))]
    [TrackColor(0.386f, 0.907f, 0.738f)]
    public class Image_FillAmountControlTrack : TrackAsset,IBaseTrack
    {

    }
}
