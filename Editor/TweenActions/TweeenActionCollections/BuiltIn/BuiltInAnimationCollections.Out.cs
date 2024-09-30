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
            outAnimationCollection.animationCollections.Add(new TweenActionEffect("Fade Out", "Fade")
            {
                image = "crate_with_heart.png",
                animationSteps = new List<TweenActionStep>
        {
            new TweenActionStep
            {
                EndPos = "0.0f",
                StartPos = "1.0f",
                tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
            }
        }
            });

            // Slide Out
            outAnimationCollection.animationCollections.Add(new TweenActionEffect("Slide Out", "Fade")
            {
                image = "crate_with_heart.png",
                animationSteps = new List<TweenActionStep>
        {
            new TweenActionStep
            {
                EndPos = new Vector2(-250, 0).ToString(), // 修改为 Vector2
                StartPos = string.Empty,
                isRelative = true,
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
        }
            });

            // Grow Out
            outAnimationCollection.animationCollections.Add(new TweenActionEffect("Grow Out", "Scale")
            {
                image = "Assets/TweenTimeline/BuiltInConfigs/Editor Default Resources/Gifs/GrowOut.gif",
                animationSteps = new List<TweenActionStep>
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
        }
            });

            // Shrink Out
            outAnimationCollection.animationCollections.Add(new TweenActionEffect("Shrink Out", "Scale")
            {
                image = "shrink_out_example.png",
                animationSteps = new List<TweenActionStep>
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
        }
            });

            // Spin Out
            outAnimationCollection.animationCollections.Add(new TweenActionEffect("Spin Out", "Scale")
            {
                image = "spin_out_example.png",
                animationSteps = new List<TweenActionStep>
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
        }
            });

            // Twist Out
            outAnimationCollection.animationCollections.Add(new TweenActionEffect("Twist Out", "Scale")
            {
                image = "twist_out_example.png",
                animationSteps = new List<TweenActionStep>
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
        }
            });

            // Move & Scale Out
            outAnimationCollection.animationCollections.Add(new TweenActionEffect("Move & Scale Out", "Scale")
            {
                image = "move_and_scale_out_example.png",
                durationToken = DurationToken.ExtraLong4,
                animationSteps = new List<TweenActionStep>
        {
            new TweenActionStep
            {
                EndPos = new Vector3(200, 0, 0).ToString(),
                StartPos = string.Empty,
                isRelative = true,
                tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionControlBehaviour>(), // 更新控制行为
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
        }
            });

            // Mask Unveil
            outAnimationCollection.animationCollections.Add(new TweenActionEffect("Mask Unveil", "Mask")
            {
                image = "mask_unveil_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<TweenActionStep>
        {
            new TweenActionStep
            {
                EndPos = new Vector4(100f, 0, 0, 0).ToString(),
                StartPos = Vector4.zero.ToString(),
                tweenMethod = GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                label = "Padding",
                startTimeOffset = 500f,
            }
        }
            });

            // Mask Contract
            outAnimationCollection.animationCollections.Add(new TweenActionEffect("Mask Contract", "Mask")
            {
                image = "mask_contract_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<TweenActionStep>
        {
            new TweenActionStep
            {
                EndPos = new Vector4(0f, 50f, 0f, 50f).ToString(),
                StartPos = Vector4.zero.ToString(),
                tweenMethod = GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                label = "PaddingCenter",
                startTimeOffset = 500f,
            }
        }
            });

            // Mask Reduce
            outAnimationCollection.animationCollections.Add(new TweenActionEffect("Mask Reduce", "Mask")
            {
                image = "mask_reduce_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<TweenActionStep>
        {
            new TweenActionStep
            {
                EndPos = new Vector4(50f, 50f, 50f, 50f).ToString(),
                StartPos = Vector4.zero.ToString(),
                tweenMethod = GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                label = "PaddingResize",
                startTimeOffset = 500f,
            }
        }
            });

            // Mask Collapse
            outAnimationCollection.animationCollections.Add(new TweenActionEffect("Mask Collapse", "Mask")
            {
                image = "mask_collapse_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<TweenActionStep>
        {
            new TweenActionStep
            {
                EndPos = new Vector2Int(50, 50).ToString(),
                StartPos = Vector2Int.zero.ToString(),
                tweenMethod = GetTweenMethodName<RectMask2D_SoftnessControlBehaviour>(),
                label = "Softness",
                startTimeOffset = 500f,
            }
        }
            });

            return outAnimationCollection;
        }

    }
}