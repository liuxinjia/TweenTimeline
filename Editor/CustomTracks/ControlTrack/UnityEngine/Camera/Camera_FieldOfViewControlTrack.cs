
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [TrackClipType(typeof(Camera_FieldOfViewControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    [TrackColor(0.647f, 0.7f, 0.028f)]
    public class Camera_FieldOfViewControlTrack : TrackAsset,IBaseTrack
    {

    }
}
