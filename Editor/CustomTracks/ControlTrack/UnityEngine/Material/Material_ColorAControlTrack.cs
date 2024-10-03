
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.MaterialTween
{
    [TrackClipType(typeof(Material_ColorAControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Material))]
    [TrackColor(0.545f, 0.376f, 0.33f)]
    public class Material_ColorAControlTrack : TrackAsset,IBaseTrack
    {

    }
}
