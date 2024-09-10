using System;
using System.Collections.Generic;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public class TypeKeyframes
    {
        public SerializableType Type;
        public List<PropertyKeyframes> Properties;

        public TypeKeyframes(Type type)
        {
            Type = new SerializableType(type);
            Properties = new List<PropertyKeyframes>();
        }
    }
}