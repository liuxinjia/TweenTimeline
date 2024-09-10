using System.Collections.Generic;
using Cr7Sund.GraphicTweeen;
using Cr7Sund.RectTransformTweeen;
using Cr7Sund.TransformTweeen;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public partial class AnimationContainerBuilder
    {

        private AnimationCollection CreateCustomAnimationCollection()
        {
            var customAnimationCollection = new AnimationCollection("Custom");

            // Transform: Move Animation
            customAnimationCollection.animationCollections.Add(new AnimationEffect("Move", "Transform")
            {
                image = "custom_move_example.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = "500", // 目标位置
                StartPos = "0", // 起始位置
                tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionXControlBehaviour>(), // 控制位置
                label = "MoveX",
            }
        }
            });

            // Transform: Scale Animation
            customAnimationCollection.animationCollections.Add(new AnimationEffect("Scale", "Transform")
            {
                image = "custom_scale_example.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = Vector3.one.ToString(), // 目标缩放尺寸
                StartPos = new Vector3(2.0f, 2.0f, 2.0f).ToString(), // 起始缩放尺寸
                tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(), // 控制缩放
                label = "Scale",
            }
        }
            });

            // Transform: Rotate Animation
            customAnimationCollection.animationCollections.Add(new AnimationEffect("Rotate", "Transform")
            {
                image = "custom_rotate_example.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = Quaternion.Euler(0, 0, 360).eulerAngles.ToString(), // 目标旋转角度
                StartPos = Vector3.zero.ToString(), // 起始角度
                tweenMethod = GetTweenMethodName<Transform_RotationControlBehaviour>(), // 控制旋转
                label = "Rotate",
            }
        }
            });

            // Style: Opacity Animation
            customAnimationCollection.animationCollections.Add(new AnimationEffect("Opacity", "Style")
            {
                image = "custom_opacity_example.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = "0.0f", // 目标透明度
                StartPos = "1.0f", // 起始透明度
                tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(), // 控制透明度
                label = "Opacity",
            }
        }
            });


            // Other: Resize Animation
            customAnimationCollection.animationCollections.Add(new AnimationEffect("Resize", "Other")
            {
                image = "custom_resize_example.png",
                animationSteps = new List<AnimationStep>
        {
            new AnimationStep
            {
                EndPos = new Vector2(100, 100).ToString(), // 目标尺寸
                StartPos = new Vector2(50, 50).ToString(), // 起始尺寸
                tweenMethod = GetTweenMethodName<RectTransform_SizeDeltaControlBehaviour>(), // 控制尺寸
                label = "Resize",
            }
        }
            });


            return customAnimationCollection;
        }
    }
}