using System.Collections.Generic;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public  partial class AnimationContainerBuilder
    {
        private AnimationCollection CreateInAnimationCollection()
        {
            var inAnimationCollection = new AnimationCollection("In");

            List<AnimationEffect> animationCollections = inAnimationCollection.animationCollections;

            // 添加 "In" 类别的动画预设
            // Fade In
            animationCollections.Add(new AnimationEffect("Fade In", "Fade")
            {
                image = "crate_with_heart.png",
                animationSteps = new List<AnimationStep>
            {
                new AnimationStep
                {
                    EndPos = "1.0f",
                    StartPos = "0.0f",
                    tweenMethod = GetTweenMethodName<GraphicColorAControlBehaviour>(),
                }
            }
            });

            // Slide In
            animationCollections.Add(new AnimationEffect("Slide In", "Fade")
            {
                image = "crate_with_heart.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = "-250",
                StartPos = string.Empty,
            useCurPos = true,
                tweenMethod = "RectTransformAnchoredPositionX",
                label = "MoveX",
            },
            new AnimationStep
            {
                EndPos = "1.0f",
                StartPos = "0.0f",
                tweenMethod = "GraphicColorA",
                label = "Alpha",
            }
        }
            });

            // Grow In
            animationCollections.Add(new AnimationEffect("Grow In", "Scale")
            {
                image = "crate_with_heart.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = Vector3.one.ToString(),
                StartPos = Vector3.zero.ToString(),
                label = "Scale",
                tweenMethod = GetTweenMethodName<TransformLocalScaleControlBehaviour>(),
            },
            new AnimationStep
            {
                EndPos = "1.0f",
                StartPos = "0.0f",
                label = "Fade",
                tweenMethod = GetTweenMethodName<GraphicColorAControlBehaviour>(),
            }
        }
            });

            // Shrink In
            animationCollections.Add(new AnimationEffect("Shrink In", "Scale")
            {
                image = "shrink_in_example.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = Vector3.one.ToString(),
                StartPos = new Vector3(2.0f, 2.0f, 2.0f).ToString(),
                label = "Scale",
                tweenMethod = GetTweenMethodName<TransformLocalScaleControlBehaviour>(),
            },
            new AnimationStep
            {
                EndPos = "1.0f",
                StartPos = "0.0f",
                label = "Fade",
                tweenMethod = GetTweenMethodName<GraphicColorAControlBehaviour>(),
            }
        }
            });

            // Spin In
            animationCollections.Add(new AnimationEffect("Spin In", "Scale")
            {
                image = "spin_in_example.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = Vector3.one.ToString(),
                StartPos = new Vector3(0.5f, 0.5f, 0.5f).ToString(),
                label = "Scale",
                tweenMethod = GetTweenMethodName<TransformLocalScaleControlBehaviour>(),
            },
            new AnimationStep
            {
                EndPos = Quaternion.Euler(0, 0, 360).eulerAngles.ToString(),
                StartPos = Vector3.zero.ToString(),
                label = "Rotate",
                tweenMethod = GetTweenMethodName<TransformRotationControlBehaviour>(),
            },
            new AnimationStep
            {
                EndPos = "1.0f",
                StartPos = "0.0f",
                label = "Fade",
                tweenMethod = GetTweenMethodName<GraphicColorAControlBehaviour>(),
            }
        }
            });

            // Twist In
            animationCollections.Add(new AnimationEffect("Twist In", "Scale")
            {
                image = "twist_in_example.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = Vector3.one.ToString(),
                StartPos = new Vector3(0.5f, 0.5f, 0.5f).ToString(),
                label = "Scale",
                tweenMethod = GetTweenMethodName<TransformLocalScaleControlBehaviour>(),
            },
            new AnimationStep
            {
                EndPos = Quaternion.Euler(0, 0, 720).eulerAngles.ToString(),
                StartPos = Vector3.zero.ToString(),
                label = "Rotate",
                tweenMethod = GetTweenMethodName<TransformRotationControlBehaviour>(),
            },
            new AnimationStep
            {
                EndPos = "1.0f",
                StartPos = "0.0f",
                label = "Fade",
                tweenMethod = GetTweenMethodName<GraphicColorAControlBehaviour>(),
            }
        }
            });

            // Move & Scale In
            animationCollections.Add(new AnimationEffect("Move & Scale In", "Scale")
            {
                image = "move_and_scale_in_example.png",
                durationToken = DurationToken.ExtraLong4,
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = "-200",
                StartPos = string.Empty,
useCurPos = true,
                tweenMethod = GetTweenMethodName<RectTransformAnchoredPositionXControlBehaviour>(),
                label = "MoveX",
                startTimeOffset = 400f,
            },
            new AnimationStep
            {
                EndPos = "1.0f",
                StartPos = "0.0f",
                label = "Fade",
                tweenMethod = GetTweenMethodName<GraphicColorAControlBehaviour>(),
                startTimeOffset = 400f,
            },
            new AnimationStep
            {
                EndPos = new Vector3(0.8f, 0.8f, 0.8f).ToString(),
                StartPos = Vector3.one.ToString(),
                tweenMethod = GetTweenMethodName<TransformLocalScaleControlBehaviour>(),
                label = "Scale",
                startTimeOffset = -400f,
            },
        }
            });

            // Mask Reveal
            animationCollections.Add(new AnimationEffect("Mask Reveal", "Mask")
            {
                image = "mask_reveal_fade_in_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = Vector4.zero.ToString(),
                StartPos = new Vector4(100f, 0, 0, 0).ToString(),
                tweenMethod = GetTweenMethodName<RectMask2DPaddingControlBehaviour>(),
                label = "Padding",
                startTimeOffset = 500f,
            }
        }
            });

            // Mask Center
            animationCollections.Add(new AnimationEffect("Mask Center", "Mask")
            {
                image = "mask_center_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = Vector4.zero.ToString(),
                StartPos = new Vector4(0f, 50f, 0f, 50f).ToString(),
                tweenMethod = GetTweenMethodName<RectMask2DPaddingControlBehaviour>(),
                label = "PaddingCenter",
                startTimeOffset = 500f,
            }
        }
            });

            // Mask Resize
            animationCollections.Add(new AnimationEffect("Mask Resize", "Mask")
            {
                image = "mask_resize_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = Vector4.zero.ToString(),
                StartPos = new Vector4(50f, 50f, 50f, 50f).ToString(),
                tweenMethod = GetTweenMethodName<RectMask2DPaddingControlBehaviour>(),
                label = "PaddingResize",
                startTimeOffset = 500f,
            }
        }
            });

            // Mask Expand
            animationCollections.Add(new AnimationEffect("Mask Expand", "Mask")
            {
                image = "mask_expand_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = Vector2.zero.ToString(),
                StartPos = new Vector2(50, 50).ToString(),
                tweenMethod = GetTweenMethodName<RectMask2DSoftnessControlBehaviour>(),
                label = "Softness",
                startTimeOffset = 500f,
            }
        }
            });


            return inAnimationCollection;
        }
    }
}