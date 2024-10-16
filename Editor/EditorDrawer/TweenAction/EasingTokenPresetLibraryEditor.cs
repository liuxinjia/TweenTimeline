﻿using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace Cr7Sund.TweenTimeLine
{
    [CustomEditor(typeof(EasingTokenPresetLibrary))]
    public class EasingTokenPresetLibraryEditor : UnityEditor.Editor
    {
        private static Dictionary<string, AnimationCurve> curveDictionary;

        public static Dictionary<string, AnimationCurve> CurveDictionary
        {
            get
            {
                if (curveDictionary == null)
                {
                    ResetFromCurvePreset();
                }
                return curveDictionary;
            }
        }

        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            var listProp = serializedObject.FindProperty("easingTokenPresets");
            var property = new PropertyField(listProp);

            var resetBtn = new Button(ResetFromCurvePreset);
            resetBtn.text = "Rebuild";

            container.Add(resetBtn);
            container.Add(property);

            return container;
        }

        private static void ResetFromCurvePreset()
        {
            curveDictionary = new();
            var easingTokenPresetLibrary = AssetDatabase.LoadAssetAtPath<EasingTokenPresetLibrary>(TweenTimelineDefine.easingTokenPresetsPath);
            ConstructPresets(easingTokenPresetLibrary);

            foreach (var item in easingTokenPresetLibrary.easingTokenPresets)
            {
                var name = item.Name;
                var curve = item.Curve;

                if (curve != null && name != null && !curveDictionary.ContainsKey(name))
                {
                    curveDictionary.Add(name, curve);
                }
            }

        }

        private static void ConstructPresets(EasingTokenPresetLibrary easingTokenPresetLibrary)
        {
            Dictionary<string, string> paths = GetCurvePresetLibrary();
            foreach (var item in paths)
            {
                var easingLibrary = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(item.Value);
                EasingCurveExtensions.GenerateCurveDict(easingLibrary, in curveDictionary);
            }

            if (easingTokenPresetLibrary.easingTokenPresets == null ||
                  easingTokenPresetLibrary.easingTokenPresets.Count <= 0)
            {
                RebuildCurvePreset(easingTokenPresetLibrary);
            }

        }

        public static void RebuildCurvePreset(EasingTokenPresetLibrary easingTokenPresetLibrary)
        {
            easingTokenPresetLibrary.easingTokenPresets = new();

            var derivedTypes = TweenTimelineDefine.DerivedEaseTokenTypes;

            foreach (var presetType in derivedTypes)
            {
                EasingTokenPresetsFactory.GenerateEasingTokenPresets(
                    easingTokenPresetLibrary,
                    presetType,
                    CurveDictionary);
            }

            // serializedObject.ApplyModifiedProperties();
            AssetDatabase.SaveAssetIfDirty(easingTokenPresetLibrary);
            AssetDatabase.Refresh();
        }

        public static Dictionary<string, string> GetCurvePresetLibrary()
        {
            var presetGUIDs = AssetDatabase.FindAssets("t:CurvePresetLibrary"
            , new[]
            {
                TweenTimelineDefine.BuiltInCurvePresetFolder, TweenTimelineDefine.CustomCurvePresetFolder
            }
            );
            var curvePresetDict = new Dictionary<string, string>();
            foreach (var presetGuid in presetGUIDs)
            {
                var presetPath = AssetDatabase.GUIDToAssetPath(presetGuid);
                //MaterialEaseEquations
                string name = Path.GetFileNameWithoutExtension(presetPath).ToString().Replace("EaseEquations", "");
                curvePresetDict.Add(name, presetPath);
            }

            return curvePresetDict;
        }

        public static void UpdatePresetLibrary(BaseEasingTokenPreset preset)
        {
            if (!EasingTokenPresetLibraryEditor.CurveDictionary.ContainsKey(preset.Name))
            {
                EasingTokenPresetLibraryEditor.CurveDictionary.Add(preset.Name, preset.Curve);
            }
            else
            {
                EasingTokenPresetLibraryEditor.CurveDictionary[preset.Name] = preset.Curve;
            }
        }
    }
}
