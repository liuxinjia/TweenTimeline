
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
    [TrackColor(0.275f, 0.084f, 0.197f)]
    public class Tween_TimeScaleControlTrack : TrackAsset,IBaseTrack
    {

    }
}
