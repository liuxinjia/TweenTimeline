/// @creator: Slipp Douglas Thompson
/// @license: Public Domain per The Unlicense.  See <http://unlicense.org/>.
/// @purpose: Genericized Unity3D SerializedProperty value access.
/// @why: Because this functionality should be built-into Unity.
/// @usage: Use as you would a native SerializedProperty method;
/// 	e.g. `Debug.Log(mySerializedProperty.Value<Color>());`
/// @intended project path: Assets/Plugins/Editor/UnityEditor Extensions/SerializedPropertyValueExtension.cs
/// @interwebsouce: https://gist.github.com/capnslipp/8516384

using System;
using System.Reflection;
using System.Collections.Generic; // for: KeyValuePair<,>
using UnityEngine;
using UnityEditor;


namespace Cr7Sund.TweenTimeLine
{
    public static class SerializedPropertyValueExtension
    {
        /// @note: switch/case derived from the decompilation of SerializedProperty's internal SetToValueOfTarget() method.
        public static ValueT Value<ValueT>(this SerializedProperty thisSP)
        {
            Type valueType = typeof(ValueT);

            // First, do special Type checks
            if (valueType.IsEnum)
                return (ValueT)Enum.ToObject(valueType, thisSP.enumValueIndex);

            // Next, check for literal UnityEngine struct-types
            // @note: ->object->ValueT double-casts because C# is too dumb to realize that that the ValueT in each situation is the exact type needed.
            // 	e.g. `return thisSP.colorValue` spits _error CS0029: Cannot implicitly convert type `UnityEngine.Color' to `ValueT'_
            // 	and `return (ValueT)thisSP.colorValue;` spits _error CS0030: Cannot convert type `UnityEngine.Color' to `ValueT'_
            if (typeof(Color).IsAssignableFrom(valueType))
                return (ValueT)(object)thisSP.colorValue;
            else if (typeof(LayerMask).IsAssignableFrom(valueType))
                return (ValueT)(object)thisSP.intValue;
            else if (typeof(Vector2).IsAssignableFrom(valueType))
                return (ValueT)(object)thisSP.vector2Value;
            else if (typeof(Vector3).IsAssignableFrom(valueType))
                return (ValueT)(object)thisSP.vector3Value;
            else if (typeof(Rect).IsAssignableFrom(valueType))
                return (ValueT)(object)thisSP.rectValue;
            else if (typeof(AnimationCurve).IsAssignableFrom(valueType))
                return (ValueT)(object)thisSP.animationCurveValue;
            else if (typeof(Bounds).IsAssignableFrom(valueType))
                return (ValueT)(object)thisSP.boundsValue;
            else if (typeof(Gradient).IsAssignableFrom(valueType))
                return (ValueT)(object)SafeGradientValue(thisSP);
            else if (typeof(Quaternion).IsAssignableFrom(valueType))
                return (ValueT)(object)thisSP.quaternionValue;

            // Next, check if derived from UnityEngine.Object base class
            if (typeof(UnityEngine.Object).IsAssignableFrom(valueType))
                return (ValueT)(object)thisSP.objectReferenceValue;

            // Finally, check for native type-families
            if (typeof(int).IsAssignableFrom(valueType))
                return (ValueT)(object)thisSP.intValue;
            else if (typeof(bool).IsAssignableFrom(valueType))
                return (ValueT)(object)thisSP.boolValue;
            else if (typeof(float).IsAssignableFrom(valueType))
                return (ValueT)(object)thisSP.floatValue;
            else if (typeof(string).IsAssignableFrom(valueType))
                return (ValueT)(object)thisSP.stringValue;
            else if (typeof(char).IsAssignableFrom(valueType))
                return (ValueT)(object)thisSP.intValue;

            // And if all fails, throw an exception.
            throw new NotImplementedException("Unimplemented propertyType " + thisSP.propertyType + ".");
        }

        public static dynamic Value(this SerializedProperty thisSP)
        {
            switch (thisSP.propertyType)
            {
                case SerializedPropertyType.Integer:
                    return thisSP.intValue;
                case SerializedPropertyType.Boolean:
                    return thisSP.boolValue;
                case SerializedPropertyType.Float:
                    return thisSP.floatValue;
                case SerializedPropertyType.String:
                    return thisSP.stringValue;
                case SerializedPropertyType.Color:
                    return thisSP.colorValue;
                case SerializedPropertyType.ObjectReference:
                    return thisSP.objectReferenceValue;
                case SerializedPropertyType.LayerMask:
                    return thisSP.intValue;
                case SerializedPropertyType.Enum:
                    int enumI = thisSP.enumValueIndex;
                    return new KeyValuePair<int, string>(enumI, thisSP.enumNames[enumI]);
                case SerializedPropertyType.Vector2:
                    return thisSP.vector2Value;
                case SerializedPropertyType.Vector3:
                    return thisSP.vector3Value;
                case SerializedPropertyType.Rect:
                    return thisSP.rectValue;
                case SerializedPropertyType.ArraySize:
                    return thisSP.intValue;
                case SerializedPropertyType.Character:
                    return (char)thisSP.intValue;
                case SerializedPropertyType.AnimationCurve:
                    return thisSP.animationCurveValue;
                case SerializedPropertyType.Bounds:
                    return thisSP.boundsValue;
                case SerializedPropertyType.Gradient:
                    return SafeGradientValue(thisSP);
                case SerializedPropertyType.Quaternion:
                    return thisSP.quaternionValue;

                default:
                    throw new NotImplementedException("Unimplemented propertyType " + thisSP.propertyType + ".");
            }
        }

        /// Access to SerializedProperty's internal gradientValue property getter, in a manner that'll only soft break (returning null) if the property changes or disappears in future Unity revs.
        static Gradient SafeGradientValue(SerializedProperty sp)
        {
            BindingFlags instanceAnyPrivacyBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            PropertyInfo propertyInfo = typeof(SerializedProperty).GetProperty(
                "gradientValue",
                instanceAnyPrivacyBindingFlags,
                null,
                typeof(Gradient),
                new Type[0],
                null
            );
            if (propertyInfo == null)
                return null;

            Gradient gradientValue = propertyInfo.GetValue(sp, null) as Gradient;
            return gradientValue;
        }

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
                    return typeof(Enum); // 或者根据需要返回具体的 Enum 类型
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
                    return null; // 或者抛出异常
            }
        }
    }
}