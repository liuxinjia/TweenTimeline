
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using UnityEngine.UI;
namespace Cr7Sund.ShadowTween
{
    [TrackClipType(typeof(Shadow_EffectColorControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.UI.Shadow))]
    [TrackColor(0.781f, 0.528f, 0.449f)]
    public class Shadow_EffectColorControlTrack : TrackAsset,IBaseTrack
    {

    }
}
