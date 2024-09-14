
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.GraphicTween
{
    [TrackClipType(typeof(Graphic_ColorAControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.Graphic))]
    public class Graphic_ColorAControlTrack : TrackAsset,IBaseTrack
    {

    }
}
