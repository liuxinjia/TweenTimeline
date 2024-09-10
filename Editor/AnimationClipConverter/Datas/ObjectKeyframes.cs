using System;
using System.Collections.Generic;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public class ObjectKeyframes
    {
        public string ObjectKey;
        public Type ObjectType;
        public List<TypeKeyframes> Types;

        public ObjectKeyframes(string objectKey, Type bindingType)
        {
            ObjectKey = objectKey;
            ObjectType = bindingType;
            Types = new List<TypeKeyframes>();
        }
    }
}