using System;
using System.Collections.Generic;
using Cr7Sund.Editor.CurvePreset;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.Assertions;
namespace Cr7Sund.TweenTimeLine
{
    internal static class EasingTokenPresetsFactory
    {
        public static void GenerateEasingTokenPresets(EasingTokenPresetLibrary presetLibrary,Type presetType,  string path)
        {
            Assert.IsTrue(typeof(BaseEasingTokenPreset).IsAssignableFrom(presetType));
            
            var instancec = Activator.CreateInstance(presetType) as BaseEasingTokenPreset;
            var tokenEnum = instancec.TokenType;
            var enums = Enum.GetValues(tokenEnum.GetType());
            var materialEasingLibrary = AssetDatabase.LoadAssetAtPath<Preset>(path);
            var curveDictionary = CurvePresetEditTools.GenerateCurveDict(materialEasingLibrary);

            if (curveDictionary == null)
            {
                return;
            }
            foreach (object item in enums)
            {
                string presetName = item.ToString();
                if (presetName == "Custom")
                {
                    continue;
                }
                if (!curveDictionary.ContainsKey(presetName))
                {
                    continue;
                }
                
                AnimationCurve animationCurve = curveDictionary[presetName];
                presetLibrary.easingTokenPresets.Add(instancec);
                instancec.Init((Enum)item, new EasingWrapper(animationCurve));
            }
        }

    }
}
