using System.Collections.Generic;
namespace Cr7Sund.TweenTimeLine
{
    public class GenTrackInfo
    {
        public List<GenClipInfo> clipInfos = new();
        public string InstanceID;

        public GenTrackInfo(int instanceID)
        {
            this.InstanceID = instanceID.ToString();
        }
        public GenTrackInfo(string instanceID)
        {
            this.InstanceID = instanceID.ToString();
        }
    }
}
