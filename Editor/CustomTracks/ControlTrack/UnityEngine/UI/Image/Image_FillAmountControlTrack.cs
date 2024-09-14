
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ImageTween
{
    [TrackClipType(typeof(Image_FillAmountControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.Image))]
    public class Image_FillAmountControlTrack : TrackAsset,IBaseTrack
    {

    }
}
