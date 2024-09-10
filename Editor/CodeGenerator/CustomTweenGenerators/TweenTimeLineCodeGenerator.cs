using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Cr7Sund.TweenTimeLine
{
    public class TweenTimeLineCodeGenerator
    {
        public const string OutputPath = @"C:\Users\liux4\Documents\UnityProjects\MyMiniGame\Assets\Plugins\TweenTimeline\Editor\Customs\";

        public static void GenerateCode(IEnumerable<ComponentValuePair> parameters)
        {
            Parallel.ForEach(parameters, parameter =>
            {
                string typeName = GetTypeName(parameter.ComponentType);
                string processedPropertyMethod = ProcessPropertyMethod(parameter.GetPropertyMethod);
                string identifier = $"{typeName}_{processedPropertyMethod}";
                string includeNamespaceName = GetNamespaceName(parameter.ComponentType);

                string controlAssetFolder = Path.Combine(OutputPath, "ControlAsset", includeNamespaceName.Replace('.', Path.DirectorySeparatorChar), typeName);
                string primeTweenBehaviourFolder = Path.Combine(OutputPath, "PrimeTweenBehaviours", includeNamespaceName.Replace('.', Path.DirectorySeparatorChar), typeName);
                string controlTrackFolder = Path.Combine(OutputPath, "ControlTrack", includeNamespaceName.Replace('.', Path.DirectorySeparatorChar), typeName);

                Directory.CreateDirectory(controlAssetFolder);
                Directory.CreateDirectory(primeTweenBehaviourFolder);
                Directory.CreateDirectory(controlTrackFolder);

                string namespaceName = $"Cr7Sund.{typeName}Tweeen";
                string controlAssetCode = GenerateControlAssetCode(namespaceName,includeNamespaceName, identifier, parameter.ComponentType, parameter.ValueType);
                string controlBehaviourCode = GenerateControlBehaviourCode(namespaceName,includeNamespaceName, identifier, parameter.ComponentType, parameter.ValueType, parameter.GetPropertyMethod, parameter.SetPropertyMethod, parameter.PreTweenMethod);
                string controlTrackCode = GenerateControlTrackCode(namespaceName,includeNamespaceName, identifier, parameter.ComponentType, parameter.ValueType);

                File.WriteAllText(Path.Combine(controlAssetFolder, $"{identifier}ControlAsset.cs"), controlAssetCode);
                File.WriteAllText(Path.Combine(primeTweenBehaviourFolder, $"{identifier}ControlBehaviour.cs"), controlBehaviourCode);
                File.WriteAllText(Path.Combine(controlTrackFolder, $"{identifier}ControlTrack.cs"), controlTrackCode);
            });
        }

        private static string GenerateControlAssetCode(string namespaceName, string includeNameSpace, string identifier, string componentType, string valueType)
        {
            string customNamespaceName = string.Empty;
            if (includeNameSpace != "UnityEngine")
            {
                customNamespaceName = $"using {includeNameSpace};";
            }
            return $@"
using System;
using UnityEngine;
using Cr7Sund.TweenTimeLine;
{customNamespaceName}
namespace {namespaceName}
{{
    public class {identifier}ControlAsset : BaseControlAsset<{identifier}ControlBehaviour, {componentType}, {valueType}>
    {{
    }}
}}
";
        }

        private static string GenerateControlBehaviourCode(string namespaceName,string includeNameSpace, string identifier, string componentType, string valueType, string getPropertyMethod, string setPropertyMethod, string tweenMethod)
        {
            string customNamespaceName = string.Empty;
            if (includeNameSpace != "UnityEngine" &&
             includeNameSpace != "PrimeTween")
            {
                customNamespaceName = $"using {includeNameSpace};";
            }
            return $@"
using System;
using UnityEngine;
using PrimeTween;
using Cr7Sund.TweenTimeLine;
{customNamespaceName}
namespace {namespaceName}
{{
    [Serializable]
    public  class {identifier}ControlBehaviour : BaseControlBehaviour<{componentType}, {valueType}>
    {{
        protected override PrimeTween.Tween OnCreateTween({componentType} target, double duration, {valueType} startValue)
        {{
            return PrimeTween.Tween.{tweenMethod}(target, startValue: startValue,
                  ease: PrimEase, endValue: _endPos, duration: (float)duration);
        }}

        protected override object OnGet({componentType} target)
        {{
            return target.{getPropertyMethod};
        }}

        protected override void OnSet({componentType} target, {valueType} updateValue)
        {{
            {setPropertyMethod}
        }}
    }}
}}
";
        }

        private static string GenerateControlTrackCode(string namespaceName,string includeNameSpace, string identifier, string componentType, string valueType)
        {
            string customNamespaceName = string.Empty;
            if (includeNameSpace != "UnityEngine")
            {
                customNamespaceName = $"using {includeNameSpace};";
            }
            return $@"
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
{customNamespaceName}
namespace {namespaceName}
{{
    [TrackClipType(typeof({identifier}ControlAsset))]
    [TrackBindingType(typeof({componentType}))]
    public class {identifier}ControlTrack : TrackAsset,IBaseTrack
    {{

    }}
}}
";
        }

        public static string GetTypeName(string fullType)
        {
            return fullType.Substring(fullType.LastIndexOf('.') + 1);
        }

        private static string GetNamespaceName(string fullType)
        {
            int lastDotIndex = fullType.LastIndexOf('.');
            return lastDotIndex > 0 ? fullType.Substring(0, lastDotIndex) : string.Empty;
        }

        public static string ProcessPropertyMethod(string propertyMethod)
        {
            // 去除括号并将点号后的部分转换为 PascalCase
            string methodWithoutParentheses = propertyMethod.Split('(')[0];
            string[] parts = methodWithoutParentheses.Split('.');
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = char.ToUpper(parts[i][0]) + parts[i].Substring(1);
            }
            return string.Join(string.Empty, parts);
        }

    }
}
