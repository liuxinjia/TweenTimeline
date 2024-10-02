
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
    [TrackColor(0.403f, 0.101f, 0.779f)]
    public class Graphic_ColorAControlTrack : TrackAsset,IBaseTrack
    {

    }
}
