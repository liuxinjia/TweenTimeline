using System;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
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

        private VisualElement DrawEasePresetField(SerializedProperty _easePresetProp)
        {
            if(_easePresetProp.managedReferenceValue == null){
                var easePropInstance = Activator.CreateInstance<MaterialEasingTokenPreset>() as BaseEasingTokenPreset;
                _easePresetProp.managedReferenceValue = easePropInstance;
            }
            Type currentType = _easePresetProp.managedReferenceValue?.GetType();

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
                    _easePresetProp.managedReferenceValue = easePropInstance;

                    var cureProperty = _easePresetProp.FindPropertyRelative(curvePropertyPath);
                    var tokenProperty = _easePresetProp.FindPropertyRelative(tokenKeyPropertyPath);
                    if (cureProperty != null)
                    {
                        cureProperty.animationCurveValue = GetEaseAnimationCurve(easePropInstance.Name);
                    }
                    // tokenProperty.enumValueIndex = enumValues.IndexOf();

                    _easePresetProp.serializedObject.ApplyModifiedProperties();
                    _easePresetProp.serializedObject.Update();
                }
            });

            // PropertyField easePresetField = new PropertyField(_easePresetProp, "Ease Preset");
            var easePresetField = CreateEasePresetField(_easePresetProp, currentType);

            var container = new VisualElement();
            container.Add(popupField);
            container.Add(easePresetField);
            return container;
        }

        private VisualElement CreateEasePresetField(SerializedProperty _easePresetProp, Type presetType)
        {
            var container = new VisualElement();

            var cureProperty = _easePresetProp.FindPropertyRelative(curvePropertyPath);
            var tokenProperty = _easePresetProp.FindPropertyRelative(tokenKeyPropertyPath);
            if (tokenProperty == null)
            {
                return new PropertyField(_easePresetProp);
            }

            var curveField = new CurveField();
            curveField.name = "EasingCurve";
            curveField.style.minHeight = 200;
            if (cureProperty != null)
            {
                curveField.BindProperty(cureProperty);
            }
            else
            {
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
            }
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
                    _easePresetProp.serializedObject.ApplyModifiedProperties();
                    _easePresetProp.serializedObject.Update();
                });

                container.Add(popField);
            }
            container.Add(curveField);
            return container;
        }
        private static string GetCurveName(string curveName)
        {
            string evtNewValue = curveName.Replace(" ", "");
            return evtNewValue;
        }

        private AnimationCurve GetEaseAnimationCurve(string curveName)
        {
            if (EasingTokenPresetsFactory.easeToEquationsMap.ContainsKey(curveName))
            {
                curveName = EasingTokenPresetsFactory.easeToEquationsMap[curveName];
            }

            if (EasingTokenPresetLibraryEditor.CurveDictionary.ContainsKey(curveName))
            {
                return EasingTokenPresetLibraryEditor.CurveDictionary[curveName];
            }
            else
            {
                return AnimationCurve.Constant(0, 1, 1);
            }
        }
    }
}
