using UnityEngine;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    [TrackClipType(typeof(RectTransformControlAsset))]
    [TrackBindingType(typeof(RectTransform))]
    public class RectTransformControlTrack : TrackAsset { }
}