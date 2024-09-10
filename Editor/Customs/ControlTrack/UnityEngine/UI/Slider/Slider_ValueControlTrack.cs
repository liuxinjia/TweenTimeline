
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.SliderTweeen
{
    [TrackClipType(typeof(Slider_ValueControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.Slider))]
    public class Slider_ValueControlTrack : TrackAsset,IBaseTrack
    {

    }
}
