#if UNITY_EDITOR

using System.Reflection;
using PrimeTween;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public class PrimeTweenManagerExposer
    {
        const int defaultInitialCapacity = 200;

        public static void Init()
        {
            if (PrimeTweenManager.Instance == null)
            {
                var go = GameObject.Find("PrimeTweenManager");
                PrimeTweenManager instance = null;
                if (go == null)
                {
                    go = new GameObject(nameof(PrimeTweenManager));
                    instance = go.AddComponent<PrimeTweenManager>();
                }
                instance = go.GetComponent<PrimeTweenManager>();
                if (instance.pool == null)
                {
                    var initMethod = typeof(PrimeTweenManager).GetMethod("init", BindingFlags.Instance | BindingFlags.NonPublic);
                    initMethod.Invoke(instance, new object[]
                    {
                             200
                    });
                }

                PrimeTweenManager.Instance = instance;
            }
        }

        public static void Destroy()
        {
            var go = GameObject.Find("PrimeTweenManager");
            if (go)
                GameObject.DestroyImmediate(go);
            if (PrimeTweenManager.Instance == null)
            {
                PrimeTweenManager.Instance = null;
            }

        }
    }
}
#endif