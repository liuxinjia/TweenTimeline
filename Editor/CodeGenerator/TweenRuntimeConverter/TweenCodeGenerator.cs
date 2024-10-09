using System.Collections.Generic;
using UnityEngine;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.IO;
using UnityEditor;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using UnityEngine.Timeline;
using System;
using Cr7Sund.Timeline.Extension;
using UnityEngine.Assertions;
using System.Text;

namespace Cr7Sund.TweenTimeLine
{
    public class TweenCodeGenerator
    {
        private Dictionary<string, Tuple<string, string>> _contentDict = new();
        private Regex _tweenPatternRegex = new Regex(@"PrimeTween\.Tween\.(\w+)");
        private static readonly Regex _onValueChangeRegex = new Regex(@"onValueChange:\s*\(\s*[^)]*\s*\)\s*=>\s*(.*)", RegexOptions.Singleline);
        private static EasingTokenPresetLibrary _easingTokenPresetLibrary;

        [MenuItem("Tools/GenerateRunTimeCode")]
        public static void GenerateRunTimeCode()
        {
            var instance = new TweenCodeGenerator();
            instance.Generate();
        }

        public async void Generate()
        {

            EditorUtility.DisplayProgressBar("Generating Code", "Constructing Method Content Dictionary", 0f);
            await ConstructMethodContentDict();
            EditorUtility.ClearProgressBar();

            EditorUtility.DisplayProgressBar("Generating Code", "Constructing Tween Sequences", 0.5f);
            var sequences = ConstructAllTweenSequence();
            EditorUtility.ClearProgressBar();

            EditorUtility.DisplayProgressBar("Generating Code", "Creating Tween Code File", 1f);
            await CreateTweenGenFile(sequences);
            EditorUtility.ClearProgressBar();

            EditorUtility.DisplayProgressBar("Generating Code", "Creating Tween Extension File", 1f);
            await CreateTweenExtensionFile(sequences);
            EditorUtility.ClearProgressBar();
        }

        public async Task CreateTweenGenFile(Dictionary<string, GenTweenSequence> sequences)
        {
            string file = Path.Combine(TweenTimelineDefine.GenRuntimePath, "GenerateTween.cs");

            await using var writeStream = new StreamWriter(file);
            await GenerateTweenCodeAsync(sequences.Values.ToList(), writeStream);
        }

        public async Task CreateTweenExtensionFile(Dictionary<string, GenTweenSequence> sequences)
        {
            string file = Path.Combine(TweenTimelineDefine.GenRuntimePath, "ITweenBindingExtenstion.cs");

            await using var writeStream = new StreamWriter(file);
            await GenerateTweenExtensionCodeAsync(sequences.Values.ToList(), writeStream);
        }

        public async Task GenerateTweenCodeAsync(List<GenTweenSequence> sequences, StreamWriter writer)
        {
            await writer.WriteLineAsync("using Cr7Sund;");
            await writer.WriteLineAsync("using Cr7Sund.TweenTimeLine;");
            await writer.WriteLineAsync("using PrimeTween;");
            await writer.WriteLineAsync("using UnityEngine;");
            await writer.WriteLineAsync();
            await writer.WriteLineAsync("public static class GenerateTween");
            await writer.WriteLineAsync("{");

            for (int i = 0; i < sequences.Count; i++)
            {
                var sequence = sequences[i];
                await GenerateSequenceCodeAsync(writer, sequence);

                if (sequence.endDynamicParams.Count > 0)
                {
                    await GenerateDynamicSequenceCodeAsync(writer, sequence);
                }
            }

            await writer.WriteLineAsync("}");
            EditorUtility.ClearProgressBar();
        }

