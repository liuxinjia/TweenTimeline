
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [TrackClipType(typeof(Camera_FarClipPlaneControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    [TrackColor(0.817f, 0.836f, 0.529f)]
    public class Camera_FarClipPlaneControlTrack : TrackAsset,IBaseTrack
    {

    }
}
