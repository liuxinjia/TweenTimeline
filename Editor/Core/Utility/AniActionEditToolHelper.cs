using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace Cr7Sund.TweenTimeLine
{
    public class AniActionEditToolHelper
    {

        public static Type GetFirstGenericType(Type type)
        {
            Type genericBaseType = type.BaseType; // Get the base type
            if (genericBaseType.IsGenericType)
            {
                Type[] genericArguments = genericBaseType.GetGenericArguments();
                if (genericArguments.Length > 0)
                {
                    Type firstGenericArgument = genericArguments[0];
                    return firstGenericArgument;
                }
                else
                {
                    throw new Exception("No generic arguments found.");
                }
            }
            else
            {
                throw new Exception("The base type is not a generic type.");
            }
        }
        public static Type GetSecondGenericType(Type type)
        {
            Type genericBaseType = type.BaseType; // Get the base type
            if (genericBaseType.IsGenericType)
            {
                Type[] genericArguments = genericBaseType.GetGenericArguments();
                if (genericArguments.Length > 1)
                {
                    Type firstGenericArgument = genericArguments[1];
                    return firstGenericArgument;
                }
                else
                {
                    throw new Exception("No generic arguments found.");
                }
            }
            else
            {
                throw new Exception("The base type is not a generic type.");
            }
        }
        public static void ChangeValue(VisualElement visualElement, Type type, string value)
        {
            // Convert the value back to the original type
            var objectValue = TypeConverter.ConvertToOriginalType(value, type);

            if (type == typeof(Vector3))
            {
                var element = visualElement as Vector3Field;
                if (element != null)
                {
                    element.value = (Vector3)objectValue;
                }
            }
            else if (type == typeof(int))
            {
                var element = visualElement as IntegerField;
                if (element != null)
                {
                    element.value = (int)objectValue;
                }
            }
            else if (type == typeof(float))
            {
                var element = visualElement as FloatField;
                if (element != null)
                {
                    element.value = (float)objectValue;
                }
            }
            else if (type == typeof(string))
            {
                var element = visualElement as TextField;
                if (element != null)
                {
                    element.value = (string)objectValue;
                }
            }
            else if (type == typeof(bool))
            {
                var element = visualElement as Toggle;
                if (element != null)
                {
                    element.value = (bool)objectValue;
                }
            }
            else if (type == typeof(Color))
            {
                var element = visualElement as ColorField;
                if (element != null)
                {
                    element.value = (Color)objectValue;
                }
            }
            else if (type == typeof(Vector2))
            {
                var element = visualElement as Vector2Field;
                if (element != null)
                {
                    element.value = (Vector2)objectValue;
                }
            }
            else if (type == typeof(Vector4))
            {
                var element = visualElement as Vector4Field;
                if (element != null)
                {
                    element.value = (Vector4)objectValue;
                }
            }
            else if (type.IsEnum)
            {
                var element = visualElement as EnumField;
                if (element != null)
                {
                    element.value = (Enum)objectValue;
                }
            }
        }
        public static VisualElement CreateValueField(string label, Type type, string value, Action<string> onValueChange)
        {
            // Convert the value back to the original type
            var objectValue = TypeConverter.ConvertToOriginalType(value, type);

            if (objectValue == null || objectValue.GetType() != type)
            {
                Debug.LogWarning($"{objectValue} is not type {type}");
                objectValue = Activator.CreateInstance(type);
            }
            if (type == typeof(Vector3))
            {
                var element = new Vector3Field();
                element.value = (Vector3)objectValue;
                element.label = label; // 设置 label
                element.RegisterValueChangedCallback(evt =>
                {
                    var newValue = evt.newValue.ToString();
                    onValueChange?.Invoke(newValue);
                });
                return element;
            }
            else if (type == typeof(int))
            {
                var element = new IntegerField();
                element.value = (int)objectValue;
                element.label = label; // 设置 label
                element.RegisterValueChangedCallback(evt =>
                {
                    var newValue = evt.newValue.ToString();
                    onValueChange?.Invoke(newValue);
                });
                return element;
            }
            else if (type == typeof(float))
            {
                var element = new FloatField();
                element.value = (float)objectValue;
                element.label = label; // 设置 label
                element.RegisterValueChangedCallback(evt =>
                {
                    var newValue = evt.newValue.ToString();
                    onValueChange?.Invoke(newValue);
                });
                return element;
            }
            else if (type == typeof(string))
            {
                var element = new TextField();
                element.value = (string)objectValue;
                element.label = label; // 设置 label
                element.RegisterValueChangedCallback(evt =>
                {
                    var newValue = evt.newValue;
                    onValueChange?.Invoke(newValue);
                });
                return element;
            }
            else if (type == typeof(bool))
            {
                var element = new Toggle();
                element.value = (bool)objectValue;
                element.label = label; // 设置 label
                element.RegisterValueChangedCallback(evt =>
                {
                    var newValue = evt.newValue.ToString();
                    onValueChange?.Invoke(newValue);
                });
                return element;
            }
            else if (type == typeof(Color))
            {
                var element = new ColorField();
                element.value = (Color)objectValue;
                element.label = label; // 设置 label
                element.RegisterValueChangedCallback(evt =>
                {
                    var newValue = evt.newValue.ToString();
                    onValueChange?.Invoke(newValue);
                });
                return element;
            }
            else if (type == typeof(Vector2))
            {
                var element = new Vector2Field();
                element.value = (Vector2)objectValue;
                element.label = label; // 设置 label
                element.RegisterValueChangedCallback(evt =>
                {
                    var newValue = evt.newValue.ToString();
                    onValueChange?.Invoke(newValue);
                });
                return element;
            }
            else if (type == typeof(Vector2Int))
            {
                var element = new Vector2IntField();
                element.value = (Vector2Int)objectValue;
                element.label = label; // 设置 label
                element.RegisterValueChangedCallback(evt =>
                {
                    var newValue = evt.newValue.ToString();
                    onValueChange?.Invoke(newValue);
                });
                return element;
            }
            else if (type == typeof(Vector3))
            {
                var element = new Vector3Field();
                element.value = (Vector3)objectValue;
                element.label = label; // 设置 label
                element.RegisterValueChangedCallback(evt =>
                {
                    var newValue = evt.newValue.ToString();
                    onValueChange?.Invoke(newValue);
                });
                return element;
            }
            else if (type == typeof(Quaternion))
            {
                var element = new Vector3Field();
                Quaternion euler = default;
                if (objectValue is Vector3 vector3)
                {
                    euler = Quaternion.Euler(vector3);
                }
                element.value = euler.eulerAngles;
                element.label = label; // 设置 label
                element.RegisterValueChangedCallback(evt =>
                {
                    onValueChange?.Invoke(Quaternion.Euler(evt.newValue).ToString());
                });
                return element;
            }
            else if (type == typeof(Vector4))
            {
                var element = new Vector4Field();
                element.value = (Vector4)objectValue;
                element.label = label; // 设置 label
                element.RegisterValueChangedCallback(evt =>
                {
                    var newValue = evt.newValue.ToString();
                    onValueChange?.Invoke(newValue);
                });
                return element;
            }
            else if (type.IsEnum)
            {
                var element = new EnumField((Enum)objectValue);
                element.label = label; // 设置 label
                element.RegisterValueChangedCallback(evt =>
                {
                    var newValue = evt.newValue.ToString();
                    onValueChange?.Invoke(newValue);
                });
                return element;
            }

            throw new InvalidOperationException($"can not create filedDrawer{type} , {label}");
        }
        public static float ConvertDuration(float duration)
        {
            return Mathf.Abs(duration / 1000f);
        }
        public static Texture2D LoadImageFromPath(string imagePath)
        {
            var texture2D = AssetDatabase.LoadAssetAtPath<Texture2D>(imagePath);
            if (texture2D == null)
            {
                texture2D = UnityEditor.EditorGUIUtility.IconContent("BuildSettings.Switch On@2x").image as Texture2D;
            }
            return texture2D;
        }
    }
}
