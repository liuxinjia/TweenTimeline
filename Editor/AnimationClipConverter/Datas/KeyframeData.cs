﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyframeData
{
    public float Time;
    public float Value;
    public float InTangent;
    public float OutTangent;
    public string Property;
    public float InWeight;
    public float OutWeight;
    public string Path;
    public SerializableType Type;
    public string ObjectKeyPair;


    public class KeyframeDataTimeComparer : IComparer<KeyframeData>
    {
        public int Compare(KeyframeData x, KeyframeData y)
        {
            return x.Time.CompareTo(y.Time);
        }
    }

    public static implicit operator Keyframe(KeyframeData keyframeData)
    {
        var keyframe = new Keyframe(
            keyframeData.Time,
            keyframeData.Value,
            keyframeData.InTangent,
            keyframeData.OutTangent,
            keyframeData.InWeight,
            keyframeData.OutWeight
        );
        return keyframe;
    }
}
