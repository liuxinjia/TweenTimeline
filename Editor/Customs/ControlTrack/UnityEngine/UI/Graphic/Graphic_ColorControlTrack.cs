
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.GraphicTweeen
{
    [TrackClipType(typeof(Graphic_ColorControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.Graphic))]
    public class Graphic_ColorControlTrack : TrackAsset,IBaseTrack
    {

    }
}
