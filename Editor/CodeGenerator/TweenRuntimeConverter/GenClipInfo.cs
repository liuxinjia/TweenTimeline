using System.Collections.Generic;
namespace Cr7Sund.TweenTimeLine
{
    public class GenClipInfo
    {
        public float DelayTime; // start- prevClip
        public float Duration;
        public object EndValue;
        public object StartValue;
        public string TweenMethod;
        public string CustomTweenMethod;
        public string EaseName;
        public string BindType;
        public string BindName;
        public List<GenMarkInfo> genMarkInfos = new();

    }
}
