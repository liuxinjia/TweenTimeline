using System;
using System.Collections.Generic;
using System.Reflection;
using Cr7Sund.GraphicTween;
using Cr7Sund.TransformTween;
using PrimeTween;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    [System.Serializable]
    public class TweenActionStep
    {
        public string label;
        public string tweenMethod;
        public string EndPos;
        public string StartPos;
        public bool isRelative;
        public float startTimeOffset; // 动画结束时间点（正数为时间点，0为整个duration, 负数表示提前开始）
        public HashSet<string> NotAdditiveSet = new()
        {
            nameof(Transform_LocalScaleControlBehaviour),
        };

        public TweenActionStep()
        {
        }

        public TweenActionStep(string endPos, string startPos, string methodName)
        {
            EndPos = endPos;
            StartPos = startPos;
            tweenMethod = methodName;
        }

        public Type GetComponentType()
        {
            Type tweenBehaviourType = GetTweenBehaviourType();

            var componentType = AniActionEditToolHelper.GetFirstGenericType(tweenBehaviourType);
            return componentType;
        }

        public Type GetTweenBehaviourType()
        {
            var methodName = $"{tweenMethod}";
            string fullTypeName = GetMethodFullType(methodName);
            Assembly assembly = GetTweenTrackAssembly(fullTypeName);

            return assembly.GetType(fullTypeName);
        }

        public static Assembly GetTweenTrackAssembly(string fullTypeName)
        {
            var tweenBehaviourType = typeof(Graphic_ColorAControlBehaviour).Assembly.GetType(fullTypeName);
            if (tweenBehaviourType == null)
            {
                tweenBehaviourType = TweenTimelineDefine.CustomAssembly.GetType(fullTypeName);
                if (tweenBehaviourType == null)
                {
                    throw new Exception($"Don't find behaviour Component {fullTypeName}");
                }
            }

            return tweenBehaviourType.Assembly;
        }

        private static string GetMethodFullType(string methodName)
        {
            string prefix = methodName.Split('_')[0];
            if (methodName.StartsWith("TMP_Text"))
            {
                prefix = "TMP_Text";
            }
            return $"Cr7Sund.{prefix}Tween.{methodName}";
        }

        public Type GetComponentValueType()
        {
            var tweenBehaviourType = GetTweenBehaviourType();
            var componentType = AniActionEditToolHelper.GetSecondGenericType(tweenBehaviourType);
            return componentType;
        }


        public void GetAnimUnitClipInfo(TweenActionEffect animAction, EasingTokenPresetLibrary easingTokenPresetLibrary,
            out ClipInfoContext clipInfo, out Component component)
        {
            clipInfo = new ClipInfoContext();
            var componentValueType = GetComponentValueType();
            var componentType = GetComponentType();
            component = animAction.target.GetComponent(componentType);
            Type tweenBehaviourType = GetTweenBehaviourType();
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
                    endPos = TypeConverter.ConvertToOriginalType(EndPos, componentValueType);
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
                float offsetStartTime = AniActionEditToolHelper.ConvertDuration(startTimeOffset);
                duration = offsetStartTime > 0
                    ? offsetStartTime
                    : duration + offsetStartTime;
            }

            clipInfo.startPos = startPos;
            clipInfo.endPos = endPos;
            clipInfo.duration = duration;
            MaterialEasingToken animActionEaseToken = animAction.easeToken;
            clipInfo.easePreset = easingTokenPresetLibrary.GetEasePreset(animActionEaseToken);
        }

        public object GetCurPos(TweenActionEffect animAction, EasingTokenPresetLibrary easingTokenPresetLibrary)
        {
            Type tweenBehaviourType;
            Component component;
            MethodInfo createTweenMethodInfo, getMethodInfo, setMethodInfo;
            object target;
            GetTweenMethodInfo(animAction, easingTokenPresetLibrary, out tweenBehaviourType, out var clpInfo, out component, out createTweenMethodInfo, out getMethodInfo, out setMethodInfo, out target);

            var initPos = getMethodInfo.Invoke(target, new[]
            {
                component
            });

            return initPos;
        }

        public Tween GenerateTween(TweenActionEffect animAction, EasingTokenPresetLibrary easingTokenPresetLibrary, out Action onResetAction)
        {
            Type tweenBehaviourType;
            Component component;
            MethodInfo createTweenMethodInfo, getMethodInfo, setMethodInfo;
            object target;
            GetTweenMethodInfo(animAction, easingTokenPresetLibrary, out tweenBehaviourType, out var trackInfo, 
            out component, out createTweenMethodInfo, out getMethodInfo, out setMethodInfo, out target);

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

        private void GetTweenMethodInfo(TweenActionEffect animAction, EasingTokenPresetLibrary easingTokenPresetLibrary,
         out Type tweenBehaviourType, out ClipInfoContext clipInfo, out Component component, out MethodInfo createTweenMethodInfo, out MethodInfo getMethodInfo, out MethodInfo setMethodInfo, out object target)
        {
            tweenBehaviourType = GetTweenBehaviourType();
            GetAnimUnitClipInfo(animAction, easingTokenPresetLibrary, out clipInfo, out component);
            createTweenMethodInfo = tweenBehaviourType.GetMethod("OnCreateTween", BindingFlags.NonPublic | BindingFlags.Instance);
            getMethodInfo = tweenBehaviourType.GetMethod("OnGet", BindingFlags.NonPublic | BindingFlags.Instance);
            setMethodInfo = tweenBehaviourType.GetMethod("OnSet", BindingFlags.NonPublic | BindingFlags.Instance);
            target = Activator.CreateInstance(tweenBehaviourType);
        }

    }

}
