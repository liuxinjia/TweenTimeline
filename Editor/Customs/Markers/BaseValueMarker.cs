using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    public interface IValueMaker
    {
        object Value { get; }
    }

    public abstract class ValueMaker : Marker, IValueMaker
    {
        public abstract object Value { get; }
    }
    public abstract class BaseValueMarker<TValue> : ValueMaker, IValueMaker, INotification, INotificationOptionProvider
    {
        [SerializeField] private TValue value;
        // [TextArea(1, 15)] public string note;

        public PropertyName id { get; }

        public NotificationFlags flags => NotificationFlags.TriggerInEditMode;

        public override object Value => value;
    }
}