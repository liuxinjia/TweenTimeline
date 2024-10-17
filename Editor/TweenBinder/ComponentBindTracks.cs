using System.Collections.Generic;

namespace Cr7Sund.TweenTimeLine
{
    [System.Serializable]
    public class ComponentBindTracks
    {
        public string tweenName;
        public List<string> trackTypeNames = new();
        public List<string> bindTargets = new();
        public List<string> bindTypes = new();
    }
}
