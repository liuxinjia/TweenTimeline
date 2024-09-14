
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using TMPro;
namespace Cr7Sund.TMP_TextTween
{
    [TrackClipType(typeof(TMP_Text_CharacterSpacingControlAsset))]
    [TrackBindingType(typeof(TMPro.TMP_Text))]
    public class TMP_Text_CharacterSpacingControlTrack : TrackAsset,IBaseTrack
    {

    }
}
