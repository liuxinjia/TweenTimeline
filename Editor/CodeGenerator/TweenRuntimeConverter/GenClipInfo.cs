﻿using System.Collections.Generic;
using PrimeTween;
using Assert = UnityEngine.Assertions.Assert;
namespace Cr7Sund.TweenTimeLine
{
    public class GenClipInfo
    {
        public float DelayTime; // start- prevClip
        public float Duration;
        public object EndValue;
        public object StartValue;
        public bool IsDynamicPos;
        public string TweenMethod;
        public string CustomTweenMethod;
        public string EaseName;
        public string BindType;
        private string bindName;
        public List<GenMarkInfo> genMarkInfos = new();

        public string BindName
        {
            get
            {
                Assert.IsNotNull(BindType, $"{bindName} don have bind type");
                return $"{bindName}_{TypeConverter.GetSimplifyTypeName(BindType)}";
            }
            set => bindName = value;
        }
    }
}
