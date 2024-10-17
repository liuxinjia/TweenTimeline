using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

namespace Cr7Sund
{
    public class TimelineWelcomeWindow : EditorWindow
    {
        private const string TweenTimelinePackageID = "com.cr7sund.tweentimeline";
        const string PrimeTweenPackageId = "com.kyrylokuzyk.primetween";
        const string UIEffectPackageId = "com.coffee.ui-effect";

        const string pluginName = "PrimeTween";
        // Assets/Plugins/PrimeTween/internal/tw/Zip/com.kyrylokuzyk.primetween.tgz
        string PrimeTweenTgzPath => "Assets/Plugins/" + pluginName + "/internal/" + "tw/Zip/" + PrimeTweenPackageId + ".tgz";
        const string TweenTimelineTgzPath = " tgz";
        const string BuiltInConfig = "BuiltInConfig";
        const string CustomConfig = "CustomConfig";
        const string CustomSample = "Demo";

        [SerializeField]
        private VisualTreeAsset m_VisualTreeAsset = default;

        [MenuItem("Window/UI Toolkit/TimelineWelcomeWindow")]
        public static void ShowExample()
        {
            TimelineWelcomeWindow wnd = GetWindow<TimelineWelcomeWindow>();
            wnd.titleContent = new GUIContent("TimelineWelcomeWindow");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;



            // Instantiate UXML
            VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();

            labelFromUXML.Q<Button>("ImportCore").RegisterCallback<ClickEvent>(_ => ImportCore());
            labelFromUXML.Q<Button>("ImportDemo").RegisterCallback<ClickEvent>(_ => ImportDemo());
            labelFromUXML.Q<Button>("ImportCoreOffline").RegisterCallback<ClickEvent>(_ => ImportDemo(false));
            labelFromUXML.Q<Button>("ImportDemoOffline").RegisterCallback<ClickEvent>(_ => ImportDemo(false));
            root.Add(labelFromUXML);
        }

        public void ImportCore(bool withUPM = true)
        {
            string packageCachePath = Path.Combine(Application.dataPath, "..", "Library", "PackageCache", PrimeTweenPackageId);
            string primeTweenManageFilePath = Path.Combine(packageCachePath, @"Runtime\Internal\PrimeTweenManager.cs");

#if !PRIME_TWEEN_INSTALLED
            var path = $"file:../{PrimeTweenTgzPath}";
            InstallPlugins(path);
#endif
            OverWritePrimetween(primeTweenManageFilePath);

            InstallPackage(TweenTimelinePackageID, withUPM);
            // Import Config
            // ImportBuiltInConfig();
        }

        private void ImportDemo(bool withUPM = true)
        {
            var exporter = new PackageSamplesExporter(TweenTimelinePackageID, CustomSample);
            exporter.ExportSamples();

            // exporter = new PackageSamplesExporter(TweenTimelinePackageID, CustomConfig);
            // exporter.ExportSamples();

            InstallPackage(UIEffectPackageId, withUPM);
        }

        private static void InstallPackage(string packageID, bool withUPM)
        {
            if (withUPM)
            {
                // installPlugin(TweenTimelinePackageID);
                OpenUPMInstaller.InstallPackage(packageID);
            }
            else
            {
                // AssetDatabase.ImportPackage("", false);
                var path = $"file:../{packageID}";
                InstallPlugins(path);
            }
        }

        private void ImportBuiltInConfig()
        {
             string packageCachePath = Path.Combine(Application.dataPath, "..", "Library", "PackageCache");
            string zipPath = Path.Combine(packageCachePath, @"\TweenTimelineInstaller\Zip\BuiltInConfigs.zip");
            string extractPath = Path.Combine(Application.dataPath,"");

            // System.IO.Compression.ZipFile.CreateFromDirectory(startPath, zipPath);
            System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
            // var exporter = new PackageSamplesExporter(TweenTimelinePackageID, BuiltInConfig);
            // exporter.ExportSamples();
        }


        static void OverWritePrimetween(string filePath)
        {
            string content = File.ReadAllText(filePath);

            string pattern = @"void\s+Reset\s*\(\s*\)\s*\{[^}]*Assert\.IsFalse\s*\(\s*Application\.isPlaying\s*\)[^}]*Debug\.LogError\s*\(\s*manualInstanceCreationIsNotAllowedMessage\s*\)[^}]*DestroyImmediate\s*\(\s*this\s*\)[^}]*\}";

            string replacement = @"        void Reset() {
            // Assert.IsFalse(Application.isPlaying);
            // Debug.LogError(manualInstanceCreationIsNotAllowedMessage);
            // DestroyImmediate(this);
        }";

            string newContent = Regex.Replace(content, pattern, replacement);

            if (content != newContent)
            {
                File.WriteAllText(filePath, newContent);
                Debug.Log($"Modified: {filePath}");
            }
            else
            {
                Debug.LogError("Try to disable Reset manually at {filePath}");
            }
        }

        public void AddTag()
        {
            AddTag("Panel");
            AddTag("Composite");
        }

        static void InstallPlugins(string path)
        {
            var addRequest = Client.Add(path);
            while (!addRequest.IsCompleted)
            {
            }
            if (addRequest.Status == StatusCode.Success)
            {
                Debug.Log($"{path} installed successfully.\n");
            }
            else
            {
                Debug.LogError($"Please re-import the plugin from the Asset Store and check that the file exists: [{path}].\n\n{addRequest.Error?.message}\n");
            }
        }

        static bool CheckPluginInstalled(string pluginPackageId)
        {
            var listRequest = Client.List(true);
            while (!listRequest.IsCompleted)
            {
            }
            return listRequest.Result.Any(_ => _.name == pluginPackageId);
        }

        private static void AddTag(string tag)
        {
            if (!IsTagExists(tag))
            {
                SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
                SerializedProperty tagsProp = tagManager.FindProperty("tags");

                int index = tagsProp.arraySize;
                tagsProp.InsertArrayElementAtIndex(index);
                SerializedProperty newTag = tagsProp.GetArrayElementAtIndex(index);
                newTag.stringValue = tag;

                tagManager.ApplyModifiedProperties();
            }
        }

        static bool IsTagExists(string tag)
        {
            foreach (string existingTag in UnityEditorInternal.InternalEditorUtility.tags)
            {
                if (existingTag.Equals(tag))
                    return true;
            }
            return false;
        }

    }
}