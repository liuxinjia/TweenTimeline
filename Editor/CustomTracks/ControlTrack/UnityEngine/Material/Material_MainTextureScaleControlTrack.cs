
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.MaterialTween
{
    [TrackClipType(typeof(Material_MainTextureScaleControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Material))]
    [TrackColor(0.048f, 0.578f, 0.091f)]
    public class Material_MainTextureScaleControlTrack : TrackAsset,IBaseTrack
    {

    }
}
