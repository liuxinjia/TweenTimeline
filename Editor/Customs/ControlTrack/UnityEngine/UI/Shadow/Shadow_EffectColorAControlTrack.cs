
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ShadowTweeen
{
    [TrackClipType(typeof(Shadow_EffectColorAControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.Shadow))]
    public class Shadow_EffectColorAControlTrack : TrackAsset,IBaseTrack
    {

    }
}
