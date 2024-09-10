using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public class BuiltInAnimationBuilder
    {
        public const string outPutPath = @"C:\Users\liux4\Documents\UnityProjects\MyMiniGame\Assets\Plugins\TweenTimeline\Editor\AnimationAction\AnimationCollections\BuiltIn\BuiltInAnimationCollections";


        [MenuItem("Tools/CreateBuildInAnimation")]
        public static void CreateBuildInAnimation()
        {
            string category = "Base";

            var tweenConfig = AssetDatabase.LoadAssetAtPath<TweenGenConfig>(AssetDatabase.GUIDToAssetPath(AnimationClipConverter.TweenConfigGUID));

            var builder = new BuiltInAnimationBuilder();
            builder.Gen(tweenConfig, category).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception is AggregateException aggregateException)
                    {
                        foreach (var item in aggregateException.InnerExceptions)
                        {
                            Debug.LogException(item);
                        }
                    }
                    else
                    {
                        Debug.LogException(task.Exception);
                    }
                }
                else
                {
                    Debug.Log("Built-in animations generated successfully.");
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private static void GetGenConfigs(TweenGenConfig tweenGenConfig, out List<string> lines, out List<string> namespaces)
        {
            lines = new List<string>();
            namespaces = new List<string>();
            foreach (ComponentValuePair item in tweenGenConfig.componentValuePairs)
            {
                lines.Add(GenerateAnimationCode(item, out var namespaceName));
                if (!namespaces.Contains(namespaceName))
                {
                    namespaces.Add(namespaceName);
                }
            }
        }

        private async Task Gen(TweenGenConfig tweenGenConfig, string category)
        {
            string filePath = $"{outPutPath}.{category}.cs";

            // if (!File.Exists(filePath))
            // {
            //     File.Create(filePath);
            // }

            GetGenConfigs(tweenGenConfig, out var lines, out var namespaces);

            var finalContent = new StringBuilder();
            finalContent.Append(StartGen(category, namespaces));
            foreach (var generatedLine in lines)
            {
                finalContent.Append(generatedLine);
            }
            finalContent.AppendLine(EndGen());

            await File.WriteAllTextAsync(filePath, finalContent.ToString(), Encoding.UTF8);
        }

        public string StartGen(string category, List<string> namespaces)
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
    public partial class AnimationContainerBuilder
    {{
        private AnimationCollection Create{category}AnimationCollection()
        {{
            var customAnimationCollection = new AnimationCollection(""{category}"");
";
        }

        public string EndGen()
        {
            return $@"            return customAnimationCollection;
        }}
    }}
}}";
        }

        static string GenerateAnimationCode(ComponentValuePair method, out string namespaceName)
        {
            string typeName = TweenTimeLineCodeGenerator.GetTypeName(method.ComponentType);
            namespaceName = $"using Cr7Sund.{typeName}Tweeen;";
            string processedPropertyMethod = TweenTimeLineCodeGenerator.ProcessPropertyMethod(method.GetPropertyMethod);
            string identifier = $"{typeName}_{processedPropertyMethod}";
            return $@"
            customAnimationCollection.animationCollections.Add(new AnimationEffect( ""{method.GetPropertyMethod}"",""{typeName}"")
            {{
                image = ""custom_{identifier}_example.png"",
                animationSteps = new List<AnimationStep>
                {{
                    new AnimationStep
                    {{
                        EndPos = ""{GetDefaultValue(method.ValueType)}"", 
                        isRelative = true,
                        tweenMethod = GetTweenMethodName<{identifier}ControlBehaviour>(),
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
                    return string.Empty;  // 字符串直接返回空值
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
