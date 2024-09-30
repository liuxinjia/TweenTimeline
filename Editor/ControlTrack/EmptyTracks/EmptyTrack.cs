using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof(UnityEngine.Component))]
    public class EmptyTrack : TrackAsset, IBaseTrack
    {

    }
}