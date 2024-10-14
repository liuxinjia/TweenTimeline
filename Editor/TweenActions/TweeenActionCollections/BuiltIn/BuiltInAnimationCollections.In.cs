using System.Collections.Generic;
using Cr7Sund.GraphicTween;
using Cr7Sund.RectMask2DTween;
using Cr7Sund.RectTransformTween;
using Cr7Sund.TransformTween;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public partial class TweenActionContainerBuilder
    {
        private TweenCollection CreateInAnimationCollection()
        {
            var inAnimationCollection = new TweenCollection(TweenTimelineDefine.InDefine);

            List<TweenActionEffect> animationCollections = inAnimationCollection.animationCollections;

            // Fade In
            var fadeInEffect = new TweenActionEffect();
            animationCollections.Add(fadeInEffect);
            fadeInEffect.label = "Fade In";
            fadeInEffect.effectCategory = "Fade";
            fadeInEffect.image = "Assets/TweenTimeline/BuiltInConfigs/Editor Default Resources/Gifs/FadeIn.gif";
            fadeInEffect.durationToken = DurationToken.Medium2;
            fadeInEffect.timeEasePairs = TimeEasePairs.StandardDecelerate;
            fadeInEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = "1.0f",
                    StartPos = "0.0f",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                }
            };

            // Slide In
            var slideInEffect = new TweenActionEffect();
            animationCollections.Add(slideInEffect);
            slideInEffect.label = "Slide In";
            slideInEffect.effectCategory = "Fade";
            slideInEffect.image = "Assets/TweenTimeline/BuiltInConfigs/Editor Default Resources/Gifs/SlideIn.gif";
            slideInEffect.durationToken = DurationToken.Medium2;
            slideInEffect.timeEasePairs = TimeEasePairs.StandardDecelerate;
            slideInEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = new Vector3(250, 0, 0).ToString(),
                    StartPos = string.Empty,
                    tweenOperationType = TweenActionStep.TweenOperationType.Additive,
                    tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionControlBehaviour>(),
                    label = "MoveX",
                },
                new TweenActionStep
                {
                    EndPos = "1.0f",
                    StartPos = "0.0f",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                    label = "Alpha",
                }
            };

            // Grow In
            var growInEffect = new TweenActionEffect();
            animationCollections.Add(growInEffect);
            growInEffect.label = "Grow In";
            growInEffect.effectCategory = "Scale";
            growInEffect.image = "Assets/TweenTimeline/BuiltInConfigs/Editor Default Resources/Gifs/GrowIn.gif";
            growInEffect.durationToken = DurationToken.Medium2;
            growInEffect.timeEasePairs = TimeEasePairs.StandardDecelerate;
            growInEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = Vector3.one.ToString(),
                    StartPos = Vector3.zero.ToString(),
                    label = "Scale",
                    tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
                },
                new TweenActionStep
                {
                    EndPos = "1.0f",
                    StartPos = "0.0f",
                    label = "Fade",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                }
            };

            // Shrink In
            var shrinkInEffect = new TweenActionEffect();
            animationCollections.Add(shrinkInEffect);
            shrinkInEffect.label = "Shrink In";
            shrinkInEffect.effectCategory = "Scale";
            shrinkInEffect.image = "shrink_in_example.png";
            shrinkInEffect.durationToken = DurationToken.Medium2;
            shrinkInEffect.timeEasePairs = TimeEasePairs.StandardDecelerate;
            shrinkInEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = Vector3.one.ToString(),
                    StartPos = new Vector3(2.0f, 2.0f, 2.0f).ToString(),
                    label = "Scale",
                    tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
                },
                new TweenActionStep
                {
                    EndPos = "1.0f",
                    StartPos = "0.0f",
                    label = "Fade",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                }
            };

            // Spin In
            var spinInEffect = new TweenActionEffect();
            animationCollections.Add(spinInEffect);
            spinInEffect.label = "Spin In";
            spinInEffect.effectCategory = "Scale";
            spinInEffect.image = "spin_in_example.png";
            spinInEffect.durationToken = DurationToken.Medium2;
            spinInEffect.timeEasePairs = TimeEasePairs.StandardDecelerate;
            spinInEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = Vector3.one.ToString(),
                    StartPos = new Vector3(0.5f, 0.5f, 0.5f).ToString(),
                    label = "Scale",
                    tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
                },
                new TweenActionStep
                {
                    EndPos = new Vector3(0, 0, 360).ToString(),
                    StartPos = Vector3.zero.ToString(),
                    label = "Rotate",
                    tweenMethod = GetTweenMethodName<Transform_LocalEulerAnglesControlBehaviour>(),
                },
                new TweenActionStep
                {
                    EndPos = "1.0f",
                    StartPos = "0.0f",
                    label = "Fade",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                }
            };

            // Twist In
            var twistInEffect = new TweenActionEffect();
            animationCollections.Add(twistInEffect);
            twistInEffect.label = "Twist In";
            twistInEffect.effectCategory = "Scale";
            twistInEffect.image = "twist_in_example.png";
            twistInEffect.durationToken = DurationToken.Medium2;
            twistInEffect.timeEasePairs = TimeEasePairs.StandardDecelerate;
            twistInEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = Vector3.one.ToString(),
                    StartPos = new Vector3(0.5f, 0.5f, 0.5f).ToString(),
                    label = "Scale",
                    tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
                },
                new TweenActionStep
                {
                    EndPos = new Vector3(0, 0, 720).ToString(),
                    StartPos = Vector3.zero.ToString(),
                    label = "Rotate",
                    tweenMethod = GetTweenMethodName<Transform_LocalEulerAnglesControlBehaviour>(),
                },
                new TweenActionStep
                {
                    EndPos = "1.0f",
                    StartPos = "0.0f",
                    label = "Fade",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                }
            };

            // Move & Scale In
            var moveScaleInEffect = new TweenActionEffect();
            animationCollections.Add(moveScaleInEffect);
            moveScaleInEffect.label = "Move & Scale In";
            moveScaleInEffect.effectCategory = "Scale";
            moveScaleInEffect.image = "move_and_scale_in_example.png";
            moveScaleInEffect.durationToken = DurationToken.Medium2;
            moveScaleInEffect.timeEasePairs = TimeEasePairs.StandardDecelerate;
            moveScaleInEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = new Vector3(-200, 0, 0).ToString(),
                    StartPos = string.Empty,
                    tweenOperationType = TweenActionStep.TweenOperationType.Additive,
                    tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionControlBehaviour>(),
                    label = "MoveX",
                    startTimeOffset = 400f,
                },
                new TweenActionStep
                {
                    EndPos = "1.0f",
                    StartPos = "0.0f",
                    label = "Fade",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                    startTimeOffset = 400f,
                },
                new TweenActionStep
                {
                    EndPos = new Vector3(0.8f, 0.8f, 0.8f).ToString(),
                    StartPos = Vector3.one.ToString(),
                    tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
                    label = "Scale",
                    startTimeOffset = -400f,
                },
            };

            // Mask Reveal
            var maskRevealEffect = new TweenActionEffect();
            animationCollections.Add(maskRevealEffect);
            maskRevealEffect.label = "Mask Reveal";
            maskRevealEffect.effectCategory = "Mask";
            maskRevealEffect.image = "Assets/TweenTimeline/BuiltInConfigs/Editor Default Resources/Gifs/MaskReveal.gif";
            maskRevealEffect.durationToken = DurationToken.Medium2;
            maskRevealEffect.timeEasePairs = TimeEasePairs.StandardDecelerate;
            maskRevealEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = Vector4.zero.ToString(),
                    StartPos = new Vector4(100f, 0, 0, 0).ToString(),
                    tweenMethod = GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                    label = "Padding",
                    startTimeOffset = 500f,
                }
            };

            // Mask Center
            var maskCenterEffect = new TweenActionEffect();
            animationCollections.Add(maskCenterEffect);
            maskCenterEffect.label = "Mask Center";
            maskCenterEffect.effectCategory = "Mask";
            maskCenterEffect.image = "mask_center_example.png";
            maskCenterEffect.durationToken = DurationToken.Medium2;
            maskCenterEffect.timeEasePairs = TimeEasePairs.StandardDecelerate;
            maskCenterEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = Vector4.zero.ToString(),
                    StartPos = new Vector4(0f, 50f, 0f, 50f).ToString(),
                    tweenMethod = GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                    label = "PaddingCenter",
                    startTimeOffset = 500f,
                }
            };

            // Mask Resize
            var maskResizeEffect = new TweenActionEffect();
            animationCollections.Add(maskResizeEffect);
            maskResizeEffect.label = "Mask Resize";
            maskResizeEffect.effectCategory = "Mask";
            maskResizeEffect.image = "mask_resize_example.png";
            maskResizeEffect.durationToken = DurationToken.Medium2;
            maskResizeEffect.timeEasePairs = TimeEasePairs.StandardDecelerate;
            maskResizeEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = Vector4.zero.ToString(),
                    StartPos = new Vector4(50f, 50f, 50f, 50f).ToString(),
                    tweenMethod = GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                    label = "PaddingResize",
                    startTimeOffset = 500f,
                }
            };

            // Mask Expand
            var maskExpandEffect = new TweenActionEffect();
            animationCollections.Add(maskExpandEffect);
            maskExpandEffect.label = "Mask Expand";
            maskExpandEffect.effectCategory = "Mask";
            maskExpandEffect.image = "mask_expand_example.png";
            maskExpandEffect.durationToken = DurationToken.Medium2;
            maskExpandEffect.timeEasePairs = TimeEasePairs.StandardDecelerate;
            maskExpandEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = Vector2Int.zero.ToString(),
                    StartPos = new Vector2Int(50, 50).ToString(),
                    tweenMethod = GetTweenMethodName<RectMask2D_SoftnessControlBehaviour>(),
                    label = "Softness",
                    startTimeOffset = 500f,
                }
            };

            return inAnimationCollection;
        }
    }
}