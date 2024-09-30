
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [TrackClipType(typeof(Camera_PixelRectControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    [TrackColor(0.967f, 0.022f, 0.561f)]
    public class Camera_PixelRectControlTrack : TrackAsset,IBaseTrack
    {

    }
}
