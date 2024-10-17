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
        DelayResetTime, // you can not close the automatic reset action, bu you can control the delay time of reset action
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
        private static TweenTimelineSettings _settingsAsset;
        public static TweenTimelineSettings Instance
        {
            get
            {
                if (_settingsAsset == null)
                {
                    string tweenConfigPath = "Assets/TweenTimelineConfig/BuiltInConfigs/Editor Default Resources/TweenTimelineSettings.asset";
                    _settingsAsset = AssetDatabase.LoadAssetAtPath<TweenTimelineSettings>(tweenConfigPath);
                    if (_settingsAsset == null)
                    {
                        _settingsAsset = ScriptableObject.CreateInstance<TweenTimelineSettings>();

                        AssetDatabase.CreateAsset(_settingsAsset, tweenConfigPath);
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
            {ActionEditorSettings.DelayResetTime, new Tuple<object, string>(1.0f, string.Empty)},
            {TweenPreferenceDefine.CustomTweenAssemblyName, new Tuple<object, string>(
                "TweenTimeline.Sample.Editor", string.Empty)},
            {TweenPreferenceDefine.CustomTweenPath, new Tuple<object, string>("Assets/Plugins/TweenTimeline/Sample", string.Empty)},
            {TweenPreferenceDefine.BuiltInLibraryPath, new Tuple<object, string>("Assets/TweenTimelineConfig/", string.Empty)},
        };

        public void Reset()
        {
            foreach (var item in Enum.GetValues(typeof(ActionEditorSettings)))
            {
                AddSetting(item.ToString(), "Action Editor ", string.Empty);
            }

            foreach (var item in Enum.GetValues(typeof(TweenPreferenceDefine)))
            {
                AddSetting(item.ToString(), "Custom ", string.Empty);
            }
            foreach (var item in Enum.GetValues(typeof(TweenGenSettings)))
            {
                AddSetting(item.ToString(), "GenSettings ", string.Empty);
            }

            foreach (var item in _customPairs)
            {
                UpdateValue(item.Key.ToString(), item.Value.Item1, item.Value.Item2);
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
            if(existingSetting == null){
                Debug.LogError($"Miss Settings {key}");
            }
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
