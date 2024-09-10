
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ShadowTweeen
{
    [TrackClipType(typeof(Shadow_EffectColorControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.Shadow))]
    public class Shadow_EffectColorControlTrack : TrackAsset,IBaseTrack
    {

    }
}
