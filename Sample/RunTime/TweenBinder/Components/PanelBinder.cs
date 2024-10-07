using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    public class PanelBinder : ComponentBinderAdapter
    {
        public TimelineAsset timelineAsset;
        public string inTweenName;
        public string outTweenName;

        public virtual void PlayInTween()
        {
            this.Play(inTweenName);
        }

        public virtual void PlayOutTween()
        {
            this.Play(outTweenName);
        }
    }
}
