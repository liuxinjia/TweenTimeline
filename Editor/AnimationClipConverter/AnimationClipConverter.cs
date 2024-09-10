using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace Cr7Sund.TweenTimeLine
{
    public static class AnimationClipConverter
    {
        public const string TweenConfigGUID = "6f93bbb0aec8a6640a0e7680745f1d32";
        private static Dictionary<string, ComponentValuePair> tweenGenConfigs = new();
        public static Dictionary<Type, Dictionary<string, string>> ReplaceProperty = new Dictionary<Type, Dictionary<string, string>>()
        {
            {
                typeof(TextMeshProUGUI), new Dictionary<string, string>
                {
                    {
                        "m_fontColor", "color"
                    }
                }
            },
            // { typeof(PathMoveHelper), new Dictionary<string, string> { { "m_Value", "Value" }, { "Point2", "GetPoint2" }, { "Point1", "GetPoint1" } } },
            // { typeof(BezierMoveCurve), new Dictionary<string, string> { { "m_Value", "Value" } } },
            // { typeof(SkeletonGraphic), new Dictionary<string, string> { { "m_Color", "color" } } },
            // { typeof(UIParticle), new Dictionary<string, string> { { "m_Scale3D", "scale3D" } } },
        };


        [MenuItem("Tools/ConvertClip")]
        public static void ConvertClip()
        {
            var clip = Selection.activeObject as AnimationClip;
            InitTweenConfigs();

            var curveLibrary = new CurveLibraryCenter();
            curveLibrary.CreateCurveWrapperLibrary();
            curveLibrary.ConvertClip(clip);
            curveLibrary.GenCurveInfoDict();

            GenClipInfos(clip, curveLibrary);
        }


        public static List<GenClipInfo> GenClipInfos(AnimationClip clip, CurveLibraryCenter curveLibrary)
        {
            List<GenClipInfo> clipInfos = new();
            var keyWrapper = GenerateKeyFrameDatas(clip);
            string clipName = clip.name;

            foreach (var animTargetInfo in keyWrapper.Objects)
            {
                foreach (var typeKeyFrames in animTargetInfo.Types)
                {
                    foreach (PropertyKeyframes frameProperty in typeKeyFrames.Properties)
                    {
                        string targetTypeName, tweenMethod, curveName;
                        GetClipInfoName(clipName, typeKeyFrames.Type, frameProperty.PropertyName,
                        out targetTypeName, out tweenMethod, out curveName);
                        if (string.IsNullOrEmpty(curveName))
                        {
                            continue;
                        }
                        if (!curveLibrary.TryGetCurve(curveName, out var existingCurve))
                        {
                            Debug.LogError($"Curve not found for: {curveName}");
                            continue; // Skip to the next iteration
                        }

                        // RectTransformSizeDeltaControlTrack
                        var clipInfo = new GenClipInfo()
                        {
                            Duration =
                                clip.length,
                            EaseName =
                                curveName,
                            TweenMethod =
                                tweenMethod,
                            EndValue =
                                frameProperty.Keyframes[0].Value, // the curve is inverse
                            BindType = targetTypeName,
                            StartValue =
                                frameProperty.Keyframes[frameProperty.Keyframes.Count - 1].Value
                        };

                        clipInfos.Add(clipInfo);
                    }
                }
            }

            return clipInfos;
        }

        public static GenClipInfo GenClipInfo(string targetObject, string propertyName, AnimationClip clip, KeyframeDataWrapper keyWrapper)
        {
            GenClipInfo resultClipInfo = null;
            string clipName = clip.name;

            foreach (ObjectKeyframes animTargetInfo in keyWrapper.Objects)
            {
                if (!string.IsNullOrEmpty(animTargetInfo.ObjectKey) &&
                  animTargetInfo.ObjectKey != targetObject)
                {
                    continue;
                }
                foreach (TypeKeyframes typeKeyFrames in animTargetInfo.Types)
                {
                    foreach (PropertyKeyframes frameProperty in typeKeyFrames.Properties)
                    {
                        if (frameProperty.PropertyName != propertyName)
                        {
                            continue;
                        }

                        string targetTypeName, tweenMethod, curveName;
                        GetClipInfoName(clipName, typeKeyFrames.Type, frameProperty.PropertyName,
                        out targetTypeName, out tweenMethod, out curveName);
                        if (string.IsNullOrEmpty(curveName))
                        {
                            continue;
                        }
                        // if (!curveLibrary.TryGetCurve(curveName, out var existingCurve))
                        // {
                        //     Debug.LogError($"Curve not found for: {curveName}");
                        //     continue; // Skip to the next iteration
                        // }

                        // RectTransformSizeDeltaControlTrack
                        resultClipInfo = new GenClipInfo()
                        {
                            Duration =
                                clip.length,
                            EaseName =
                                curveName,
                            TweenMethod =
                                tweenMethod,
                            EndValue =
                                frameProperty.Keyframes[0].Value, // the curve is inverse
                            BindType = targetTypeName,
                            StartValue =
                                frameProperty.Keyframes[frameProperty.Keyframes.Count - 1].Value
                        };

                    }
                }
            }

            return resultClipInfo;
        }

        public static List<string> GenTrackInfos(string targetObject, KeyframeDataWrapper keyWrapper)
        {
            var properties = new HashSet<string>();

            foreach (ObjectKeyframes animTargetInfo in keyWrapper.Objects)
            {
                if (!string.IsNullOrEmpty(animTargetInfo.ObjectKey) &&
                 animTargetInfo.ObjectKey != targetObject)
                {
                    continue;
                }
                foreach (TypeKeyframes typeKeyFrames in animTargetInfo.Types)
                {
                    foreach (PropertyKeyframes frameProperty in typeKeyFrames.Properties)
                    {
                        if (!properties.Contains(frameProperty.PropertyName))
                            properties.Add(frameProperty.PropertyName);
                    }
                }
            }

            return properties.ToList();
        }

        public static KeyframeDataWrapper GenerateKeyFrameDatas(AnimationClip clip)
        {
            EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(clip);
            List<ObjectKeyframes> keyframeDataList = new List<ObjectKeyframes>();

            foreach (EditorCurveBinding binding in curveBindings)
            {
                AnimationCurve editorCurve = AnimationUtility.GetEditorCurve(clip, binding);
                Array.Sort(editorCurve.keys, (t1, t2) => t1.time.CompareTo(t2.time));
                // TODO remove duplicate curves
                for (int i = 0; i < editorCurve.keys.Length; i++)
                {
                    Keyframe keyframe = editorCurve.keys[i];
                    KeyframeData data = new KeyframeData
                    {
                        Time = keyframe.time,
                        Value = keyframe.value,
                        InTangent = keyframe.inTangent,
                        OutTangent = keyframe.outTangent,
                        Property = binding.propertyName,
                        InWeight = keyframe.inWeight,
                        OutWeight = keyframe.outWeight,
                        Path = binding.path,
                        Type = new SerializableType(binding.type),
                    };
                    AnimationClipConverter.MapProperty(binding.type, ref data.Property);

                    var value = keyframeDataList.Find(t1 => t1.ObjectKey == binding.path);
                    if (value == null)
                    {
                        keyframeDataList.Add(value = new ObjectKeyframes(binding.path, binding.type));
                    }

                    TypeKeyframes typeKeyframes = value.Types.Find(t1 => t1.Type == binding.type);
                    if (typeKeyframes == null)
                    {
                        value.Types.Add(typeKeyframes = new TypeKeyframes(binding.type));
                    }

                    PropertyKeyframes propertyKeyframes =
                        typeKeyframes.Properties.Find(t1 => t1.PropertyName == data.Property);
                    if (propertyKeyframes == null)
                    {
                        typeKeyframes.Properties.Add(propertyKeyframes = new PropertyKeyframes(data.Property));
                    }

                    propertyKeyframes.Keyframes.Add(data);
                }
            }
            var keyframeDataWrapper = new KeyframeDataWrapper();
            keyframeDataWrapper.AnimationName = clip.name;
            keyframeDataWrapper.Objects = keyframeDataList;
            return keyframeDataWrapper;
        }


        public static void InitTweenConfigs()
        {
            tweenGenConfigs = new();
            var tweenConfig = AssetDatabase.LoadAssetAtPath<TweenGenConfig>(AssetDatabase.GUIDToAssetPath(TweenConfigGUID));
            foreach (var item in tweenConfig.componentValuePairs)
            {
                string key = $"{item.GetPropertyMethod.ToUpper()}";
                if (!tweenGenConfigs.ContainsKey(key))
                    tweenGenConfigs.Add(key, item);
            }
        }

        public static void GetClipInfoName(string clipName, Type targetType, string propertyName, out string targetTypeName, out string tweenMethod, out string curveName)
        {
            clipName = clipName.Replace(" ", "");
            string simplifyTypeName = GetSimplifyTypeName(targetType.ToString());
            targetTypeName = targetType.ToString();
            tweenMethod = GetTweenMethod(propertyName, out var typeName);
            if (!string.IsNullOrEmpty(typeName))
            {
                targetTypeName = typeName;
            }
            if (string.IsNullOrEmpty(tweenMethod))
            {
                curveName = string.Empty;
            }
            else
            {
                curveName = $"{clipName}{simplifyTypeName}{tweenMethod}";
            }
        }

        public static HashSet<string> ignoreName = new HashSet<string>() {
              "enabled",
             "_endPos" };

        private static string GetTweenMethod(string propertyName, out string targetType)
        {
            targetType = null;

            string key = $"{propertyName.ToUpper()}";
            if (tweenGenConfigs.ContainsKey(key))
            {
                targetType = tweenGenConfigs[key].ComponentType;
                return tweenGenConfigs[key].PreTweenMethod;
            }
            else
            {
                if (!ignoreName.Contains(propertyName))
                    Debug.LogError($"Dont find  tween method : {propertyName}");
                return string.Empty;
            }
        }

        private static string GetSimplifyTypeName(string fullType)
        {
            return fullType.Substring(fullType.LastIndexOf('.') + 1);
        }

        public static void MapProperty(Type type, ref string property)
        {
            if (ReplaceProperty.TryGetValue(type, out var map))
            {
                var key = GetVariableName(property);
                if (map.TryGetValue(key, out var value))
                {
                    property = ReplaceVariableName(property, value);
                    return;
                }
            }

            if (type.Namespace?.StartsWith("UnityEngine") ?? false)
            {
                property = property.Replace("m_", "");
                property = property.Substring(0, 1).ToLower() + property.Substring(1);
            }

            if (type.Namespace?.StartsWith("UnityEngine") ?? false)
            {
                property = property.Replace("m_", "");
                property = property.Substring(0, 1).ToLower() + property.Substring(1);
                if (property.Contains("Raw."))
                    property = property.Replace("Raw.", ".");
            }
            if (typeof(Component).IsAssignableFrom(type))
            {
                if (property == "m_Enabled")
                {
                    property = "enabled";
                }
            }
        }


        private static string GetVariableName(string input)
        {
            string[] parts = input.Split('.');
            if (parts.Length == 0)
            {
                return input;
            }
            if (parts.Length - 2 >= 0 && parts.Length - 2 < parts.Length)
            {
                string result = parts[parts.Length - 2];
                return result;
            }
            return parts[parts.Length - 1];
        }

        private static string ReplaceVariableName(string input, string replacement)
        {
            string[] parts = input.Split('.');
            int index = parts.Length - 2;
            if (parts.Length == 0)
            {
                return replacement;
            }
            if (parts.Length - 2 >= 0 && parts.Length - 2 < parts.Length)
            {
                parts[index] = replacement;
            }
            else
            {
                parts[parts.Length - 1] = replacement;
            }
            string result = string.Join(".", parts);
            return result;
        }



        private static (bool isStruct, Type currentType) GetFieldType(Type objectType, string propertyPath)
        {
            if (objectType == null || string.IsNullOrEmpty(propertyPath))
            {
                return (false, null);
            }

            string[] properties = propertyPath.Split('.');
            bool isStruct = false;
            Type currentType = objectType;

            for (var index = 0; index < properties.Length; index++)
            {
                var property = properties[index];
                PropertyInfo propertyInfo = currentType.GetProperty(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (propertyInfo != null)
                {
                    currentType = propertyInfo.PropertyType;
                }
                else
                {
                    FieldInfo fieldInfo = currentType.GetField(property, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (fieldInfo != null)
                    {
                        currentType = fieldInfo.FieldType;
                    }
                }

                if (properties.Length > 1 && index == properties.Length - 2)
                {
                    if (currentType.IsValueType)
                    {
                        isStruct = true;
                    }
                }
            }

            return (isStruct, currentType);
        }

    }
}
