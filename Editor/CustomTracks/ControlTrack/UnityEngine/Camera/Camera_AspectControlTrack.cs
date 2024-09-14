
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [TrackClipType(typeof(Camera_AspectControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    public class Camera_AspectControlTrack : TrackAsset,IBaseTrack
    {

    }
}
