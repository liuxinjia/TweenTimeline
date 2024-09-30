
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.MaterialTween
{
    [TrackClipType(typeof(Material_MainTextureOffsetControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Material))]
    [TrackColor(0.954f, 0.777f, 0.942f)]
    public class Material_MainTextureOffsetControlTrack : TrackAsset,IBaseTrack
    {

    }
}
