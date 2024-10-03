
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ShadowTween
{
    [TrackClipType(typeof(Shadow_EffectColorAControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.Shadow))]
    [TrackColor(0.4f, 0.556f, 0.765f)]
    public class Shadow_EffectColorAControlTrack : TrackAsset,IBaseTrack
    {

    }
}
