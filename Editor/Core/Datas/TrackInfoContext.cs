using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public class TrackInfoContext
    {
        public Component component;
        public Type trackType;

        public Type BindType;
        public string BindTargetName;
        public List<ClipInfoContext> clipInfos;
        internal Transform parent;
        internal string tweenIdentifier;

        public TrackInfoContext()
        {
            clipInfos = new List<ClipInfoContext>();
        }

    }
}
