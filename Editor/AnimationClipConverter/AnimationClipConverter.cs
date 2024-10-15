using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    public static class AnimationClipConverter
    {
        private static Dictionary<Type, Dictionary<string, string>> ReplaceProperty = new Dictionary<Type, Dictionary<string, string>>()
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

        private static readonly HashSet<string> _ignoreName = new HashSet<string>()
        {
            "enabled",
            "isActive",
            "sprite"
        };

        public static KeyframeDataWrapper GenerateKeyFrameDatas(AnimationClip clip)
        {
            EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(clip);
            List<ObjectKeyframes> keyframeDataList = new List<ObjectKeyframes>();

            foreach (EditorCurveBinding binding in curveBindings)
            {
                AnimationCurve editorCurve = AnimationUtility.GetEditorCurve(clip, binding);
                Keyframe[] keys = editorCurve.keys;
                Array.Sort(keys, (t1, t2) => t1.time.CompareTo(t2.time));
                // TODO remove duplicate curves
                for (int i = 0; i < keys.Length; i++)
                {
                    Keyframe keyframe = keys[i];
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
                    MapProperty(binding.type, ref data.Property);

                    var value = keyframeDataList.Find(t1 => t1.ObjectPath == binding.path);
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

        public static KeyframeDataWrapper GenerateObjectKeyFrameDatas(AnimationClip clip)
        {
            EditorCurveBinding[] curveBindings = AnimationUtility.GetObjectReferenceCurveBindings(clip);
            List<ObjectKeyframes> keyframeDataList = new List<ObjectKeyframes>();

            foreach (EditorCurveBinding binding in curveBindings)
            {
                var keys = AnimationUtility.GetObjectReferenceCurve(clip, binding);
                Array.Sort(keys, (t1, t2) => t1.time.CompareTo(t2.time));
                // TODO remove duplicate curves
                for (int i = 0; i < keys.Length; i++)
                {
                    var keyframe = keys[i];
                    KeyframeData data = new KeyframeData
                    {
                        Time = keyframe.time,
                        Value = keyframe.value,
                        // InTangent = keyframe.inTangent,
                        // OutTangent = keyframe.outTangent,
                        Property = binding.propertyName,
                        // InWeight = keyframe.inWeight,
                        // OutWeight = keyframe.outWeight,
                        Path = binding.path,
                        Type = new SerializableType(binding.type),
                    };
                    MapProperty(binding.type, ref data.Property);

                    var value = keyframeDataList.Find(t1 => t1.ObjectPath == binding.path);
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

        public static void GetClipInfoName(string clipName, Type targetType, string propertyName, string bindTarget,
         out string targetTypeName, out string tweenIdentifier, out string curveName)
        {
            clipName = clipName.Replace(" ", "");
            targetTypeName = targetType.ToString();
            tweenIdentifier = GetTweenMethod(propertyName, out var typeName);

            if (!string.IsNullOrEmpty(typeName))
            {
                targetTypeName = typeName;
            }
            if (string.IsNullOrEmpty(tweenIdentifier))
            {
                curveName = string.Empty;
            }
            else
            {
                curveName = $"{clipName}{bindTarget}{tweenIdentifier}";
            }
        }

        private static string GetTweenMethod(string propertyName, out string targetType)
        {
            targetType = null;
            var identifier = string.Empty;

            string key = $"{propertyName.ToUpper()}";
            if (TweenConfigCacher.tweenGenInfoCaches.ContainsKey(key))
            {
                targetType = TweenConfigCacher.tweenGenInfoCaches[key].ComponentType;
                // string tweenMethod = TweenConfigCacher.tweenGenInfoCaches[key].GetTweenMethod();
                identifier = TweenCustomTrackCodeGenerator.GetTweenBehaviourIdentifier(TweenConfigCacher.tweenGenInfoCaches[key]);
                return identifier;
            }
            else
            {
                if (!_ignoreName.Contains(propertyName))
                {
                    Debug.LogError($"Dont find  tween method : {propertyName} \n Please check in TweenGenTrackConfig Settings");
                }
                return string.Empty;
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

        #region  TrackContext

        public static List<GenTrackInfo> ConstructGenTracks(AnimationClip clip,
                EasingTokenPresetLibrary easingTokenPresetLibrary,
                   KeyframeDataWrapper keyWrapper, double startTime, int instanceID,
                   in Dictionary<string, Tuple<string, string>> contentDict
                   )
        {
            var trackInfos = CreateTrackContexts(null, clip
                             , easingTokenPresetLibrary, keyWrapper, startTime);

            var genTrackInfos = new List<GenTrackInfo>(trackInfos.Count);
            foreach (var trackInfoContext in trackInfos)
            {
                string instanceId = instanceID.ToString() + trackInfoContext.BindTargetName + trackInfoContext.tweenIdentifier;

                var genTrackInfo = new GenTrackInfo(instanceId);
                foreach (var clipInfoContext in trackInfoContext.clipInfos)
                {
                    BaseEasingTokenPreset easePreset = clipInfoContext.easePreset;
                    string easeName = easePreset?.Name ?? string.Empty;
                    string bindTypeName = trackInfoContext.BindType?.FullName ?? string.Empty;
                    var behaviourTypeName = $"{trackInfoContext.tweenIdentifier}ControlBehaviour";
                    string tweenMethod = string.Empty;
                    string customTweenMethod = string.Empty;
                    if (contentDict.ContainsKey(behaviourTypeName))
                    {
                        var methodContent = contentDict[behaviourTypeName];
                        tweenMethod = methodContent.Item1;
                        customTweenMethod = methodContent.Item2;
                    }
                    var genClipInfo = new GenClipInfo()
                    {
                        DelayTime = (float)clipInfoContext.start,
                        Duration =
                                  (float)clipInfoContext.duration,
                        EaseName =
                                  easeName,
                        TweenMethod =
                                  tweenMethod,
                        CustomTweenMethod =
                         customTweenMethod,
                        EndValue =
                                  clipInfoContext.endPos, // the curve is inverse
                        BindType = bindTypeName,
                        BindName = trackInfoContext.BindTargetName,
                        StartValue = clipInfoContext.startPos
                    };
                    for (int j = 0; j < clipInfoContext.markInfos.Count; j++)
                    {
                        var genMarkInfo = new GenMarkInfo();
                        genMarkInfo.filedName = clipInfoContext.markInfos[j].FieldName;
                        genMarkInfo.Time = (float)clipInfoContext.markInfos[j].Time;
                        genMarkInfo.value = GenMarkInfo.ConvertValue
                            (clipInfoContext.markInfos[j].UpdateValue);

                        genClipInfo.genMarkInfos.Add(genMarkInfo);
                    }

                    genTrackInfo.clipInfos.Add(genClipInfo);
                }
                genTrackInfos.Add(genTrackInfo);
            }
            return genTrackInfos;
        }


        public static List<TrackInfoContext> CreateTrackContexts(UnityEngine.GameObject target, AnimationClip clip,
          EasingTokenPresetLibrary easingTokenPresetLibrary,
             KeyframeDataWrapper keyWrapper, double startTime)
        {
            var trackInfos = new List<TrackInfoContext>();

            foreach (ObjectKeyframes animTargetInfo in keyWrapper.Objects)
            {
                if (string.IsNullOrEmpty(animTargetInfo.ObjectPath))
                {
                    continue;
                }

                ProcessAnimationTarget(target, animTargetInfo, clip, easingTokenPresetLibrary,
                 startTime, trackInfos);
            }

            return trackInfos;
        }

        private static void ProcessAnimationTarget(UnityEngine.GameObject target,
        ObjectKeyframes animTargetInfo, AnimationClip clip, EasingTokenPresetLibrary easingTokenPresetLibrary,
             double startTime, in List<TrackInfoContext> trackInfoContexts)
        {
            foreach (TypeKeyframes typeKeyFrames in animTargetInfo.Types)
            {
                foreach (PropertyKeyframes frameProperty in typeKeyFrames.Properties)
                {
                    Transform targetTrans = null;
                    Transform trackRoot = null;
                    if (target != null)
                    {
                        targetTrans = target.transform.Find(animTargetInfo.ObjectPath);
                        if (targetTrans == null)
                        {
                            Debug.LogWarning($"Please check the animation bind path {animTargetInfo.ObjectPath} don't exist in {target.transform}");
                            continue;
                        }
                        // Assert.IsNotNull(targetTrans, $"Please check the animation bind path {animTargetInfo.ObjectPath} don't exist in {target.transform}");
                        trackRoot = BindUtility.GetAttachRoot(targetTrans.transform);
                        if (trackRoot == null) continue;
                    }

                    string targetTypeName, curveName;
                    string bindTarget = animTargetInfo.GetObjectName();
                    AnimationClipConverter.GetClipInfoName(clip.name, typeKeyFrames.Type,
                    frameProperty.PropertyName, bindTarget,
                        out targetTypeName, out string tweenIdentifier, out curveName);

                    TrackInfoContext trackInfo = null;
                    int findIndex = easingTokenPresetLibrary.FindEasePreset(curveName);
                    if (findIndex >= 0)
                    {
                        trackInfo = AddValueTrack(animTargetInfo, clip, startTime, easingTokenPresetLibrary, frameProperty,
                         targetTrans, targetTypeName, tweenIdentifier, curveName);
                    }
                    else
                    {
                        trackInfo = AddObjectTrack(animTargetInfo, clip, startTime, frameProperty, targetTrans);
                    }

                    if (trackInfo != null)
                    {
                        trackInfo.parent = trackRoot;
                        trackInfoContexts.Add(trackInfo);
                    }
                }
            }
        }

        private static TrackInfoContext AddValueTrack(ObjectKeyframes animTargetInfo, AnimationClip clip
         , double startTime, EasingTokenPresetLibrary easingTokenPresetLibrary,
         PropertyKeyframes frameProperty, Transform transform,
         string targetTypeName, string tweenIdentifier, string curveName)
        {
            var simplifyTypeName = TypeConverter.GetSimplifyTypeName(targetTypeName);
            var trackName = $"Cr7Sund.{simplifyTypeName}Tween.{tweenIdentifier}ControlTrack";
            Assembly assembly = TweenActionStep.GetTweenTrackAssembly(trackName);
            Type trackType = assembly.GetType(trackName);
            var assetName = $"Cr7Sund.{simplifyTypeName}Tween.{tweenIdentifier}ControlAsset";
            var trackAssetType = assembly.GetType(assetName);

            var trackInfo = new TrackInfoContext();
            Component component = null;
            if (transform != null)
                component = transform.GetComponent(animTargetInfo.ObjectType);
            trackInfo.component = component;
            trackInfo.BindType = animTargetInfo.ObjectType;
            trackInfo.BindTargetName = animTargetInfo.GetObjectName();
            trackInfo.tweenIdentifier = tweenIdentifier;
            trackInfo.trackType = trackType;

            ClipInfoContext clipInfoContext = new ClipInfoContext
            {
                start = startTime,
                duration = clip.length,
                trackAssetType = trackAssetType,
                easePreset = easingTokenPresetLibrary.GetEasePreset(curveName),
                startPos = frameProperty.Keyframes[0].Value,
                endPos = frameProperty.Keyframes[^1].Value
            };

            trackInfo.clipInfos.Add(clipInfoContext);
            return trackInfo;
        }

        private static TrackInfoContext AddObjectTrack(ObjectKeyframes animTargetInfo,
                  AnimationClip clip, double startTime,
                PropertyKeyframes frameProperty, Transform transform)
        {
            KeyframeData startKeyframeData = frameProperty.Keyframes[0];
            bool isActiveProp = startKeyframeData.Property == TweenTimelineDefine.IsActiveFieldName;
            if (!isActiveProp &&
             startKeyframeData.Value.GetType() == typeof(float))
            {
                return null;
            }
            var trackInfo = new TrackInfoContext();
            if (isActiveProp)
            {
                trackInfo.component = transform;
            }
            else
            {
                if (transform != null)
                {
                    trackInfo.component = transform.GetComponent(animTargetInfo.ObjectType);
                }
            }

            trackInfo.trackType = typeof(EmptyTrack);
            trackInfo.BindType = animTargetInfo.ObjectType;
            trackInfo.BindTargetName = animTargetInfo.GetObjectName();

            ClipInfoContext clipInfoContext = new ClipInfoContext
            {
                start = startTime,
                duration = clip.length,
                trackAssetType = typeof(EmptyControlAsset),
                easePreset = null,
                startPos = null,
                endPos = null,
            };
            trackInfo.clipInfos.Add(clipInfoContext);

            bool constant = true;
            MarkInfoContext frameValue = null;
            for (int i = 0; i < frameProperty.Keyframes.Count; i++)
            {
                object value = frameProperty.Keyframes[i].Value;
                if (value is float floatValue)
                {
                    value = floatValue != 0;
                }
                var markInfo = new MarkInfoContext();
                markInfo.FieldName = frameProperty.PropertyName;
                markInfo.UpdateValue = value;
                markInfo.Time = frameProperty.Keyframes[i].Time + startTime;

                if (frameValue != null
                    && frameValue.UpdateValue != markInfo.UpdateValue)
                {
                    constant = false;
                }
                frameValue = markInfo;

                clipInfoContext.markInfos.Add(markInfo);
            }
            if (!constant)
            {
                return trackInfo;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region  Curve 

        public static void CreateCurves(IEnumerable<TrackAsset> tracks, EasingTokenPresetLibrary easingTokenPresetLibrary)
        {
            foreach (TrackAsset track in tracks)
            {
                if (track is AnimationTrack animationTrack)
                {
                    IEnumerable<TimelineClip> clips = animationTrack.GetClips();
                    foreach (var timelineClip in clips)
                    {
                        var clip = timelineClip.animationClip;
                        AddPresets(easingTokenPresetLibrary, AnimationClipConverter.CreateCurvePresets(clip));
                    }

                    if (animationTrack.infiniteClip != null)
                    {
                        AddPresets(easingTokenPresetLibrary, AnimationClipConverter.CreateCurvePresets(animationTrack.infiniteClip));
                    }
                }
            }

            AssetDatabase.SaveAssetIfDirty(easingTokenPresetLibrary);
            AssetDatabase.Refresh();
        }

        public static List<CustomCurveEasingTokenPreset> CreateCurve(AnimationClip clip, EasingTokenPresetLibrary easingTokenPresetLibrary)
        {
            List<CustomCurveEasingTokenPreset> easingTokenPresetsToAdd = AnimationClipConverter.CreateCurvePresets(clip);
            AddPresets(easingTokenPresetLibrary, easingTokenPresetsToAdd);
            AssetDatabase.SaveAssetIfDirty(easingTokenPresetLibrary);
            AssetDatabase.Refresh();

            return easingTokenPresetsToAdd;
        }

        public static List<CustomCurveEasingTokenPreset> CreateCurvePresets(AnimationClip clip, string curveName = "")
        {
            List<CustomCurveEasingTokenPreset> curves = new();

            EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(clip);
            foreach (EditorCurveBinding binding in curveBindings)
            {
                AnimationCurve editorCurve = AnimationUtility.GetEditorCurve(clip, binding);
                editorCurve = NormalizeCurveClamp(editorCurve, binding.propertyName);
                // editorCurve = NormalizeCurve(editorCurve, binding.propertyName);

                Keyframe[] keys = editorCurve.keys;
                Array.Sort(keys, (t1, t2) => t1.time.CompareTo(t2.time));
                bool constant = true;
                float curKeyFrameData = float.MaxValue; // for the sake of god, you should never assign the max value
                for (int i = 0; i < keys.Length; i++)
                {
                    if (curKeyFrameData != float.MaxValue &&
                        keys[i].value != curKeyFrameData)
                    {
                        constant = false;
                        break;
                    }
                    curKeyFrameData = keys[i].value;
                }

                if (constant)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(curveName))
                {
                    string clipName = clip.name;
                    string propertyName = binding.propertyName;
                    AnimationClipConverter.MapProperty(binding.type, ref propertyName);
                    string targetTypeName;
                    var target = binding.path.Split('/').Last();

                    AnimationClipConverter.GetClipInfoName(clipName, binding.type, propertyName, target,
                     out targetTypeName, out var tweenIdentifier, out curveName);
                }
                if (string.IsNullOrEmpty(curveName))
                {
                    continue;
                }
                var curve = new CustomCurveEasingTokenPreset()
                {
                    animationCurve = editorCurve,
                    tokenKey = curveName
                };

                curves.Add(curve);
            }

            return curves;
        }

        public static void AddPresets(EasingTokenPresetLibrary easingTokenPresetLibrary, IEnumerable<BaseEasingTokenPreset> easingTokenPresetsToAdd)
        {
            foreach (var preset in easingTokenPresetsToAdd)
            {
                easingTokenPresetLibrary.AddPreset(preset); // Reuse AddPreset to handle duplicates
                EasingTokenPresetLibraryEditor.UpdatePresetLibrary(preset);
            }
        }



        private static AnimationCurve NormalizeCurve(AnimationCurve curve, string propertyName)
        {
            if (curve.length == 0)
                return curve;

            float minTime = float.MaxValue, maxTime = float.MinValue;
            float minValue = float.MaxValue, maxValue = float.MinValue;

            // Find the range of time and value
            foreach (var key in curve.keys)
            {
                minTime = Mathf.Min(minTime, key.time);
                maxTime = Mathf.Max(maxTime, key.time);
                minValue = Mathf.Min(minValue, key.value);
                maxValue = Mathf.Max(maxValue, key.value);
            }

            float timeRange = maxTime - minTime;
            float valueRange = maxValue - minValue;

            // Prevent division by zero
            if (Mathf.Approximately(timeRange, 0f) || Mathf.Approximately(valueRange, 0f))
            {
                Debug.LogWarning($"The time range or value range of the curve is zero, cannot be fully normalized. {propertyName}");
                return curve;
            }

            AnimationCurve normalizedCurve = new AnimationCurve();

            foreach (var key in curve.keys)
            {
                float normalizedTime = (key.time - minTime) / timeRange;
                float normalizedValue = (key.value - minValue) / valueRange;

                // Adjust the tangent
                float normalizedInTangent = key.inTangent * (timeRange / valueRange);
                float normalizedOutTangent = key.outTangent * (timeRange / valueRange);

                Keyframe normalizedKey = new Keyframe(
                    normalizedTime,
                    normalizedValue,
                    normalizedInTangent,
                    normalizedOutTangent
                )
                {
                    weightedMode = key.weightedMode,
                    inWeight = key.inWeight,
                    outWeight = key.outWeight
                };

                normalizedCurve.AddKey(normalizedKey);
            }

            // Keep the tangent mode consistent
            for (int i = 0; i < curve.length; i++)
            {
                AnimationUtility.SetKeyLeftTangentMode(normalizedCurve, i, AnimationUtility.GetKeyLeftTangentMode(curve, i));
                AnimationUtility.SetKeyRightTangentMode(normalizedCurve, i, AnimationUtility.GetKeyRightTangentMode(curve, i));
            }

            return normalizedCurve;
        }

        // Normal time[0-1], value[0-1]
        private static AnimationCurve NormalizeCurveClamp(AnimationCurve curve, string propertyName)
        {
            if (curve.length < 2)
            {
                Debug.LogWarning($"Curve has less than 2 keyframes, cannot be normalized. {propertyName}");
                return curve;
            }

            var start = curve[0];
            var end = curve[curve.length - 1];

            // Calculate the ratio needed to adjust the end value to 0 or 1
            float ratio;
            if (Mathf.Abs(end.value) < Mathf.Epsilon)
            {
                ratio = 1f; // Avoid division by zero
            }
            else if (Mathf.Abs(end.value - 1f) < Mathf.Abs(end.value))
            {
                ratio = 1f / end.value; // Adjust to make end value 1
            }
            else
            {
                ratio = 1f / Mathf.Abs(end.value); // Adjust to make end value -1 or 1
            }

            AnimationCurve normalizedCurve = new AnimationCurve();

            foreach (var key in curve.keys)
            {
                float normalizedTime = Mathf.InverseLerp(start.time, end.time, key.time);
                float normalizedValue = key.value * ratio;

                // Adjust tangents
                float normalizedInTangent = key.inTangent * ratio;
                float normalizedOutTangent = key.outTangent * ratio;

                Keyframe normalizedKey = new Keyframe(
                    normalizedTime,
                    normalizedValue,
                    normalizedInTangent,
                    normalizedOutTangent
                )
                {
                    weightedMode = key.weightedMode,
                    inWeight = key.inWeight,
                    outWeight = key.outWeight
                };

                normalizedCurve.AddKey(normalizedKey);
            }

            // Ensure start and end values are exactly 0 or 1
            normalizedCurve.MoveKey(0, new Keyframe(0f, Mathf.Round(normalizedCurve[0].value)));
            normalizedCurve.MoveKey(normalizedCurve.length - 1, new Keyframe(1f, Mathf.Round(normalizedCurve[normalizedCurve.length - 1].value)));

            // Keep the tangent mode consistent
            for (int i = 0; i < normalizedCurve.length; i++)
            {
                AnimationUtility.SetKeyLeftTangentMode(normalizedCurve, i, AnimationUtility.GetKeyLeftTangentMode(curve, i));
                AnimationUtility.SetKeyRightTangentMode(normalizedCurve, i, AnimationUtility.GetKeyRightTangentMode(curve, i));
            }

            return normalizedCurve;
        }
        #endregion
    }
}
