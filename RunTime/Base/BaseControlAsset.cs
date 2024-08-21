using UnityEngine;
using UnityEngine.Playables;

namespace Cr7Sund.TweenTimeLine
{
    public class BaseControlAsset<TBehaviour, TInstance> : PlayableAsset
        where TBehaviour : BaseControlBehaviour<TInstance>, new()
        where TInstance : class
    {
        public TBehaviour template;


        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<TBehaviour>.Create(graph, template);
            return playable;
        }
    }
}