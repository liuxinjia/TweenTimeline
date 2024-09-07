using Cr7Sund.TweenTimeLine.Editor;
using PrimeTween;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    [CustomPropertyDrawer(typeof(EasingTokenPreset))]
    public class EasingTokenPresetDrawer : PropertyDrawer
    {
        private EasingTokenPresets easingTokenPreset;
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            easingTokenPreset = AssetDatabase.LoadAssetAtPath<EasingTokenPresets>(AssetDatabase.GUIDToAssetPath(AnimationEditorWindow.easingTokenPresetGuid));

            var materialTokenKeyProperty = property.FindPropertyRelative("materialTokenKey");
            var jitterTokenKeyProperty = property.FindPropertyRelative("jitterTokenKey");

            // Create MaterialEasingToken dropdown
            var materialKeyField = new EnumField();
            materialKeyField.label = "Material Easing Token";
            materialKeyField.name = "MaterialEasingToken"; // Added name
            materialKeyField.BindProperty(materialTokenKeyProperty);
            materialKeyField.RegisterValueChangedCallback(evt =>
            {

                UpdateFieldVisibility(container,
                    (MaterialEasingToken)materialTokenKeyProperty.enumValueFlag,
                    (JitterEasingToken)jitterTokenKeyProperty.enumValueFlag);
            });
            container.Add(materialKeyField);

            // Create JitterEasingToken dropdown
            var jitterKeyField = new EnumField();
            jitterKeyField.label = "Jitter Easing Token";
            jitterKeyField.name = "JitterEasingToken"; // Added name
            jitterKeyField.BindProperty(jitterTokenKeyProperty);
            jitterKeyField.RegisterValueChangedCallback(evt =>
            {

                UpdateFieldVisibility(container,
                    (MaterialEasingToken)materialTokenKeyProperty.enumValueFlag,
                    (JitterEasingToken)jitterTokenKeyProperty.enumValueFlag);
            });
            container.Add(jitterKeyField);

            // Create AnimationCurve editor
            var animationCurveField = new CurveField("Animation Curve");
            animationCurveField.name = "AnimationCurve"; // Added name
            animationCurveField.BindProperty(property.FindPropertyRelative("animationCurve"));
            container.Add(animationCurveField);

            // Create PrimeTween.Ease dropdown
            var easeField = new EnumField();
            easeField.label = "Ease";
            easeField.name = "Ease"; // Added name
            easeField.BindProperty(property.FindPropertyRelative("ease"));
            container.Add(easeField);

            // Initial field visibility update
            UpdateFieldVisibility(container,
                (MaterialEasingToken)materialTokenKeyProperty.enumValueFlag,
                (JitterEasingToken)jitterTokenKeyProperty.enumValueFlag);

            return container;
        }

        private void UpdateFieldVisibility(VisualElement container,  MaterialEasingToken materialEasingToken, JitterEasingToken jitterEasingToken)
        {
            var materialKeyField = container.Q<EnumField>("MaterialEasingToken");
            var jitterKeyField = container.Q<EnumField>("JitterEasingToken");
            var curveField = container.Q<CurveField>("AnimationCurve");
            var easeField = container.Q<EnumField>("Ease");

            var easePreset = easingTokenPreset.GetEasePreset( jitterEasingToken, materialEasingToken);
            curveField.value = easePreset.animationCurve;

            bool isJitter = jitterEasingToken != JitterEasingToken.Custom;
            bool isMaterial = materialEasingToken != MaterialEasingToken.Custom;
            bool isEasing = !isJitter && !isMaterial;

            materialKeyField.style.display = !isJitter ? DisplayStyle.Flex : DisplayStyle.None;
            jitterKeyField.style.display = !isMaterial ? DisplayStyle.Flex : DisplayStyle.None;
            curveField.style.display = !isEasing ? DisplayStyle.Flex : DisplayStyle.None;
            easeField.style.display = isEasing ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}