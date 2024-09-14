
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ShadowTween
{
    [TrackClipType(typeof(Shadow_EffectDistanceControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.Shadow))]
    public class Shadow_EffectDistanceControlTrack : TrackAsset,IBaseTrack
    {

    }
}
