using System.Collections.Generic;

namespace Cr7Sund.TweenTimeLine
{
    [System.Serializable]
    public class TweenActionEffect
    {
        public string label;
        public string image;
        public string effectCategory;
        public string collectionCategory;

        public UnityEngine.GameObject target;
        public DurationToken durationToken;
        public MaterialEasingToken easeToken;


        public List<TweenActionStep> animationSteps;


        public TweenActionEffect()
        {
            animationSteps = new();
            durationToken = DurationToken.Short1;
            easeToken = MaterialEasingToken.Emphasized;
        }

        public TweenActionEffect(string name, string category)
        {
            label = name;
            effectCategory = category;
            durationToken = DurationToken.Medium2;
            easeToken = MaterialEasingToken.Standard;
            image = string.Empty;
            collectionCategory = string.Empty;
            target = null;
            animationSteps = new List<TweenActionStep>();
        }

        public void CopyFrom(TweenActionEffect animAction)
        {
            // if (animAction == null)
            // {
            //     throw new ArgumentNullException(nameof(animAction), "Cannot copy from a null object.");
            // }

            // target = animAction.target;
            animationSteps = new List<TweenActionStep>(animAction.animationSteps);
            image = animAction.image;
            durationToken = animAction.durationToken;
            easeToken = animAction.easeToken;
            effectCategory = animAction.effectCategory;
            collectionCategory = animAction.collectionCategory;
            label = animAction.label;
        }

        public float ConvertDuration()
        {
            return AniActionEditToolHelper.ConvertDuration((float)durationToken);
        }
    }

}
