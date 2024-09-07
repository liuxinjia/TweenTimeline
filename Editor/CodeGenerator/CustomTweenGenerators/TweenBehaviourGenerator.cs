using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;
using PrimeTween;

namespace Cr7Sund.TweenTimeLine
{
    [System.Serializable]
    public class ComponentValuePair
    {
        public string ComponentType;
        public string ValueType;
        public string GetPropertyMethod;
        public string SetPropertyMethod;
        public string PreTweenMethod;

        public ComponentValuePair()
        {

        }
        public ComponentValuePair(string componentType, string valueType, string getPropertyMethod, string setPropertyMethod, string preTweenMethod)
        {
            ComponentType = componentType;
            ValueType = valueType;
            GetPropertyMethod = getPropertyMethod;
            SetPropertyMethod = setPropertyMethod;
            PreTweenMethod = preTweenMethod;
        }

        private static string ConvertFormat(string methodName, Dependency dep)
        {
            string result = $"{getMethodPrefix(dep)}{methodName}";
            if (result == "UIAlpha")
            {
                return "Alpha";
            }
            if (result == "UIColor")
            {
                return "Color";
            }
            return result;
        }
        public static string getMethodPrefix(Dependency dep)
        {
            switch (dep)
            {
                case Dependency.UNITY_UGUI_INSTALLED:
                    return "UI";
                case Dependency.AUDIO_MODULE_INSTALLED:
                    return "Audio";
                case Dependency.PHYSICS_MODULE_INSTALLED:
                case Dependency.PHYSICS2D_MODULE_INSTALLED:
                    return nameof(Rigidbody);
                case Dependency.None:
                case Dependency.PRIME_TWEEN_EXPERIMENTAL:
                case Dependency.UI_ELEMENTS_MODULE_INSTALLED:
                case Dependency.TEXT_MESH_PRO_INSTALLED:
                    return null;
            }
            return dep.ToString();
        }

        public static string ToLowerFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            char[] chars = input.ToCharArray();
            chars[0] = char.ToLower(chars[0]);
            return new string(chars);
        }

        public static string LowerType(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            if (input == "Float" || input == "Int")
            {
                return ToLowerFirstLetter(input);
            }
            return input;
        }


        public static implicit operator ComponentValuePair(MethodGenerationData methodDescData)
        {
            return new ComponentValuePair(
                 methodDescData.targetType,
                 LowerType(methodDescData.propertyType.ToString()),
                 getPropertyMethod(methodDescData),
                 setPropertyMethod(methodDescData),
                 ConvertFormat(methodDescData.methodName, methodDescData.dependency));
        }

        private static Regex replaceBracketRegex = new Regex(@"(\w+)\[(\d+)\]");
        private static string ReplaceArrayIndexWithProperty(string input)
        {
            string replacementEvaluator(Match match)
            {
                string objectName = match.Groups[1].Value;
                int index = int.Parse(match.Groups[2].Value);

                // Define a mapping from index to property name
                string propertyName = index switch
                {
                    0 => "x",
                    1 => "y",
                    2 => "z",
                    _ => $"property{index}" // Default case if index is not mapped
                };

                return $"{objectName}.{propertyName}";
            }


            return replaceBracketRegex.Replace(input, new MatchEvaluator(replacementEvaluator));
        }

        private static string getPropertyMethod(MethodGenerationData methodDescData)
        {
            var result = methodDescData.propertyName;
            if (string.IsNullOrEmpty(result))
            {
                var convertStr = ReplaceArrayIndexWithProperty(methodDescData.propertyGetter);
                result = convertStr;
            }
            if (string.IsNullOrEmpty(result))
            {
                result = methodDescData.propertyGetter;
                if (methodDescData.methodName == "LocalRotation")
                {
                    return ToLowerFirstLetter(methodDescData.methodName);
                }
                else if (methodDescData.methodName == "Scale")
                {
                    return "localScale";
                }
                else if (methodDescData.methodName == "TweenTimeScale")
                {
                    return "timeScale";
                }
                else if (methodDescData.methodName == "Rotation")
                {
                    return "rotation";
                }
                else
                {
                    UnityEngine.Debug.Log($"{methodDescData.methodName} \n {methodDescData}");
                    // throw new System.Exception();
                }
            }
            return result;
        }

        static string RemoveLastDotAndAfterIfStyle(string input)
        {
            // Check if the input contains "style"
            if (input.Contains("style"))
            {
                // Find the position of the last dot
                int lastDotIndex = input.LastIndexOf('.');

                // If a dot is found, return the substring up to the last dot
                if (lastDotIndex != -1)
                {
                    return input.Substring(0, lastDotIndex);
                }
            }

            // If "style" is not found or no dot is found, return the original string
            return input;
        }
        private static string setPropertyMethod(MethodGenerationData methodDescData)
        {
            var result = methodDescData.propertyName;
            if (string.IsNullOrEmpty(result))
            {
                var convertStr = ReplaceArrayIndexWithProperty(methodDescData.propertySetter);
                result = convertStr;
            }
            if (string.IsNullOrEmpty(result))
            {
                var convertStr = ReplaceArrayIndexWithProperty(methodDescData.propertyGetter);
                result = convertStr;
            }


            var valueType = LowerType(methodDescData.propertyType.ToString());
            result = RemoveLastDotAndAfterIfStyle(result);

            if (valueType == "Vector2")
                result = ReplaceVector2ComponentAssignment(result);
            if (valueType == "Vector3")
                result = ReplaceVector3ComponentAssignment(result);
            if (valueType == "float")
            {
                if (TryReplaceColorComponentAssignment(result, out var output))
                {
                    result = output;
                }
                else
                {
                    result = ReplaceFloatComponentAssignment(result);
                }
            }
            if (valRegex.IsMatch(result))
            {   // Perform the replacement
                result = valRegex.Replace(result, "(updateValue)");
                result = $"target.{result} ;";
                result = result.Replace("_target", "target");
            }
            else
            {
                result = $"target.{result} = updateValue;";
            }

            return result;
        }
        static readonly Regex replaceSingleVector3Regex = new Regex(@"(\w+)\.(localPosition|localScale|position|anchoredPosition3D)\.(x|y|z)\s*=\s*(\w+);");
        static readonly Regex replaceSingleVector2Regex = new Regex(@"(\w+)\.(offsetMax|offsetMin|pivot|anchoredPosition)\.(x|y|z)\s*=\s*(\w+);");

