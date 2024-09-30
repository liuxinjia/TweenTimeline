using System;
using System.Collections.Generic;
using System.Linq;

namespace Cr7Sund.TweenTimeLine
{
    public class ClipInfoContext
    {
        public List<MarkInfoContext> markInfos = new();
        public double start;
        public double duration;

        public BaseEasingTokenPreset easePreset;
        public object startPos;
        public object endPos;

        internal string GetDisplayName(Type type)
        {
            // RectTransform_PivotControlAsset
            return type.Name.Split('_').LastOrDefault().Replace("ControlAsset", "");
        }
    }
}
