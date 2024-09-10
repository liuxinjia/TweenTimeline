using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public class KeyframeDataWrapper 
    {
        public string AnimationName;
        public List<ObjectKeyframes> Objects;

        public KeyframeDataWrapper()
        {
            Objects = new List<ObjectKeyframes>();
        }
    }
}