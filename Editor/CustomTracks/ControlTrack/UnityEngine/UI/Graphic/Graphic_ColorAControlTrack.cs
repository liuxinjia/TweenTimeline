
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.GraphicTween
{
    [TrackClipType(typeof(Graphic_ColorAControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.Graphic))]
    [TrackColor(0.684f, 0.076f, 0.245f)]
    public class Graphic_ColorAControlTrack : TrackAsset,IBaseTrack
    {

    }
}
