
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.MaterialTween
{
    [TrackClipType(typeof(Material_ColorAControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Material))]
    public class Material_ColorAControlTrack : TrackAsset,IBaseTrack
    {

    }
}
