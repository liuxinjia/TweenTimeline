
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [TrackClipType(typeof(Camera_BackgroundColorControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    [TrackColor(0.735f, 0.195f, 0.535f)]
    public class Camera_BackgroundColorControlTrack : TrackAsset,IBaseTrack
    {

    }
}
