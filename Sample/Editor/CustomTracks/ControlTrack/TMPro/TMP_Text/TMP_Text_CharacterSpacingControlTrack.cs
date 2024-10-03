
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using TMPro;
namespace Cr7Sund.TMP_TextTween
{
    [TrackClipType(typeof(TMP_Text_CharacterSpacingControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(TMPro.TMP_Text))]
    [TrackColor(0.248f, 0.596f, 0.544f)]
    public class TMP_Text_CharacterSpacingControlTrack : TrackAsset,IBaseTrack
    {

    }
}
