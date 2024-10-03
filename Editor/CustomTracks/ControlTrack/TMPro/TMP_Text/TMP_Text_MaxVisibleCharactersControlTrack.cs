
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
    [TrackColor(0.702f, 0.209f, 0.207f)]
    public class TMP_Text_MaxVisibleCharactersControlTrack : TrackAsset,IBaseTrack
    {

    }
}
