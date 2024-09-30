using System;
using System.Collections.Generic;
using System.Linq;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public class ObjectKeyframes
    {
        public string ObjectPath;
        public Type ObjectType;
        public List<TypeKeyframes> Types;

        public ObjectKeyframes(string objectKey, Type bindingType)
        {
            ObjectPath = objectKey;
            ObjectType = bindingType;
            Types = new List<TypeKeyframes>();
        }

        public string GetObjectName()
        {
            return ObjectPath.Split('/').Last();
        }
    }
}