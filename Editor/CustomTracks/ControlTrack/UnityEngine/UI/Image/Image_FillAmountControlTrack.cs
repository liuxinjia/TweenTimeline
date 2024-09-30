
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
    [TrackColor(0.369f, 0.376f, 0.862f)]
    public class Image_FillAmountControlTrack : TrackAsset,IBaseTrack
    {

    }
}
