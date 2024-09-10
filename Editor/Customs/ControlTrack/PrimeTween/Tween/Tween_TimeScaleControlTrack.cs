
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using PrimeTween;
namespace Cr7Sund.TweenTweeen
{
    [TrackClipType(typeof(Tween_TimeScaleControlAsset))]
    [TrackBindingType(typeof(PrimeTween.Tween))]
    public class Tween_TimeScaleControlTrack : TrackAsset,IBaseTrack
    {

    }
}
