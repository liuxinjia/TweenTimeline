
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;

namespace Cr7Sund.CameraTween
{
    [TrackClipType(typeof(Camera_OrthographicSizeControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Camera))]
    [TrackColor(0.789f, 0.153f, 0.826f)]
    public class Camera_OrthographicSizeControlTrack : TrackAsset,IBaseTrack
    {

    }
}
