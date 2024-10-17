using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Cr7Sund
{
    // duplicate of AnimationCurveExtensions
    public static class EasingCurveExtensions
    {
       

        public static IEnumerable GetPresets(UnityEngine.Object presetLibrary)
        {
            if (presetLibrary == null)
            {
                return null;
            }
            Type type = presetLibrary.GetType();
            var presets = type.GetField("m_Presets", BindingFlags.NonPublic | BindingFlags.Instance);
            if (presets == null)
            {
                return null;
            }
            object input = presets.GetValue(presetLibrary);

            var enumerable = input as IEnumerable;

            return enumerable;
        }

        public static void GenerateCurveDict(UnityEngine.Object easingLibrary,
            in Dictionary<string, AnimationCurve> curveDictionary)
        {
            var curves = EasingCurveExtensions.GetPresets(easingLibrary);
            if (curves == null)
            {
                return;
            }

            EasingCurveExtensions.ConstructCurveDict(curveDictionary, curves);

        }

        private static void ConstructCurveDict(in Dictionary<string, AnimationCurve> curveDictionary, IEnumerable curves)
        {
            foreach (var item in curves)
            {
                Type type = item.GetType();
                var curveValue = type.GetField("m_Curve", BindingFlags.NonPublic | BindingFlags.Instance);
                var nameValue = item.GetType().GetField("m_Name", BindingFlags.NonPublic | BindingFlags.Instance);

                var name = nameValue.GetValue(item) as string;
                var curve = curveValue.GetValue(item) as AnimationCurve;

                if (curve != null && name != null && !curveDictionary.ContainsKey(name))
                {
                    curveDictionary.Add(name, curve);
                }
            }
        }
    }

}
