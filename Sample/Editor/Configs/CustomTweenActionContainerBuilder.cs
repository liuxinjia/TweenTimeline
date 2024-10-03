using System.Collections.Generic;
using UnityEngine;
using Cr7Sund.TMP_TextTween;
using Cr7Sund.RectTransformTween;
using Cr7Sund.UITransitionEffectTween;


namespace Cr7Sund.TweenTimeLine
{
    public class CustomTweenActionContainerBuilder
    {
        public static List<TweenActionEffect> CreateCustomAnimationCollection()
        {
           var animEffect =  new List<TweenActionEffect> ();

            animEffect.Add(new TweenActionEffect( "characterSpacing","TMP_Text")
            {
                image = "custom_TMP_Text_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        tweenOperationType=TweenActionStep.TweenOperationType.Additive,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<TMP_Text_CharacterSpacingControlBehaviour>(),
                        label = "characterSpacing",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "sizeDelta.x","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        tweenOperationType=TweenActionStep.TweenOperationType.Additive,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_SizeDeltaXControlBehaviour>(),
                        label = "sizeDelta.x",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "anchorMax.x","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        tweenOperationType=TweenActionStep.TweenOperationType.Additive,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_AnchorMaxXControlBehaviour>(),
                        label = "anchorMax.x",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "effectFactor","UITransitionEffect")
            {
                image = "custom_UITransitionEffect_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        tweenOperationType=TweenActionStep.TweenOperationType.Additive,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<UITransitionEffect_EffectFactorControlBehaviour>(),
                        label = "effectFactor",
                    }
                }
            }); 
             return animEffect;
            }
    }
}
