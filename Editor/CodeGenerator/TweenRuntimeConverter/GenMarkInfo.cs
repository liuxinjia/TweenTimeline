using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public class GenMarkInfo
    {
        public float Time;
        public string value;
        public string addictiveValue;
        public string filedName;

        public GenMarkInfo()
        {

        }

        public GenMarkInfo(IValueMaker valueMaker)
        {
            this.Time = (float)valueMaker.time;
            this.value = ConvertValue(valueMaker.Value);
            this.filedName = valueMaker.FieldName;
        }

        public static string ConvertValue(object value)
        {
            if (value is bool boolValue)
            {
                return boolValue.ToString().ToLower(); // Convert bool to "true" or "false"
            }
            else if (value is string strValue)
            {
                return $"\"{strValue}\""; // Wrap string in quotes
            }
            else if (value is float floatValue)
            {
                return $"{floatValue}f";
            }
            else if (value is Sprite sprite )
            {
                return sprite == null ? string.Empty:  $"\"{sprite.name}\"";
            }
            return value.ToString(); // Default conversion
        }
    }
}
