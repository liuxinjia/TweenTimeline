
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.RectTransformTween
{
    [TrackClipType(typeof(RectTransform_AnchorMaxControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.RectTransform))]
    [TrackColor(0.94f, 0.262f, 0.568f)]
    public class RectTransform_AnchorMaxControlTrack : TrackAsset,IBaseTrack
    {

    }
}
