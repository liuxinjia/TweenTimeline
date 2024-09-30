using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public class BindUtility
    {
        public static Transform GetAttachRoot(Transform bindObj)
        {
            HashSet<string> validTags = new HashSet<string> { TweenTimelineDefine.PanelTag, TweenTimelineDefine.CompositeTag };

            Transform current = bindObj;
            while (current != null)
            {
                if (validTags.Contains(current.tag))
                {
                    return current;
                }
                current = current.parent;
            }

            return null;
        }
    }
}