        private async Task GenerateSequenceCodeAsync(StreamWriter writer, GenTweenSequence sequence)
        {
            string parentTrackName = sequence.parentTrackName;

            await writer.WriteLineAsync($"    public static Sequence {parentTrackName}Tween(ITweenBinding binding, int cycles = 1, CycleMode cycleMode = CycleMode.Restart, Ease sequenceEase = Ease.Linear)");
            await writer.WriteLineAsync("    {");

            // Multiple trackInfos, use Sequence.Create()
            await writer.WriteLineAsync("        return Sequence.Create(cycles, cycleMode, sequenceEase)");

            for (int j = 0; j < sequence.trackInfos.Count; j++)
            {
                var track = sequence.trackInfos[j];
                if (track.clipInfos.Count < 1)
                {
                    continue;
                }

                if (track.clipInfos.Count == 1)
                {
                    var clip = track.clipInfos[0];
                    if (string.IsNullOrEmpty(clip.TweenMethod)
                    && string.IsNullOrEmpty(clip.CustomTweenMethod))
                    {
                        await writer.WriteLineAsync($"           .Group(Sequence.Create()");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(clip.CustomTweenMethod))
                        {
                            await writer.WriteLineAsync($"           .Group(Tween.{clip.TweenMethod}(binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), " +
                                                        $"startValue: {GetStartValue(clip)}, endValue: {GetEndValue(clip)}, duration: {clip.Duration}f, startDelay: {clip.DelayTime}f, ease: binding.GetEasing(\"{clip.EaseName}\")))");
                        }
                        else
                        {
                            await writer.WriteLineAsync($"           .Group(Tween.{clip.TweenMethod}(binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), " +
                                                        $"startValue: {GetStartValue(clip)}, endValue: {GetEndValue(clip)}, duration: {clip.Duration}f, startDelay: {clip.DelayTime}f, ease: binding.GetEasing(\"{clip.EaseName}\"), onValueChange: (target, updateValue) => {clip.CustomTweenMethod}))");
                        }
                    }

                    await GenMarkInfoCodeAsync(writer, clip);

                    if (string.IsNullOrEmpty(clip.TweenMethod))
                    {
                        await writer.WriteLineAsync($"           )");
                    }
                }
                else
                {
                    await writer.WriteLineAsync("            .Group(Sequence.Create()");
                    for (int k = 0; k < track.clipInfos.Count; k++)
                    {
                        var clip = track.clipInfos[k];
                        if (!string.IsNullOrEmpty(clip.CustomTweenMethod)
                        || !string.IsNullOrEmpty(clip.TweenMethod))
                        {
                            if (string.IsNullOrEmpty(clip.CustomTweenMethod))
                            {
                                await writer.WriteLineAsync($"                .Chain(Tween.{clip.TweenMethod}(binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), " +
                                                            $"startValue: {GetStartValue(clip)}, endValue: {GetEndValue(clip)}, duration: {clip.Duration}f, startDelay: {clip.DelayTime}f, ease: binding.GetEasing(\"{clip.EaseName}\")))");
                            }
                            else
                            {
                                await writer.WriteLineAsync($"                .Chain(Tween.{clip.TweenMethod}(binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), " +
                                                            $"startValue: {GetStartValue(clip)}, endValue: {GetEndValue(clip)}, duration: {clip.Duration}f, startDelay: {clip.DelayTime}f, ease: binding.GetEasing(\"{clip.EaseName}\"), onValueChange: (target, updateValue) => {clip.CustomTweenMethod}))");
                            }
                        }

                        // Generate code for genMarkInfos
                        await GenMarkInfoCodeAsync(writer, clip);
                    }
                    await writer.WriteLineAsync("            )");
                }

            }

            await writer.WriteLineAsync("            ;");
            await writer.WriteLineAsync("    }");
            await writer.WriteLineAsync();
        }

