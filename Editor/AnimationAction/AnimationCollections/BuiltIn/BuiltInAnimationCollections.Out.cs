using System.Collections.Generic;
using Cr7Sund.GraphicTweeen;
using Cr7Sund.RectMask2DTweeen;
using Cr7Sund.RectTransformTweeen;
using Cr7Sund.TransformTweeen;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public partial class AnimationContainerBuilder
    {
        public void Build(AnimationContainer container)
        {
            container.animationContainers.Add(CreateInAnimationCollection());
            container.animationContainers.Add(CreateOutAnimationCollection());
            // container.animationContainers.Add(CreateCustomAnimationCollection());
            container.animationContainers.Add(CreateBaseAnimationCollection());
        }

        private AnimationCollection CreateOutAnimationCollection()
        {
            var outAnimationCollection = new AnimationCollection("Out");

            // Fade Out
            outAnimationCollection.animationCollections.Add(new AnimationEffect("Fade Out", "Fade")
            {
                image = "crate_with_heart.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = "0.0f",
                StartPos = "1.0f",
                tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
            }
        }
            });

            // Slide Out
            outAnimationCollection.animationCollections.Add(new AnimationEffect("Slide Out", "Fade")
            {
                image = "crate_with_heart.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = "250",
                StartPos = string.Empty,
         isRelative = true,
          tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionXControlBehaviour>(),
                label = "MoveX",
            },
            new AnimationStep
            {
                EndPos = "0.0f",
                StartPos = "1.0f",
                tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                label = "Alpha",
            }
        }
            });

            // Grow Out
            outAnimationCollection.animationCollections.Add(new AnimationEffect("Grow Out", "Scale")
            {
                image = "crate_with_heart.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = new Vector3(2.0f, 2.0f, 2.0f).ToString(),
                StartPos = Vector3.one.ToString(),
                label = "Scale",
                tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
            },
            new AnimationStep
            {
                EndPos = "0.0f",
                StartPos = "1.0f",
                label = "Fade",
                tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
            }
        }
            });

            // Shrink Out
            outAnimationCollection.animationCollections.Add(new AnimationEffect("Shrink Out", "Scale")
            {
                image = "shrink_out_example.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = Vector3.zero.ToString(),
                StartPos = Vector3.one.ToString(),
                label = "Scale",
                tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
            },
            new AnimationStep
            {
                EndPos = "0.0f",
                StartPos = "1.0f",
                label = "Fade",
                tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
            }
        }
            });

            // Spin Out
            outAnimationCollection.animationCollections.Add(new AnimationEffect("Spin Out", "Scale")
            {
                image = "spin_out_example.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = new Vector3(0.5f, 0.5f, 0.5f).ToString(),
                StartPos = Vector3.one.ToString(),
                label = "Scale",
                tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
            },
            new AnimationStep
            {
                EndPos = Quaternion.Euler(0, 0, -360).eulerAngles.ToString(),
                StartPos = Vector3.zero.ToString(),
                label = "Rotate",
                tweenMethod = GetTweenMethodName<Transform_RotationControlBehaviour>(),
            },
            new AnimationStep
            {
                EndPos = "0.0f",
                StartPos = "1.0f",
                label = "Fade",
                tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
            }
        }
            });

            // Twist Out
            outAnimationCollection.animationCollections.Add(new AnimationEffect("Twist Out", "Scale")
            {
                image = "twist_out_example.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = new Vector3(0.5f, 0.5f, 0.5f).ToString(),
                StartPos = Vector3.one.ToString(),
                label = "Scale",
                tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
            },
            new AnimationStep
            {
                EndPos = Quaternion.Euler(0, 0, -720).eulerAngles.ToString(),
                StartPos = Vector3.zero.ToString(),
                label = "Rotate",
                tweenMethod = GetTweenMethodName<Transform_RotationControlBehaviour>(),
            },
            new AnimationStep
            {
                EndPos = "0.0f",
                StartPos = "1.0f",
                label = "Fade",
                tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
            }
        }
            });

            // Move & Scale Out
            outAnimationCollection.animationCollections.Add(new AnimationEffect("Move & Scale Out", "Scale")
            {
                image = "move_and_scale_out_example.png",
                durationToken = DurationToken.ExtraLong4,
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = "200",
                StartPos = string.Empty,
isRelative = true,
                tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionXControlBehaviour>(),
                label = "MoveX",
                startTimeOffset = 400f,
            },
            new AnimationStep
            {
                EndPos = "0.0f",
                StartPos = "1.0f",
                label = "Fade",
                tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                startTimeOffset = 400f,
            },
            new AnimationStep
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
            outAnimationCollection.animationCollections.Add(new AnimationEffect("Mask Unveil", "Mask")
            {
                image = "mask_unveil_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
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
            outAnimationCollection.animationCollections.Add(new AnimationEffect("Mask Contract", "Mask")
            {
                image = "mask_contract_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
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
            outAnimationCollection.animationCollections.Add(new AnimationEffect("Mask Reduce", "Mask")
            {
                image = "mask_reduce_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
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
            outAnimationCollection.animationCollections.Add(new AnimationEffect("Mask Collapse", "Mask")
            {
                image = "mask_collapse_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = new Vector2(50, 50).ToString(),
                StartPos = Vector2.zero.ToString(),
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