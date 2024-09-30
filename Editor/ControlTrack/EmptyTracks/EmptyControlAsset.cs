using UnityEngine;
using UnityEngine.Playables;

namespace Cr7Sund.TweenTimeLine
{
    public class EmptyControlAsset : ControlAsset
    {
        public EmptyBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<EmptyBehaviour>.Create(graph, template);
            return playable;
        }
    }
}