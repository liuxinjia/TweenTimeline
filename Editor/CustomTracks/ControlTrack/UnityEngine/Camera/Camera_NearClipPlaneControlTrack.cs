
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [TrackClipType(typeof(Camera_NearClipPlaneControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    [TrackColor(0.766f, 0.064f, 0.775f)]
    public class Camera_NearClipPlaneControlTrack : TrackAsset,IBaseTrack
    {

    }
}
