using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using NoiseCrimeStudios.Demo.Easing.CurveCreation;
using EaseEquations = NoiseCrimeStudios.Core.Features.Easing.EasingEquationsDouble.JitterEquations;
using static NoiseCrimeStudios.Demo.Easing.CurveCreation.EasingToAnimationCurve;
using NoiseCrimeStudios.Core.Features.Easing;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Cr7Sund.Editor.CurvePreset
{

    public class CurvePresetEditTools : EditorWindow
    {

        [MenuItem("Tools/CurvePresetEditTools")]
        public static void Init()
        {
            var win = GetWindow<CurvePresetEditTools>();
        }

        private AnimationCurve m_Curve = null;
        private Dictionary<string, AnimationCurve> curveDictionary = new Dictionary<string, AnimationCurve>();
        private UnityEngine.Object selectObject;

        private const string VisualTreeAssetGUID = "941bf131ffc492149bfa168c298daf6e";
        private const string StyleAssetGUID = "1dd31a206fe72d949b066fcf91e36c06";

        private void OnEnable()
        {
            selectObject = Selection.activeObject;
        }

        private void CreateGUI()
        {
            var visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(AssetDatabase.GUIDToAssetPath(VisualTreeAssetGUID));
            visualTreeAsset.CloneTree(rootVisualElement);

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(StyleAssetGUID));
            rootVisualElement.styleSheets.Add(styleSheet);

            var objectField = rootVisualElement.Q<ObjectField>("objectField");
            objectField.objectType = typeof(UnityEditor.AndroidBuildType).Assembly.GetType("UnityEditor.CurvePresetLibrary");
            var curveListContainer = rootVisualElement.Q<ScrollView>("curveListContainer");
            var saveButton = rootVisualElement.Q<Button>("saveButton");
            var deleteButton = rootVisualElement.Q<Button>("deleteButton");
            var createButton = rootVisualElement.Q<Button>("createButton");
            var curveLookField = rootVisualElement.Q<IMGUIContainer>("curveLookField");

            if (objectField.objectType == selectObject.GetType())
                objectField.value = selectObject;
            objectField.RegisterValueChangedCallback(evt =>
            {
                selectObject = evt.newValue;
                CreateAnimCurves();
            });

            saveButton.RegisterCallback<ClickEvent>(evt => SavePresets());
            deleteButton.RegisterCallback<ClickEvent>(evt => ClearPresets());
            createButton.RegisterCallback<ClickEvent>(evt => CreatePresets());

            curveLookField.onGUIHandler = IMGUICode;

            CreateAnimCurves();

            void IMGUICode()
            {
                Rect R = GUILayoutUtility.GetRect(300, 300, 100, 100, GUILayout.Width(200));
                EditorGUIUtility.DrawCurveSwatch(R, m_Curve, null, Color.green, Color.black);
            }
        }

        private void CreateAnimCurves()
        {
            if (selectObject == null)
            {
                return;
            }
            var objectField = rootVisualElement.Q<ObjectField>("objectField");
            if (objectField.objectType != selectObject.GetType())
            {
                return;
            }
            VisualElement curveListContainer = rootVisualElement.Q<VisualElement>("curveListContainer");
            curveListContainer.Clear();
            curveDictionary = GenerateCurveDict(selectObject);
            foreach (var item in curveDictionary)
            {
                // Create curve field UI element
                var curveField = new CurveField(item.Key);
                curveField.value = item.Value;
                curveField.name = item.Key;
                curveField.RegisterCallback<MouseDownEvent>(evt =>
                {
                    CurveField targetCurveField = null;
                    if (evt.target is Label label)
                    {
                        targetCurveField = rootVisualElement.Q<CurveField>(label.text);
                    }
                    else if (evt.target is CurveField)
                    {
                        targetCurveField = evt.target as CurveField;
                    }
                    m_Curve = targetCurveField.value;
                    curveDictionary[name] = targetCurveField.value;
                });
                curveField.RegisterValueChangedCallback(evt =>
                {
                    curveDictionary[name] = evt.newValue;
                    m_Curve = evt.newValue;
                });
                curveListContainer.Add(curveField);
            }
        }

        public static Dictionary<string, AnimationCurve> GenerateCurveDict(UnityEngine.Object presetLibrary)
        {
            var curves = GetPresets(presetLibrary);
            if (curves == null)
            {
                return null;
            }

            Dictionary<string, AnimationCurve> curveDictionary = new();
            foreach (var item in curves)
            {
                var curveValue = item.GetType().GetField("m_Curve", BindingFlags.NonPublic | BindingFlags.Instance);
                var nameValue = item.GetType().GetField("m_Name", BindingFlags.NonPublic | BindingFlags.Instance);

                var curve = curveValue.GetValue(item) as AnimationCurve;
                var name = nameValue.GetValue(item) as string;

                if (curve != null && name != null && !curveDictionary.ContainsKey(name))
                {
                    curveDictionary.Add(name, curve);
                }
            }

            return curveDictionary;
        }

        void CreatePresets()
        {
            var mo = new ExampleEaseToAnimationCurve();
            ConversionProperties cp = mo.CreateConversionProperties();
            cp.m_SmoothTangentMaxAngle = 180f;

            foreach (EaseEquations equation in Enum.GetValues(typeof(EaseEquations)))
            {
                switch (equation)
                {
                    // ease 
                    // ----
                    // case EaseEquations.BounceEaseIn:
                    // case EaseEquations.BounceEaseInOut:
                    // case EaseEquations.BounceEaseOut:
                    // case EaseEquations.BounceEaseOutIn:

                    //jitter
                    // ----
                    case EaseEquations.Bounce:
                        cp.m_SmoothTangentMaxAngle = 60f;
                        break;
                    default:
                        cp.m_SmoothTangentMaxAngle = 180f;
                        break;
                }
                AnimationCurve ac = new AnimationCurve();
                var easeAnimCurve = new EasingToAnimationCurve();
                var easeMethod = (Ease)Delegate.CreateDelegate(typeof(Ease), typeof(EasingEquationsDouble).GetMethod(equation.ToString()));

                easeAnimCurve.ConvertEaseMethodToCurve(easeMethod, equation, ac, cp, false);
                SaveSetting(ac, equation.ToString());
            }

            if(!selectObject.name.EndsWith("EaseEquations"))
            {
                // see EasingTokenPresetLibraryEditor.cs
                Debug.LogError("For the sake of god !!! Please end the file name with EaseEquations");
            }
            AssetDatabase.SaveAssetIfDirty(selectObject);
            AssetDatabase.Refresh();
        }

        void SaveSetting(AnimationCurve animationCurve, string curveName)
        {
            var presetLibrary = selectObject;
            Type type = presetLibrary.GetType();
            var addMethod = type.GetMethod("Add", BindingFlags.Public | BindingFlags.Instance);
            addMethod.Invoke(presetLibrary, new object[]
            {
                animationCurve, curveName
            });

            EditorUtility.SetDirty(presetLibrary);
        }

        private void SavePresets()
        {
            int index = 0;
            foreach (var item in curveDictionary)
            {
                ReplaceCurvePreset(item.Value, index++);
            }

            AssetDatabase.SaveAssetIfDirty(selectObject);
            AssetDatabase.Refresh();
        }

        private void ClearPresets()
        {
            int index = 0;
            var presets = GetPresets(selectObject);
            foreach (var item in presets)
            {
                index++;
            }

            for (int i = index - 1; i >= 0; i--)
            {
                RemoveCurvePreset(i);
            }
            AssetDatabase.SaveAssetIfDirty(selectObject);
            AssetDatabase.Refresh();
        }

        void ReplaceCurvePreset(AnimationCurve animationCurve, int index)
        {
            var presetLibrary = selectObject;
            Type type = presetLibrary.GetType();
            var addMethod = type.GetMethod("Replace", BindingFlags.Public | BindingFlags.Instance);
            addMethod.Invoke(presetLibrary, new object[]
            {
                index, animationCurve
            });

            EditorUtility.SetDirty(presetLibrary);
        }

        void RemoveCurvePreset(int index)
        {
            Debug.Log(index);
            var presetLibrary = selectObject;
            Type type = presetLibrary.GetType();
            var addMethod = type.GetMethod("Remove", BindingFlags.Public | BindingFlags.Instance);
            addMethod.Invoke(presetLibrary, new object[]
            {
                index
            });

            EditorUtility.SetDirty(presetLibrary);

        }

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


    }

}
