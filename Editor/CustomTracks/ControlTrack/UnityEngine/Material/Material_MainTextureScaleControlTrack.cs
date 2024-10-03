
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.MaterialTween
{
    [TrackClipType(typeof(Material_MainTextureScaleControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Material))]
    [TrackColor(0.532f, 0.9f, 0.923f)]
    public class Material_MainTextureScaleControlTrack : TrackAsset,IBaseTrack
    {

    }
}
