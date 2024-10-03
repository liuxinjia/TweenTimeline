
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [TrackClipType(typeof(Camera_NearClipPlaneControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    [TrackColor(0.068f, 0.171f, 0.889f)]
    public class Camera_NearClipPlaneControlTrack : TrackAsset,IBaseTrack
    {

    }
}
