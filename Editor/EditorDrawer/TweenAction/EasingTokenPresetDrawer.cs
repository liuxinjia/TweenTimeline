using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    [CustomPropertyDrawer(typeof(BaseEasingTokenPreset))]
    public class EasingTokenPresetDrawer : PropertyDrawer
    {
        private string tokenKeyPropertyPath = "tokenKey";
        private string curvePropertyPath = "animationCurve";

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return DrawEasePresetField(property);
        }

        private VisualElement DrawEasePresetField(SerializedProperty easePresetProp)
        {
            if (easePresetProp.managedReferenceValue == null)
            {
                var easePropInstance = Activator.CreateInstance<MaterialEasingTokenPreset>() as BaseEasingTokenPreset;
                easePresetProp.managedReferenceValue = easePropInstance;
            }
            Type currentType = easePresetProp.managedReferenceValue?.GetType();

            var derivedTypes = TweenTimelineDefine.DerivedEaseTokenTypes;
            string[] typeNames = derivedTypes.Select(t => t.Name).ToArray();
            int currentIndex = Array.FindIndex(derivedTypes, type => type == currentType);
            var popupField = new PopupField<string>("Ease Preset Type", typeNames.ToList(), currentIndex);

            popupField.RegisterValueChangedCallback(evt =>
            {
                int selectedIndex = typeNames.ToList().IndexOf(evt.newValue);
                Type selectedType = derivedTypes[selectedIndex];

                if (selectedType != currentType)
                {
                    // 1. since EasingTokenPresetLibrary also use this
                    // to avoid change EasingTokenPresetLibrary
                    // we don't use reference 
                    // 2. we need to store it and don'want to chane the unique instance at library
                    var easePropInstance = Activator.CreateInstance(selectedType) as BaseEasingTokenPreset;
                    easePresetProp.managedReferenceValue = easePropInstance;

                    var cureProperty = easePresetProp.FindPropertyRelative(curvePropertyPath);
                    // var tokenProperty = _easePresetProp.FindPropertyRelative(tokenKeyPropertyPath);
                    if (cureProperty != null)
                    {
                        cureProperty.animationCurveValue = GetEaseAnimationCurve(easePropInstance.Name);
                    }
                    // tokenProperty.enumValueIndex = enumValues.IndexOf();

                    easePresetProp.serializedObject.ApplyModifiedProperties();
                    easePresetProp.serializedObject.Update();
                }
            });

            // PropertyField easePresetField = new PropertyField(_easePresetProp, "Ease Preset");
            var easePresetField = CreateEasePresetField(easePresetProp, currentType);

            var container = new VisualElement();
            container.Add(popupField);
            container.Add(easePresetField);
            return container;
        }

        private VisualElement CreateEasePresetField(SerializedProperty easePresetProp, Type presetType)
        {
            var container = new VisualElement();

            var curveProperty = easePresetProp.FindPropertyRelative(curvePropertyPath);
            var tokenProperty = easePresetProp.FindPropertyRelative(tokenKeyPropertyPath);

            var curveField = new CurveField();
            curveField.name = "EasingCurve";
            curveField.style.minHeight = 200;

            string presetName = string.Empty;
            if (tokenProperty.propertyType == SerializedPropertyType.Enum)
            {
                presetName = tokenProperty.enumNames[tokenProperty.enumValueIndex];
            }
            else
            {
                presetName = tokenProperty.stringValue;
            }
            presetName = GetCurveName(presetName);

            curveField.value = GetEaseAnimationCurve(presetName);
            // curveField.SetEnabled(false);

            if (tokenProperty.propertyType == SerializedPropertyType.Enum)
            {
                var enumValues = tokenProperty.enumDisplayNames.ToList<string>();
                int currentIndex = tokenProperty.enumValueIndex;
                PopupField<string> popField = new PopupField<string>("EaseToken", enumValues, currentIndex);
                popField.RegisterValueChangedCallback(evt =>
                {
                    string curveName = GetCurveName(evt.newValue);
                    curveField.value = GetEaseAnimationCurve(curveName);
                    tokenProperty.enumValueIndex = enumValues.IndexOf(evt.newValue);
                    easePresetProp.serializedObject.ApplyModifiedProperties();
                    easePresetProp.serializedObject.Update();
                });

                container.Add(popField);
            }
            else if (tokenProperty.propertyType == SerializedPropertyType.String)
            {
                var customCurveContainer = new VisualElement();
                customCurveContainer.style.flexDirection = FlexDirection.Row;
                var customCurveNameField = new TextField("Curve Name");
                // customCurveNameField.BindProperty(tokenProperty);
                string curveName = tokenProperty.stringValue;
                if (string.IsNullOrEmpty(curveName))
                {
                    curveName = $"New Curve";
                }
                customCurveNameField.value = curveName;

                var saveBtn = new Button(() =>
                {
                    if (easePresetProp.managedReferenceValue is CustomCurveEasingTokenPreset customCurveEasingTokenPreset)
                    {
                        SaveCustomCurve(customCurveEasingTokenPreset, curveField.value, customCurveNameField.value);
                    }
                }
                );
                saveBtn.text = "Save";
                customCurveContainer.Add(customCurveNameField);
                customCurveContainer.Add(saveBtn);
                container.Add(customCurveContainer);
            }

            container.Add(curveField);
            return container;
        }

        private void SaveCustomCurve(CustomCurveEasingTokenPreset easingTokenPreset,
         AnimationCurve curve, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError("try to add a empty name curve Preset");
                return;
            }
            if (EasingTokenPresetLibraryEditor.CurveDictionary.ContainsKey(name)
            && easingTokenPreset.tokenKey != name)
            {
                Debug.LogError("try to add a same name curve Preset");
                return;
            }

            var easingTokenPresetLibrary = AssetDatabase.LoadAssetAtPath<EasingTokenPresetLibrary>(TweenTimelineDefine.easingTokenPresetsPath);
            if (easingTokenPreset.tokenKey != name)
            {
                easingTokenPresetLibrary.RemovePreset(easingTokenPreset);
                EasingTokenPresetLibraryEditor.CurveDictionary.Remove(easingTokenPreset.tokenKey);
            }

            easingTokenPreset.animationCurve = curve;
            easingTokenPreset.tokenKey = name;
            easingTokenPresetLibrary.AddPreset(easingTokenPreset);
            EasingTokenPresetLibraryEditor.UpdatePresetLibrary(easingTokenPreset);
        }


        private static string GetCurveName(string curveName)
        {
            string evtNewValue = curveName.Replace(" ", "");
            return evtNewValue;
        }

        private AnimationCurve GetEaseAnimationCurve(string curveName)
        {
            if (!string.IsNullOrEmpty(curveName))
            {
                if (EasingTokenPresetsFactory.easeToEquationsMap.ContainsKey(curveName))
                {
                    curveName = EasingTokenPresetsFactory.easeToEquationsMap[curveName];
                }

                // Assert.IsFalse(curveName.Any(char.IsWhiteSpace), $"{curveName} contains white space");
                if (EasingTokenPresetLibraryEditor.CurveDictionary.ContainsKey(curveName))
                {
                    var animationCurve = EasingTokenPresetLibraryEditor.CurveDictionary[curveName];
                    // var copyCurve = new AnimationCurve();
                    // copyCurve.CopyFrom(animationCurve);
                    return animationCurve;
                }
            }
            return AnimationCurve.Linear(0, 0, 1, 1);
        }

        private bool ContainsCurveSource(string curveName)
        {
            if (EasingTokenPresetsFactory.easeToEquationsMap.ContainsKey(curveName))
            {
                curveName = EasingTokenPresetsFactory.easeToEquationsMap[curveName];
            }

            return EasingTokenPresetLibraryEditor.CurveDictionary.ContainsKey(curveName);
        }
    }
}
