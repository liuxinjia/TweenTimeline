using System;
using System.Collections.Generic;
using System.Reflection;
using Cr7Sund.TweenTimeLine.Editor;
using Cr7Sund.GraphicTweeen;
using Cr7Sund.TransformTweeen;
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

        public Dictionary<string, List<AnimationEffect>> FilterActionCategory(string filterName, GameObject selectGO)
        {
            var actionCategoryList = new Dictionary<string, List<AnimationEffect>>();

            foreach (var action in animationCollections)
            {
                if (!action.label.Contains(filterName)) continue;

                bool containsComponent = false;
                foreach (var actionUnit in action.animationSteps)
                {
                    var componentType = actionUnit.GetComponentType();
                    if (!typeof(Component).IsAssignableFrom(componentType))
                    {
                        continue;
                    }
                    if (selectGO == null)
                    {
                        continue;
                    }

                    var component = selectGO.GetComponent((Type)componentType);
                    containsComponent = component != null;
                }

                if (containsComponent)
                {
                    if (!actionCategoryList.TryGetValue(action.effectCategory, out var list))
                    {
                        list = new List<AnimationEffect>();
                        actionCategoryList.Add(action.effectCategory, list);
                    }
                    list.Add(action);
                }
            }

            return actionCategoryList;
        }
        public Dictionary<string, List<AnimationEffect>> GetAnimActionCategory(GameObject selectGO)
        {
            var actionCategoryList = new Dictionary<string, List<AnimationEffect>>();

            foreach (var action in animationCollections)
            {
                if (action.label.Contains('.'))
                {
                    continue;
                }

                bool containsComponent = false;
                foreach (var actionUnit in action.animationSteps)
                {
                    var componentType = actionUnit.GetComponentType();
                    if (componentType == typeof(UnityEngine.Transform)) // default show
                    {
                        containsComponent = true;
                        continue;
                    }

                    if (!typeof(Component).IsAssignableFrom(componentType))
                    {
                        continue;
                    }
                    if (selectGO == null)
                    {
                        continue;
                    }

                    var component = selectGO.GetComponent((Type)componentType);
                    containsComponent = component != null;
                }

                if (containsComponent)
                {
                    if (!actionCategoryList.TryGetValue(action.effectCategory, out var list))
                    {
                        list = new List<AnimationEffect>();
                        actionCategoryList.Add(action.effectCategory, list);
                    }
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
        public bool isRelative;
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
            var methodName = $"{tweenMethod}";
            string fullTypeName = GetMethodFullType(methodName);
            var tweenBehaviourType = typeof(Graphic_ColorAControlBehaviour).Assembly.GetType(fullTypeName);
            if (tweenBehaviourType == null)
            {
                Debug.LogError(methodName);
            }
            var componentType = AniActionEditToolHelper.GetFirstGenericType(tweenBehaviourType);
            return componentType;
        }

        private static string GetMethodFullType(string methodName)
        {
            string prefix = methodName.Split('_')[0];
            if (methodName.StartsWith("TMP_Text"))
            {
                prefix = "TMP_Text";
            }
            return $"Cr7Sund.{prefix}Tweeen.{methodName}";
        }

        public Type GetComponentValueType()
        {
            var methodName = $"{tweenMethod}";
            string fullTypeName = GetMethodFullType(methodName);
            var tweenBehaviourType = typeof(Graphic_ColorAControlBehaviour).Assembly.GetType(fullTypeName);
            var componentType = AniActionEditToolHelper.GetSecondGenericType(tweenBehaviourType);
            return componentType;
        }
        public HashSet<string> NotAdditiveSet = new()
        {
            nameof(Transform_LocalScaleControlBehaviour),
        };
        
        public void GetAnimUnitTrackInfo(AnimationEffect animAction, EasingTokenPresets easingTokenPreset,
            out TrackInfo trackInfo, out Component component)
        {
            trackInfo = new TrackInfo();
            var componentValueType = GetComponentValueType();
            var componentType = GetComponentType();
            component = animAction.target.GetComponent(componentType);
            var fullTypeName = GetMethodFullType(tweenMethod);
            var tweenBehaviourType = typeof(Graphic_ColorAControlBehaviour).Assembly.GetType(fullTypeName);
            var getMethodInfo = tweenBehaviourType.GetMethod("OnGet", BindingFlags.NonPublic | BindingFlags.Instance);
            var target = Activator.CreateInstance(tweenBehaviourType);

            object startPos = null, endPos = null;
            var initPos = getMethodInfo.Invoke(target, new[]
            {
                component
            });
            if (isRelative)
            {
                startPos = initPos;
                if (NotAdditiveSet.Contains((tweenMethod)))
                {
                    endPos = TypeConverter.ConvertToOriginalType(EndPos,componentValueType);
                }
                else
                {
                    endPos = TypeConverter.AddDelta(startPos, EndPos, componentValueType);
                }
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

        public object GetCurPos(AnimationEffect animAction, EasingTokenPresets easingTokenPreset)
        {
            Type tweenBehaviourType;
            TrackInfo trackInfo;
            Component component;
            MethodInfo createTweenMethodInfo, getMethodInfo, setMethodInfo;
            object target;
            GetTweenMethodInfo(animAction, easingTokenPreset, out tweenBehaviourType, out trackInfo, out component, out createTweenMethodInfo, out getMethodInfo, out setMethodInfo, out target);

            var initPos = getMethodInfo.Invoke(target, new[]
              {
                component
            });

            return initPos;
        }

        public Tween GenerateTween(AnimationEffect animAction, EasingTokenPresets easingTokenPreset, out Action onResetAction)
        {
            Type tweenBehaviourType;
            TrackInfo trackInfo;
            Component component;
            MethodInfo createTweenMethodInfo, getMethodInfo, setMethodInfo;
            object target;
            GetTweenMethodInfo(animAction, easingTokenPreset, out tweenBehaviourType, out trackInfo, out component, out createTweenMethodInfo, out getMethodInfo, out setMethodInfo, out target);

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

        private void GetTweenMethodInfo(AnimationEffect animAction, EasingTokenPresets easingTokenPreset, out Type tweenBehaviourType, out TrackInfo trackInfo, out Component component, out MethodInfo createTweenMethodInfo, out MethodInfo getMethodInfo, out MethodInfo setMethodInfo, out object target)
        {
            var methodName = $"{tweenMethod}";
            var fullTypeName = GetMethodFullType(methodName);
            tweenBehaviourType = typeof(Graphic_ColorAControlBehaviour).Assembly.GetType(fullTypeName);
            GetAnimUnitTrackInfo(animAction, easingTokenPreset, out trackInfo, out component);
            createTweenMethodInfo = tweenBehaviourType.GetMethod("OnCreateTween", BindingFlags.NonPublic | BindingFlags.Instance);
            getMethodInfo = tweenBehaviourType.GetMethod("OnGet", BindingFlags.NonPublic | BindingFlags.Instance);
            setMethodInfo = tweenBehaviourType.GetMethod("OnSet", BindingFlags.NonPublic | BindingFlags.Instance);
            target = Activator.CreateInstance(tweenBehaviourType);
        }

    }

}
