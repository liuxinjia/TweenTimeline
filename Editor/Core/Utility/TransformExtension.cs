using UnityEngine;
using System.Collections.Generic;
using System.Text;
namespace Cr7Sund.TweenTimeLine
{
    public static class TransformExtensions
    {
        public static string GetFullPathTrans(this Transform child, Transform root)
        {
            if (!TweenTimelinePreferencesProvider.GetBool(TweenGenSettings.UseFullPathName))
            {
                return child.gameObject.name;
            }

            // 使用 StringBuilder 来高效构建路径字符串
            StringBuilder pathBuilder = new StringBuilder();
            List<string> pathParts = new List<string>();

            Transform current = child;
            while (current != null && current != root)
            {
                pathParts.Add(current.gameObject.name);
                current = current.parent;
            }

            // 如果没有找到父节点，记录错误并返回子节点名称
            if (current != root)
            {
                Debug.LogError($"Parent {root.name} is not an ancestor of child {child.name}");
                return child.gameObject.name;
            }

            // 反转路径部分并构建完整路径
            for (int i = pathParts.Count - 1; i >= 0; i--)
            {
                pathBuilder.Append(pathParts[i]);
                if (i > 0) pathBuilder.Append('/');
            }

            return pathBuilder.ToString();
        }
        public static Transform FindChildByName(this Transform parent, string childName)
        {
            if(childName == parent.name){
                return parent;
            }
            // 首先检查直接子节点
            Transform directChild = parent.Find(childName);
            if (directChild != null)
            {
                return directChild;
            }

            // 如果直接子节点中没有找到，递归搜索所有子节点
            foreach (Transform child in parent)
            {
                Transform found = FindChildByName(child, childName);
                if (found != null)
                {
                    return found;
                }
            }

            // 如果没有找到，返回null
            return null;
        }

    }

}