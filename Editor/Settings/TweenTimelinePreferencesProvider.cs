using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

namespace Cr7Sund.TweenTimeLine
{
    public class TweenTimelinePreferencesProvider : SettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            return new TweenTimelinePreferencesProvider("Cr7Sund/TweenTimeline", SettingsScope.User);
        }

        private TweenTimelinePreferencesProvider(string path, SettingsScope scope) : base(path, scope) { }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            CreateSettingsUI(rootElement);
        }

        private void CreateSettingsUI(VisualElement root)
        {
            root.Clear();

            var categories = new HashSet<string>();

            foreach (var setting in TweenTimelineSettings.Instance.settings)
            {
                if (!categories.Contains(setting.category))
                {
                    categories.Add(setting.category);
                    var categoryLabel = new Label(setting.category)
                    {
                        style =
                        {
                            fontSize = 18,
                            unityFontStyleAndWeight = FontStyle.Bold,
                            marginBottom = 10,
                            color = new Color(0.298f, 0.784f, 0.318f) // #4CAF50
                        }
                    };
                    root.Add(categoryLabel);
                }

                if (setting.settingType == typeof(bool).FullName)
                {
                    AddBoolPref(root, setting.label, setting.key, setting.toolTips);
                }
                else if (setting.settingType == typeof(float).FullName)
                {
                    AddFloatPref(root, setting.label, setting.key, setting.toolTips);
                }
                else if (setting.settingType == typeof(string).FullName)
                {
                    AddStringPref(root, setting.label, setting.key, setting.toolTips);
                }
            }
        }

        private void AddBoolPref(VisualElement root, string label, string key, string toolTips)
        {
            var toggle = new Toggle(label)
            {
                value = EditorPrefs.GetBool(key),
                style =
                {
                    marginBottom = 10
                }
            };
            toggle.tooltip = toolTips;

            toggle.RegisterValueChangedCallback(evt =>
            {
                EditorPrefs.SetBool(key, evt.newValue);
                
            });

            root.Add(toggle);
        }

        private void AddFloatPref(VisualElement root, string label, string key, string toolTips)
        {
            var floatField = new FloatField(label)
            {
                value = EditorPrefs.GetFloat(key),
                style =
                {
                    marginBottom = 10
                }
            };
            floatField.tooltip = toolTips;
            floatField.RegisterValueChangedCallback(evt =>
            {
                EditorPrefs.SetFloat(key, evt.newValue);
            });

            root.Add(floatField);
        }

        private void AddStringPref(VisualElement root, string label, string key, string toolTips)
        {
            var textField = new TextField(label)
            {
                value = EditorPrefs.GetString(key),
                style =
                {
                    marginBottom = 10
                }
            };
            textField.tooltip = toolTips;

            textField.RegisterValueChangedCallback(evt =>
            {
                EditorPrefs.SetString(key, evt.newValue);
            });

            root.Add(textField);
        }


        public static bool GetBool(Enum key) => EditorPrefs.GetBool(key.ToString());
        public static float GetFloat(Enum key) => EditorPrefs.GetFloat(key.ToString());
        public static string GetString(Enum key) => EditorPrefs.GetString(key.ToString());
    }
}
