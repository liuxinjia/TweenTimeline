using System.Collections.Generic;

namespace Cr7Sund.TweenTimeLine
{
    [System.Serializable]
    public class ComponentTween
    {
        public string category;
        public List<ComponentBindTracks> tweenNames = new();
    }
}
