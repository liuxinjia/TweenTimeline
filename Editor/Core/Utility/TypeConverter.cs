using System;
using UnityEngine;

public static class TypeConverter
{
    public static object ConvertToOriginalType(string storedString, Type targetType)
    {
        // Return default values if the string is null or empty
        if (string.IsNullOrEmpty(storedString))
        {
            if (targetType == typeof(int)) return default(int);
            if (targetType == typeof(float)) return default(float);
            if (targetType == typeof(double)) return default(double);
            if (targetType == typeof(bool)) return default(bool);
            if (targetType == typeof(string)) return string.Empty;
            if (targetType == typeof(Vector2)) return default(Vector2);
            if (targetType == typeof(Vector2Int)) return default(Vector2Int);
            if (targetType == typeof(Vector3)) return default(Vector3);
            if (targetType == typeof(Vector4)) return default(Vector4);
            if (targetType == typeof(Quaternion)) return default(Quaternion);
            if (targetType == typeof(Color)) return default(Color);
        }

        // Continue with parsing if the string is not null or empty
        if (targetType == typeof(int))
        {
            if (int.TryParse(storedString, out int intValue))
            {
                return intValue;
            }
        }
        else if (targetType == typeof(float))
        {
            storedString = storedString.TrimEnd('f', 'F');
            if (float.TryParse(storedString, out float floatValue))
            {
                return floatValue;
            }
        }
        else if (targetType == typeof(double))
        {
            storedString = storedString.TrimEnd('d', 'D');
            if (double.TryParse(storedString, out double doubleValue))
            {
                return doubleValue;
            }
        }
        else if (targetType == typeof(bool))
        {
            if (bool.TryParse(storedString, out bool boolValue))
            {
                return boolValue;
            }
        }
        else if (targetType == typeof(string))
        {
            return storedString;
        }
        else if (targetType == typeof(Vector2))
        {
            var components = storedString.Trim('(', ')').Split(',');
            if (components.Length == 2 &&
                float.TryParse(components[0], out float x) &&
                float.TryParse(components[1], out float y))
            {
                return new Vector2(x, y);
            }
            if (components.Length == 3)
            {
                if (float.TryParse(components[0], out float vx) &&
                    float.TryParse(components[1], out float vy) &&
                    float.TryParse(components[2], out float vz))
                {
                    return new Vector2(vx, vy);
                }
            }
        }
        else if (targetType == typeof(Vector2Int))
        {
            var components = storedString.Trim('(', ')').Split(',');
            if (components.Length == 2 &&
                int.TryParse(components[0], out int x) &&
                int.TryParse(components[1], out int y))
            {
                return new Vector2Int(x, y);
            }
        }
        else if (targetType == typeof(Vector3))
        {
            if (storedString == "0")
            {
                return Vector2.zero;
            }
            var components = storedString.Trim('(', ')').Split(',');
            if (components.Length == 2)
            {
                if (float.TryParse(components[0], out float x) &&
                    float.TryParse(components[1], out float y))
                {
                    return new Vector3(x, y, 0);
                }
            }
            if (components.Length == 3)
            {
                if (float.TryParse(components[0], out float x) &&
                    float.TryParse(components[1], out float y) &&
                    float.TryParse(components[2], out float z))
                {
                    return new Vector3(x, y, z);
                }
            }
        }
        else if (targetType == typeof(Quaternion))
        {
            var components = storedString.Trim('(', ')').Split(',');
            if (components.Length == 3)
            {
                if (float.TryParse(components[0], out float dx) &&
                    float.TryParse(components[1], out float dy) &&
                    float.TryParse(components[2], out float dz))
                    return new Vector3(dx, dy, dz);
            }
            if (components.Length == 4)
                if (float.TryParse(components[0], out float dx) &&
                    float.TryParse(components[1], out float dy) &&
                    float.TryParse(components[2], out float dz) &&
                    float.TryParse(components[3], out float dw))
                {
                    return new Quaternion(dx, dy, dz, dw);
                }
        }
        else if (targetType == typeof(Vector4))
        {
            var components = storedString.Trim('(', ')').Split(',');
            if (components.Length == 4 &&
                float.TryParse(components[0], out float x) &&
                float.TryParse(components[1], out float y) &&
                float.TryParse(components[2], out float z) &&
                float.TryParse(components[3], out float w))
            {
                return new Vector4(x, y, z, w);
            }
        }
        else if (targetType == typeof(Color))
        {
            var storedStringTrimmed = storedString.StartsWith("RGBA")
                ? storedString.Substring(4).Trim()
                : storedString;

            var components = storedStringTrimmed.Trim('(', ')').Split(',');

            if (components.Length == 4 &&
                float.TryParse(components[0], out float r) &&
                float.TryParse(components[1], out float g) &&
                float.TryParse(components[2], out float b) &&
                float.TryParse(components[3], out float a))
            {
                return new Color(r, g, b, a);
            }
        }

