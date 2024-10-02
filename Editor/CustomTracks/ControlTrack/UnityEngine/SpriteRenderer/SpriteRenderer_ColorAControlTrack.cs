
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.SpriteRendererTween
{
    [TrackClipType(typeof(SpriteRenderer_ColorAControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.SpriteRenderer))]
    [TrackColor(0.198f, 0.296f, 0.605f)]
    public class SpriteRenderer_ColorAControlTrack : TrackAsset,IBaseTrack
    {

    }
}