        private static async Task GenMarkInfoCodeAsync(StreamWriter writer, GenClipInfo clip)
        {
            // Generate code for genMarkInfos
            foreach (var mark in clip.genMarkInfos)
            {
                if (mark.filedName == "PlayAudioClip")
                {
                    await writer.WriteLineAsync($"                .InsertCallback({mark.Time}f, binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), (target) => {{ binding.{mark.filedName}(target, {mark.addictiveValue}, {mark.value});}})");
                }
                else if (mark.filedName == "StopAudioClip")
                {
                    await writer.WriteLineAsync($"                .InsertCallback({mark.Time}f, binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), (target) => target.Stop())");
                }
                else if (mark.filedName == TweenTimelineDefine.IsActiveFieldName)
                {
                    await writer.WriteLineAsync($"                .InsertCallback({mark.Time}f, binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), (target) => {{ target.Fade({mark.value});}})");
                }
                else if (mark.filedName == "sprite")
                {
                    await writer.WriteLineAsync($"                .InsertCallback({mark.Time}f, binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), (target) => {{ binding.SetSprite(target, {mark.value});}})");
                }
                else
                {
                    await writer.WriteLineAsync($"                .InsertCallback({mark.Time}f, binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), (target) => {{ target.{mark.filedName}= {mark.value};}})");
                }
            }
        }

        private async Task GenerateDynamicSequenceCodeAsync(StreamWriter writer, GenTweenSequence sequence)
        {
            string parentTrackName = sequence.parentTrackName;
            var paramsSb = new StringBuilder();
            foreach (var item in sequence.startDynamicParams)
            {
                paramsSb.Append(',');
                paramsSb.Append(GetStartPrams(item.Key, sequence));
            }
            foreach (var item in sequence.endDynamicParams)
            {
                paramsSb.Append(',');
                paramsSb.Append(GetEndPrams(item.Key, sequence));
            }
            paramsSb.AppendLine();

            await writer.WriteLineAsync(@$"    public static Sequence {parentTrackName}Tween(ITweenBinding binding {paramsSb.ToString()},    int cycles = 1, CycleMode cycleMode = CycleMode.Restart, Ease sequenceEase = Ease.Linear)");
            await writer.WriteLineAsync("    {");


            await writer.WriteLineAsync("        return Sequence.Create(cycles, cycleMode, sequenceEase)");

            for (int j = 0; j < sequence.trackInfos.Count; j++)
            {
                var track = sequence.trackInfos[j];
                if (track.clipInfos.Count < 1)
                {
                    continue;
                }

                if (track.clipInfos.Count == 1)
                {
                    var clip = track.clipInfos[0];
                    if (string.IsNullOrEmpty(clip.CustomTweenMethod)
                    && string.IsNullOrEmpty(clip.TweenMethod))
                    {
                        await writer.WriteLineAsync($"           .Group(Sequence.Create()");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(clip.CustomTweenMethod))
                        {
                            await writer.WriteLineAsync($"           .Group(Tween.{clip.TweenMethod}(binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), " +
                                                        $"startValue: {GetStartValue(clip, sequence)}, endValue: {GetEndValue(clip, sequence)}, duration: {clip.Duration}f, startDelay: {clip.DelayTime}f, ease: binding.GetEasing(\"{clip.EaseName}\")))");
                        }
                        else
                        {
                            await writer.WriteLineAsync($"           .Group(Tween.{clip.TweenMethod}(binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), " +
                                                        $"startValue: {GetStartValue(clip, sequence)}, endValue: {GetEndValue(clip, sequence)}, duration: {clip.Duration}f, startDelay: {clip.DelayTime}f, ease: binding.GetEasing(\"{clip.EaseName}\"), onValueChange: (target, updateValue) => {clip.CustomTweenMethod}))");
                        }
                    }

                    // Generate code for genMarkInfos
                    await GenMarkInfoCodeAsync(writer, clip);

                    if (string.IsNullOrEmpty(clip.TweenMethod))
                    {
                        await writer.WriteLineAsync($"           )");
                    }
                }
                else
                {
                    await writer.WriteLineAsync("            .Group(Sequence.Create()");
                    for (int k = 0; k < track.clipInfos.Count; k++)
                    {
                        var clip = track.clipInfos[k];
                        if (!string.IsNullOrEmpty(clip.CustomTweenMethod)
                             || !string.IsNullOrEmpty(clip.TweenMethod))
                        {
                            if (string.IsNullOrEmpty(clip.CustomTweenMethod))
                            {
                                await writer.WriteLineAsync($"                .Chain(Tween.{clip.TweenMethod}(binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), " +
                                                            $"startValue: {GetStartValue(clip, sequence)}, endValue: {GetEndValue(clip, sequence)}, duration: {clip.Duration}f, startDelay: {clip.DelayTime}f, ease: binding.GetEasing(\"{clip.EaseName}\")))");
                            }
                            else
                            {
                                await writer.WriteLineAsync($"                .Chain(Tween.{clip.TweenMethod}(binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), " +
                                                            $"startValue: {GetStartValue(clip, sequence)}, endValue: {GetEndValue(clip, sequence)}, duration: {clip.Duration}f, startDelay: {clip.DelayTime}f, ease: binding.GetEasing(\"{clip.EaseName}\"), onValueChange: (target, updateValue) => {clip.CustomTweenMethod}))");
                            }
                        }

                        // Generate code for genMarkInfos
                        await GenMarkInfoCodeAsync(writer, clip);
                    }
                    await writer.WriteLineAsync("            )");
                }

            }

