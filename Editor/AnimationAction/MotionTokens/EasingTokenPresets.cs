using System;
using System.Collections.Generic;
using System.Linq;
using Cr7Sund.Editor.CurvePreset;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PrimeTween;
using UnityEditor;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    // [CreateAssetMenu(menuName = "Cr7Sund/TweenTimeLine/EasingTokenPresets", fileName = "EasingTokenPresets")]
    [System.Serializable]
    public class EasingTokenPresets : ScriptableObject
    {
        public List<EasingTokenPreset> easingTokenPresets;
        public const string materialEasingPresetGUID = "626b2e9a46d7d0344b5490f6c36dcbd0";
        public const string jitterEasingPresetGUID = "425ea5fde3aeee14c83c8280bcf49a8f";

        [MenuItem("Tools/EasingTokenPresets")]
        public static void CreateDefaulEasingTokenPresets()
        {
            var instance = ScriptableObject.CreateInstance<EasingTokenPresets>();
            instance.easingTokenPresets = new();
            instance.GenerateEasingTokenPresets(typeof(MaterialEasingToken), materialEasingPresetGUID);
            instance.GenerateEasingTokenPresets(typeof(JitterEasingToken), jitterEasingPresetGUID);

            AssetDatabase.CreateAsset(instance, "Assets/Plugins/TweenTimeline/Editor/AnimationAction/EasingTokenPresets.asset");
            AssetDatabase.SaveAssetIfDirty(instance);
        }

        private void GenerateEasingTokenPresets(Type tokenType, string guid)
        {
            var enums = Enum.GetValues(tokenType);
            var materialEasingLibrary = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(AssetDatabase.GUIDToAssetPath(guid));
            var curveDictionary = CurvePresetEditTools.GenerateCurveDict(materialEasingLibrary);

            foreach (object item in enums)
            {
                if (item.ToString() == "Custom")
                {
                    continue;
                }
                var materialEum = tokenType == typeof(MaterialEasingToken) ? (Enum)item : MaterialEasingToken.Custom;
                var jitterEnum = tokenType == typeof(JitterEasingToken) ? (Enum)item : JitterEasingToken.Custom;
                AnimationCurve animationCurve = curveDictionary[item.ToString()];
                string name = item.ToString();
                easingTokenPresets.Add(CreateEasingTokenPreset(materialEum, jitterEnum, animationCurve));
            }
        }

        private static EasingTokenPreset CreateEasingTokenPreset(Enum materialToken, Enum jitterToken, AnimationCurve curve)
        {
            EasingTokenPreset preset = new EasingTokenPreset
            {
                ease = PrimeTween.Ease.Custom,
                materialTokenKey = (MaterialEasingToken)materialToken,
                jitterTokenKey = (JitterEasingToken)jitterToken,
                animationCurve = curve
            };
            return preset;
        }

        public bool TryGetEasePreset(string easeName, out Easing resultEase)
        {
            var findIndex = -1;
            if (Enum.TryParse<MaterialEasingToken>(easeName, out var materialToken))
            {
                findIndex = easingTokenPresets.FindIndex(token => token.materialTokenKey.Equals(materialToken));
            }
            if (findIndex < 0)
            {
                if (Enum.TryParse<JitterEasingToken>(easeName, out var jitterToken))
                {
                    findIndex = easingTokenPresets.FindIndex(token => token.jitterTokenKey.Equals(jitterToken));
                }
            }
            if (findIndex < 0)
            {
                if (Enum.TryParse<Ease>(easeName, out var builtInEase))
                {
                    resultEase = builtInEase;
                    return true;
                }
                else
                {
                    resultEase = default;
                    return false;
                }
            }
            resultEase = easingTokenPresets[findIndex].animationCurve;
            return true;
        }

        public EasingTokenPreset GetEasePreset(JitterEasingToken jitterToken)
        {
            var preset = easingTokenPresets.FirstOrDefault(token => token.jitterTokenKey.Equals(jitterToken));
            return preset;
        }

        public EasingTokenPreset GetEasePreset(JitterEasingToken jitterToken, MaterialEasingToken materialTokenKey)
        {
            var preset = easingTokenPresets.FirstOrDefault(token => token.jitterTokenKey.Equals(jitterToken)
             && token.materialTokenKey.Equals(materialTokenKey));
            return preset;
        }

        internal EasingTokenPreset GetEasePreset(MaterialEasingToken materialTokenKey)
        {
            var preset = easingTokenPresets.First(token => token.materialTokenKey.Equals(materialTokenKey));
            return preset;
        }

        internal AnimationCurve GetAnimationCurve(string easeName)
        {
            var findIndex = easingTokenPresets.FindIndex(token => token.jitterTokenKey.Equals(Enum.Parse<JitterEasingToken>(easeName)));
            if (findIndex < 0)
            {
                findIndex = easingTokenPresets.FindIndex(token => token.jitterTokenKey.Equals(Enum.Parse<JitterEasingToken>(easeName)));
            }
            if (findIndex < 0)
            {
                throw new IndexOutOfRangeException();
            }
            return easingTokenPresets[findIndex].animationCurve;
        }
    }

    [System.Serializable]
    public struct EasingTokenPreset
    {
        public MaterialEasingToken materialTokenKey;
        public JitterEasingToken jitterTokenKey;
        public AnimationCurve animationCurve;
        public PrimeTween.Ease ease;


        public string Name
        {
            get
            {
                if (materialTokenKey != MaterialEasingToken.Custom)
                {
                    return materialTokenKey.ToString();
                }
                if (jitterTokenKey != JitterEasingToken.Custom)
                {
                    return jitterTokenKey.ToString();
                }
                return ease.ToString();
            }
        }
    }
}