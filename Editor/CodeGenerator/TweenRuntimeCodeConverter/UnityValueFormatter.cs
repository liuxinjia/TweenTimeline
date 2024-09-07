namespace Cr7Sund.TweenTimeLine
{
    public static class UnityValueFormatter
    {
        public static string FormatValue(object obj)
        {
            var type = obj.GetType().Name;
            var value = obj.ToString();
            switch (type)
            {
                case "Vector2":
                    return FormatVector2(value);
                case "Vector3":
                    return FormatVector3(value);
                case "Quaternion":
                    return FormatQuaternion(value);
                case "Color":
                    return FormatColor(value);
                case "float":
                case "Single":
                case "Float":
                    return $"{value}f";
                default:
                    return value;
            }
        }

        private static string FormatVector2(string value)
        {
            // Expected format: "x, y"
            var parts = value.Trim('(', ')').Split(',');
            if (parts.Length == 2)
            {
                return $"new Vector2({parts[0].Trim()}f, {parts[1].Trim()}f)";
            }
            return value; // Return the original value if format is not as expected
        }

        private static string FormatVector3(string value)
        {
            // Expected format: "x, y, z"
            var parts = value.Trim('(', ')').Split(',');
            if (parts.Length == 3)
            {
                return $"new Vector3({parts[0].Trim()}f, {parts[1].Trim()}f, {parts[2].Trim()}f)";
            }
            return value; // Return the original value if format is not as expected
        }

        private static string FormatQuaternion(string value)
        {
            // Expected format: "x, y, z, w"
            var parts = value.Trim('(', ')').Split(',');
            if (parts.Length == 4)
            {
                return $"new Quaternion({parts[0].Trim()}f, {parts[1].Trim()}f, {parts[2].Trim()}f, {parts[3].Trim()}f)";
            }
            return value; // Return the original value if format is not as expected
        }

        private static string FormatColor(string value)
        {
            // Expected format: "r, g, b, a"
            var parts = value.Trim('(', ')').Split(',');
            if (parts.Length == 4)
            {
                return $"new Color({parts[0].Trim()}f, {parts[1].Trim()}f, {parts[2].Trim()}f, {parts[3].Trim()}f)";
            }
            return value; // Return the original value if format is not as expected
        }
    }
}
