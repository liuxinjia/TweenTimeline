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

            // 添加 TweenTimelineDefine.InDefine 类别的动画预设
            // Fade In
            animationCollections.Add(new TweenActionEffect("Fade In", "Fade")
            {
                image = "Assets/TweenTimeline/BuiltInConfigs/Editor Default Resources/Gifs/FadeIn.gif",
                animationSteps = new List<TweenActionStep>
            {
                new TweenActionStep
                {
                    EndPos = "1.0f",
                    StartPos = "0.0f",
                    tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                }
            }
            });

            // Slide In
            animationCollections.Add(new TweenActionEffect("Slide In", "Fade")
            {
                image = "Assets/TweenTimeline/BuiltInConfigs/Editor Default Resources/Gifs/SlideIn.gif",
                animationSteps = new List<TweenActionStep>
        {
            new TweenActionStep
            {
                EndPos = new Vector3(250, 0, 0).ToString(),
                StartPos = string.Empty,
                isRelative = true,
                tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionControlBehaviour>(), // 更新控制行为
                label = "MoveX",
            },
            new TweenActionStep
            {
                EndPos = "1.0f",
                StartPos = "0.0f",
                tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                label = "Alpha",
            }
        }
            });

            // Grow In
            animationCollections.Add(new TweenActionEffect("Grow In", "Scale")
            {
                image = "Assets/TweenTimeline/BuiltInConfigs/Editor Default Resources/Gifs/GrowIn.gif",
                animationSteps = new List<TweenActionStep>
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
        }
            });

            // Shrink In
            animationCollections.Add(new TweenActionEffect("Shrink In", "Scale")
            {
                image = "shrink_in_example.png",
                animationSteps = new List<TweenActionStep>
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
        }
            });

            // Spin In
            animationCollections.Add(new TweenActionEffect("Spin In", "Scale")
            {
                image = "spin_in_example.png",
                animationSteps = new List<TweenActionStep>
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
        }
            });

            // Twist In
            animationCollections.Add(new TweenActionEffect("Twist In", "Scale")
            {
                image = "twist_in_example.png",
                animationSteps = new List<TweenActionStep>
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
        }
            });

            // Move & Scale In
            animationCollections.Add(new TweenActionEffect("Move & Scale In", "Scale")
            {
                image = "move_and_scale_in_example.png",
                durationToken = DurationToken.ExtraLong4,
                animationSteps = new List<TweenActionStep>
        {
            new TweenActionStep
            {
                EndPos = new Vector3(-200, 0, 0).ToString(),
                StartPos = string.Empty,
                isRelative = true,
                tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionControlBehaviour>(), // 更新控制行为
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
        }
            });

            // Mask Reveal
            animationCollections.Add(new TweenActionEffect("Mask Reveal", "Mask")
            {
                image = "Assets/TweenTimeline/BuiltInConfigs/Editor Default Resources/Gifs/MaskReveal.gif",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<TweenActionStep>
        {
            new TweenActionStep
            {
                EndPos = Vector4.zero.ToString(),
                StartPos = new Vector4(100f, 0, 0, 0).ToString(),
                tweenMethod = GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                label = "Padding",
                startTimeOffset = 500f,
            }
        }
            });

            // Mask Center
            animationCollections.Add(new TweenActionEffect("Mask Center", "Mask")
            {
                image = "mask_center_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<TweenActionStep>
        {
            new TweenActionStep
            {
                EndPos = Vector4.zero.ToString(),
                StartPos = new Vector4(0f, 50f, 0f, 50f).ToString(),
                tweenMethod = GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                label = "PaddingCenter",
                startTimeOffset = 500f,
            }
        }
            });

            // Mask Resize
            animationCollections.Add(new TweenActionEffect("Mask Resize", "Mask")
            {
                image = "mask_resize_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<TweenActionStep>
        {
            new TweenActionStep
            {
                EndPos = Vector4.zero.ToString(),
                StartPos = new Vector4(50f, 50f, 50f, 50f).ToString(),
                tweenMethod = GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                label = "PaddingResize",
                startTimeOffset = 500f,
            }
        }
            });

            // Mask Expand
            animationCollections.Add(new TweenActionEffect("Mask Expand", "Mask")
            {
                image = "mask_expand_example.png",
                durationToken = DurationToken.Medium2,
                animationSteps = new List<TweenActionStep>
        {
            new TweenActionStep
            {
                EndPos = Vector2Int.zero.ToString(),
                StartPos = new Vector2Int(50, 50).ToString(),
                tweenMethod = GetTweenMethodName<RectMask2D_SoftnessControlBehaviour>(),
                label = "Softness",
                startTimeOffset = 500f,
            }
        }
            });


            return inAnimationCollection;
        }
    }
}