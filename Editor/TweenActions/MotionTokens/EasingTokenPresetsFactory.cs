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
        public static Dictionary<string, string> easeToEquationsMap = new Dictionary<string, string>
        {
            {
                "Linear", "Linear"
            },
            {
                "InSine", "SineEaseIn"
            },
            {
                "OutSine", "SineEaseOut"
            },
            {
                "InOutSine", "SineEaseInOut"
            },
            {
                "InQuad", "QuadEaseIn"
            },
            {
                "OutQuad", "QuadEaseOut"
            },
            {
                "InOutQuad", "QuadEaseInOut"
            },
            {
                "InCubic", "CubicEaseIn"
            },
            {
                "OutCubic", "CubicEaseOut"
            },
            {
                "InOutCubic", "CubicEaseInOut"
            },
            {
                "InQuart", "QuartEaseIn"
            },
            {
                "OutQuart", "QuartEaseOut"
            },
            {
                "InOutQuart", "QuartEaseInOut"
            },
            {
                "InQuint", "QuintEaseIn"
            },
            {
                "OutQuint", "QuintEaseOut"
            },
            {
                "InOutQuint", "QuintEaseInOut"
            },
            {
                "InExpo", "ExpoEaseIn"
            },
            {
                "OutExpo", "ExpoEaseOut"
            },
            {
                "InOutExpo", "ExpoEaseInOut"
            },
            {
                "InCirc", "CircEaseIn"
            },
            {
                "OutCirc", "CircEaseOut"
            },
            {
                "InOutCirc", "CircEaseInOut"
            },
            {
                "InElastic", "ElasticEaseIn"
            },
            {
                "OutElastic", "ElasticEaseOut"
            },
            {
                "InOutElastic", "ElasticEaseInOut"
            },
            {
                "InBack", "BackEaseIn"
            },
            {
                "OutBack", "BackEaseOut"
            },
            {
                "InOutBack", "BackEaseInOut"
            },
            {
                "InBounce", "BounceEaseIn"
            },
            {
                "OutBounce", "BounceEaseOut"
            },
            {
                "InOutBounce", "BounceEaseInOut"
            }
        };

        public static void GenerateEasingTokenPresets(EasingTokenPresetLibrary presetLibrary,
            Type presetType,
            Dictionary<string, AnimationCurve> curveDictionary)
        {
            Assert.IsTrue(typeof(BaseEasingTokenPreset).IsAssignableFrom(presetType));

            var tokenEnum = presetType.GetField("tokenKey",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (!tokenEnum.FieldType.IsEnum)
            {
                return;
            }

            var enums = Enum.GetValues(tokenEnum.FieldType);

            foreach (object item in enums)
            {
                var instance = Activator.CreateInstance(presetType) as BaseEasingTokenPreset;
                string presetName = item.ToString();
                if (presetName == "Custom")
                {
                    continue;
                }
                if (typeof(EaseTokenPreset).IsAssignableFrom(presetType))
                {
                    if (easeToEquationsMap.ContainsKey(presetName))
                    {
                        presetName = easeToEquationsMap[presetName];
                    }
                }
                if (!curveDictionary.ContainsKey(presetName))
                {
                    continue;
                }

                AnimationCurve animationCurve = curveDictionary[presetName];
                instance.Init(item.ToString(), new EasingWrapper(animationCurve));
                presetLibrary.AddPreset(instance);
            }
        }

    }
}
