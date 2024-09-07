using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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
                string identifier = $"{typeName}{processedPropertyMethod}";
                string namespaceName = GetNamespaceName(parameter.ComponentType);

                string controlAssetFolder = Path.Combine(OutputPath, "ControlAsset", namespaceName.Replace('.', Path.DirectorySeparatorChar), typeName);
                string primeTweenBehaviourFolder = Path.Combine(OutputPath, "PrimeTweenBehaviour", namespaceName.Replace('.', Path.DirectorySeparatorChar), typeName);
                string controlTrackFolder = Path.Combine(OutputPath, "ControlTrack", namespaceName.Replace('.', Path.DirectorySeparatorChar), typeName);

                // 确保文件夹路径存在
                Directory.CreateDirectory(controlAssetFolder);
                Directory.CreateDirectory(primeTweenBehaviourFolder);
                Directory.CreateDirectory(controlTrackFolder);

                string controlAssetCode = GenerateControlAssetCode(namespaceName, identifier, parameter.ComponentType, parameter.ValueType);
                string controlBehaviourCode = GenerateControlBehaviourCode(namespaceName, identifier, typeName, parameter.ValueType, parameter.GetPropertyMethod, parameter.SetPropertyMethod, parameter.PreTweenMethod);
                string controlTrackCode = GenerateControlTrackCode(namespaceName, identifier, typeName, parameter.ValueType);

                // 将文件写入相应的文件夹中
                File.WriteAllText(Path.Combine(controlAssetFolder, $"{identifier}ControlAsset.cs"), controlAssetCode);
                File.WriteAllText(Path.Combine(primeTweenBehaviourFolder, $"{identifier}ControlBehaviour.cs"), controlBehaviourCode);
                File.WriteAllText(Path.Combine(controlTrackFolder, $"{identifier}ControlTrack.cs"), controlTrackCode);
            });
        }

        private static string GenerateControlAssetCode(string namespaceName, string identifier, string componentType, string valueType)
        {
            return $@"
using System;
using {namespaceName};
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{{
    public class {identifier}ControlAsset : BaseControlAsset<{identifier}ControlBehaviour, {componentType}, {valueType}>
    {{
    }}
}}
";
        }

        private static string GenerateControlBehaviourCode(string namespaceName, string identifier, string componentType, string valueType, string getPropertyMethod, string setPropertyMethod, string tweenMethod)
        {
            return $@"
using System;
using {namespaceName};
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
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

        private static string GenerateControlTrackCode(string namespaceName, string identifier, string componentType, string valueType)
        {
            return $@"
using System;
using {namespaceName};
using UnityEngine;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{{
    [TrackClipType(typeof({identifier}ControlAsset))]
    [TrackBindingType(typeof({componentType}))]
    public class {identifier}ControlTrack : TrackAsset
    {{

    }}
}}
";
        }

        private static string GetTypeName(string fullType)
        {
            return fullType.Substring(fullType.LastIndexOf('.') + 1);
        }

        private static string GetNamespaceName(string fullType)
        {
            int lastDotIndex = fullType.LastIndexOf('.');
            return lastDotIndex > 0 ? fullType.Substring(0, lastDotIndex) : string.Empty;
        }

        private static string ProcessPropertyMethod(string propertyMethod)
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
