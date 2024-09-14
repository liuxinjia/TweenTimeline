// using System.Text.RegularExpressions;
// using UnityEngine;
//
// namespace Cr7Sund.TweenTimeLine
// {
//     public static class MethodDataAdapter
//     {
//         private static readonly Regex ReplaceSingleVector3Regex = new Regex(@"(\w+)\.(localPosition|localScale|position|anchoredPosition3D)\.(x|y|z)\s*=\s*(\w+);");
//         private static readonly Regex ReplaceSingleVector2Regex = new Regex(@"(\w+)\.(offsetMax|offsetMin|pivot|anchoredPosition)\.(x|y|z)\s*=\s*(\w+);");
//
//         private static readonly Regex ColorComponentAssignmentRegex = new Regex(@"(\w+)\.(\w*[cC]olor)\.(r|g|b|a)\s*=\s*(\w+);");
//         private static readonly Regex ColorPatternRegex = new Regex(@"\b\w*[cC]olor\b");
//         private static readonly Regex ReplacePosRegex = new Regex(@"(\w+)\.(\w+)\s*=\s*(\w+);");
//
//         public static string ReplaceFloatComponentAssignment(string input)
//         {
//             string ReplacementVector3Evaluator(Match match)
//             {
//                 string objectName = match.Groups[1].Value;
//                 string vectorField = match.Groups[2].Value;
//                 string component = match.Groups[3].Value;
//                 string updateValue = match.Groups[4].Value;
//
//                 string xComponent = component == "x" ? updateValue : $"{objectName}.{vectorField}.x";
//                 string yComponent = component == "y" ? updateValue : $"{objectName}.{vectorField}.y";
//                 string zComponent = component == "z" ? updateValue : $"{objectName}.{vectorField}.z";
//
//                 return $"{objectName}.{vectorField} = new Vector3({xComponent}, {yComponent}, {zComponent});";
//             }
//
//             string ReplacementVector2Evaluator(Match match)
//             {
//                 string objectName = match.Groups[1].Value;
//                 string vectorField = match.Groups[2].Value;
//                 string component = match.Groups[3].Value;
//                 string updateValue = match.Groups[4].Value;
//
//                 string xComponent = component == "x" ? updateValue : $"{objectName}.{vectorField}.x";
//                 string yComponent = component == "y" ? updateValue : $"{objectName}.{vectorField}.y";
//
//                 return $"{objectName}.{vectorField} = new Vector3({xComponent}, {yComponent});";
//             }
//
//             if (ReplaceSingleVector3Regex.IsMatch(input))
//                 return ReplaceSingleVector3Regex.Replace(input, new MatchEvaluator(ReplacementVector3Evaluator));
//             else
//                 return ReplaceSingleVector2Regex.Replace(input, new MatchEvaluator(ReplacementVector2Evaluator));
//         }
//
//         public static bool TryReplaceColorComponentAssignment(string input, out string output)
//         {
//             output = input;
//             if (ColorPatternRegex.IsMatch(input))
//             {
//                 string ReplacementEvaluator(Match match)
//                 {
//                     string objectName = match.Groups[1].Value;
//                     string colorField = match.Groups[2].Value;
//                     string component = match.Groups[3].Value;
//                     string updateValue = match.Groups[4].Value;
//
//                     string rComponent = component == "r" ? updateValue : $"{objectName}.{colorField}.r";
//                     string gComponent = component == "g" ? updateValue : $"{objectName}.{colorField}.g";
//                     string bComponent = component == "b" ? updateValue : $"{objectName}.{colorField}.b";
//                     string aComponent = component == "a" ? updateValue : $"{objectName}.{colorField}.a";
//
//                     return $"{objectName}.{colorField} = new Color({rComponent}, {gComponent}, {bComponent}, {aComponent});";
//                 }
//                 output = ColorComponentAssignmentRegex.Replace(input, new MatchEvaluator(ReplacementEvaluator));
//                 return true;
//             }
//
//             return false;
//         }
//
//         public static string ReplaceVector3ComponentAssignment(string input)
//         {
//             string ReplacementEvaluator(Match match)
//             {
//                 string objectName = match.Groups[1].Value;
//                 string component = match.Groups[2].Value;
//                 string updateValue = match.Groups[3].Value;
//
//                 string xComponent = component == "x" ? updateValue : $"{updateValue}.x";
//                 string yComponent = component == "y" ? updateValue : $"{updateValue}.y";
//                 string zComponent = component == "z" ? updateValue : $"{updateValue}.z";
//
//                 return $"{objectName}.{component} = new Vector3({xComponent}, {yComponent}, {zComponent});";
//             }
//
//             return ReplacePosRegex.Replace(input, new MatchEvaluator(ReplacementEvaluator));
//         }
//
//         public static string ReplaceVector2ComponentAssignment(string input)
//         {
//             string ReplacementEvaluator(Match match)
//             {
//                 string objectName = match.Groups[1].Value;
//                 string component = match.Groups[2].Value;
//                 string updateValue = match.Groups[3].Value;
//
//                 string xComponent = component == "x" ? updateValue : $"{updateValue}.x";
//                 string yComponent = component == "y" ? updateValue : $"{updateValue}.y";
//
//                 return $"{objectName}.{component} = new Vector2({xComponent}, {yComponent});";
//             }
//
//             return ReplacePosRegex.Replace(input, new MatchEvaluator(ReplacementEvaluator));
//         }
//
//         public static string ConvertFormat(string methodName, Dependency dep)
//         {
//             string result = $"{GetMethodPrefix(dep)}{methodName}";
//             if (result == "UIAlpha")
//             {
//                 return "Alpha";
//             }
//             if (result == "UIColor")
//             {
//                 return "Color";
//             }
//             return result;
//         }
//
//         public static string GetMethodPrefix(Dependency dep)
//         {
//             switch (dep)
//             {
//                 case Dependency.UNITY_UGUI_INSTALLED:
//                     return "UI";
//                 case Dependency.AUDIO_MODULE_INSTALLED:
//                     return "Audio";
//                 case Dependency.PHYSICS_MODULE_INSTALLED:
//                 case Dependency.PHYSICS2D_MODULE_INSTALLED:
//                     return nameof(Rigidbody);
//                 case Dependency.None:
//                 case Dependency.PRIME_TWEEN_EXPERIMENTAL:
//                 case Dependency.UI_ELEMENTS_MODULE_INSTALLED:
//                 case Dependency.TEXT_MESH_PRO_INSTALLED:
//                     return null;
//             }
//             return dep.ToString();
//         }
//
//         public static string ToLowerFirstLetter(string input)
//         {
//             if (string.IsNullOrEmpty(input))
//             {
//                 return input;
//             }
//
//             char[] chars = input.ToCharArray();
//             chars[0] = char.ToLower(chars[0]);
//             return new string(chars);
//         }
//
//         public static string ToUpperFirstLetter(string input)
//         {
//             if (string.IsNullOrEmpty(input))
//             {
//                 return input;
//             }
//
//             char[] chars = input.ToCharArray();
//             chars[0] = char.ToUpper(chars[0]);
//             return new string(chars);
//         }
//
//         public static string LowerType(string input)
//         {
//             if (string.IsNullOrEmpty(input))
//             {
//                 return input;
//             }
//
//             if (input == "Float" || input == "Int")
//             {
//                 return ToLowerFirstLetter(input);
//             }
//             return input;
//         }
//
//         public static string UpperType(string input)
//         {
//             if (string.IsNullOrEmpty(input))
//             {
//                 return input;
//             }
//
//             if (input == "float" || input == "int")
//             {
//                 return ToUpperFirstLetter(input);
//             }
//             return input;
//         }
//
//         private static Regex ReplaceBracketRegex = new Regex(@"(\w+)\[(\d+)\]");
//         public static string ReplaceArrayIndexWithProperty(string input)
//         {
//             string ReplacementEvaluator(Match match)
//             {
//                 string objectName = match.Groups[1].Value;
//                 int index = int.Parse(match.Groups[2].Value);
//
//                 string propertyName = index switch
//                 {
//                     0 => "x",
//                     1 => "y",
//                     2 => "z",
//                     _ => $"property{index}"
//                 };
//
//                 return $"{objectName}.{propertyName}";
//             }
//
//             return ReplaceBracketRegex.Replace(input, new MatchEvaluator(ReplacementEvaluator));
//         }
//     }
// }