        static readonly Regex colorComponentAssignmentRegex = new Regex(@"(\w+)\.(\w*[cC]olor)\.(r|g|b|a)\s*=\s*(\w+);");
        static Regex colorPatternRegex = new Regex(@"\b\w*[cC]olor\b");
        static Regex replacePosRegex = new Regex(@"(\w+)\.(\w+)\s*=\s*(\w+);");
        static Regex valRegex = new Regex(@"\bval\b(?=\s*\))");


        static string ReplaceFloatComponentAssignment(string input)
        {
            // Define the replacement logic
            string replacementVector3Evaluator(Match match)
            {
                string objectName = match.Groups[1].Value;
                string vectorField = match.Groups[2].Value;
                string component = match.Groups[3].Value;
                string updateValue = match.Groups[4].Value;

                // Determine the components to use in the new Vector3
                string xComponent = component == "x" ? updateValue : $"{objectName}.{vectorField}.x";
                string yComponent = component == "y" ? updateValue : $"{objectName}.{vectorField}.y";
                string zComponent = component == "z" ? updateValue : $"{objectName}.{vectorField}.z";

                return $"{objectName}.{vectorField} = new Vector3({xComponent}, {yComponent}, {zComponent});";
            }
            string replacementVector2Evaluator(Match match)
            {
                string objectName = match.Groups[1].Value;
                string vectorField = match.Groups[2].Value;
                string component = match.Groups[3].Value;
                string updateValue = match.Groups[4].Value;

                // Determine the components to use in the new Vector3
                string xComponent = component == "x" ? updateValue : $"{objectName}.{vectorField}.x";
                string yComponent = component == "y" ? updateValue : $"{objectName}.{vectorField}.y";

                return $"{objectName}.{vectorField} = new Vector3({xComponent}, {yComponent});";
            }

            // Perform the replacement
            if (replaceSingleVector3Regex.IsMatch(input))
                return replaceSingleVector3Regex.Replace(input, new MatchEvaluator(replacementVector3Evaluator));
            else
                return replaceSingleVector2Regex.Replace(input, new MatchEvaluator(replacementVector2Evaluator));
        }

        static bool TryReplaceColorComponentAssignment(string input, out string output)
        {
            output = input;
            if (colorPatternRegex.IsMatch(input))
            {
                string replacementEvaluator(Match match)
                {
                    string objectName = match.Groups[1].Value;
                    string colorField = match.Groups[2].Value;
                    string component = match.Groups[3].Value;
                    string updateValue = match.Groups[4].Value;

                    // 根据更新的组件生成新的 Vector4
                    string rComponent = component == "r" ? updateValue : $"{objectName}.{colorField}.r";
                    string gComponent = component == "g" ? updateValue : $"{objectName}.{colorField}.g";
                    string bComponent = component == "b" ? updateValue : $"{objectName}.{colorField}.b";
                    string aComponent = component == "a" ? updateValue : $"{objectName}.{colorField}.a";

                    return $"{objectName}.{colorField} = new Color({rComponent}, {gComponent}, {bComponent}, {aComponent});";
                }
                output = colorComponentAssignmentRegex.Replace(input, new MatchEvaluator(replacementEvaluator));
                return true;
            }

            return false;
        }

        static string ReplaceVector3ComponentAssignment(string input)
        {
            // Define the replacement logic
            string replacementEvaluator(Match match)
            {
                string objectName = match.Groups[1].Value;
                string component = match.Groups[2].Value;
                string updateValue = match.Groups[3].Value;

                // Determine the components to keep based on the matched component
                string xComponent = component == "x" ? updateValue : $"{updateValue}.x";
                string yComponent = component == "y" ? updateValue : $"{updateValue}.y";
                string zComponent = component == "z" ? updateValue : $"{updateValue}.z";

                return $"{objectName}.{component} = new Vector3({xComponent}, {yComponent}, {zComponent});";
            }

            // Perform the replacement
            return replacePosRegex.Replace(input, new MatchEvaluator(replacementEvaluator));
        }

        static string ReplaceVector2ComponentAssignment(string input)
        {
            // Define the replacement logic
            string replacementEvaluator(Match match)
            {
                string objectName = match.Groups[1].Value;
                string component = match.Groups[2].Value;
                string updateValue = match.Groups[3].Value;

                // Determine the components to keep based on the matched component
                string xComponent = component == "x" ? updateValue : $"{updateValue}.x";
                string yComponent = component == "y" ? updateValue : $"{updateValue}.y";

                return $"{objectName}.{component} = new Vector2({xComponent}, {yComponent});";
            }


            // Perform the replacement
            return replacePosRegex.Replace(input, new MatchEvaluator(replacementEvaluator));
        }


    }
}
