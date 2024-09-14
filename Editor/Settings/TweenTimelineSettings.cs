using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace Cr7Sund.TweenTimeLine
{

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



        public void Reset()
        {
            foreach (var item in Enum.GetValues(typeof(TweenPreferenceDefine)))
            {
                AddSetting(item.ToString(), "Custom ", string.Empty);
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

                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
