using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    public class PanelBinder : ComponentBinderAdapter
    {
        public TimelineAsset timelineAsset;
        public string inTweenName;
        public string outTweenName;

        public void PlayInTween()
        {
            this.Play(inTweenName);
        }

        public void PlayOutTween()
        {
            this.Play(outTweenName);
        }
    }
}
