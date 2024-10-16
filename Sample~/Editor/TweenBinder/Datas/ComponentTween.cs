using System.Collections.Generic;

namespace Cr7Sund.TweenTimeLine
{
    [System.Serializable]
    public class ComponentTween
    {
        public string category;
        public List<ComponentBindTracks> tweenNames = new();

        public override bool Equals(object obj)
        {
            if (obj is ComponentTween other)
            {
                if (other.category != this.category)
                {
                    if (!TweenTimelineDefine.UIComponentTypeMatch.ContainsKey(other.category)
                    || !TweenTimelineDefine.UIComponentTypeMatch.ContainsKey(this.category))
                    {
                        return false;
                    }
                    return TweenTimelineDefine.UIComponentTypeMatch[other.category] ==
                        TweenTimelineDefine.UIComponentTypeMatch[this.category];
                }
                return other.category == this.category
                 ;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
