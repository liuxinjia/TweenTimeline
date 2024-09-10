
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using PrimeTween;
namespace Cr7Sund.SequenceTweeen
{
    [TrackClipType(typeof(Sequence_TimeScaleControlAsset))]
    [TrackBindingType(typeof(PrimeTween.Sequence))]
    public class Sequence_TimeScaleControlTrack : TrackAsset,IBaseTrack
    {

    }
}
