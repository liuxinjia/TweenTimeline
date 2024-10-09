using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    public class PanelBinder : ComponentBinderAdapter
    {
        public string inTweenName;
        public string outTweenName;

        public virtual void PlayInTween()
        {
            this.PlayTween(inTweenName);
        }

        public virtual void PlayOutTween()
        {

            this.PlayTween(outTweenName);
        }
    }
}
