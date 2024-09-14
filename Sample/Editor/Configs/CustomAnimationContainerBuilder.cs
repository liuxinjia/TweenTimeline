using System.Collections.Generic;
using UnityEngine;
using Cr7Sund.TMP_TextTween;


namespace Cr7Sund.TweenTimeLine
{
    public class CustomAnimationContainerBuilder
    {
        public static List<TweenActionEffect> CreateCustomAnimationCollection()
        {
            List<TweenActionEffect> animEffect = new();
            animEffect.Add(new TweenActionEffect("characterSpacing", "TMP_Text")
            {
                image = "custom_TMP_Text_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0",
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<TMP_Text_CharacterSpacingControlBehaviour>(),
                        label = "characterSpacing",
                    }
                }
            });
            return animEffect;
        }
    }
}
