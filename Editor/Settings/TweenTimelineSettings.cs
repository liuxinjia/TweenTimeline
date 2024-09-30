using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace Cr7Sund.TweenTimeLine
{
    public enum ActionEditorSettings
    {
        EnableGifPreview,
        AlwaysCreateTrack,
        DealyResetTime, // you can not close the automatic reset action, bu you can control the delay time of reset action
    }
    public enum TweenElemenSettings
    {
    }

    public enum TweenGenSettings
    {
        UseFullPathName,
    }

    public enum TweenPreferenceDefine
    {
        // Custom
        CustomTweenAssemblyName,
        CustomTweenPath, // Assets/Plugins/TweenTimeline/Sample/
        BuiltInLibraryPath, // Assets/TweenTimeline/
    }

    // [CreateAssetMenu(fileName = "TweenTimelineSettings", menuName = "Cr7Sund/TweenTimelineSettings")]
    public class TweenTimelineSettings : ScriptableObject
    {
        private const string TweenConfigGUID = "c6a3e85204b9957469ca407e5fbb362e";
        private static TweenTimelineSettings _settingsAsset;
        public static TweenTimelineSettings Instance
        {
            get
            {
                if (_settingsAsset == null)
                {
                    string tweenConfigPath = AssetDatabase.GUIDToAssetPath(TweenConfigGUID);
                    _settingsAsset = AssetDatabase.LoadAssetAtPath<TweenTimelineSettings>(tweenConfigPath);
                    if (_settingsAsset == null)
                    {
                        string path = $"Assets/Plugins/TweenTimeline/Editor/Settings/TweenTimelineSettings.asset";
                        _settingsAsset = ScriptableObject.CreateInstance<TweenTimelineSettings>();

                        AssetDatabase.CreateAsset(_settingsAsset, path);
                        AssetDatabase.SaveAssets();
                    }
                }
                return _settingsAsset;
            }
        }

        public List<PreferSettingData> settings = new List<PreferSettingData>();

        public static Dictionary<Enum, Tuple<object, string>> _customPairs = new Dictionary<Enum, Tuple<object, string>>()
        {
            {ActionEditorSettings.EnableGifPreview, new Tuple<object, string>(true, string.Empty)},
            {ActionEditorSettings.AlwaysCreateTrack, new Tuple<object, string>(false, string.Empty)},
            {TweenGenSettings.UseFullPathName, new Tuple<object, string>(false, string.Empty)},
            {ActionEditorSettings.DealyResetTime, new Tuple<object, string>(1.0f, string.Empty)},
        };

        public void Reset()
        {
            foreach (var item in Enum.GetValues(typeof(ActionEditorSettings)))
            {
                AddSetting(item.ToString(), "Action Editor ", string.Empty);
            }

            foreach (var item in Enum.GetValues(typeof(TweenElemenSettings)))
            {
                AddSetting(item.ToString(), "Tween Element ", string.Empty);
            }

            foreach (var item in Enum.GetValues(typeof(TweenPreferenceDefine)))
            {
                AddSetting(item.ToString(), "Custom ", string.Empty);
            }

            foreach (var item in _customPairs)
            {
                UpdateValue(item.Key.ToString(), item.Value, string.Empty);
            }
        }

        public void UpdateValue<T>(string key, T defaultValue, string toolTips)
        {
            if (defaultValue == null)
            {
                return;
            }

            var settingType = defaultValue.GetType().FullName;
            var existingSetting = this.settings.FirstOrDefault(s => s.key == key);
            existingSetting.settingType = settingType;
            existingSetting.toolTips = toolTips;

            if (defaultValue == null)
            {
            }
            else if (defaultValue is bool boolValue)
            {
                EditorPrefs.SetBool(key, boolValue);
            }
            else if (defaultValue is float floatValue)
            {
                EditorPrefs.SetFloat(key, floatValue);
            }
            else if (defaultValue is string stringValue)
            {
                if (!string.IsNullOrEmpty(stringValue))
                    EditorPrefs.SetString(key, stringValue);
            }

        }

        public void AddSetting<T>(string label, string category, T defaultValue)
        {
            this.AddSetting<T>(label, label, category, defaultValue);
        }


        public void AddSetting<T>(string label, string key, string category, T defaultValue)
        {
            var settingType = typeof(T).FullName;

            var existingSetting = this.settings.FirstOrDefault(s => s.key == key);

            if (existingSetting != null)
            {
                existingSetting.category = category;
                existingSetting.label = label;
                existingSetting.settingType = settingType;
            }
            else
            {
                this.settings.Add(new PreferSettingData
                {
                    category = category,
                    label = label,
                    key = key,
                    settingType = settingType,
                });
            }

            UpdateValue<T>(key, defaultValue, string.Empty);
        }
    }
}
