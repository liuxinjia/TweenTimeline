
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
    [TrackColor(0.691f, 0.249f, 0.7f)]
    public class Image_FillAmountControlTrack : TrackAsset,IBaseTrack
    {

    }
}
