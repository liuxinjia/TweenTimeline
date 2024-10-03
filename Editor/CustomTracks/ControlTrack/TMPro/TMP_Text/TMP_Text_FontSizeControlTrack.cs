
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
using TMPro;
namespace Cr7Sund.TMP_TextTween
{
    [TrackClipType(typeof(TMP_Text_FontSizeControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(TMPro.TMP_Text))]
    [TrackColor(0.373f, 0.015f, 0.851f)]
    public class TMP_Text_FontSizeControlTrack : TrackAsset,IBaseTrack
    {

    }
}
