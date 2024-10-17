
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using Coffee.UIEffects;
namespace Cr7Sund.UITransitionEffectTween
{
    [TrackClipType(typeof(UITransitionEffect_EffectFactorControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(Coffee.UIEffects.UITransitionEffect))]
    [TrackColor(0.934f, 0.175f, 0.961f)]
    public class UITransitionEffect_EffectFactorControlTrack : TrackAsset,IBaseTrack
    {

    }
}
