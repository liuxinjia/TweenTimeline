
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [TrackClipType(typeof(Camera_OrthographicSizeControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    [TrackColor(0.309f, 0.487f, 0.255f)]
    public class Camera_OrthographicSizeControlTrack : TrackAsset,IBaseTrack
    {

    }
}
