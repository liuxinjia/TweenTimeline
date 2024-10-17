using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
namespace Cr7Sund.TweenTimeLine
{
    public class TweenTimelineDefine
    {
        public static string tweenLibraryPath => $"{BuiltInConfigEditorFolder}/TweenActionLibrary.asset";
        public static string easingTokenPresetsPath => $"{BuiltInConfigRuntimeFolder}/EasingTokenPresets.asset";



        public static string animTokenPresetsPath => $"{BuiltInConfigEditorFolder}/AnimTokenPresets.asset";
        public static string componentTweenCollectionPath => $"{BuiltInConfigEditorFolder}/ComponentTweenCollection.asset";

        #region Window Style
        public const string windowVisualTreeAssetGUID = "5d602cb17b57b46439b7bd1be265e07a";
        public const string windowStyleGUID = "fb42482ea3524b845aae708f4c63ffc7";
        public const string unitItemVisualTreeAssetGUID = "c0db593d4dafda847be427f46256930f";
        public const string unitItemStyleGUID = "64e80b7565634e3469b5214321257731";
        public const string gridVisualTreeAssetGUID = "f0b1d90a7f1c4214be7f11bb316bf762";
        public const string gridItemStyleGUID = "bcf8978c76b6418ca38f4a57fe81056c";
        public const string gridItemVisualTreeAssetGUID = "815c36bee7924d8dae6f349c09ca0b26";
        public const string gridItemItemStyleGUID = "409d5890bb0d4c05acd7025f26b8d165";

        public static Vector2 windowMaxSize = new Vector2(620f, 500f);
        public static Background recordOnBackground;
        public static Background recordOffBackground;
        public static Background plaBackground;
        public static Background stopBackground;
        #endregion

        // Assets/Plugins/TweenTimeline/Sample/Editor/TweenTimeline.Sample.Editor.asmdef
        private static Assembly customAssembly;

        public static Assembly CustomAssembly
        {
            get
            {
                if (customAssembly == null)
                {
                    foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        if(item.GetName().Name == TweenTimelinePreferencesProvider.GetString(
                                    TweenPreferenceDefine.CustomTweenAssemblyName)){
                            customAssembly = item;
                            break;
                        }
                    }
                }
                return customAssembly;
            }
        }
        // =>
        //     customAssembly ??= Assembly.LoadFile(
        //         PathUtility.ConvertToAbsolutePath(
        //         TweenTimelinePreferencesProvider.GetString(
        //         TweenPreferenceDefine.CustomTweenAssemblyName)));

        #region Custom
        // Assets/Plugins/TweenTimeline/Sample/RuntTime/CodeGens
        public static string GenRuntimePath =>
            $"{TweenTimelinePreferencesProvider.GetString(TweenPreferenceDefine.CustomTweenPath)}/RunTime/CodeGens";

        // Assets\Plugins\TweenTimeline\Sample\Editor\Configs
        public static string SampleEditorPath =>
            $"{TweenTimelinePreferencesProvider.GetString(TweenPreferenceDefine.CustomTweenPath)}/Editor/Configs";

        // Assets/TweenTimeline/Customs/Editor/Datas
        public static string EditorDataSourcePath =>
            $"{CustomConfigEditorFolder}/Datas";
        // Assets/TweenTimeline/Customs/Resources/Datas
        public static string RuntimDataSourePath =>
            $"{CustomConfigRuntimeFolder}/Datas";
        #endregion

        #region Config
        public static string BuiltInConfigPath =>
            $"{TweenTimelinePreferencesProvider.GetString(TweenPreferenceDefine.BuiltInLibraryPath)}BuiltInConfigs";
        public static string CustomConfigPath =>
            $"{TweenTimelinePreferencesProvider.GetString(TweenPreferenceDefine.BuiltInLibraryPath)}Customs";


