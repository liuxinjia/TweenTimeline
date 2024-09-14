using System;
using UnityEngine;
using UnityEditor;


namespace Cr7Sund.TweenTimeLine
{
    public static class SerializedPropertyValueExtension
    {
        public static Type GetRealType(SerializedPropertyType propertyType)
        {
            switch (propertyType)
            {
                case SerializedPropertyType.Integer:
                    return typeof(int);
                case SerializedPropertyType.Boolean:
                    return typeof(bool);
                case SerializedPropertyType.Float:
                    return typeof(float);
                case SerializedPropertyType.String:
                    return typeof(string);
                case SerializedPropertyType.Color:
                    return typeof(Color);
                case SerializedPropertyType.ObjectReference:
                    return typeof(UnityEngine.Object);
                case SerializedPropertyType.LayerMask:
                    return typeof(int);
                case SerializedPropertyType.Enum:
                    return typeof(Enum); 
                case SerializedPropertyType.Vector2:
                    return typeof(Vector2);
                case SerializedPropertyType.Vector3:
                    return typeof(Vector3);
                case SerializedPropertyType.Rect:
                    return typeof(Rect);
                case SerializedPropertyType.AnimationCurve:
                    return typeof(AnimationCurve);
                case SerializedPropertyType.Bounds:
                    return typeof(Bounds);
                case SerializedPropertyType.Gradient:
                    return typeof(Gradient);
                case SerializedPropertyType.Quaternion:
                    return typeof(Quaternion);
                default:
                    return null; 
            }
        }
    }
}