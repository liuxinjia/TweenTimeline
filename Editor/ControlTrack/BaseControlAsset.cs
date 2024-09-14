using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    public abstract class ControlAsset : PlayableAsset, ITimelineClipAsset
    {
        public ClipCaps clipCaps => ClipCaps.None;
    }

    public class BaseControlAsset<TBehaviour, TInstance, TValue> : ControlAsset
        where TBehaviour : BaseControlBehaviour<TInstance, TValue>, new()
        // where TInstance : UnityEngine.Object
    {
        public TBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
           var playable = ScriptPlayable<TBehaviour>.Create(graph, template);
           return playable;
        }

    }

}
