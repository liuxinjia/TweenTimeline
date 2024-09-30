
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
    [TrackColor(0.876f, 0.901f, 0.04f)]
    public class Sequence_TimeScaleControlTrack : TrackAsset,IBaseTrack
    {

    }
}
