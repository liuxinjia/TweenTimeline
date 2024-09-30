
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [TrackClipType(typeof(RectTransform_AnchoredPosition3DControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.RectTransform))]
    [TrackColor(0.185f, 0.284f, 0.367f)]
    public class RectTransform_AnchoredPosition3DControlTrack : TrackAsset,IBaseTrack
    {

    }
}
