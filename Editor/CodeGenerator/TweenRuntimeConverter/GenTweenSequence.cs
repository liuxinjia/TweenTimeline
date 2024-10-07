using System.Collections.Generic;
namespace Cr7Sund.TweenTimeLine
{
    public class GenTweenSequence
    {
        public string parentTrackName;
        public List<GenTrackInfo> trackInfos;
        public Dictionary<GenClipInfo, string> startDynamicParams = new();
        public Dictionary<GenClipInfo, string> endDynamicParams = new();
    }
}
