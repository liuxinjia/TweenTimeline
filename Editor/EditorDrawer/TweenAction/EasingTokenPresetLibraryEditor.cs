using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Cr7Sund.Editor.CurvePreset;
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
                    curveDictionary = new();
                    Dictionary<string, string> paths = EasingTokenPresetLibraryEditor.GetCurvePresetLibrary();
                    foreach (var item in paths)
                    {
                        var easingLibrary = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(item.Value);
                        CurvePresetEditTools.GenerateCurveDict(easingLibrary, in curveDictionary);
                    }
                }
                return curveDictionary;
            }
        }

        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            var listProp = serializedObject.FindProperty("easingTokenPresets");
            var property = new PropertyField(listProp);

            var resetBtn = new Button(Rebuild);
            resetBtn.text = "Rebuild";

            container.Add(resetBtn);
            container.Add(property);

            return container;
        }

        public void Rebuild()
        {
            var easingTokenPresetLibrary = target as EasingTokenPresetLibrary;
            easingTokenPresetLibrary.easingTokenPresets = new();

            var derivedTypes = TweenTimelineDefine.DerivedEaseTokenTypes;

            foreach (var presetType in derivedTypes)
            {

                EasingTokenPresetsFactory.GenerateEasingTokenPresets(
                    easingTokenPresetLibrary,
                    presetType,
                    CurveDictionary);
            }

            serializedObject.ApplyModifiedProperties();
            AssetDatabase.SaveAssetIfDirty(target);
            AssetDatabase.Refresh();
        }

        public static Dictionary<string, string> GetCurvePresetLibrary()
        {
            var presetGUIDs = AssetDatabase.FindAssets("t:CurvePresetLibrary", new[]
            {
                TweenTimelineDefine.BuiltInCurvePresetFolder, TweenTimelineDefine.CustomCurvePresetFolder
            });
            var curvePreseDict = new Dictionary<string, string>();
            foreach (var presetGuid in presetGUIDs)
            {
                var presetPath = AssetDatabase.GUIDToAssetPath(presetGuid);
                //MaterialEaseEquations
                string name = Path.GetFileNameWithoutExtension(presetPath).ToString().Replace("EaseEquations", "");
                curvePreseDict.Add(name, presetPath);
            }

            return curvePreseDict;
        }
    }
}
