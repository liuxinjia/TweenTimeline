
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using TMPro;
namespace Cr7Sund.TMP_TextTweeen
{
    [TrackClipType(typeof(TMP_Text_MaxVisibleCharactersControlAsset))]
    [TrackBindingType(typeof(TMPro.TMP_Text))]
    public class TMP_Text_MaxVisibleCharactersControlTrack : TrackAsset,IBaseTrack
    {

    }
}
