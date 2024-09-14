using UnityEngine;
using UnityEditor;
using System.IO;

namespace Cr7Sund.TweenTimeLine
{
    public static class PathUtility
    {
        public static string ConvertToAbsolutePath(string relativePath)
        {
            // 确保路径使用正斜杠
            relativePath = relativePath.Replace('\\', '/');

            // 如果已经是绝对路径，直接返回
            if (Path.IsPathRooted(relativePath))
            {
                return relativePath;
            }

            // 获取项目根目录路径
            string projectPath = Path.GetDirectoryName(Application.dataPath);

            // 如果相对路径以 "Assets" 开头
            if (relativePath.StartsWith("Assets"))
            {
                // 移除 "Assets" 并与项目路径组合
                string pathWithoutAssets = relativePath.Substring("Assets".Length).TrimStart('/');
                return Path.Combine(projectPath, pathWithoutAssets);
            }

            // 如果不以 "Assets" 开头，假设它是相对于项目根目录的路径
            return Path.GetFullPath(Path.Combine(projectPath, relativePath));
        }
        public static string ConvertToRelativePath(string absolutePath)
        {
            // 确保路径使用正斜杠
            absolutePath = absolutePath.Replace('\\', '/');

            // 获取项目的 Assets 文件夹路径
            string assetsPath = Application.dataPath;

            // 如果绝对路径在 Assets 文件夹内
            if (absolutePath.StartsWith(assetsPath))
            {
                // 移除 Assets 路径前缀，并添加 "Assets" 到开头
                return "Assets" + absolutePath.Substring(assetsPath.Length);
            }

            // 如果路径不在 Assets 文件夹内，尝试获取相对于项目根目录的路径
            string projectPath = Directory.GetParent(Application.dataPath).FullName;
            string relativePath = Path.GetRelativePath(projectPath, absolutePath);

            // 如果相对路径以 "Assets" 开头，直接返回
            if (relativePath.StartsWith("Assets"))
            {
                return relativePath;
            }

            // 如果路径完全在项目文件夹外，返回原始路径并记录警告
            if (relativePath.StartsWith(".."))
            {
                Debug.LogWarning($"Path is outside of the project folder: {absolutePath}");
                return absolutePath;
            }

            // 其他情况，添加 "Assets/" 前缀
            return "Assets/" + relativePath;
        }
    }


}
