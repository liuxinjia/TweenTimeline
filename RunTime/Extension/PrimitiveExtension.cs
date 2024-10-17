using UnityEngine;

namespace Cr7Sund
{
    public static class PrimitiveExtension
    {
        public static Color WithAlpha(this Color c, float alpha)
        {
            c.a = alpha;
            return c;
        }

        public static Vector2 WithComponent(this Vector2 v, int index, float val)
        {
            v[index] = val;
            return v;
        }

        public static Vector3 WithComponent(this Vector3 v, int index, float val)
        {
            v[index] = val;
            return v;
        }
    }
}