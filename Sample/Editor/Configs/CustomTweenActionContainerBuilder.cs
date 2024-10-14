using System.Collections.Generic;
using UnityEngine;
using Cr7Sund.TMP_TextTween;
using Cr7Sund.RectTransformTween;
using Cr7Sund.UITransitionEffectTween;
using System.Reflection.Emit;


namespace Cr7Sund.TweenTimeLine
{
    public class CustomTweenActionContainerBuilder
    {
        public static List<TweenActionEffect> CreateCustomAnimationCollection()
        {
            var animEffect = new List<TweenActionEffect>();

            TweenActionEffect sizeXEffect = new TweenActionEffect();
            sizeXEffect.label = "sizeDelta.x";
            sizeXEffect.effectCategory = "RectTransform";
            sizeXEffect.image = "custom_RectTransform_example.png";
            sizeXEffect.collectionCategory = "Custom";
            sizeXEffect.animationSteps = new List<TweenActionStep>(){
                new TweenActionStep
                {
                    EndPos = "0",
                    tweenOperationType = TweenActionStep.TweenOperationType.Additive,
                    tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_SizeDeltaXControlBehaviour>(),
                    label = "sizeDelta.x",
                }
            };
            animEffect.Add(sizeXEffect);

            TweenActionEffect anchorMaxXEffect = new TweenActionEffect();
            anchorMaxXEffect.label = "anchorMax.x";
            anchorMaxXEffect.effectCategory = "RectTransform";
            anchorMaxXEffect.image = "custom_RectTransform_example.png";
            anchorMaxXEffect.collectionCategory = "Custom";
            anchorMaxXEffect.animationSteps = new List<TweenActionStep>(){
                new TweenActionStep
                {
                    EndPos = "0",
                    tweenOperationType = TweenActionStep.TweenOperationType.Additive,
                    tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_AnchorMaxXControlBehaviour>(),
                    label = "anchorMax.x",
                }
            };
            animEffect.Add(anchorMaxXEffect);

            TweenActionEffect effectFactorEffect = new TweenActionEffect();
            effectFactorEffect.label = "effectFactor";
            effectFactorEffect.effectCategory = "UITransitionEffect";
            effectFactorEffect.image = "custom_UITransitionEffect_example.png";
            effectFactorEffect.collectionCategory = "Custom";
            effectFactorEffect.animationSteps = new List<TweenActionStep>(){
                new TweenActionStep
                {
                    EndPos = "0",
                    tweenOperationType = TweenActionStep.TweenOperationType.Additive,
                    tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<UITransitionEffect_EffectFactorControlBehaviour>(),
                    label = "effectFactor",
                }
            };
            animEffect.Add(effectFactorEffect);

            return animEffect;
        }
    }
}
