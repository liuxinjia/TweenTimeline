using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEditor.PackageManager.UI;
using System.IO;
using System.Linq;

namespace Cr7Sund
{
    public class PackageSamplesExporter 
    {
        private string _packageName = "";
        private string _sampleName;
        private ListRequest _listRequest;

        public PackageSamplesExporter(string packageName, string sampleName)
        {
            _packageName = packageName;
            _sampleName = sampleName;

        }

        public void ExportSamples()
        {
            _listRequest = Client.List();
            while (!_listRequest.IsCompleted)
            {

            }
            ListProgress();
        }

        private void ListProgress()
        {
            if (!_listRequest.IsCompleted)
                return;


            if (_listRequest.Status == StatusCode.Success)
            {
                foreach (var package in _listRequest.Result)
                {
                    if (package.name == _packageName)
                    {
                        ImportPackageSamples(package);
                        break;
                    }
                }
            }
            
            {
                Debug.LogError($"Failed to get package list: {_listRequest.Error.message}");
            }
        }

        private void ImportPackageSamples(UnityEditor.PackageManager.PackageInfo package)
        {
            var samples = Sample.FindByPackage(_packageName, package.version).ToArray();

            if (samples == null || samples.Length == 0)
            {
                Debug.LogWarning($"No samples found in package: {package.name}");
                return;
            }

            foreach (var sample in samples)
            {
                if (sample.displayName != _sampleName) continue;
                string samplePath = Path.Combine("Assets/Samples", package.name, sample.displayName);
                if (!Directory.Exists(samplePath))
                {
                    Debug.Log($"Importing sample: {sample.displayName}");
                    sample.Import();
                }
                else
                {
                    Debug.Log($"Sample already imported: {samplePath}");
                }
            }
        }
    }
}