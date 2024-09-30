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
        public static TweenCollection CreateBaseAnimationCollection()
        {
            var customAnimationCollection = new TweenCollection("Custom");
            var animEffect = customAnimationCollection.animationCollections;
            
            animEffect.Add(new TweenActionEffect( "range","Light")
            {
                image = "custom_Light_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Light_RangeControlBehaviour>(),
                        label = "range",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "shadowStrength","Light")
            {
                image = "custom_Light_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Light_ShadowStrengthControlBehaviour>(),
                        label = "shadowStrength",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "intensity","Light")
            {
                image = "custom_Light_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Light_IntensityControlBehaviour>(),
                        label = "intensity",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "orthographicSize","Camera")
            {
                image = "custom_Camera_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Camera_OrthographicSizeControlBehaviour>(),
                        label = "orthographicSize",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "backgroundColor","Camera")
            {
                image = "custom_Camera_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Camera_BackgroundColorControlBehaviour>(),
                        label = "backgroundColor",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "aspect","Camera")
            {
                image = "custom_Camera_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Camera_AspectControlBehaviour>(),
                        label = "aspect",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "farClipPlane","Camera")
            {
                image = "custom_Camera_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Camera_FarClipPlaneControlBehaviour>(),
                        label = "farClipPlane",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "fieldOfView","Camera")
            {
                image = "custom_Camera_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Camera_FieldOfViewControlBehaviour>(),
                        label = "fieldOfView",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "nearClipPlane","Camera")
            {
                image = "custom_Camera_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Camera_NearClipPlaneControlBehaviour>(),
                        label = "nearClipPlane",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "pixelRect","Camera")
            {
                image = "custom_Camera_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(x:0.00, y:0.00, width:0.00, height:0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Camera_PixelRectControlBehaviour>(),
                        label = "pixelRect",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "rect","Camera")
            {
                image = "custom_Camera_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(x:0.00, y:0.00, width:0.00, height:0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Camera_RectControlBehaviour>(),
                        label = "rect",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "localRotation.eulerAngles","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_LocalRotationEulerAnglesControlBehaviour>(),
                        label = "localRotation.eulerAngles",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "localScale","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
                        label = "localScale",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "eulerAngles","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_EulerAnglesControlBehaviour>(),
                        label = "eulerAngles",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "value","Slider")
            {
                image = "custom_Slider_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Slider_ValueControlBehaviour>(),
                        label = "value",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "GetNormalizedPosition()","ScrollRect")
            {
                image = "custom_ScrollRect_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<ScrollRect_GetNormalizedPositionControlBehaviour>(),
                        label = "GetNormalizedPosition()",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "horizontalNormalizedPosition","ScrollRect")
            {
                image = "custom_ScrollRect_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<ScrollRect_HorizontalNormalizedPositionControlBehaviour>(),
                        label = "horizontalNormalizedPosition",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "verticalNormalizedPosition","ScrollRect")
            {
                image = "custom_ScrollRect_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<ScrollRect_VerticalNormalizedPositionControlBehaviour>(),
                        label = "verticalNormalizedPosition",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "pivot.x","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_PivotXControlBehaviour>(),
                        label = "pivot.x",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "pivot.y","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_PivotYControlBehaviour>(),
                        label = "pivot.y",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "pivot","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_PivotControlBehaviour>(),
                        label = "pivot",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "anchorMax","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_AnchorMaxControlBehaviour>(),
                        label = "anchorMax",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "anchorMin","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_AnchorMinControlBehaviour>(),
                        label = "anchorMin",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "anchoredPosition3D","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_AnchoredPosition3DControlBehaviour>(),
                        label = "anchoredPosition3D",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "anchoredPosition3D.x","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_AnchoredPosition3DXControlBehaviour>(),
                        label = "anchoredPosition3D.x",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "anchoredPosition3D.y","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_AnchoredPosition3DYControlBehaviour>(),
                        label = "anchoredPosition3D.y",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "anchoredPosition3D.z","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_AnchoredPosition3DZControlBehaviour>(),
                        label = "anchoredPosition3D.z",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "effectDistance","Shadow")
            {
                image = "custom_Shadow_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Shadow_EffectDistanceControlBehaviour>(),
                        label = "effectDistance",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "effectColor.a","Shadow")
            {
                image = "custom_Shadow_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Shadow_EffectColorAControlBehaviour>(),
                        label = "effectColor.a",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "effectColor","Shadow")
            {
                image = "custom_Shadow_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Shadow_EffectColorControlBehaviour>(),
                        label = "effectColor",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "GetPreferredSize()","LayoutElement")
            {
                image = "custom_LayoutElement_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<LayoutElement_GetPreferredSizeControlBehaviour>(),
                        label = "GetPreferredSize()",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "preferredWidth","LayoutElement")
            {
                image = "custom_LayoutElement_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<LayoutElement_PreferredWidthControlBehaviour>(),
                        label = "preferredWidth",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "preferredHeight","LayoutElement")
            {
                image = "custom_LayoutElement_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<LayoutElement_PreferredHeightControlBehaviour>(),
                        label = "preferredHeight",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "position","Rigidbody")
            {
                image = "custom_Rigidbody_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Rigidbody_PositionControlBehaviour>(),
                        label = "position",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "rotation","Rigidbody")
            {
                image = "custom_Rigidbody_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00000, 0.00000, 0.00000, 1.00000)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Rigidbody_RotationControlBehaviour>(),
                        label = "rotation",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "position","Rigidbody2D")
            {
                image = "custom_Rigidbody2D_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Rigidbody2D_PositionControlBehaviour>(),
                        label = "position",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "rotation","Rigidbody2D")
            {
                image = "custom_Rigidbody2D_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Rigidbody2D_RotationControlBehaviour>(),
                        label = "rotation",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "GetFlexibleSize()","LayoutElement")
            {
                image = "custom_LayoutElement_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<LayoutElement_GetFlexibleSizeControlBehaviour>(),
                        label = "GetFlexibleSize()",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "flexibleWidth","LayoutElement")
            {
                image = "custom_LayoutElement_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<LayoutElement_FlexibleWidthControlBehaviour>(),
                        label = "flexibleWidth",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "flexibleHeight","LayoutElement")
            {
                image = "custom_LayoutElement_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<LayoutElement_FlexibleHeightControlBehaviour>(),
                        label = "flexibleHeight",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "GetMinSize()","LayoutElement")
            {
                image = "custom_LayoutElement_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<LayoutElement_GetMinSizeControlBehaviour>(),
                        label = "GetMinSize()",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "minWidth","LayoutElement")
            {
                image = "custom_LayoutElement_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<LayoutElement_MinWidthControlBehaviour>(),
                        label = "minWidth",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "minHeight","LayoutElement")
            {
                image = "custom_LayoutElement_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<LayoutElement_MinHeightControlBehaviour>(),
                        label = "minHeight",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "position","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_PositionControlBehaviour>(),
                        label = "position",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "position.x","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_PositionXControlBehaviour>(),
                        label = "position.x",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "position.y","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_PositionYControlBehaviour>(),
                        label = "position.y",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "position.z","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_PositionZControlBehaviour>(),
                        label = "position.z",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "localPosition","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_LocalPositionControlBehaviour>(),
                        label = "localPosition",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "localPosition.x","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_LocalPositionXControlBehaviour>(),
                        label = "localPosition.x",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "localPosition.y","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_LocalPositionYControlBehaviour>(),
                        label = "localPosition.y",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "localPosition.z","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_LocalPositionZControlBehaviour>(),
                        label = "localPosition.z",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "rotation.eulerAngles","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_RotationEulerAnglesControlBehaviour>(),
                        label = "rotation.eulerAngles",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "localEulerAngles","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_LocalEulerAnglesControlBehaviour>(),
                        label = "localEulerAngles",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "localScale","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_LocalScaleControlBehaviour>(),
                        label = "localScale",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "localScale.x","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_LocalScaleXControlBehaviour>(),
                        label = "localScale.x",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "localScale.y","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_LocalScaleYControlBehaviour>(),
                        label = "localScale.y",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "localScale.z","Transform")
            {
                image = "custom_Transform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Transform_LocalScaleZControlBehaviour>(),
                        label = "localScale.z",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "color","Graphic")
            {
                image = "custom_Graphic_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Graphic_ColorControlBehaviour>(),
                        label = "color",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "color","SpriteRenderer")
            {
                image = "custom_SpriteRenderer_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<SpriteRenderer_ColorControlBehaviour>(),
                        label = "color",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "color","Material")
            {
                image = "custom_Material_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Material_ColorControlBehaviour>(),
                        label = "color",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "color","Light")
            {
                image = "custom_Light_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Light_ColorControlBehaviour>(),
                        label = "color",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "anchoredPosition","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_AnchoredPositionControlBehaviour>(),
                        label = "anchoredPosition",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "anchoredPosition.x","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_AnchoredPositionXControlBehaviour>(),
                        label = "anchoredPosition.x",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "anchoredPosition.y","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_AnchoredPositionYControlBehaviour>(),
                        label = "anchoredPosition.y",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "sizeDelta","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_SizeDeltaControlBehaviour>(),
                        label = "sizeDelta",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "alpha","CanvasGroup")
            {
                image = "custom_CanvasGroup_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<CanvasGroup_AlphaControlBehaviour>(),
                        label = "alpha",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "color.a","Graphic")
            {
                image = "custom_Graphic_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Graphic_ColorAControlBehaviour>(),
                        label = "color.a",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "color.a","SpriteRenderer")
            {
                image = "custom_SpriteRenderer_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<SpriteRenderer_ColorAControlBehaviour>(),
                        label = "color.a",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "color.a","Material")
            {
                image = "custom_Material_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Material_ColorAControlBehaviour>(),
                        label = "color.a",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "mainTextureOffset","Material")
            {
                image = "custom_Material_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Material_MainTextureOffsetControlBehaviour>(),
                        label = "mainTextureOffset",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "mainTextureScale","Material")
            {
                image = "custom_Material_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Material_MainTextureScaleControlBehaviour>(),
                        label = "mainTextureScale",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "fillAmount","Image")
            {
                image = "custom_Image_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Image_FillAmountControlBehaviour>(),
                        label = "fillAmount",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "volume","AudioSource")
            {
                image = "custom_AudioSource_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<AudioSource_VolumeControlBehaviour>(),
                        label = "volume",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "pitch","AudioSource")
            {
                image = "custom_AudioSource_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<AudioSource_PitchControlBehaviour>(),
                        label = "pitch",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "panStereo","AudioSource")
            {
                image = "custom_AudioSource_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<AudioSource_PanStereoControlBehaviour>(),
                        label = "panStereo",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "timeScale","Tween")
            {
                image = "custom_Tween_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Tween_TimeScaleControlBehaviour>(),
                        label = "timeScale",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "timeScale","Sequence")
            {
                image = "custom_Sequence_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<Sequence_TimeScaleControlBehaviour>(),
                        label = "timeScale",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "GetResolvedStyleRect()","VisualElement")
            {
                image = "custom_VisualElement_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(x:0.00, y:0.00, width:0.00, height:0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<VisualElement_GetResolvedStyleRectControlBehaviour>(),
                        label = "GetResolvedStyleRect()",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "position","ITransform")
            {
                image = "custom_ITransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<ITransform_PositionControlBehaviour>(),
                        label = "position",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "rotation","ITransform")
            {
                image = "custom_ITransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00000, 0.00000, 0.00000, 1.00000)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<ITransform_RotationControlBehaviour>(),
                        label = "rotation",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "scale","ITransform")
            {
                image = "custom_ITransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<ITransform_ScaleControlBehaviour>(),
                        label = "scale",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "layout.size","VisualElement")
            {
                image = "custom_VisualElement_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<VisualElement_LayoutSizeControlBehaviour>(),
                        label = "layout.size",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "GetTopLeft()","VisualElement")
            {
                image = "custom_VisualElement_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<VisualElement_GetTopLeftControlBehaviour>(),
                        label = "GetTopLeft()",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "style.color.value","VisualElement")
            {
                image = "custom_VisualElement_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<VisualElement_StyleColorValueControlBehaviour>(),
                        label = "style.color.value",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "style.backgroundColor.value","VisualElement")
            {
                image = "custom_VisualElement_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "RGBA(1.000, 1.000, 1.000, 1.000)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<VisualElement_StyleBackgroundColorValueControlBehaviour>(),
                        label = "style.backgroundColor.value",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "offsetMin","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_OffsetMinControlBehaviour>(),
                        label = "offsetMin",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "offsetMin.x","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_OffsetMinXControlBehaviour>(),
                        label = "offsetMin.x",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "offsetMin.y","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_OffsetMinYControlBehaviour>(),
                        label = "offsetMin.y",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "offsetMax","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_OffsetMaxControlBehaviour>(),
                        label = "offsetMax",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "offsetMax.x","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_OffsetMaxXControlBehaviour>(),
                        label = "offsetMax.x",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "offsetMax.y","RectTransform")
            {
                image = "custom_RectTransform_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectTransform_OffsetMaxYControlBehaviour>(),
                        label = "offsetMax.y",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "maxVisibleCharacters","TMP_Text")
            {
                image = "custom_TMP_Text_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "0", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<TMP_Text_MaxVisibleCharactersControlBehaviour>(),
                        label = "maxVisibleCharacters",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "padding","RectMask2D")
            {
                image = "custom_RectMask2D_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0.00, 0.00, 0.00, 0.00)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectMask2D_PaddingControlBehaviour>(),
                        label = "padding",
                    }
                }
            });
            animEffect.Add(new TweenActionEffect( "softness","RectMask2D")
            {
                image = "custom_RectMask2D_example.png",
                collectionCategory = "Custom",
                animationSteps = new List<TweenActionStep>
                {
                    new TweenActionStep
                    {
                        EndPos = "(0, 0)", 
                        isRelative = true,
                        tweenMethod = TweenActionContainerBuilder.GetTweenMethodName<RectMask2D_SoftnessControlBehaviour>(),
                        label = "softness",
                    }
                }
            }); 
             return customAnimationCollection;
            }
    }
}
