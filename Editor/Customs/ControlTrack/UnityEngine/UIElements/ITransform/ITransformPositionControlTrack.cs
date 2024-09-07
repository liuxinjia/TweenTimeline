
using System;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    [TrackClipType(typeof(ITransformPositionControlAsset))]
    [TrackBindingType(typeof(ITransform))]
    public class ITransformPositionControlTrack : TrackAsset
    {

    }
}
