using System.IO;
using UnityEngine;
namespace Cr7Sund.TweenTimeLine
{
    public static class FolderLocationChecker
    {
        public enum FolderLocation
        {
            NotFound,
            InAssets,
            InPackages
        }

        public static FolderLocation CheckFolderLocation(string folderName)
        {
            // Check in Assets
            string assetsPath = Path.Combine(Application.dataPath, folderName);
            if (Directory.Exists(assetsPath))
            {
                return FolderLocation.InAssets;
            }

            // Check in Packages
            string packagesPath = Path.Combine(Application.dataPath, "..", "Packages");
            string[] packageDirectories = Directory.GetDirectories(packagesPath);

            foreach (string packageDir in packageDirectories)
            {
                string potentialPath = Path.Combine(packageDir, folderName);
                if (Directory.Exists(potentialPath))
                {
                    return FolderLocation.InPackages;
                }
            }

            // Check in PackageCache (for packages installed via Package Manager)
            string packageCachePath = Path.Combine(Application.dataPath, "..", "Library", "PackageCache");
            if (Directory.Exists(packageCachePath))
            {
                string[] packageCacheDirectories = Directory.GetDirectories(packageCachePath);
                foreach (string packageCacheDir in packageCacheDirectories)
                {
                    string potentialPath = Path.Combine(packageCacheDir, folderName);
                    if (Directory.Exists(potentialPath))
                    {
                        return FolderLocation.InPackages;
                    }
                }
            }

            // If not found in either location
            return FolderLocation.NotFound;
        }

        public static string GetFolderPath(string folderName)
        {
            // Check in Assets
            string assetsPath = Path.Combine(Application.dataPath, folderName);
            if (Directory.Exists(assetsPath))
            {
                return assetsPath;
            }

            // Check in Packages
            string packagesPath = Path.Combine(Application.dataPath, "..", "Packages");
            string[] packageDirectories = Directory.GetDirectories(packagesPath);

            foreach (string packageDir in packageDirectories)
            {
                string potentialPath = Path.Combine(packageDir, folderName);
                if (Directory.Exists(potentialPath))
                {
                    return potentialPath;
                }
            }

            // Check in PackageCache
            string packageCachePath = Path.Combine(Application.dataPath, "..", "Library", "PackageCache");
            if (Directory.Exists(packageCachePath))
            {
                string[] packageCacheDirectories = Directory.GetDirectories(packageCachePath);
                foreach (string packageCacheDir in packageCacheDirectories)
                {
                    string potentialPath = Path.Combine(packageCacheDir, folderName);
                    if (Directory.Exists(potentialPath))
                    {
                        return potentialPath;
                    }
                }
            }

            // If not found in either location
            return null;
        }
    }
}