using UnityEngine;
using Object = UnityEngine.Object;
using UnityEditor;

namespace Cr7Sund.TweenTimeLine
{
    public static class AddressableLoader
    {
        public static T Load<T>(string name) where T : Object
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name} name");
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                return AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }
            return Resources.Load<T>(name);
        }
    }
}
