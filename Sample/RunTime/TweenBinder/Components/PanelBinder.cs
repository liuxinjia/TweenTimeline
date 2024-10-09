using PrimeTween;
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

        public virtual void PlayYourTween(string tweeName)
        {
            this.PlayTween(tweeName);
        }
        public virtual Sequence PlayInSequence()
        {
            return this.PlayTween(inTweenName);
        }

        public virtual Sequence PlayOutSequence()
        {
            return this.PlayTween(outTweenName);
        }

        public virtual void PlayOutTween()
        {

            this.PlayTween(outTweenName);
        }
    }
}
