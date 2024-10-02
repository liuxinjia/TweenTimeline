
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [TrackClipType(typeof(RectTransform_AnchoredPosition3DControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.RectTransform))]
    [TrackColor(0.412f, 0.24f, 0.83f)]
    public class RectTransform_AnchoredPosition3DControlTrack : TrackAsset,IBaseTrack
    {

    }
}
