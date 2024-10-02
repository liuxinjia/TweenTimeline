using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


namespace Cr7Sund.TweenTimeLine
{
    public static class SerializedPropertyValueExtension
    {

        public static VisualElement CreateField(SerializedProperty serializedProperty)
        {
            switch (serializedProperty.propertyType)
            {
                case SerializedPropertyType.Integer:
                    var intField = new IntegerField(serializedProperty.displayName);
                    intField.value = serializedProperty.intValue;
                    intField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.intValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return intField;

                case SerializedPropertyType.Boolean:
                    var toggle = new Toggle(serializedProperty.displayName);
                    toggle.value = serializedProperty.boolValue;
                    toggle.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.boolValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return toggle;

                case SerializedPropertyType.Float:
                    var floatField = new FloatField(serializedProperty.displayName);
                    floatField.value = serializedProperty.floatValue;
                    floatField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.floatValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return floatField;

                case SerializedPropertyType.String:
                    var textField = new TextField(serializedProperty.displayName);
                    textField.value = serializedProperty.stringValue;
                    textField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.stringValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return textField;

                case SerializedPropertyType.Color:
                    var colorField = new ColorField(serializedProperty.displayName);
                    colorField.value = serializedProperty.colorValue;
                    colorField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.colorValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return colorField;

                case SerializedPropertyType.ObjectReference:
                    var objectField = new ObjectField(serializedProperty.displayName);
                    objectField.objectType = serializedProperty.objectReferenceValue?.GetType() ?? typeof(UnityEngine.Object);
                    objectField.value = serializedProperty.objectReferenceValue;
                    objectField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.objectReferenceValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return objectField;

                case SerializedPropertyType.Vector2:
                    var vector2Field = new Vector2Field(serializedProperty.displayName);
                    vector2Field.value = serializedProperty.vector2Value;
                    vector2Field.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.vector2Value = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return vector2Field;

                case SerializedPropertyType.Vector3:
                    var vector3Field = new Vector3Field(serializedProperty.displayName);
                    vector3Field.value = serializedProperty.vector3Value;
                    vector3Field.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.vector3Value = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return vector3Field;

                case SerializedPropertyType.Vector4:
                    var vector4Field = new Vector4Field(serializedProperty.displayName);
                    vector4Field.value = serializedProperty.vector4Value;
                    vector4Field.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.vector4Value = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return vector4Field;

                case SerializedPropertyType.Rect:
                    var rectField = new RectField(serializedProperty.displayName);
                    rectField.value = serializedProperty.rectValue;
                    rectField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.rectValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return rectField;

                case SerializedPropertyType.ArraySize:
                    var arraySizeField = new IntegerField(serializedProperty.displayName);
                    arraySizeField.value = serializedProperty.arraySize;
                    arraySizeField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.arraySize = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return arraySizeField;

                case SerializedPropertyType.Character:
                    var charField = new TextField(serializedProperty.displayName);
                    charField.maxLength = 1;
                    charField.value = serializedProperty.stringValue;
                    charField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.stringValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return charField;

                case SerializedPropertyType.AnimationCurve:
                    var curveField = new CurveField(serializedProperty.displayName);
                    curveField.value = serializedProperty.animationCurveValue;
                    curveField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.animationCurveValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return curveField;

                case SerializedPropertyType.Bounds:
                    var boundsField = new BoundsField(serializedProperty.displayName);
                    boundsField.value = serializedProperty.boundsValue;
                    boundsField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.boundsValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return boundsField;

                case SerializedPropertyType.Gradient:
                    var gradientField = new GradientField(serializedProperty.displayName);
                    gradientField.value = serializedProperty.gradientValue;
                    gradientField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.gradientValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return gradientField;

                case SerializedPropertyType.Quaternion:
                    var quaternionField = new Vector4Field(serializedProperty.displayName);
                    quaternionField.value = new Vector4(
                        serializedProperty.quaternionValue.x,
                        serializedProperty.quaternionValue.y,
                        serializedProperty.quaternionValue.z,
                        serializedProperty.quaternionValue.w
                    );
                    quaternionField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.quaternionValue = new Quaternion(
                            evt.newValue.x,
                            evt.newValue.y,
                            evt.newValue.z,
                            evt.newValue.w
                        );
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return quaternionField;

                case SerializedPropertyType.ExposedReference:
                    var exposedField = new ObjectField(serializedProperty.displayName);
                    exposedField.objectType = serializedProperty.exposedReferenceValue?.GetType() ?? typeof(UnityEngine.Object);
                    exposedField.value = serializedProperty.exposedReferenceValue;
                    exposedField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.exposedReferenceValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return exposedField;

                case SerializedPropertyType.Vector2Int:
                    var vector2IntField = new Vector2IntField(serializedProperty.displayName);
                    vector2IntField.value = serializedProperty.vector2IntValue;
                    vector2IntField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.vector2IntValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return vector2IntField;

                case SerializedPropertyType.Vector3Int:
                    var vector3IntField = new Vector3IntField(serializedProperty.displayName);
                    vector3IntField.value = serializedProperty.vector3IntValue;
                    vector3IntField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.vector3IntValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return vector3IntField;

                case SerializedPropertyType.RectInt:
                    var rectIntField = new RectIntField(serializedProperty.displayName);
                    rectIntField.value = serializedProperty.rectIntValue;
                    rectIntField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.rectIntValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return rectIntField;

                case SerializedPropertyType.BoundsInt:
                    var boundsIntField = new BoundsIntField(serializedProperty.displayName);
                    boundsIntField.value = serializedProperty.boundsIntValue;
                    boundsIntField.RegisterValueChangedCallback(evt =>
                    {
                        serializedProperty.boundsIntValue = evt.newValue;
                        serializedProperty.serializedObject.ApplyModifiedProperties();
                    });
                    return boundsIntField;

                default:
                    // 对于未处理的类型，返回一个只读的 PropertyField
                    var propertyField = new PropertyField(serializedProperty);
                    propertyField.SetEnabled(false);
                    return propertyField;
            }
        }

        public static Type GetRealType(SerializedPropertyType propertyType)
        {
            switch (propertyType)
            {
                case SerializedPropertyType.Integer:
                    return typeof(int);
                case SerializedPropertyType.Boolean:
                    return typeof(bool);
                case SerializedPropertyType.Float:
                    return typeof(float);
                case SerializedPropertyType.String:
                    return typeof(string);
                case SerializedPropertyType.Color:
                    return typeof(Color);
                case SerializedPropertyType.ObjectReference:
                    return typeof(UnityEngine.Object);
                case SerializedPropertyType.LayerMask:
                    return typeof(int);
                case SerializedPropertyType.Enum:
                    return typeof(Enum);
                case SerializedPropertyType.Vector2:
                    return typeof(Vector2);
                case SerializedPropertyType.Vector3:
                    return typeof(Vector3);
                case SerializedPropertyType.Rect:
                    return typeof(Rect);
                case SerializedPropertyType.AnimationCurve:
                    return typeof(AnimationCurve);
                case SerializedPropertyType.Bounds:
                    return typeof(Bounds);
                case SerializedPropertyType.Gradient:
                    return typeof(Gradient);
                case SerializedPropertyType.Quaternion:
                    return typeof(Quaternion);
                default:
                    return null;
            }
        }
    }
}