
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [TrackClipType(typeof(Camera_PixelRectControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    public class Camera_PixelRectControlTrack : TrackAsset,IBaseTrack
    {

    }
}
