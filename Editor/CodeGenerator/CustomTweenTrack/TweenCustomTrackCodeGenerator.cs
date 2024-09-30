using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Cr7Sund.TweenTimeLine
{
    public class TweenCustomTrackCodeGenerator
    {
        public static void GenerateCode(string outputPath, IEnumerable<TweenComponentData> parameters)
        {
            Parallel.ForEach(parameters, parameter =>
            {
                string typeName = GetTypeName(parameter.ComponentType);
                // string processedPropertyMethod = ProcessPropertyMethod(parameter.GetPropertyMethod);
                string identifier = TweenCustomTrackCodeGenerator.GetTweenBehaviourIdentifier(parameter);
                string includeNamespaceName = GetNamespaceName(parameter.ComponentType);

                string controlAssetFolder = Path.Combine(outputPath, "ControlAsset", includeNamespaceName.Replace('.', Path.DirectorySeparatorChar), typeName);
                string primeTweenBehaviourFolder = Path.Combine(outputPath, "PrimeTweenBehaviours", includeNamespaceName.Replace('.', Path.DirectorySeparatorChar), typeName);
                string controlTrackFolder = Path.Combine(outputPath, "ControlTrack", includeNamespaceName.Replace('.', Path.DirectorySeparatorChar), typeName);

                Directory.CreateDirectory(controlAssetFolder);
                Directory.CreateDirectory(primeTweenBehaviourFolder);
                Directory.CreateDirectory(controlTrackFolder);

                string namespaceName = $"Cr7Sund.{typeName}Tween";
                string controlBehaviourCode = string.Empty;
                string preTweenMethod = parameter.GetTweenMethod();
                string setPropertyMethod = parameter.GetSetMethod();
                string customPropertyMethod = parameter.GetCustomTweenMethod();

                string controlAssetCode = GenerateControlAssetCode(namespaceName, includeNamespaceName, identifier, parameter.ComponentType, parameter.ValueType);
                if (preTweenMethod != "Custom")
                    controlBehaviourCode = GenerateControlBehaviourCode(namespaceName, includeNamespaceName, identifier, parameter.ComponentType, parameter.ValueType, parameter.GetPropertyMethod, setPropertyMethod, preTweenMethod);
                else
                    controlBehaviourCode = GenerateControlCustomBehaviourCode(namespaceName, includeNamespaceName, identifier, parameter.ComponentType, parameter.ValueType, parameter.GetPropertyMethod, setPropertyMethod, preTweenMethod, customPropertyMethod);
                string controlTrackCode = GenerateControlTrackCode(namespaceName, includeNamespaceName, identifier, parameter.ComponentType, parameter.ValueType);

                File.WriteAllText(Path.Combine(controlAssetFolder, $"{identifier}ControlAsset.cs"), controlAssetCode);
                File.WriteAllText(Path.Combine(primeTweenBehaviourFolder, $"{identifier}ControlBehaviour.cs"), controlBehaviourCode);
                File.WriteAllText(Path.Combine(controlTrackFolder, $"{identifier}ControlTrack.cs"), controlTrackCode);
            });

            Debug.Log("Start ");
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

        private static string GenerateControlBehaviourCode(string namespaceName, string includeNameSpace, string identifier, string componentType, string valueType, string getPropertyMethod, string setPropertyMethod, string tweenMethod)
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

        private static string GenerateControlCustomBehaviourCode(string namespaceName, string includeNameSpace, string identifier, string componentType, string valueType, string getPropertyMethod,
         string setPropertyMethod, string tweenMethod, string customTweenMethod)
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
                  ease: PrimEase, endValue: _endPos, duration: (float)duration, 
                  onValueChange: (target, updateValue) => {customTweenMethod});
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

        private static string GenerateControlTrackCode(string namespaceName, string includeNameSpace, string identifier, string componentType, string valueType)
        {
            string customNamespaceName = string.Empty;
            if (includeNameSpace != "UnityEngine")
            {
                customNamespaceName = $"using {includeNameSpace};";
            }

            var rand = new System.Random();
            return $@"
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Cr7Sund.TweenTimeLine;
{customNamespaceName}
namespace {namespaceName}
{{
    [TrackClipType(typeof({identifier}ControlAsset))]
    [TrackClipType(typeof(EmptyControlAsset))]
    [TrackBindingType(typeof({componentType}))]
    [TrackColor({GetRandomDoubleUsingNext(rand)}f, {GetRandomDoubleUsingNext(rand)}f, {GetRandomDoubleUsingNext(rand)}f)]
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
            string methodWithoutParentheses = propertyMethod.Split('(')[0];
            string[] parts = methodWithoutParentheses.Split('.');
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = char.ToUpper(parts[i][0]) + parts[i].Substring(1);
            }
            return string.Join(string.Empty, parts);
        }

        public static string GetTweenBehaviourIdentifier(TweenComponentData tweenComponentData)
        {
            string typeName = GetTypeName(tweenComponentData.ComponentType);
            string processedPropertyMethod = ProcessPropertyMethod(tweenComponentData.GetPropertyMethod);
            return $"{typeName}_{processedPropertyMethod}";
        }

        public static double GetRandomDoubleUsingNext(System.Random random)
        {
            return random.Next(0, 1001) / 1000.0;
        }
    }
}
