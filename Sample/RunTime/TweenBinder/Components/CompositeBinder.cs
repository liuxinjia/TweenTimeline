using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    public class CompositeBinder : PanelBinder
    {
        public int loopCount = 1;

        public override void PlayInTween()
        {
            this.Play(inTweenName, cycles: loopCount);
        }

        public override void PlayOutTween()
        {
            this.Play(outTweenName, cycles: loopCount);
        }
    }
}
