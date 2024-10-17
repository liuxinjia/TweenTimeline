using PrimeTween;
using UnityEngine;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    public class CompositeBinder : PanelBinder
    {
        public override void PlayInTween()
        {

            this.PlayTween(inTweenName);
        }

        public override void PlayOutTween()
        {

            this.PlayTween(outTweenName);
        }
    }
}
