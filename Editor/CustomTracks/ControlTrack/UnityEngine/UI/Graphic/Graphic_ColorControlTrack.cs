
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.GraphicTween
{
    [TrackClipType(typeof(Graphic_ColorControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.Graphic))]
    [TrackColor(0.626f, 0.039f, 0.502f)]
    public class Graphic_ColorControlTrack : TrackAsset,IBaseTrack
    {

    }
}
