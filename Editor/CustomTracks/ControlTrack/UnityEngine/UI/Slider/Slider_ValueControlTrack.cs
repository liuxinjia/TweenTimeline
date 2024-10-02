
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.SliderTween
{
    [TrackClipType(typeof(Slider_ValueControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.Slider))]
    [TrackColor(0.265f, 0.057f, 0.868f)]
    public class Slider_ValueControlTrack : TrackAsset,IBaseTrack
    {

    }
}
