
using System;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    [TrackClipType(typeof(ITransformScaleControlAsset))]
    [TrackBindingType(typeof(ITransform))]
    public class ITransformScaleControlTrack : TrackAsset
    {

    }
}