        Debug.LogWarning($"Cannot convert '{storedString}' to type '{targetType.Name}'.");

        return Activator.CreateInstance(targetType);
    }

    public static object AddDelta(object startValue, string deltaString, Type targetType)
    {
        // If deltaString is null or empty, return the startValue
        if (string.IsNullOrEmpty(deltaString))
        {
            return startValue;
        }

        if (targetType == typeof(int))
        {
            if (startValue is int intValue && int.TryParse(deltaString, out int deltaValue))
            {
                return intValue + deltaValue;
            }
        }
        else if (targetType == typeof(float))
        {
            deltaString = deltaString.TrimEnd('f', 'F');
            if (startValue is float floatValue && float.TryParse(deltaString, out float deltaValue))
            {
                return floatValue + deltaValue;
            }
        }
        else if (targetType == typeof(double))
        {
            deltaString = deltaString.TrimEnd('d', 'D');
            if (startValue is double doubleValue && double.TryParse(deltaString, out double deltaValue))
            {
                return doubleValue + deltaValue;
            }
        }
        else if (targetType == typeof(Vector2))
        {
            if (startValue is Vector2 vector2Value)
            {
                var deltaComponents = deltaString.Trim('(', ')').Split(',');
                if (deltaComponents.Length == 2)
                {
                    if (float.TryParse(deltaComponents[0], out float dx) &&
                    float.TryParse(deltaComponents[1], out float dy))
                    {
                        return new Vector2(vector2Value.x + dx, vector2Value.y + dy);
                    }
                }

                if (deltaComponents.Length == 3)
                {
                    if (float.TryParse(deltaComponents[0], out float dx) &&
                        float.TryParse(deltaComponents[1], out float dy) &&
                        float.TryParse(deltaComponents[2], out float dz))
                    {
                        return new Vector2(vector2Value.x + dx, vector2Value.y + dy);
                    }
                }
            }
        }
        else if (targetType == typeof(Vector2Int))
        {
            if (startValue is Vector2Int vector2Value)
            {
                var deltaComponents = deltaString.Trim('(', ')').Split(',');
                if (deltaComponents.Length == 2)
                {
                    if (int.TryParse(deltaComponents[0], out int dx) &&
                                       int.TryParse(deltaComponents[1], out int dy))
                    {
                        return new Vector2Int(vector2Value.x + dx, vector2Value.y + dy);
                    }
                }
                if (deltaComponents.Length == 3)
                {
                    if (float.TryParse(deltaComponents[0], out float dx) &&
                        float.TryParse(deltaComponents[1], out float dy) &&
                        float.TryParse(deltaComponents[2], out float dz))
                    {
                        return new Vector2(vector2Value.x + dx, vector2Value.y + dy);
                    }
                }
            }
        }
        else if (targetType == typeof(Vector3))
        {
            if (startValue is Vector3 vector3Value)
            {
                if (deltaString == "0")
                {
                    return Vector2.zero;
                }
                var deltaComponents = deltaString.Trim('(', ')').Split(',');
                if (deltaComponents.Length == 2)
                {
                    if (float.TryParse(deltaComponents[0], out float dx) &&
                        float.TryParse(deltaComponents[1], out float dy))
                    {
                        return new Vector3(vector3Value.x + dx, vector3Value.y + dy, vector3Value.z);
                    }
                }
                if (deltaComponents.Length == 3)
                {
                    if (float.TryParse(deltaComponents[0], out float dx) &&
                        float.TryParse(deltaComponents[1], out float dy) &&
                        float.TryParse(deltaComponents[2], out float dz))
                    {
                        return new Vector3(vector3Value.x + dx, vector3Value.y + dy, vector3Value.z + dz);
                    }
                }
            }
        }
        else if (targetType == typeof(Quaternion))
        {
            if (startValue is Quaternion quaternionValue)
            {
                var deltaComponents = deltaString.Trim('(', ')').Split(',');
                if (deltaComponents.Length == 3)
                {
                    if (float.TryParse(deltaComponents[0], out float dx) &&
                        float.TryParse(deltaComponents[1], out float dy) &&
                        float.TryParse(deltaComponents[2], out float dz))
                        return quaternionValue * Quaternion.Euler(new Vector3(dx, dy, dz));
                }
                if (deltaComponents.Length == 4)
                {
                    if (float.TryParse(deltaComponents[0], out float dx) &&
                        float.TryParse(deltaComponents[1], out float dy) &&
                        float.TryParse(deltaComponents[2], out float dz) &&
                        float.TryParse(deltaComponents[3], out float dw))
                    {
                        var rotation = new Quaternion(dx, dy, dz, dw);
                        // var rotation = Quaternion.Euler(new Vector3(dx, dy, dz));
                        return rotation * quaternionValue;
                    }
                }
            }
        }
        else if (targetType == typeof(Vector4))
        {
            if (startValue is Vector4 vector4Value)
            {
                var deltaComponents = deltaString.Trim('(', ')').Split(',');
                if (deltaComponents.Length == 4 &&
                    float.TryParse(deltaComponents[0], out float dx) &&
                    float.TryParse(deltaComponents[1], out float dy) &&
                    float.TryParse(deltaComponents[2], out float dz) &&
                    float.TryParse(deltaComponents[3], out float dw))
                {
                    return new Vector4(vector4Value.x + dx, vector4Value.y + dy,
                        vector4Value.z + dz, vector4Value.w + dw);
                }
            }
        }
        else if (targetType == typeof(Color))
        {
            if (startValue is Color colorValue)
            {
                var deltaComponents = deltaString.Trim('(', ')').Split(',');
                if (deltaComponents.Length == 4 &&
                    float.TryParse(deltaComponents[0], out float dr) &&
                    float.TryParse(deltaComponents[1], out float dg) &&
                    float.TryParse(deltaComponents[2], out float db) &&
                    float.TryParse(deltaComponents[3], out float da))
                {
                    return new Color(
                        Mathf.Clamp01(colorValue.r + dr),
                        Mathf.Clamp01(colorValue.g + dg),
                        Mathf.Clamp01(colorValue.b + db),
                        Mathf.Clamp01(colorValue.a + da)
                    );
                }
            }
        }
        else if (targetType == typeof(bool))
        {
            throw new InvalidOperationException("Cannot add delta to boolean values.");
        }
        else if (targetType == typeof(string))
        {
            throw new InvalidOperationException("Cannot add delta to string values.");
        }

        throw new InvalidOperationException($"Cannot add delta to type '{targetType.Name}'.");
    }


    public static string GetSimplifyTypeName(string fullType)
    {
        return fullType.Substring(fullType.LastIndexOf('.') + 1);
    }
}
