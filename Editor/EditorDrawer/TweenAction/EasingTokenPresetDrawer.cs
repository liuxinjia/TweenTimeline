using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    [CustomPropertyDrawer(typeof(BaseEasingTokenPreset))]
    public class EasingTokenPresetDrawer : PropertyDrawer
    {
        private string tokenKeyPropertyPath = "tokenKey";
        private string curePropertyPath = "animationCurve";

        private EasingTokenPresetLibrary _easingTokenPresetLibrary;
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _easingTokenPresetLibrary = AssetDatabase.LoadAssetAtPath<EasingTokenPresetLibrary>(TweenTimelineDefine.easingTokenPresetsPath);
            return DrawEasePresetField(property);
        }

        private VisualElement DrawEasePresetField(SerializedProperty _easePresetProp)
        {
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
                    var easePropInstance = Activator.CreateInstance(selectedType) as BaseEasingTokenPreset;
                    _easePresetProp.managedReferenceValue = easePropInstance;

                    var cureProperty = _easePresetProp.FindPropertyRelative(curePropertyPath);
                    var tokenProperty = _easePresetProp.FindPropertyRelative(tokenKeyPropertyPath);
                    if (tokenProperty != null && _easingTokenPresetLibrary != null)
                    {
                        cureProperty.animationCurveValue = _easingTokenPresetLibrary.GetEaseAnimationCurve(easePropInstance.Name);
                    }

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
            var cureProperty = _easePresetProp.FindPropertyRelative(curePropertyPath);
            var tokenProperty = _easePresetProp.FindPropertyRelative(tokenKeyPropertyPath);

            if (tokenProperty == null)
            {
                return new PropertyField(_easePresetProp);
            }

            var curveField = new CurveField();
            curveField.style.minHeight = 200;
            curveField.BindProperty(cureProperty);

            var enumType = presetType.GetField(tokenKeyPropertyPath, BindingFlags.Instance | BindingFlags.Public);
            var enumValues = Enum.GetNames(enumType.FieldType).ToList<string>();

            int currentIndex = tokenProperty.enumValueIndex;
            var popField = new PopupField<string>("EaseToken", enumValues, currentIndex);
            popField.RegisterValueChangedCallback(evt =>
            {
                if (_easingTokenPresetLibrary)
                {
                    curveField.value = _easingTokenPresetLibrary.GetEaseAnimationCurve(evt.newValue);
                }
                tokenProperty.enumValueIndex = enumValues.IndexOf(evt.newValue);
                _easePresetProp.serializedObject.ApplyModifiedProperties();
                _easePresetProp.serializedObject.Update();
            });

            var container = new VisualElement();
            container.Add(curveField);
            container.Add(popField);
            return container;
        }

    }
}
