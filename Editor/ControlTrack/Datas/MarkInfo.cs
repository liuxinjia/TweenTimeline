using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Cr7Sund.TweenTimeLine
{
    public class MarkInfo
    {
        public object UpdateValue;
        public string FieldName;
        public double Time;
        public int InstanceID;

        public MarkInfo(IValueMaker field)
        {
            UpdateValue = field.Value;
            FieldName = field.FieldName;
            Time = field.time;
            InstanceID = field.instanceID;
        }

        public void Set(object target, object updateValue)
        {
            Assert.IsNotNull(updateValue);
            Assert.IsNotNull(target);

            if (FieldName == TweenTimelineDefine.IsActiveFieldName)
            {
                var targetComponent = target as Graphic;
                targetComponent.Fade((bool)updateValue);
                return;
            }

            var fieldInfo = target.GetType().GetField(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (fieldInfo == null)
            {
                var propInfo = target.GetType().GetProperty(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                Assert.IsNotNull(propInfo, $"{target} don't have set property {FieldName}");
                propInfo.SetMethod.Invoke(target, new object[] { updateValue });
            }
            else
            {
                fieldInfo.SetValue(target, updateValue);
            }
        }

        public object Get(object target)
        {
            if (string.IsNullOrEmpty(FieldName))
            {
                return null;
            }

            if (FieldName == TweenTimelineDefine.IsActiveFieldName)
            {
                return UpdateValue;
            }
            var fieldInfo = target.GetType().GetField(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (fieldInfo == null)
            {
                var propInfo = target.GetType().GetProperty(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                return propInfo.GetMethod.Invoke(target, null);
            }
            else
            {
                return fieldInfo.GetValue(target);
            }
        }

    }
}
