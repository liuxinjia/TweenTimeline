
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [TrackClipType(typeof(Camera_FieldOfViewControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    [TrackColor(0.285f, 0.4f, 0.297f)]
    public class Camera_FieldOfViewControlTrack : TrackAsset,IBaseTrack
    {

    }
}
