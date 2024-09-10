using System.Collections.Generic;
using UnityEngine;
using Cr7Sund.LightTweeen;
using Cr7Sund.CameraTweeen;
using Cr7Sund.TransformTweeen;
using Cr7Sund.SliderTweeen;
using Cr7Sund.ScrollRectTweeen;
using Cr7Sund.RectTransformTweeen;
using Cr7Sund.ShadowTweeen;
using Cr7Sund.LayoutElementTweeen;
using Cr7Sund.RigidbodyTweeen;
using Cr7Sund.Rigidbody2DTweeen;
using Cr7Sund.GraphicTweeen;
using Cr7Sund.SpriteRendererTweeen;
using Cr7Sund.MaterialTweeen;
using Cr7Sund.CanvasGroupTweeen;
using Cr7Sund.ImageTweeen;
using Cr7Sund.AudioSourceTweeen;
using Cr7Sund.TweenTweeen;
using Cr7Sund.SequenceTweeen;
using Cr7Sund.VisualElementTweeen;
using Cr7Sund.ITransformTweeen;
using Cr7Sund.TMP_TextTweeen;
using Cr7Sund.RectMask2DTweeen;


namespace Cr7Sund.TweenTimeLine
{
    public partial class AnimationContainerBuilder
    {
        private AnimationCollection CreateBaseAnimationCollection()
        {
            var customAnimationCollection = new AnimationCollection("Base");

            customAnimationCollection.animationCollections.Add(new AnimationEffect( "range","Light")
            {
                image = "custom_Light_Range_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Light_RangeControlBehaviour>(),
                        label = "range",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "shadowStrength","Light")
            {
                image = "custom_Light_ShadowStrength_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Light_ShadowStrengthControlBehaviour>(),
                        label = "shadowStrength",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "intensity","Light")
            {
                image = "custom_Light_Intensity_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Light_IntensityControlBehaviour>(),
                        label = "intensity",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "orthographicSize","Camera")
            {
                image = "custom_Camera_OrthographicSize_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_OrthographicSizeControlBehaviour>(),
                        label = "orthographicSize",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "backgroundColor","Camera")
            {
                image = "custom_Camera_BackgroundColor_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_BackgroundColorControlBehaviour>(),
                        label = "backgroundColor",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "aspect","Camera")
            {
                image = "custom_Camera_Aspect_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_AspectControlBehaviour>(),
                        label = "aspect",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "farClipPlane","Camera")
            {
                image = "custom_Camera_FarClipPlane_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_FarClipPlaneControlBehaviour>(),
                        label = "farClipPlane",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "fieldOfView","Camera")
            {
                image = "custom_Camera_FieldOfView_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_FieldOfViewControlBehaviour>(),
                        label = "fieldOfView",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "nearClipPlane","Camera")
            {
                image = "custom_Camera_NearClipPlane_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_NearClipPlaneControlBehaviour>(),
                        label = "nearClipPlane",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "pixelRect","Camera")
            {
                image = "custom_Camera_PixelRect_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(x:0.00, y:0.00, width:0.00, height:0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_PixelRectControlBehaviour>(),
                        label = "pixelRect",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "rect","Camera")
            {
                image = "custom_Camera_Rect_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(x:0.00, y:0.00, width:0.00, height:0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_RectControlBehaviour>(),
                        label = "rect",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "localRotation","Transformation ")
            {
                image = "custom_Transform_LocalRotation_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_LocalRotationControlBehaviour>(),
                        label = "localRotation",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "localScale","Transformation ")
            {
                image = "custom_Transform_LocalScale_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = Vector3.zero.ToString(), 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
                        label = "localScale",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "rotation","Transformation ")
            {
                image = "custom_Transform_Rotation_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_RotationControlBehaviour>(),
                        label = "rotation",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "value","Slider")
            {
                image = "custom_Slider_Value_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Slider_ValueControlBehaviour>(),
                        label = "value",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "GetNormalizedPosition()","ScrollRect")
            {
                image = "custom_ScrollRect_GetNormalizedPosition_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<ScrollRect_GetNormalizedPositionControlBehaviour>(),
                        label = "GetNormalizedPosition()",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "horizontalNormalizedPosition","ScrollRect")
            {
                image = "custom_ScrollRect_HorizontalNormalizedPosition_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<ScrollRect_HorizontalNormalizedPositionControlBehaviour>(),
                        label = "horizontalNormalizedPosition",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "verticalNormalizedPosition","ScrollRect")
            {
                image = "custom_ScrollRect_VerticalNormalizedPosition_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<ScrollRect_VerticalNormalizedPositionControlBehaviour>(),
                        label = "verticalNormalizedPosition",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "pivot.x","Basic Layout")
            {
                image = "custom_RectTransform_PivotX_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_PivotXControlBehaviour>(),
                        label = "pivot.x",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "pivot.y","Basic Layout")
            {
                image = "custom_RectTransform_PivotY_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_PivotYControlBehaviour>(),
                        label = "pivot.y",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "pivot","Basic Layout")
            {
                image = "custom_RectTransform_Pivot_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_PivotControlBehaviour>(),
                        label = "pivot",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "anchorMax","Basic Layout")
            {
                image = "custom_RectTransform_AnchorMax_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchorMaxControlBehaviour>(),
                        label = "anchorMax",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "anchorMin","Basic Layout")
            {
                image = "custom_RectTransform_AnchorMin_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchorMinControlBehaviour>(),
                        label = "anchorMin",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "anchoredPosition3D","Transformation (UI)")
            {
                image = "custom_RectTransform_AnchoredPosition3D_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchoredPosition3DControlBehaviour>(),
                        label = "anchoredPosition3D",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "anchoredPosition3D.x","Transformation (UI)")
            {
                image = "custom_RectTransform_AnchoredPosition3DX_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchoredPosition3DXControlBehaviour>(),
                        label = "anchoredPosition3D.x",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "anchoredPosition3D.y","Transformation (UI)")
            {
                image = "custom_RectTransform_AnchoredPosition3DY_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchoredPosition3DYControlBehaviour>(),
                        label = "anchoredPosition3D.y",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "anchoredPosition3D.z","Transformation (UI)")
            {
                image = "custom_RectTransform_AnchoredPosition3DZ_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchoredPosition3DZControlBehaviour>(),
                        label = "anchoredPosition3D.z",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "effectDistance","Shadow")
            {
                image = "custom_Shadow_EffectDistance_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Shadow_EffectDistanceControlBehaviour>(),
                        label = "effectDistance",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "effectColor.a","Shadow")
            {
                image = "custom_Shadow_EffectColorA_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Shadow_EffectColorAControlBehaviour>(),
                        label = "effectColor.a",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "effectColor","Shadow")
            {
                image = "custom_Shadow_EffectColor_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Shadow_EffectColorControlBehaviour>(),
                        label = "effectColor",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "GetPreferredSize()","LayoutElement")
            {
                image = "custom_LayoutElement_GetPreferredSize_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_GetPreferredSizeControlBehaviour>(),
                        label = "GetPreferredSize()",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "preferredWidth","LayoutElement")
            {
                image = "custom_LayoutElement_PreferredWidth_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_PreferredWidthControlBehaviour>(),
                        label = "preferredWidth",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "preferredHeight","LayoutElement")
            {
                image = "custom_LayoutElement_PreferredHeight_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_PreferredHeightControlBehaviour>(),
                        label = "preferredHeight",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "position","Rigidbody")
            {
                image = "custom_Rigidbody_Position_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Rigidbody_PositionControlBehaviour>(),
                        label = "position",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "rotation","Rigidbody")
            {
                image = "custom_Rigidbody_Rotation_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00000, 0.00000, 0.00000, 1.00000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Rigidbody_RotationControlBehaviour>(),
                        label = "rotation",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "position","Rigidbody2D")
            {
                image = "custom_Rigidbody2D_Position_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Rigidbody2D_PositionControlBehaviour>(),
                        label = "position",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "rotation","Rigidbody2D")
            {
                image = "custom_Rigidbody2D_Rotation_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Rigidbody2D_RotationControlBehaviour>(),
                        label = "rotation",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "GetFlexibleSize()","LayoutElement")
            {
                image = "custom_LayoutElement_GetFlexibleSize_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_GetFlexibleSizeControlBehaviour>(),
                        label = "GetFlexibleSize()",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "flexibleWidth","LayoutElement")
            {
                image = "custom_LayoutElement_FlexibleWidth_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_FlexibleWidthControlBehaviour>(),
                        label = "flexibleWidth",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "flexibleHeight","LayoutElement")
            {
                image = "custom_LayoutElement_FlexibleHeight_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_FlexibleHeightControlBehaviour>(),
                        label = "flexibleHeight",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "GetMinSize()","LayoutElement")
            {
                image = "custom_LayoutElement_GetMinSize_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_GetMinSizeControlBehaviour>(),
                        label = "GetMinSize()",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "minWidth","LayoutElement")
            {
                image = "custom_LayoutElement_MinWidth_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_MinWidthControlBehaviour>(),
                        label = "minWidth",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "minHeight","LayoutElement")
            {
                image = "custom_LayoutElement_MinHeight_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_MinHeightControlBehaviour>(),
                        label = "minHeight",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "position","Transformation ")
            {
                image = "custom_Transform_Position_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_PositionControlBehaviour>(),
                        label = "position",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "position.x","Transformation ")
            {
                image = "custom_Transform_PositionX_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_PositionXControlBehaviour>(),
                        label = "position.x",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "position.y","Transformation ")
            {
                image = "custom_Transform_PositionY_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_PositionYControlBehaviour>(),
                        label = "position.y",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "position.z","Transformation ")
            {
                image = "custom_Transform_PositionZ_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_PositionZControlBehaviour>(),
                        label = "position.z",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "localPosition","Transformation ")
            {
                image = "custom_Transform_LocalPosition_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_LocalPositionControlBehaviour>(),
                        label = "localPosition",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "localPosition.x","Transformation ")
            {
                image = "custom_Transform_LocalPositionX_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_LocalPositionXControlBehaviour>(),
                        label = "localPosition.x",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "localPosition.y","Transformation ")
            {
                image = "custom_Transform_LocalPositionY_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_LocalPositionYControlBehaviour>(),
                        label = "localPosition.y",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "localPosition.z","Transformation ")
            {
                image = "custom_Transform_LocalPositionZ_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_LocalPositionZControlBehaviour>(),
                        label = "localPosition.z",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "color","Graphic")
            {
                image = "custom_Graphic_Color_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Graphic_ColorControlBehaviour>(),
                        label = "color",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "color","SpriteRenderer")
            {
                image = "custom_SpriteRenderer_Color_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<SpriteRenderer_ColorControlBehaviour>(),
                        label = "color",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "color","Material")
            {
                image = "custom_Material_Color_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Material_ColorControlBehaviour>(),
                        label = "color",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "color","Light")
            {
                image = "custom_Light_Color_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Light_ColorControlBehaviour>(),
                        label = "color",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "anchoredPosition","Transformation (UI)")
            {
                image = "custom_RectTransform_AnchoredPosition_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionControlBehaviour>(),
                        label = "anchoredPosition",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "anchoredPosition.x","Transformation (UI)")
            {
                image = "custom_RectTransform_AnchoredPositionX_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionXControlBehaviour>(),
                        label = "anchoredPosition.x",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "anchoredPosition.y","Transformation (UI)")
            {
                image = "custom_RectTransform_AnchoredPositionY_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionYControlBehaviour>(),
                        label = "anchoredPosition.y",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "sizeDelta","Transformation (UI)")
            {
                image = "custom_RectTransform_SizeDelta_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_SizeDeltaControlBehaviour>(),
                        label = "sizeDelta",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "alpha","CanvasGroup")
            {
                image = "custom_CanvasGroup_Alpha_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<CanvasGroup_AlphaControlBehaviour>(),
                        label = "alpha",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "color.a","Graphic")
            {
                image = "custom_Graphic_ColorA_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                        label = "color.a",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "color.a","SpriteRenderer")
            {
                image = "custom_SpriteRenderer_ColorA_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<SpriteRenderer_ColorAControlBehaviour>(),
                        label = "color.a",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "color.a","Material")
            {
                image = "custom_Material_ColorA_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Material_ColorAControlBehaviour>(),
                        label = "color.a",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "mainTextureOffset","Material")
            {
                image = "custom_Material_MainTextureOffset_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Material_MainTextureOffsetControlBehaviour>(),
                        label = "mainTextureOffset",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "mainTextureScale","Material")
            {
                image = "custom_Material_MainTextureScale_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Material_MainTextureScaleControlBehaviour>(),
                        label = "mainTextureScale",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "fillAmount","Image")
            {
                image = "custom_Image_FillAmount_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Image_FillAmountControlBehaviour>(),
                        label = "fillAmount",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "volume","AudioSource")
            {
                image = "custom_AudioSource_Volume_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<AudioSource_VolumeControlBehaviour>(),
                        label = "volume",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "pitch","AudioSource")
            {
                image = "custom_AudioSource_Pitch_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<AudioSource_PitchControlBehaviour>(),
                        label = "pitch",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "panStereo","AudioSource")
            {
                image = "custom_AudioSource_PanStereo_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<AudioSource_PanStereoControlBehaviour>(),
                        label = "panStereo",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "timeScale","Tween")
            {
                image = "custom_Tween_TimeScale_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Tween_TimeScaleControlBehaviour>(),
                        label = "timeScale",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "timeScale","Sequence")
            {
                image = "custom_Sequence_TimeScale_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Sequence_TimeScaleControlBehaviour>(),
                        label = "timeScale",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "GetResolvedStyleRect()","VisualElement")
            {
                image = "custom_VisualElement_GetResolvedStyleRect_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(x:0.00, y:0.00, width:0.00, height:0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<VisualElement_GetResolvedStyleRectControlBehaviour>(),
                        label = "GetResolvedStyleRect()",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "position","ITransform")
            {
                image = "custom_ITransform_Position_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<ITransform_PositionControlBehaviour>(),
                        label = "position",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "rotation","ITransform")
            {
                image = "custom_ITransform_Rotation_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00000, 0.00000, 0.00000, 1.00000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<ITransform_RotationControlBehaviour>(),
                        label = "rotation",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "scale","ITransform")
            {
                image = "custom_ITransform_Scale_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<ITransform_ScaleControlBehaviour>(),
                        label = "scale",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "layout.size","VisualElement")
            {
                image = "custom_VisualElement_LayoutSize_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<VisualElement_LayoutSizeControlBehaviour>(),
                        label = "layout.size",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "GetTopLeft()","VisualElement")
            {
                image = "custom_VisualElement_GetTopLeft_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<VisualElement_GetTopLeftControlBehaviour>(),
                        label = "GetTopLeft()",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "style.color.value","VisualElement")
            {
                image = "custom_VisualElement_StyleColorValue_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<VisualElement_StyleColorValueControlBehaviour>(),
                        label = "style.color.value",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "style.backgroundColor.value","VisualElement")
            {
                image = "custom_VisualElement_StyleBackgroundColorValue_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<VisualElement_StyleBackgroundColorValueControlBehaviour>(),
                        label = "style.backgroundColor.value",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "offsetMin","Basic Layout")
            {
                image = "custom_RectTransform_OffsetMin_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_OffsetMinControlBehaviour>(),
                        label = "offsetMin",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "offsetMin.x","Basic Layout")
            {
                image = "custom_RectTransform_OffsetMinX_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_OffsetMinXControlBehaviour>(),
                        label = "offsetMin.x",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "offsetMin.y","Basic Layout")
            {
                image = "custom_RectTransform_OffsetMinY_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_OffsetMinYControlBehaviour>(),
                        label = "offsetMin.y",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "offsetMax","Basic Layout")
            {
                image = "custom_RectTransform_OffsetMax_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_OffsetMaxControlBehaviour>(),
                        label = "offsetMax",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "offsetMax.x","Basic Layout")
            {
                image = "custom_RectTransform_OffsetMaxX_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_OffsetMaxXControlBehaviour>(),
                        label = "offsetMax.x",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "offsetMax.y","Basic Layout")
            {
                image = "custom_RectTransform_OffsetMaxY_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_OffsetMaxYControlBehaviour>(),
                        label = "offsetMax.y",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "maxVisibleCharacters","TMP_Text")
            {
                image = "custom_TMP_Text_MaxVisibleCharacters_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<TMP_Text_MaxVisibleCharactersControlBehaviour>(),
                        label = "maxVisibleCharacters",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "padding","RectMask2D")
            {
                image = "custom_RectMask2D_Padding_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0.00, 0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                        label = "padding",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new AnimationEffect( "softness","RectMask2D")
            {
                image = "custom_RectMask2D_Softness_example.png",
                animationSteps = new List<AnimationStep>
                {
                    new AnimationStep
                    {
                        EndPos = "(0, 0)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectMask2D_SoftnessControlBehaviour>(),
                        label = "softness",
                    }
                }
            });            return customAnimationCollection;
        }
    }
}