            await writer.WriteLineAsync("            ;");
            await writer.WriteLineAsync("    }");
            await writer.WriteLineAsync();
        }

        private static string GetStartValue(GenClipInfo clip, GenTweenSequence sequence)
        {
            if (sequence.startDynamicParams.TryGetValue(clip, out var paramsName))
            {
                return paramsName;
            }

            return UnityValueFormatter.FormatValue(clip.StartValue);
        }

        private static string GetEndValue(GenClipInfo clip, GenTweenSequence sequence)
        {
            if (sequence.endDynamicParams.TryGetValue(clip, out var paramsName))
            {
                return paramsName;
            }
            return UnityValueFormatter.FormatValue(clip.EndValue);
        }

        private static string GetStartValue(GenClipInfo clip)
        {
            return UnityValueFormatter.FormatValue(clip.StartValue);
        }

        private static string GetEndValue(GenClipInfo clip)
        {
            return UnityValueFormatter.FormatValue(clip.EndValue);
        }

        private string GetParamsStartPosName(GenClipInfo clip, GenTweenSequence sequence, int index)
        {
            string typeName = clip.StartValue.GetType().Name;
            return $"{clip.BindName}_Start{typeName}_{index}";
        }

        private string GetParamsEndPosName(GenClipInfo clip, GenTweenSequence sequence, int index)
        {
            string typeName = clip.StartValue.GetType().Name;
            return $"{clip.BindName}_End{typeName}_{index}";
        }

        private static string GetEndPrams(GenClipInfo clip, GenTweenSequence sequence)
        {
            if (!sequence.endDynamicParams.TryGetValue(clip, out var paramsName))
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            sb.Append(clip.EndValue.GetType().FullName);
            sb.Append(" ");
            sb.Append(paramsName);
            if (TypeConverter.CanBeConst(clip.EndValue.GetType()))
            {
                sb.Append(" = ");
                return UnityValueFormatter.FormatValue(clip.EndValue);
            }

            return sb.ToString();
        }

        private static string GetStartPrams(GenClipInfo clip, GenTweenSequence sequence)
        {
            if (!sequence.startDynamicParams.TryGetValue(clip, out var paramsName))
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            sb.Append(clip.StartValue.GetType().FullName);
            sb.Append(" ");
            sb.Append(paramsName);
            if (TypeConverter.CanBeConst(clip.StartValue.GetType()))
            {
                sb.Append(" = ");
                return UnityValueFormatter.FormatValue(clip.StartValue);
            }

            return sb.ToString();
        }

        public async Task GenerateTweenExtensionCodeAsync(List<GenTweenSequence> sequences, StreamWriter writer)
        {
            await writer.WriteLineAsync("using Cr7Sund.TweenTimeLine;");
            await writer.WriteLineAsync("using PrimeTween;");
            await writer.WriteLineAsync("using UnityEngine;");
            await writer.WriteLineAsync();
            await writer.WriteLineAsync("public static class ITweenBindingExtenstion");
            await writer.WriteLineAsync("{");

            await writer.WriteLineAsync("        public static Sequence Play(this ITweenBinding tweenBinding, string tweenBehaviour, int cycles = 1, CycleMode cycleMode = CycleMode.Restart, Ease sequenceEase = Ease.Linear)");
            await writer.WriteLineAsync("{");

            for (int i = 0; i < sequences.Count; i++)
            {
                var sequence = sequences[i];
                float progress = (i + 1) / (float)sequences.Count * 0.5f + 0.5f; // Progress from 0.5 to 1.0

                string parentTrackName = sequence.parentTrackName;
                EditorUtility.DisplayProgressBar("Generating Code", $"Generating {parentTrackName}Tween Extension", progress);
                await writer.WriteLineAsync($"    if (tweenBehaviour == nameof(GenerateTween. {parentTrackName}Tween))");
                await writer.WriteLineAsync("    {");
                await writer.WriteLineAsync($"       return GenerateTween.{parentTrackName}Tween(tweenBinding, cycles, cycleMode, sequenceEase);");
                await writer.WriteLineAsync("    }");
            }
            await writer.WriteLineAsync("            return Sequence.Create();");

            await writer.WriteLineAsync("}");
            await writer.WriteLineAsync("}");
            EditorUtility.ClearProgressBar();
        }

        public static string GetGenSequenceName(TrackAsset trackAsset)
        {
            string sequenceName = null;
            TrackAsset parent = trackAsset;
            var timelineAssetName = trackAsset.timelineAsset.name;

            while (parent != null)
            {
                string parentTrackName = parent.name;

                if (parentTrackName.EndsWith(TweenTimelineDefine.PanelTag)
                    || parentTrackName.EndsWith(TweenTimelineDefine.CompositeTag))
                {
                    if (parent.parent.name == TweenTimelineDefine.InDefine
                        || parent.parent.name == TweenTimelineDefine.OutDefine)
                    {
                        sequenceName = parentTrackName;
                        break;
                    }
                }

                bool contains = false;
                foreach (var item in TweenTimelineDefine.UIComponentTypeMatch)
                {
                    if (parentTrackName.EndsWith(item.Key))
                    {
                        if (parent.parent.name == TweenTimelineDefine.InDefine
                             || parent.parent.name == TweenTimelineDefine.OutDefine)
                        {
                            sequenceName = parentTrackName;
                            contains = true;
                            break;
                        }
                    }
                }
                if (contains)
                {
                    break;
                }
                parent = parent.parent as GroupTrack;
            }

            if (string.IsNullOrEmpty(sequenceName))
            {
                return sequenceName;
            }
            // Assert.IsNotNull(sequenceName, $"{trackAsset.name} should be endWith suitable postFix");

            if (sequenceName != timelineAssetName)
            {
                if (sequenceName.EndsWith(TweenTimelineDefine.PanelTag)
                               || sequenceName.EndsWith(TweenTimelineDefine.CompositeTag))
                {
                    Assert.IsTrue(timelineAssetName.EndsWith(TweenTimelineDefine.PanelTag)
                                                       || timelineAssetName.EndsWith(TweenTimelineDefine.CompositeTag)
                    ,$"{sequenceName} _ {timelineAssetName}");
                    sequenceName = $"{sequenceName}_{timelineAssetName}";
                }
            }
            if (parent != null)
            {
                var root = parent.parent as GroupTrack;
                if (root != null)
                {
                    Assert.IsTrue(root.name == TweenTimelineDefine.InDefine
                     || root.name == TweenTimelineDefine.OutDefine, $"the {parent.name} 's parent track shouble be in or out");

                    sequenceName = $"{sequenceName}_{root.name}";
                }
            }

            Assert.IsNotNull(sequenceName, $"{trackAsset.name} dont have parentTrack");


            return sequenceName;
        }

        private async Task ConstructMethodContentDict()
        {
            // string builtInTweenMethods = @"C:\Users\liux4\Documents\UnityProjects\MyMiniGame\Assets\Plugins\TweenTimeline\Editor\Customs";
            await constructMethodDict(TweenTimelineDefine.CustomControlTacksFolder);
            await constructMethodDict(TweenTimelineDefine.BuiltInControlTacksFolder);

            async Task constructMethodDict(string controlFolder)
            {
                await ConstructContentDict(controlFolder);
                EditorUtility.ClearProgressBar();
            }
        }

        private async Task ConstructContentDict(string tweenBehaviorMethods)
        {
            var files = Directory.EnumerateFiles(tweenBehaviorMethods, "*.cs", SearchOption.AllDirectories);

            int index = 0;
            foreach (var filePath in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                if (!fileName.EndsWith("Behaviour"))
                {
                    continue;
                }
                EditorUtility.DisplayProgressBar("Generating Code", $"Processing file {fileName}", (index + 1) / (float)1000);
                var methodContent = await AnalysisReturn(filePath);
                string[] strings = methodContent.Split("^^");
                if (strings.Length > 1)
                {
                    _contentDict.TryAdd(fileName, new Tuple<string, string>(strings[0], strings[1]));
                }
                else
                {
                    _contentDict.TryAdd(fileName, new Tuple<string, string>(strings[0], string.Empty));
                }
            }
        }

        private async Task<string> AnalysisReturn(string path)
        {
            var code = await File.ReadAllTextAsync(path);
            var tree = CSharpSyntaxTree.ParseText(code);
            var root = tree.GetRoot();

            var methodNode = root.DescendantNodes().OfType<MethodDeclarationSyntax>().FirstOrDefault();

            if (methodNode != null)
            {
                var returnStatement = methodNode.DescendantNodes().OfType<ReturnStatementSyntax>().FirstOrDefault();
                if (returnStatement != null)
                {
                    string analysisReturn = _tweenPatternRegex.Match(returnStatement.ToString()).Groups[1].Value;
                    if (analysisReturn == "Custom")
                    {
                        var onValueChangeMatch = _onValueChangeRegex.Match(returnStatement.ToString());
                        var onValueChangeContent = onValueChangeMatch.Success ? onValueChangeMatch.Groups[1].Value.Trim() : string.Empty;
                        // Remove trailing ");" if present
                        if (onValueChangeContent.EndsWith(");"))
                        {
                            onValueChangeContent = onValueChangeContent.Substring(0, onValueChangeContent.Length - 2).Trim();
                        }
                        analysisReturn = $"{analysisReturn}^^{onValueChangeContent}";
                    }
                    return analysisReturn;
                }
            }

            return string.Empty;
        }

        public Dictionary<string, GenTweenSequence> ConstructAllTweenSequence()
        {
            _easingTokenPresetLibrary = AssetDatabase.LoadAssetAtPath<EasingTokenPresetLibrary>(TweenTimelineDefine.easingTokenPresetsPath);
            TweenConfigCacher.CacheTweenConfigs();

            string inputFilePath = TweenTimelineDefine.EditorDataSourcePath;
            var assetGUIDs = AssetDatabase.FindAssets("t:TimelineAsset", new[]
            {
                inputFilePath
            });
            var resultSequences = new Dictionary<string, GenTweenSequence>();

            foreach (var assetID in assetGUIDs)
            {
                var asset = AssetDatabase.LoadAssetAtPath<TimelineAsset>(AssetDatabase.GUIDToAssetPath(assetID));
                if (asset is TimelineAsset timelineAsset)
                {
                    IterateTimelineAsset(resultSequences, timelineAsset);
                }
            }

            return resultSequences;
        }

        public void IterateTimelineAsset(Dictionary<string, GenTweenSequence> resultSequences, TimelineAsset timelineAsset)
        {
            var tracks = timelineAsset.GetRootTracks();

            TimelineWindowExposer.IterateClips((clipAsset, trackAsset) =>
                {
                    string tweenGenName = GetGenSequenceName(trackAsset.parent as GroupTrack);
                    if (string.IsNullOrEmpty(tweenGenName))
                    {
                        return;
                    }
                    if (!resultSequences.TryGetValue(tweenGenName, out var sequence))
                    {
                        sequence = new GenTweenSequence();
                        sequence.parentTrackName = tweenGenName;
                        sequence.trackInfos = new List<GenTrackInfo>();
                        resultSequences.Add(tweenGenName, sequence);
                    }

                    GenTrackInfo trackInfo = null;
                    int instanceID = trackAsset.GetInstanceID();

                    if (trackAsset is AnimationTrack customAnimationTrack)
                    {
                        GenAnimTrackInfo(clipAsset, sequence,
                         instanceID, _contentDict);
                    }
                    else
                    {
                        var trackIndex = sequence.trackInfos.FindIndex((trackInfo) => trackInfo.InstanceID.Equals(trackAsset.GetInstanceID().ToString()));
                        if (trackIndex < 0)
                        {
                            trackInfo = new GenTrackInfo(instanceID);
                            sequence.trackInfos.Add(trackInfo);
                        }
                        else
                        {
                            trackInfo = sequence.trackInfos[trackIndex];
                        }
                    }

                    if (trackAsset is AnimationTrack)
                    {
                    }
                    else if (trackAsset is AudioTrack)
                    {
                        GenAudioSequence(trackInfo, clipAsset, trackAsset);
                    }
                    else
                    {
                        GenCustomSequence(trackInfo, clipAsset, trackAsset);
                    }
                },
                tracks);

            foreach (var sequenceKeyParis in resultSequences)
            {
                GenTweenSequence sequence = sequenceKeyParis.Value;

                foreach (var genTrack in sequence.trackInfos)
                {
                    foreach (var genClip in genTrack.clipInfos)
                    {
                        if (genClip.IsDynamicPos)
                        {
                            sequence.endDynamicParams.Add(genClip, GetParamsEndPosName(genClip, sequence, sequence.endDynamicParams.Count));
                            sequence.startDynamicParams.Add(genClip, GetParamsStartPosName(genClip, sequence, sequence.startDynamicParams.Count));
                        }
                    }
                }
            }
        }

        private void GenAnimTrackInfo(TimelineClip timelineClip, GenTweenSequence sequence,
         int instanceID, Dictionary<string, Tuple<string, string>> contentDict)
        {
            // trackInfo = new GenTrackInfo(instanceID.ToString() + propertyName);
            var clip = timelineClip.animationClip;

            AnimationClipConverter.CreateCurve(clip, _easingTokenPresetLibrary);

            var keyframeDatas = AnimationClipConverter.GenerateKeyFrameDatas(clip);
            sequence.trackInfos.AddRange(
               AnimationClipConverter.ConstructGenTracks(clip
                 , _easingTokenPresetLibrary, keyframeDatas, timelineClip.start, instanceID, contentDict));

            keyframeDatas = AnimationClipConverter.GenerateObjectKeyFrameDatas(clip);
            sequence.trackInfos.AddRange(
                  AnimationClipConverter.ConstructGenTracks(clip, _easingTokenPresetLibrary,
                  keyframeDatas, timelineClip.start, instanceID, contentDict));
        }

        private void GenAudioSequence(GenTrackInfo trackInfo, TimelineClip clipAsset, TrackAsset trackAsset)
        {
            if (clipAsset.asset is AudioPlayableAsset audioAsset)
            {
                GenClipInfo clipInfo = null;
                if (trackInfo.clipInfos.Count < 1)
                {
                    clipInfo = new GenClipInfo();
                    trackInfo.clipInfos.Add(clipInfo);
                    // clipInfo.DelayTime = (float)trackAsset.start;
                    // clipInfo.Duration = (float)trackAsset.duration;
                    // clipInfo.StartValue = (float)clipAsset.clipIn;
                    clipInfo.BindType = typeof(UnityEngine.AudioSource).ToString();
                    clipInfo.BindName = trackAsset.name;
                    clipInfo.TweenMethod = string.Empty;

                    var endMarkInfo = new GenMarkInfo();
                    endMarkInfo.filedName = "StopAudioClip";
                    endMarkInfo.Time = (float)trackAsset.end;
                    trackInfo.clipInfos[0].genMarkInfos.Add(endMarkInfo);
                }

                var markInfo = new GenMarkInfo();
                markInfo.filedName = "PlayAudioClip";
                markInfo.Time = (float)clipAsset.start;
                markInfo.value = TweenTimeLine.GenMarkInfo.ConvertValue((float)clipAsset.clipIn);
                markInfo.addictiveValue = TweenTimeLine.GenMarkInfo.ConvertValue(audioAsset.clip.name);
                trackInfo.clipInfos[0].genMarkInfos.Add(markInfo);
            }
        }

        private void GenCustomSequence(GenTrackInfo trackInfo, TimelineClip clipAsset, TrackAsset trackAsset)
        {
            if (TimelineWindowExposer.GetBehaviourValue(clipAsset.asset, out var value))
            {
                var behaviour = value as IUniqueBehaviour;
                // var componentType = AniActionEditToolHelper.GetFirstGenericType(behaviour.GetType()).ToString();
                string behaviourTypeName = behaviour.GetType().Name;
                if (_contentDict.ContainsKey(behaviourTypeName))
                {
                    GenCustomSteps(trackInfo, clipAsset, trackAsset, behaviour);
                }
                else
                {
                    //e.g. empty track
                    GenMakerSteps(trackInfo, clipAsset, trackAsset, behaviour);
                }
            }
        }

        private void GenCustomSteps(GenTrackInfo trackInfo, TimelineClip clipAsset, TrackAsset trackAsset,
         IUniqueBehaviour behaviour)
        {
            string behaviourTypeName = behaviour.GetType().Name;

            BaseEasingTokenPreset easePreset = behaviour.EasePreset;

            if (easePreset == null)
            {
                throw new Exception($"clipAsset has null easePreset,  {behaviour.BindTarget} {clipAsset.displayName}, {trackAsset.timelineAsset.name}");
            }

            // we need to remove the delay time of TimeLineManager
            // since we the ui sequene should be async
            // float start = (float)(clipAsset.start - trackAsset.start);
            float start = (float)clipAsset.start;
            var methodContent = _contentDict[behaviourTypeName];
            string tweenMethod = methodContent.Item1;
            string customTweenMethod = methodContent.Item2;

            var genClipInfo = new GenClipInfo
            {
                DelayTime = start,
                Duration = (float)clipAsset.duration,
                EndValue = behaviour.EndPos,
                StartValue = behaviour.StartPos,
                IsDynamicPos = behaviour.IsDynamicPos,
                BindType = behaviour.BindType,
                BindName = behaviour.BindTarget,
                TweenMethod = tweenMethod,
                CustomTweenMethod = customTweenMethod,
                EaseName = easePreset.Name,
            };

            foreach (IMarker marker in trackAsset.GetMarkers())
            {
                if (marker is IValueMaker valueMaker
                    && marker.time >= clipAsset.start && marker.time <= clipAsset.end)
                {
                    genClipInfo.genMarkInfos.Add(new GenMarkInfo(valueMaker));
                }
            }
            trackInfo.clipInfos.Add(genClipInfo);
        }


        private void GenMakerSteps(GenTrackInfo trackInfo, TimelineClip clipAsset, TrackAsset trackAsset
          , IUniqueBehaviour behaviour)
        {
            float start = (float)clipAsset.start;

            var genClipInfo = new GenClipInfo
            {
                DelayTime = start,
                BindType = behaviour.BindType,
                BindName = behaviour.BindTarget,
            };

            foreach (IMarker marker in trackAsset.GetMarkers())
            {
                if (marker is IValueMaker valueMaker
                    && marker.time >= clipAsset.start && marker.time <= clipAsset.end)
                {
                    genClipInfo.genMarkInfos.Add(new GenMarkInfo(valueMaker));
                }
            }
            trackInfo.clipInfos.Add(genClipInfo);
        }


    }
}
