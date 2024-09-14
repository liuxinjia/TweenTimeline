using System;
using System.Collections.Generic;
using UnityEngine;
namespace Cr7Sund.TweenTimeLine
{
    [Serializable]
    public struct CurveWrapper
    {
        public AnimationCurve Curve;
        public string name;
    }

    [Serializable]
    public class CurveWrapperLibrary : ScriptableObject
    {
        public List<CurveWrapper> Curves;
        public Dictionary<string, CurveWrapper> curveDictionary;

        public void GenCurveInfoDict()
        {
            curveDictionary = new();
            foreach (var item in Curves)
            {
                curveDictionary.Add(item.name, item);
            }
        }
    }
}
