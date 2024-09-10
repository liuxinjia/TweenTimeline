using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cr7Sund.TweenTimeLine.Editor;
using UnityEditor;
using UnityEditor.UIElements;
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

            var choices = GetFieldWithType();
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

        private List<string> GetFieldWithType()
        {
            var image = new Image();
            SerializedProperty valueProp = serializedObject.FindProperty("_value");

            SerializedPropertyType propertyType1 = valueProp.propertyType;
            var propertyType = SerializedPropertyValueExtension.GetRealType(propertyType1);
            var marker = target as Marker;
            var targetComponent = TweenTimeLineDataModel.TrackObjectDict[marker.parent];
            var fields = targetComponent.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            var filterNames = fields
                .Where(field => field.FieldType == propertyType)
                .Select(field => field.Name)
                .ToList();
            var props = targetComponent.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var setProps = props
                .Where(prop => prop.SetMethod != null && prop.SetMethod.IsPublic
                && prop.PropertyType == propertyType)
                .Select(prop => prop.Name)
                .ToList();

            filterNames.AddRange(setProps);
            return filterNames;
        }

    }

}