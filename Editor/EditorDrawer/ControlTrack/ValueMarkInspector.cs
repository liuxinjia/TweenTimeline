using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

namespace Cr7Sund.TweenTimeLine
{
    [CustomEditor(typeof(ValueMaker), true)]
    public class ValueMarkInspector : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            TweenTimelineManager.InitTimeline();

            var container = new VisualElement();
            var valuePropField = new PropertyField();
            SerializedProperty fieldValueProp = serializedObject.FindProperty("_value");
            valuePropField.BindProperty(fieldValueProp);

            var choices = GetFieldWithType(fieldValueProp.propertyType == SerializedPropertyType.Boolean);
            if (choices.Count > 0)
            {
                if (string.IsNullOrEmpty(serializedObject.FindProperty("_fieldName").stringValue))
                {
                    serializedObject.FindProperty("_fieldName").stringValue = choices[0];
                    serializedObject.ApplyModifiedProperties();
                }
            }
            var index = choices.IndexOf(serializedObject.FindProperty("_fieldName").stringValue);
            index = Math.Max(0, index);
            var dropDownField = new DropdownField("FieldName", choices, index);
            container.Add(dropDownField);
            dropDownField.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                SerializedProperty fieldNameProp = serializedObject.FindProperty("_fieldName");
                fieldNameProp.stringValue = evt.newValue;
                serializedObject.ApplyModifiedProperties();
            });

            container.Add(valuePropField);
            return container;
        }

        private List<string> GetFieldWithType(bool isBoolType)
        {
            SerializedProperty valueProp = serializedObject.FindProperty("_value");

            var propertyType = SerializedPropertyValueExtension.GetRealType(valueProp.propertyType);
            var marker = target as Marker;
            // parent can be not assigned
            if (!TweenTimeLineDataModel.TrackObjectDict.ContainsKey(marker.parent))
            {
                Debug.LogError("before you try mark action, please assign the target component first");
                return new List<string>();
            }

            var targetComponent = TweenTimeLineDataModel.TrackObjectDict[marker.parent];
            var fields = targetComponent.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            var filterNames = fields
                .Where(field => field.FieldType == propertyType)
                .Select(field => field.Name)
                .ToList();
            var props = targetComponent.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var setProps = props
                .Where(prop => prop.SetMethod != null && prop.SetMethod.IsPublic
                 && propertyType.IsAssignableFrom(prop.PropertyType))
                .Select(prop => prop.Name)
                .ToList();

            filterNames.AddRange(setProps);

            if (isBoolType
             && (typeof(UnityEngine.UI.Graphic).IsAssignableFrom(targetComponent.GetType())
             || typeof(UnityEngine.CanvasGroup).IsAssignableFrom(targetComponent.GetType())))
            {
                filterNames.Add(TweenTimelineDefine.IsActiveFieldName);
            }
            return filterNames;
        }

    }

}