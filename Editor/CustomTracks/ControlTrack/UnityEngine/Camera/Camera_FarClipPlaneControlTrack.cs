
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [TrackClipType(typeof(Camera_FarClipPlaneControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    [TrackColor(0.731f, 0.307f, 0.122f)]
    public class Camera_FarClipPlaneControlTrack : TrackAsset,IBaseTrack
    {

    }
}
