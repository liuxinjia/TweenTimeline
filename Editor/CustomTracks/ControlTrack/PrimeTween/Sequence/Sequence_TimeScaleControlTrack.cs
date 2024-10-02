
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using PrimeTween;
namespace Cr7Sund.SequenceTween
{
    [TrackClipType(typeof(Sequence_TimeScaleControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(PrimeTween.Sequence))]
    [TrackColor(0.075f, 0.82f, 0.615f)]
    public class Sequence_TimeScaleControlTrack : TrackAsset,IBaseTrack
    {

    }
}
