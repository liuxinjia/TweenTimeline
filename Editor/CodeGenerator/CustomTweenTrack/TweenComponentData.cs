using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    [System.Serializable]
    public class TweenComponentData
    {
        public string ComponentType;
        public string ValueType;
        public string GetPropertyMethod;
        [SerializeField] private string SetPropertyMethod;
        [SerializeField] private string PreTweenMethod;


        public TweenComponentData()
        {
        }

        public TweenComponentData(string componentType, string valueType, string getPropertyMethod, string setPropertyMethod, string preTweenMethod)
        {
            ComponentType = componentType;
            ValueType = valueType;
            GetPropertyMethod = getPropertyMethod;
            SetPropertyMethod = setPropertyMethod;
            PreTweenMethod = preTweenMethod;
        }

        public string GetTweenMethod()
        {
            return PreTweenMethod;
        }

        public string GetSetMethod()
        {
            return SetPropertyMethod;
        }

        public string GetCustomTweenMethod()
        {
            var result = SetPropertyMethod.Replace("target.", "");
            result = result.TrimEnd(';');
            return result;
        }
    }
}
