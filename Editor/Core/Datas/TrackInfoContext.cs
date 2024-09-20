using System.Collections.Generic;

namespace Cr7Sund.TweenTimeLine
{
    public class TrackInfoContext
    {
       public List<ClipInfoContext> clipInfos;

        public TrackInfoContext()
        {
            clipInfos = new List<ClipInfoContext>();
        }
    }

    public class ClipInfoContext
    {
        public double start;
        public double duration;

        public BaseEasingTokenPreset easePreset;
        public object startPos;
        public object endPos;
    }
}
