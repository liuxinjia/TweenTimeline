
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.MaterialTweeen
{
    [TrackClipType(typeof(Material_MainTextureScaleControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Material))]
    public class Material_MainTextureScaleControlTrack : TrackAsset,IBaseTrack
    {

    }
}
