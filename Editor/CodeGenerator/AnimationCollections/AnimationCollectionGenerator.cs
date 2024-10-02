using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public class AnimationCollectionGenerator
    {
        public static void CreateBuildInAnimation(TweenGenTrackConfig tweenConfig)
        {
            var builder = new AnimationCollectionGenerator();
            try
            {
                builder.Gen(tweenConfig);
            }
            catch (Exception e)
            {
                if (e is AggregateException aggregateException)
                {
                    foreach (var item in aggregateException.InnerExceptions)
                    {
                        Debug.LogException(item);
                    }
                }
                else
                {
                    Debug.LogException(e);
                }
            }

            Debug.Log("Built-in animations generated successfully.");
        }

        private static void GetGenConfigs(TweenGenTrackConfig tweenGenTrackConfig, out List<string> lines, out List<string> namespaces)
        {
            lines = new List<string>();
            namespaces = new List<string>();

            foreach (TweenComponentData item in tweenGenTrackConfig.componentDatas)
            {
                lines.Add(GenerateAnimationCode(item, out var namespaceName));
                if (!namespaces.Contains(namespaceName))
                {
                    namespaces.Add(namespaceName);
                }
            }
        }

        private async void Gen(TweenGenTrackConfig tweenGenTrackConfig)
        {
            var configPath = AssetDatabase.GetAssetPath(tweenGenTrackConfig);
            string filePath = string.Empty;
            string customConfigPath = TweenTimelineDefine.CustomConfigPath;
            bool isCustom = false;
            if (configPath.StartsWith(customConfigPath))
            {
                // Assets\Plugins\TweenTimeline\Sample\Editor\Configs\CustomAnimationContainerBuilder.cs
                string outPutPath = PathUtility.ConvertToAbsolutePath(TweenTimelineDefine.SampleEditorPath);
                filePath = Path.Combine(outPutPath, "CustomTweenActionContainerBuilder.cs");
                isCustom = true;
            }
            else
            {
                filePath = "Assets/Plugins/TweenTimeline/Editor/TweenActions/TweeenActionCollections/BuiltIn";
                filePath = FolderLocationChecker.GetFolderPath(filePath);
                filePath = Path.Combine(filePath, "BuiltInAnimationCollections.Base.cs");
            }

            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) { }
            }

            GetGenConfigs(tweenGenTrackConfig, out var lines, out var namespaces);

            var finalContent = new StringBuilder();
            if (isCustom)
            {
                finalContent.Append(StartCustomGen(namespaces));
            }
            else
            {
                finalContent.Append(StartBaseGen(namespaces));
            }
            foreach (var generatedLine in lines)
            {
                finalContent.Append(generatedLine);
            }
            if (isCustom)
            {
                finalContent.AppendLine(EndCustomGen());
            }
            else
            {
                finalContent.AppendLine(EndBaseGen());
            }
            await File.WriteAllTextAsync(filePath, finalContent.ToString(), Encoding.UTF8);
        }

        public string StartCustomGen(List<string> namespaces)
        {
            var namespaceSB = new StringBuilder();
            foreach (var item in namespaces)
            {
                namespaceSB.AppendLine(item);
            }

            return $@"using System.Collections.Generic;
using UnityEngine;
{namespaceSB}

namespace Cr7Sund.TweenTimeLine
{{
    public class CustomTweenActionContainerBuilder
    {{
        public static List<{nameof(TweenActionEffect)}> CreateCustomAnimationCollection()
        {{
           var animEffect =  new List<{nameof(TweenActionEffect)}> ();
";
        }

        public string StartBaseGen(List<string> namespaces)
        {
            var namespaceSB = new StringBuilder();
            foreach (var item in namespaces)
            {
                namespaceSB.AppendLine(item);
            }

            return $@"using System.Collections.Generic;
using UnityEngine;
{namespaceSB}

namespace Cr7Sund.TweenTimeLine
{{
    public partial class TweenActionContainerBuilder
    {{
        public static {nameof(TweenCollection)} CreateBaseAnimationCollection()
        {{
            var customAnimationCollection = new TweenCollection(""Custom"");
            var animEffect = customAnimationCollection.animationCollections;
            ";
        }

        public string EndBaseGen()
        {
            return @" 
             return customAnimationCollection;
            }
    }
}";
        }

        public string EndCustomGen()
        {
            return @" 
             return animEffect;
            }
    }
}";
        }

        static string GenerateAnimationCode(TweenComponentData method, out string namespaceName)
        {
            string typeName = TweenCustomTrackCodeGenerator.GetTypeName(method.ComponentType);
            namespaceName = $"using Cr7Sund.{typeName}Tween;";
            string identifier = TweenCustomTrackCodeGenerator.GetTweenBehaviourIdentifier(method);
            return $@"
            animEffect.Add(new {nameof(TweenActionEffect)}( ""{method.GetPropertyMethod}"",""{typeName}"")
            {{
                image = ""custom_{typeName}_example.png"",
                collectionCategory = ""Custom"",
                animationSteps = new List<{nameof(TweenActionStep)}>
                {{
                    new {nameof(TweenActionStep)}
                    {{
                        EndPos = ""{GetDefaultValue(method.ValueType)}"", 
                        TweenActionStep.TweenOperationType.Additive
                        tweenMethod = {nameof(TweenActionContainerBuilder)}.GetTweenMethodName<{identifier}ControlBehaviour>(),
                        label = ""{method.GetPropertyMethod}"",
                    }}
                }}
            }});";
        }

        public static string GetDefaultValue(string valueType)
        {
            switch (valueType)
            {
                case "float":
                    return 0f.ToString();
                case "int":
                    return 0.ToString();
                case "bool":
                    return false.ToString();
                case "string":
                    return string.Empty; // 字符串直接返回空值
                case "Vector2":
                    return UnityEngine.Vector2.zero.ToString();
                case "Vector2Int":
                    return UnityEngine.Vector2Int.zero.ToString();
                case "Vector3":
                    return UnityEngine.Vector3.zero.ToString();
                case "Vector4":
                    return UnityEngine.Vector4.zero.ToString();
                case "Quaternion":
                    return UnityEngine.Quaternion.identity.ToString();
                case "Color":
                    return UnityEngine.Color.white.ToString();
                case "Rect":
                    return new UnityEngine.Rect(0, 0, 0, 0).ToString();
                default:
                    throw new Exception($"Unknown Type {valueType}");
            }
        }

    }
}
