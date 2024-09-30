
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using TMPro;
namespace Cr7Sund.TMP_TextTween
{
    [TrackClipType(typeof(TMP_Text_MaxVisibleCharactersControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(TMPro.TMP_Text))]
    [TrackColor(0.689f, 0.894f, 0.741f)]
    public class TMP_Text_MaxVisibleCharactersControlTrack : TrackAsset,IBaseTrack
    {

    }
}
