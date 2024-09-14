using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace Cr7Sund.TweenTimeLine
{
    [CustomEditor(typeof(EasingTokenPresetLibrary))]
    public class EasingTokenPresetLibraryEditor : UnityEditor.Editor
    {
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
            easingTokenPresetLibrary.easingTokenPresets.Clear();

            var presetGUIDs = AssetDatabase.FindAssets("t:Preset", new[]
            {
                TweenTimelineDefine.BuiltInCurvePresetFolder, TweenTimelineDefine.CustomCurvePresetFolder
            });
            var dict = new Dictionary<string, string>();
            foreach (var presetGuid in presetGUIDs)
            {
                var presetPath = AssetDatabase.GUIDToAssetPath(presetGuid);
                //MaterialEaseEquations
                string name = Path.GetFileNameWithoutExtension(presetPath).ToString().Replace("EaseEquations", "");
                dict.Add(name, presetPath);
            }
            var derivedTypes = TweenTimelineDefine.DerivedEaseTokenTypes;
            foreach (var presetType in derivedTypes)
            {
                //MaterialEasingTokenPreset
                string name = presetType.ToString().Replace("EasingTokenPreset", "");
                if (!dict.ContainsKey(name))
                {
                    Debug.LogError($"{name} don't have matched Animaiont Curve Preset");
                    continue;
                }

                EasingTokenPresetsFactory.GenerateEasingTokenPresets(easingTokenPresetLibrary,
                    presetType, 
                    dict[name]);
            }

            serializedObject.ApplyModifiedProperties();
            AssetDatabase.SaveAssetIfDirty(target);
            AssetDatabase.Refresh();
        }
    }
}
