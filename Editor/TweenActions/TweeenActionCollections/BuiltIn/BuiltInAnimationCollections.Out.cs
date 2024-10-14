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
        private TweenCollection CreateOutAnimationCollection()
        {
            var outAnimationCollection = new TweenCollection(TweenTimelineDefine.OutDefine);

            // Fade Out
            var fadeOutEffect = new TweenActionEffect();
            fadeOutEffect.label = "Fade Out";
            fadeOutEffect.effectCategory = "Fade";
            fadeOutEffect.image = "crate_with_heart.png";
            fadeOutEffect.timeEasePairs = TimeEasePairs.StandardAccelerate;
            fadeOutEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = "0.0f",
                    StartPos = "1.0f",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                }
            };
            outAnimationCollection.animationCollections.Add(fadeOutEffect);

            // Slide Out
            var slideOutEffect = new TweenActionEffect();
            slideOutEffect.label = "Slide Out";
            slideOutEffect.effectCategory = "Fade";
            slideOutEffect.image = "crate_with_heart.png";
            slideOutEffect.timeEasePairs = TimeEasePairs.StandardAccelerate;
            slideOutEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = new Vector2(-250, 0).ToString(),
                    StartPos = string.Empty,
                    tweenOperationType = TweenActionStep.TweenOperationType.Default,
                    tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionControlBehaviour>(),
                    label = "MoveX",
                },
                new TweenActionStep
                {
                    EndPos = "0.0f",
                    StartPos = "1.0f",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                    label = "Alpha",
                }
            };
            outAnimationCollection.animationCollections.Add(slideOutEffect);

            // Grow Out
            var growOutEffect = new TweenActionEffect();
            growOutEffect.label = "Grow Out";
            growOutEffect.effectCategory = "Scale";
            growOutEffect.image = "Assets/TweenTimeline/BuiltInConfigs/Editor Default Resources/Gifs/GrowOut.gif";
            growOutEffect.timeEasePairs = TimeEasePairs.StandardAccelerate;
            growOutEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = new Vector3(2.0f, 2.0f, 2.0f).ToString(),
                    StartPos = Vector3.one.ToString(),
                    label = "Scale",
                    tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
                },
                new TweenActionStep
                {
                    EndPos = "0.0f",
                    StartPos = "1.0f",
                    label = "Fade",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                }
            };
            outAnimationCollection.animationCollections.Add(growOutEffect);

            // Shrink Out
            var shrinkOutEffect = new TweenActionEffect();
            shrinkOutEffect.label = "Shrink Out";
            shrinkOutEffect.effectCategory = "Scale";
            shrinkOutEffect.image = "shrink_out_example.png";
            shrinkOutEffect.timeEasePairs = TimeEasePairs.StandardAccelerate;
            shrinkOutEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = Vector3.zero.ToString(),
                    StartPos = Vector3.one.ToString(),
                    label = "Scale",
                    tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
                },
                new TweenActionStep
                {
                    EndPos = "0.0f",
                    StartPos = "1.0f",
                    label = "Fade",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                }
            };
            outAnimationCollection.animationCollections.Add(shrinkOutEffect);

            // Spin Out
            var spinOutEffect = new TweenActionEffect();
            spinOutEffect.label = "Spin Out";
            spinOutEffect.effectCategory = "Scale";
            spinOutEffect.image = "spin_out_example.png";
            spinOutEffect.timeEasePairs = TimeEasePairs.StandardAccelerate;
            spinOutEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = new Vector3(0.5f, 0.5f, 0.5f).ToString(),
                    StartPos = Vector3.one.ToString(),
                    label = "Scale",
                    tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
                },
                new TweenActionStep
                {
                    EndPos = new Vector3(0, 0, -360).ToString(),
                    StartPos = Vector3.zero.ToString(),
                    label = "Rotate",
                    tweenMethod = GetTweenMethodName<Transform_LocalEulerAnglesControlBehaviour>(),
                },
                new TweenActionStep
                {
                    EndPos = "0.0f",
                    StartPos = "1.0f",
                    label = "Fade",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                }
            };
            outAnimationCollection.animationCollections.Add(spinOutEffect);

            // Twist Out
            var twistOutEffect = new TweenActionEffect();
            twistOutEffect.label = "Twist Out";
            twistOutEffect.effectCategory = "Scale";
            twistOutEffect.image = "twist_out_example.png";
            twistOutEffect.timeEasePairs = TimeEasePairs.StandardAccelerate;
            twistOutEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = new Vector3(0.5f, 0.5f, 0.5f).ToString(),
                    StartPos = Vector3.one.ToString(),
                    label = "Scale",
                    tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
                },
                new TweenActionStep
                {
                    EndPos = new Vector3(0, 0, -720).ToString(),
                    StartPos = Vector3.zero.ToString(),
                    label = "Rotate",
                    tweenMethod = GetTweenMethodName<Transform_LocalEulerAnglesControlBehaviour>(),
                },
                new TweenActionStep
                {
                    EndPos = "0.0f",
                    StartPos = "1.0f",
                    label = "Fade",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                }
            };
            outAnimationCollection.animationCollections.Add(twistOutEffect);

            // Move & Scale Out
            var moveAndScaleOutEffect = new TweenActionEffect();
            moveAndScaleOutEffect.label = "Move & Scale Out";
            moveAndScaleOutEffect.effectCategory = "Scale";
            moveAndScaleOutEffect.image = "move_and_scale_out_example.png";
            moveAndScaleOutEffect.durationToken = DurationToken.ExtraLong4;
            moveAndScaleOutEffect.timeEasePairs = TimeEasePairs.StandardAccelerate;
            moveAndScaleOutEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = new Vector3(200, 0, 0).ToString(),
                    StartPos = string.Empty,
                    tweenOperationType = TweenActionStep.TweenOperationType.Additive,
                    tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionControlBehaviour>(),
                    label = "MoveX",
                    startTimeOffset = 400f,
                },
                new TweenActionStep
                {
                    EndPos = "0.0f",
                    StartPos = "1.0f",
                    label = "Fade",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                    startTimeOffset = 400f,
                },
                new TweenActionStep
                {
                    EndPos = Vector3.one.ToString(),
                    StartPos = new Vector3(0.8f, 0.8f, 0.8f).ToString(),
                    tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
                    label = "Scale",
                    startTimeOffset = -400f,
                },
            };
            outAnimationCollection.animationCollections.Add(moveAndScaleOutEffect);

            // Mask Unveil
            var maskUnveilEffect = new TweenActionEffect();
            maskUnveilEffect.label = "Mask Unveil";
            maskUnveilEffect.effectCategory = "Mask";
            maskUnveilEffect.image = "mask_unveil_example.png";
            maskUnveilEffect.durationToken = DurationToken.Medium2;
            maskUnveilEffect.timeEasePairs = TimeEasePairs.StandardAccelerate;
            maskUnveilEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = new Vector4(100f, 0, 0, 0).ToString(),
                    StartPos = Vector4.zero.ToString(),
                    tweenMethod = GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                    label = "Padding",
                    startTimeOffset = 500f,
                }
            };
            outAnimationCollection.animationCollections.Add(maskUnveilEffect);

            // Mask Contract
            var maskContractEffect = new TweenActionEffect();
            maskContractEffect.label = "Mask Contract";
            maskContractEffect.effectCategory = "Mask";
            maskContractEffect.image = "mask_contract_example.png";
            maskContractEffect.durationToken = DurationToken.Medium2;
            maskContractEffect.timeEasePairs = TimeEasePairs.StandardAccelerate;
            maskContractEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = new Vector4(0f, 50f, 0f, 50f).ToString(),
                    StartPos = Vector4.zero.ToString(),
                    tweenMethod = GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                    label = "PaddingCenter",
                    startTimeOffset = 500f,
                }
            };
            outAnimationCollection.animationCollections.Add(maskContractEffect);

            // Mask Reduce
            var maskReduceEffect = new TweenActionEffect();
            maskReduceEffect.label = "Mask Reduce";
            maskReduceEffect.effectCategory = "Mask";
            maskReduceEffect.image = "mask_reduce_example.png";
            maskReduceEffect.durationToken = DurationToken.Medium2;
            maskReduceEffect.timeEasePairs = TimeEasePairs.StandardAccelerate;
            maskReduceEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = new Vector4(50f, 50f, 50f, 50f).ToString(),
                    StartPos = Vector4.zero.ToString(),
                    tweenMethod = GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                    label = "PaddingResize",
                    startTimeOffset = 500f,
                }
            };
            outAnimationCollection.animationCollections.Add(maskReduceEffect);

            // Mask Collapse
            var maskCollapseEffect = new TweenActionEffect();
            maskCollapseEffect.label = "Mask Collapse";
            maskCollapseEffect.effectCategory = "Mask";
            maskCollapseEffect.image = "mask_collapse_example.png";
            maskCollapseEffect.durationToken = DurationToken.Medium2;
            maskCollapseEffect.timeEasePairs = TimeEasePairs.StandardAccelerate;
            maskCollapseEffect.animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = new Vector2Int(50, 50).ToString(),
                    StartPos = Vector2Int.zero.ToString(),
                    tweenMethod = GetTweenMethodName<RectMask2D_SoftnessControlBehaviour>(),
                    label = "Softness",
                    startTimeOffset = 500f,
                }
            };
            outAnimationCollection.animationCollections.Add(maskCollapseEffect);

            return outAnimationCollection;
        }
    }
}