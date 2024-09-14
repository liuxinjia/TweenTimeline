using System.Collections.Generic;
using UnityEditor;

namespace Cr7Sund.TweenTimeLine
{
    public static class TweenConfigCacher
    {
        public static readonly Dictionary<string, TweenComponentData> tweenGenInfoCaches = new Dictionary<string, TweenComponentData>();

        public static void CacheTweenConfigs()
        {
            string[] searchPaths = { TweenTimelineDefine.BuiltInConfigPath, TweenTimelineDefine.CustomConfigPath };
            string[] guids = AssetDatabase.FindAssets("t:TweenGenTrackConfig", searchPaths);

            tweenGenInfoCaches.Clear();

            foreach (string guid in guids)
            {
                AddToCache(AssetDatabase.GUIDToAssetPath(guid));
            }
        }

        private static void AddToCache(string configPath)
        {
            var tweenConfig = AssetDatabase.LoadAssetAtPath<TweenGenTrackConfig>(configPath);
            if (tweenConfig == null) return;

            foreach (var item in tweenConfig.componentDatas)
            {
                string key = item.GetPropertyMethod.ToUpper();
                tweenGenInfoCaches[key] = item;
            }
        }

    }
}
