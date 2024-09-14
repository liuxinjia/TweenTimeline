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
            var fieldInfo = target.GetType().GetField(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (fieldInfo == null)
            {
                var propInfo = target.GetType().GetProperty(FieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
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
