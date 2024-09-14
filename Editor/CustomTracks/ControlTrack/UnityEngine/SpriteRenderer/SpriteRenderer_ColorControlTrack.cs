
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.SpriteRendererTween
{
    [TrackClipType(typeof(SpriteRenderer_ColorControlAsset))]
    [TrackBindingType(typeof(UnityEngine.SpriteRenderer))]
    public class SpriteRenderer_ColorControlTrack : TrackAsset,IBaseTrack
    {

    }
}
