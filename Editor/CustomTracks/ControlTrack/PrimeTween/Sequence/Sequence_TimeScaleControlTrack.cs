
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
    [TrackColor(0.951f, 0.955f, 0.083f)]
    public class Sequence_TimeScaleControlTrack : TrackAsset,IBaseTrack
    {

    }
}
