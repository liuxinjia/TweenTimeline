
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [TrackClipType(typeof(Camera_FarClipPlaneControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    [TrackColor(0.459f, 0.546f, 0.421f)]
    public class Camera_FarClipPlaneControlTrack : TrackAsset,IBaseTrack
    {

    }
}
