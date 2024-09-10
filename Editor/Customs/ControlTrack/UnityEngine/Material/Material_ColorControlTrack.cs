
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.MaterialTweeen
{
    [TrackClipType(typeof(Material_ColorControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Material))]
    public class Material_ColorControlTrack : TrackAsset,IBaseTrack
    {

    }
}
