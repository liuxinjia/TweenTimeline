
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTweeen
{
    [TrackClipType(typeof(Camera_OrthographicSizeControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    public class Camera_OrthographicSizeControlTrack : TrackAsset,IBaseTrack
    {

    }
}
