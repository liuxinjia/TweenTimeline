using System;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public class BindUtility
    {
        public static Transform GetAttachRoot(Transform bindObj)
        {
            GameObject[] panels = GameObject.FindGameObjectsWithTag("Panel");
            if (panels.Length < 0)
            {
                throw new Exception("Please assign the panel tag with Panel");
            }

            foreach (var panel in panels)
            {
                if (bindObj.IsChildOf(panel.transform))
                {
                    return panel.transform;
                }
            }

            throw new Exception($"Please attach the transfrom {bindObj} with Panel");
        }
    }
}