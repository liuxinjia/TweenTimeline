using System.Threading;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    public interface IValueMaker : IMarker
    {
        object Value { get; }
        string FieldName { get; }
        int instanceID { get; }
    }

    public abstract class ValueMaker : Marker, IValueMaker
    {
        [SerializeField] private string _fieldName;

        public abstract object Value { get; }
        public string FieldName => _fieldName;

        public int instanceID => GetInstanceID();


        public void Set(object target)
        {
            var fieldInfo = target.GetType().GetField(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (fieldInfo == null)
            {
                var propInfo = target.GetType().GetProperty(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                propInfo.SetMethod.Invoke(target, new object[] { Value });
            }
            else
            {
                fieldInfo.SetValue(target, Value);
            }
        }

        public void Get(object target)
        {
            var fieldInfo = target.GetType().GetField(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (fieldInfo == null)
            {
                var propInfo = target.GetType().GetProperty(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                propInfo.GetMethod.Invoke(target, null);
            }
            else
            {
                fieldInfo.GetValue(target);
            }
        }

    }

    public abstract class BaseValueMarker<TValue> : ValueMaker, IValueMaker, INotification, INotificationOptionProvider
    {
        [SerializeField] private TValue _value;
        // [TextArea(1, 15)] public string note;

        public PropertyName id { get; }

        public NotificationFlags flags => NotificationFlags.TriggerInEditMode;

        public override object Value => _value;

    }
}