
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using PrimeTween;
namespace Cr7Sund.TweenTween
{
    [TrackClipType(typeof(Tween_TimeScaleControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(PrimeTween.Tween))]
    [TrackColor(0.381f, 0.159f, 0.835f)]
    public class Tween_TimeScaleControlTrack : TrackAsset,IBaseTrack
    {

    }
}
