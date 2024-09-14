using System.Collections.Generic;
using UnityEngine;
using Cr7Sund.LightTween;
using Cr7Sund.CameraTween;
using Cr7Sund.TransformTween;
using Cr7Sund.SliderTween;
using Cr7Sund.ScrollRectTween;
using Cr7Sund.RectTransformTween;
using Cr7Sund.ShadowTween;
using Cr7Sund.LayoutElementTween;
using Cr7Sund.RigidbodyTween;
using Cr7Sund.Rigidbody2DTween;
using Cr7Sund.GraphicTween;
using Cr7Sund.SpriteRendererTween;
using Cr7Sund.MaterialTween;
using Cr7Sund.CanvasGroupTween;
using Cr7Sund.ImageTween;
using Cr7Sund.AudioSourceTween;
using Cr7Sund.TweenTween;
using Cr7Sund.SequenceTween;
using Cr7Sund.VisualElementTween;
using Cr7Sund.ITransformTween;
using Cr7Sund.TMP_TextTween;
using Cr7Sund.RectMask2DTween;


namespace Cr7Sund.TweenTimeLine
{
    public partial class TweenActionContainerBuilder
    {
        private TweenCollection CreateBaseAnimationCollection()
        {
            var customAnimationCollection = new TweenCollection("Custom");

            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "range","Light")
            {
                image = "custom_Light_Range_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Light_RangeControlBehaviour>(),
                        label = "range",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "shadowStrength","Light")
            {
                image = "custom_Light_ShadowStrength_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Light_ShadowStrengthControlBehaviour>(),
                        label = "shadowStrength",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "intensity","Light")
            {
                image = "custom_Light_Intensity_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Light_IntensityControlBehaviour>(),
                        label = "intensity",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "orthographicSize","Camera")
            {
                image = "custom_Camera_OrthographicSize_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_OrthographicSizeControlBehaviour>(),
                        label = "orthographicSize",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "backgroundColor","Camera")
            {
                image = "custom_Camera_BackgroundColor_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_BackgroundColorControlBehaviour>(),
                        label = "backgroundColor",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "aspect","Camera")
            {
                image = "custom_Camera_Aspect_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_AspectControlBehaviour>(),
                        label = "aspect",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "farClipPlane","Camera")
            {
                image = "custom_Camera_FarClipPlane_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_FarClipPlaneControlBehaviour>(),
                        label = "farClipPlane",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "fieldOfView","Camera")
            {
                image = "custom_Camera_FieldOfView_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_FieldOfViewControlBehaviour>(),
                        label = "fieldOfView",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "nearClipPlane","Camera")
            {
                image = "custom_Camera_NearClipPlane_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_NearClipPlaneControlBehaviour>(),
                        label = "nearClipPlane",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "pixelRect","Camera")
            {
                image = "custom_Camera_PixelRect_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(x:0.00, y:0.00, width:0.00, height:0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_PixelRectControlBehaviour>(),
                        label = "pixelRect",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "rect","Camera")
            {
                image = "custom_Camera_Rect_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(x:0.00, y:0.00, width:0.00, height:0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Camera_RectControlBehaviour>(),
                        label = "rect",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "localRotation","Transformation ")
            {
                image = "custom_Transform_LocalRotation_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_LocalRotationControlBehaviour>(),
                        label = "localRotation",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "localScale","Transformation ")
            {
                image = "custom_Transform_LocalScale_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = Vector3.zero.ToString(), 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
                        label = "localScale",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "rotation","Transformation ")
            {
                image = "custom_Transform_Rotation_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_RotationControlBehaviour>(),
                        label = "rotation",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "value","Slider")
            {
                image = "custom_Slider_Value_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Slider_ValueControlBehaviour>(),
                        label = "value",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "GetNormalizedPosition()","ScrollRect")
            {
                image = "custom_ScrollRect_GetNormalizedPosition_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<ScrollRect_GetNormalizedPositionControlBehaviour>(),
                        label = "GetNormalizedPosition()",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "horizontalNormalizedPosition","ScrollRect")
            {
                image = "custom_ScrollRect_HorizontalNormalizedPosition_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<ScrollRect_HorizontalNormalizedPositionControlBehaviour>(),
                        label = "horizontalNormalizedPosition",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "verticalNormalizedPosition","ScrollRect")
            {
                image = "custom_ScrollRect_VerticalNormalizedPosition_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<ScrollRect_VerticalNormalizedPositionControlBehaviour>(),
                        label = "verticalNormalizedPosition",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "pivot.x","Basic Layout")
            {
                image = "custom_RectTransform_PivotX_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_PivotXControlBehaviour>(),
                        label = "pivot.x",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "pivot.y","Basic Layout")
            {
                image = "custom_RectTransform_PivotY_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_PivotYControlBehaviour>(),
                        label = "pivot.y",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "pivot","Basic Layout")
            {
                image = "custom_RectTransform_Pivot_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_PivotControlBehaviour>(),
                        label = "pivot",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "anchorMax","Basic Layout")
            {
                image = "custom_RectTransform_AnchorMax_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchorMaxControlBehaviour>(),
                        label = "anchorMax",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "anchorMin","Basic Layout")
            {
                image = "custom_RectTransform_AnchorMin_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchorMinControlBehaviour>(),
                        label = "anchorMin",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "anchoredPosition3D","Transformation (UI)")
            {
                image = "custom_RectTransform_AnchoredPosition3D_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchoredPosition3DControlBehaviour>(),
                        label = "anchoredPosition3D",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "anchoredPosition3D.x","Transformation (UI)")
            {
                image = "custom_RectTransform_AnchoredPosition3DX_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchoredPosition3DXControlBehaviour>(),
                        label = "anchoredPosition3D.x",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "anchoredPosition3D.y","Transformation (UI)")
            {
                image = "custom_RectTransform_AnchoredPosition3DY_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchoredPosition3DYControlBehaviour>(),
                        label = "anchoredPosition3D.y",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "anchoredPosition3D.z","Transformation (UI)")
            {
                image = "custom_RectTransform_AnchoredPosition3DZ_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchoredPosition3DZControlBehaviour>(),
                        label = "anchoredPosition3D.z",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "effectDistance","Shadow")
            {
                image = "custom_Shadow_EffectDistance_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Shadow_EffectDistanceControlBehaviour>(),
                        label = "effectDistance",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "effectColor.a","Shadow")
            {
                image = "custom_Shadow_EffectColorA_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Shadow_EffectColorAControlBehaviour>(),
                        label = "effectColor.a",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "effectColor","Shadow")
            {
                image = "custom_Shadow_EffectColor_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Shadow_EffectColorControlBehaviour>(),
                        label = "effectColor",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "GetPreferredSize()","LayoutElement")
            {
                image = "custom_LayoutElement_GetPreferredSize_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_GetPreferredSizeControlBehaviour>(),
                        label = "GetPreferredSize()",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "preferredWidth","LayoutElement")
            {
                image = "custom_LayoutElement_PreferredWidth_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_PreferredWidthControlBehaviour>(),
                        label = "preferredWidth",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "preferredHeight","LayoutElement")
            {
                image = "custom_LayoutElement_PreferredHeight_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_PreferredHeightControlBehaviour>(),
                        label = "preferredHeight",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "position","Rigidbody")
            {
                image = "custom_Rigidbody_Position_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Rigidbody_PositionControlBehaviour>(),
                        label = "position",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "rotation","Rigidbody")
            {
                image = "custom_Rigidbody_Rotation_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00000, 0.00000, 0.00000, 1.00000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Rigidbody_RotationControlBehaviour>(),
                        label = "rotation",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "position","Rigidbody2D")
            {
                image = "custom_Rigidbody2D_Position_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Rigidbody2D_PositionControlBehaviour>(),
                        label = "position",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "rotation","Rigidbody2D")
            {
                image = "custom_Rigidbody2D_Rotation_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Rigidbody2D_RotationControlBehaviour>(),
                        label = "rotation",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "GetFlexibleSize()","LayoutElement")
            {
                image = "custom_LayoutElement_GetFlexibleSize_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_GetFlexibleSizeControlBehaviour>(),
                        label = "GetFlexibleSize()",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "flexibleWidth","LayoutElement")
            {
                image = "custom_LayoutElement_FlexibleWidth_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_FlexibleWidthControlBehaviour>(),
                        label = "flexibleWidth",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "flexibleHeight","LayoutElement")
            {
                image = "custom_LayoutElement_FlexibleHeight_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_FlexibleHeightControlBehaviour>(),
                        label = "flexibleHeight",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "GetMinSize()","LayoutElement")
            {
                image = "custom_LayoutElement_GetMinSize_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_GetMinSizeControlBehaviour>(),
                        label = "GetMinSize()",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "minWidth","LayoutElement")
            {
                image = "custom_LayoutElement_MinWidth_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_MinWidthControlBehaviour>(),
                        label = "minWidth",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "minHeight","LayoutElement")
            {
                image = "custom_LayoutElement_MinHeight_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<LayoutElement_MinHeightControlBehaviour>(),
                        label = "minHeight",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "position","Transformation ")
            {
                image = "custom_Transform_Position_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_PositionControlBehaviour>(),
                        label = "position",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "position.x","Transformation ")
            {
                image = "custom_Transform_PositionX_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_PositionXControlBehaviour>(),
                        label = "position.x",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "position.y","Transformation ")
            {
                image = "custom_Transform_PositionY_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_PositionYControlBehaviour>(),
                        label = "position.y",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "position.z","Transformation ")
            {
                image = "custom_Transform_PositionZ_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_PositionZControlBehaviour>(),
                        label = "position.z",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "localPosition","Transformation ")
            {
                image = "custom_Transform_LocalPosition_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_LocalPositionControlBehaviour>(),
                        label = "localPosition",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "localPosition.x","Transformation ")
            {
                image = "custom_Transform_LocalPositionX_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_LocalPositionXControlBehaviour>(),
                        label = "localPosition.x",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "localPosition.y","Transformation ")
            {
                image = "custom_Transform_LocalPositionY_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_LocalPositionYControlBehaviour>(),
                        label = "localPosition.y",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "localPosition.z","Transformation ")
            {
                image = "custom_Transform_LocalPositionZ_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Transform_LocalPositionZControlBehaviour>(),
                        label = "localPosition.z",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "color","Graphic")
            {
                image = "custom_Graphic_Color_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Graphic_ColorControlBehaviour>(),
                        label = "color",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "color","SpriteRenderer")
            {
                image = "custom_SpriteRenderer_Color_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<SpriteRenderer_ColorControlBehaviour>(),
                        label = "color",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "color","Material")
            {
                image = "custom_Material_Color_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Material_ColorControlBehaviour>(),
                        label = "color",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "color","Light")
            {
                image = "custom_Light_Color_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Light_ColorControlBehaviour>(),
                        label = "color",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "anchoredPosition","Transformation (UI)")
            {
                image = "custom_RectTransform_AnchoredPosition_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionControlBehaviour>(),
                        label = "anchoredPosition",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "anchoredPosition.x","Transformation (UI)")
            {
                image = "custom_RectTransform_AnchoredPositionX_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionXControlBehaviour>(),
                        label = "anchoredPosition.x",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "anchoredPosition.y","Transformation (UI)")
            {
                image = "custom_RectTransform_AnchoredPositionY_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_AnchoredPositionYControlBehaviour>(),
                        label = "anchoredPosition.y",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "sizeDelta","Transformation (UI)")
            {
                image = "custom_RectTransform_SizeDelta_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_SizeDeltaControlBehaviour>(),
                        label = "sizeDelta",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "alpha","CanvasGroup")
            {
                image = "custom_CanvasGroup_Alpha_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<CanvasGroup_AlphaControlBehaviour>(),
                        label = "alpha",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "color.a","Graphic")
            {
                image = "custom_Graphic_ColorA_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                        label = "color.a",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "color.a","SpriteRenderer")
            {
                image = "custom_SpriteRenderer_ColorA_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<SpriteRenderer_ColorAControlBehaviour>(),
                        label = "color.a",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "color.a","Material")
            {
                image = "custom_Material_ColorA_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Material_ColorAControlBehaviour>(),
                        label = "color.a",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "mainTextureOffset","Material")
            {
                image = "custom_Material_MainTextureOffset_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Material_MainTextureOffsetControlBehaviour>(),
                        label = "mainTextureOffset",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "mainTextureScale","Material")
            {
                image = "custom_Material_MainTextureScale_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Material_MainTextureScaleControlBehaviour>(),
                        label = "mainTextureScale",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "fillAmount","Image")
            {
                image = "custom_Image_FillAmount_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Image_FillAmountControlBehaviour>(),
                        label = "fillAmount",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "volume","AudioSource")
            {
                image = "custom_AudioSource_Volume_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<AudioSource_VolumeControlBehaviour>(),
                        label = "volume",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "pitch","AudioSource")
            {
                image = "custom_AudioSource_Pitch_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<AudioSource_PitchControlBehaviour>(),
                        label = "pitch",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "panStereo","AudioSource")
            {
                image = "custom_AudioSource_PanStereo_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<AudioSource_PanStereoControlBehaviour>(),
                        label = "panStereo",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "timeScale","Tween")
            {
                image = "custom_Tween_TimeScale_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Tween_TimeScaleControlBehaviour>(),
                        label = "timeScale",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "timeScale","Sequence")
            {
                image = "custom_Sequence_TimeScale_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<Sequence_TimeScaleControlBehaviour>(),
                        label = "timeScale",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "GetResolvedStyleRect()","VisualElement")
            {
                image = "custom_VisualElement_GetResolvedStyleRect_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(x:0.00, y:0.00, width:0.00, height:0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<VisualElement_GetResolvedStyleRectControlBehaviour>(),
                        label = "GetResolvedStyleRect()",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "position","ITransform")
            {
                image = "custom_ITransform_Position_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<ITransform_PositionControlBehaviour>(),
                        label = "position",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "rotation","ITransform")
            {
                image = "custom_ITransform_Rotation_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00000, 0.00000, 0.00000, 1.00000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<ITransform_RotationControlBehaviour>(),
                        label = "rotation",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "scale","ITransform")
            {
                image = "custom_ITransform_Scale_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<ITransform_ScaleControlBehaviour>(),
                        label = "scale",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "layout.size","VisualElement")
            {
                image = "custom_VisualElement_LayoutSize_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<VisualElement_LayoutSizeControlBehaviour>(),
                        label = "layout.size",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "GetTopLeft()","VisualElement")
            {
                image = "custom_VisualElement_GetTopLeft_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<VisualElement_GetTopLeftControlBehaviour>(),
                        label = "GetTopLeft()",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "style.color.value","VisualElement")
            {
                image = "custom_VisualElement_StyleColorValue_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<VisualElement_StyleColorValueControlBehaviour>(),
                        label = "style.color.value",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "style.backgroundColor.value","VisualElement")
            {
                image = "custom_VisualElement_StyleBackgroundColorValue_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<VisualElement_StyleBackgroundColorValueControlBehaviour>(),
                        label = "style.backgroundColor.value",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "offsetMin","Basic Layout")
            {
                image = "custom_RectTransform_OffsetMin_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_OffsetMinControlBehaviour>(),
                        label = "offsetMin",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "offsetMin.x","Basic Layout")
            {
                image = "custom_RectTransform_OffsetMinX_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_OffsetMinXControlBehaviour>(),
                        label = "offsetMin.x",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "offsetMin.y","Basic Layout")
            {
                image = "custom_RectTransform_OffsetMinY_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_OffsetMinYControlBehaviour>(),
                        label = "offsetMin.y",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "offsetMax","Basic Layout")
            {
                image = "custom_RectTransform_OffsetMax_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_OffsetMaxControlBehaviour>(),
                        label = "offsetMax",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "offsetMax.x","Basic Layout")
            {
                image = "custom_RectTransform_OffsetMaxX_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_OffsetMaxXControlBehaviour>(),
                        label = "offsetMax.x",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "offsetMax.y","Basic Layout")
            {
                image = "custom_RectTransform_OffsetMaxY_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectTransform_OffsetMaxYControlBehaviour>(),
                        label = "offsetMax.y",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "maxVisibleCharacters","TMP_Text")
            {
                image = "custom_TMP_Text_MaxVisibleCharacters_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<TMP_Text_MaxVisibleCharactersControlBehaviour>(),
                        label = "maxVisibleCharacters",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "padding","RectMask2D")
            {
                image = "custom_RectMask2D_Padding_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                        label = "padding",
                    }
                }
            });
            customAnimationCollection.animationCollections.Add(new TweenActionEffect( "softness","RectMask2D")
            {
                image = "custom_RectMask2D_Softness_example.png",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
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
