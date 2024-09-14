
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [TrackClipType(typeof(Camera_FieldOfViewControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    public class Camera_FieldOfViewControlTrack : TrackAsset,IBaseTrack
    {

    }
}
