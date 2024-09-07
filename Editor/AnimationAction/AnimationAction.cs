using System;
using System.Collections.Generic;
using System.Reflection;
using Cr7Sund.TweenTimeLine;
using Cr7Sund.TweenTimeLine.Editor;
using PrimeTween;
using UnityEditor;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    [System.Serializable]
    public struct AnimationCollection
    {
        public List<AnimationEffect> animationCollections;
        public string category;

        public AnimationCollection(string name)
        {
            animationCollections = new();
            this.category = name;
        }

        public Dictionary<string, List<AnimationEffect>> GetAnimActionCategory()
        {
            var actionCategoryList = new Dictionary<string, List<AnimationEffect>>();
            foreach (var action in animationCollections)
            {
                if (!actionCategoryList.TryGetValue(action.effectCategory, out var list))
                {
                    list = new List<AnimationEffect>();
                    actionCategoryList.Add(action.effectCategory, list);
                }

                bool containsComponent = true;
                foreach (var actionUnit in action.animationSteps)
                {
                    var componentType = actionUnit.GetComponentType();
                    if (componentType == typeof(UnityEngine.UI.Graphic) ||
                        componentType == typeof(UnityEngine.RectTransform) ||
                        componentType == typeof(UnityEngine.Transform))
                    {
                        continue;
                    }

                    if (Selection.activeGameObject == null)
                    {
                        continue;
                    }
                    var component = Selection.activeGameObject.GetComponent((Type)componentType);
                    if (component == null)
                    {
                        containsComponent = false;
                        break;
                    }
                }

                if (containsComponent)
                {
                    list.Add(action);
                }
            }

            return actionCategoryList;
        }
    }

    [System.Serializable]
    public class AnimationEffect
    {
        public string label;
        [HideInInspector] public string image;
        public string effectCategory;

        public UnityEngine.GameObject target;
        public DurationToken durationToken = DurationToken.Medium1;
        public MaterialEasingToken easeToken = MaterialEasingToken.Emphasized;



        public List<AnimationStep> animationSteps = new();

        public AnimationEffect()
        {
        }

        public AnimationEffect(string name, string category)
        {
            label = name;
            effectCategory = category;
            durationToken = DurationToken.Medium2;
            easeToken = MaterialEasingToken.Standard;
        }

        internal void CopyFrom(AnimationEffect animAction)
        {
            if (animAction == null)
            {
                throw new ArgumentNullException(nameof(animAction), "Cannot copy from a null object.");
            }

            // target = animAction.target;
            animationSteps = animAction.animationSteps;
            image = animAction.image;
            durationToken = animAction.durationToken;
            easeToken = animAction.easeToken;
        }

        public float ConvertDuration()
        {
            return (float)durationToken / 1000f;
        }
    }

    [System.Serializable]
    public class AnimationStep
    {
        public string label;
        public string tweenMethod;
        public string EndPos;
        public string StartPos;
        public bool useCurPos;
        public float startTimeOffset; // 动画结束时间点（正数为时间点，0为整个duration, 负数表示提前开始）
        public Component animationTarget;
        // public DurationToken durationToken;
        // public EasingToken ease;

        public AnimationStep()
        {
        }

        public AnimationStep(string endPos, string startPos, string methodName)
        {
            EndPos = endPos;
            StartPos = startPos;
            tweenMethod = methodName;
        }

        public Type GetComponentType()
        {
            var methodName = $"{tweenMethod}ControlBehaviour";
            string fullTypeName = $"Cr7Sund.TweenTimeLine.{methodName}";
            var tweenBehaviourType = typeof(GraphicColorAControlBehaviour).Assembly.GetType(fullTypeName);
            var componentType = AniActionEditToolHelper.GetFirstGenericType(tweenBehaviourType);
            return componentType;
        }
        public Type GetComponentValueType()
        {
            var methodName = $"{tweenMethod}ControlBehaviour";
            string fullTypeName = $"Cr7Sund.TweenTimeLine.{methodName}";
            var tweenBehaviourType = typeof(GraphicColorAControlBehaviour).Assembly.GetType(fullTypeName);
            var componentType = AniActionEditToolHelper.GetSecondGenericType(tweenBehaviourType);
            return componentType;
        }

        public void GetAnimUnitTrackInfo(AnimationEffect animAction, EasingTokenPresets easingTokenPreset,
            out TrackInfo trackInfo, out Component component)
        {
            trackInfo = new TrackInfo();
            var componentValueType = GetComponentValueType();
            var componentType = GetComponentType();
            component = animAction.target.GetComponent(componentType);
            var methodName = $"{tweenMethod}ControlBehaviour";
            var fullTypeName = $"Cr7Sund.TweenTimeLine.{methodName}";
            var tweenBehaviourType = typeof(GraphicColorAControlBehaviour).Assembly.GetType(fullTypeName);
            var getMethodInfo = tweenBehaviourType.GetMethod("OnGet", BindingFlags.NonPublic | BindingFlags.Instance);
            var target = Activator.CreateInstance(tweenBehaviourType);

            object startPos = null, endPos = null;
            var initPos = getMethodInfo.Invoke(target, new[]
            {
                component
            });
            if (useCurPos)
            {
                startPos = initPos;
                endPos = TypeConverter.AddDelta(startPos, EndPos, componentValueType);
            }
            else
            {
                startPos = TypeConverter.ConvertToOriginalType(StartPos, componentValueType);
                endPos = TypeConverter.ConvertToOriginalType(EndPos, componentValueType);
            }

            float duration = animAction.ConvertDuration();
            if (startTimeOffset != 0)
            {
                duration = startTimeOffset > 0
                    ? startTimeOffset
                    : duration + startTimeOffset;
            }

            trackInfo.startPos = startPos;
            trackInfo.endPos = endPos;
            trackInfo.duration = duration;
            trackInfo.easePreset = easingTokenPreset.GetEasePreset(animAction.easeToken);
        }

        public Tween GenerateTween(AnimationEffect animAction, EasingTokenPresets easingTokenPreset, out Action onResetAction)
        {
            var methodName = $"{tweenMethod}ControlBehaviour";
            string fullTypeName = $"Cr7Sund.TweenTimeLine.{methodName}";
            var tweenBehaviourType = typeof(GraphicColorAControlBehaviour).Assembly.GetType(fullTypeName);
            GetAnimUnitTrackInfo(animAction, easingTokenPreset, out var trackInfo, out var component);
            var createTweenMethodInfo = tweenBehaviourType.GetMethod("OnCreateTween", BindingFlags.NonPublic | BindingFlags.Instance);
            var getMethodInfo = tweenBehaviourType.GetMethod("OnGet", BindingFlags.NonPublic | BindingFlags.Instance);
            var setMethodInfo = tweenBehaviourType.GetMethod("OnSet", BindingFlags.NonPublic | BindingFlags.Instance);
            var target = Activator.CreateInstance(tweenBehaviourType);

            // Reflectively set properties from AnimationSettings
            FieldInfo destPosFieldInfo = tweenBehaviourType.GetField("_endPos", BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo easeFieldInfo = tweenBehaviourType.BaseType.GetField("_easePreset", BindingFlags.NonPublic | BindingFlags.Instance);

            var initPos = getMethodInfo.Invoke(target, new[]
            {
                component
            });
            destPosFieldInfo.SetValue(target, trackInfo.endPos);
            easeFieldInfo.SetValue(target, trackInfo.easePreset);

            Tween tweenValue = (PrimeTween.Tween)createTweenMethodInfo.Invoke(target, new object[]
            {
                component, trackInfo.duration,
                trackInfo.startPos
            });
            onResetAction = () =>
            {
                setMethodInfo?.Invoke(target, new[]
                {
                    component, initPos
                });
            };
            return tweenValue;
        }
    }

}