        // Assets/Plugins/TweenTimeline/Sample/Editor/CustomTracks
        public static string CustomControlTacksFolder =>
        $"{TweenTimelinePreferencesProvider.GetString(TweenPreferenceDefine.CustomTweenPath)}/Editor/CustomTracks";
        public static string BuiltInControlTacksFolder
        {
            get
            {
                var builtInCustomFolder = FolderLocationChecker.GetFolderPath("Assets/Plugins/TweenTimeline/Editor/CustomTracks");
                if (string.IsNullOrEmpty(builtInCustomFolder))
                {
                    builtInCustomFolder = FolderLocationChecker.GetFolderPath("Editor/CustomTracks");
                }
                return builtInCustomFolder;
            }
        }

        public static string CustomConfigEditorFolder => $"{CustomConfigPath}/Editor Default Resources";
        public static string CustomConfigRuntimeFolder => $"{CustomConfigPath}/Resources";
        public static string BuiltInConfigEditorFolder => $"{BuiltInConfigPath}/Editor Default Resources";
        public static string BuiltInConfigRuntimeFolder => $"{BuiltInConfigPath}/Resources";

        // Assets/Plugins/TweenTimeline/Editor/CurvePresets
        public static string BuiltInCurvePresetFolder
        {
            get
            {
                var builtInCustomFolder = FolderLocationChecker.GetFolderPath("Assets/Plugins/TweenTimeline/Editor/CurvePresets");
                if (string.IsNullOrEmpty(builtInCustomFolder))
                {
                    builtInCustomFolder = FolderLocationChecker.GetFolderPath("Editor/CurvePresets");
                }
                return PathUtility.ConvertToRelativePath(builtInCustomFolder);
            }
        }
        public static string CustomCurvePresetFolder => $"{CustomConfigEditorFolder}/CurvePresets/Editor";

        public static string BuiltInGIFPresetFolder => $"{BuiltInConfigEditorFolder}/Gifs";
        public static string CustomGIFPresetFolder => $"{CustomConfigEditorFolder}/Gifs";

        private static Type[] derivedTokenTypes;
        public static Type[] DerivedEaseTokenTypes
        {
            get
            {
                if (derivedTokenTypes == null)
                {
                    var baseType = typeof(BaseEasingTokenPreset);
                    var assemblies = new List<Assembly>();
                    assemblies.Add(typeof(MaterialEasingTokenPreset).Assembly);
                    assemblies.Add(CustomAssembly);

                    derivedTokenTypes = assemblies
                        .SelectMany(assembly => assembly.GetTypes())
                        .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                        .ToArray();
                }

                return derivedTokenTypes;
            }
        }
        #endregion

        #region  StringDefine
        public const string InDefine = "In";
        public const string OutDefine = "Out";
        public const string PanelTag = "Panel";
        public const string CompositeTag = "Composite";
        public static Dictionary<string, Type> UIComponentTypeMatch = new(){
           { "Btn", typeof(UnityEngine.UI.Button)},
           { "Button", typeof(UnityEngine.UI.Button)},
           { "Image", typeof(UnityEngine.UI.Image)},
           { "Text", typeof(UnityEngine.UI.Text)},
           { "Slider", typeof(UnityEngine.UI.Slider)},
           { "Toggle", typeof(UnityEngine.UI.Toggle)},
           { "Dropdown", typeof(UnityEngine.UI.Dropdown)},
           { "InputField", typeof(UnityEngine.UI.InputField)},
           { "Scrollbar", typeof(UnityEngine.UI.Scrollbar)},
           { "ScrollRect", typeof(UnityEngine.UI.ScrollRect)},
           { "Mask", typeof(UnityEngine.UI.Mask)},
           { "RawImage", typeof(UnityEngine.UI.RawImage)},
           { "RectMask2D", typeof(UnityEngine.UI.RectMask2D)},
           { "GridLayoutGroup", typeof(UnityEngine.UI.GridLayoutGroup)},
           { "ContentSizeFitter", typeof(UnityEngine.UI.ContentSizeFitter)},
           { "LayoutElement", typeof(UnityEngine.UI.LayoutElement)},
           { "LayoutGroup", typeof(UnityEngine.UI.LayoutGroup)},
           { "CanvasGroup", typeof(UnityEngine.CanvasGroup)},
        };

        public const string IsActiveFieldName = "isActive"; // it match more, use dictionary instead
        #endregion
    }

}
