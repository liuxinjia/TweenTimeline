
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.SpriteRendererTween
{
    [TrackClipType(typeof(SpriteRenderer_ColorControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.SpriteRenderer))]
    [TrackColor(0.272f, 0.139f, 0.344f)]
    public class SpriteRenderer_ColorControlTrack : TrackAsset,IBaseTrack
    {

    }
}
