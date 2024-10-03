
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.SpriteRendererTween
{
    [TrackClipType(typeof(SpriteRenderer_ColorAControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.SpriteRenderer))]
    [TrackColor(0.946f, 0.207f, 0.19f)]
    public class SpriteRenderer_ColorAControlTrack : TrackAsset,IBaseTrack
    {

    }
}
