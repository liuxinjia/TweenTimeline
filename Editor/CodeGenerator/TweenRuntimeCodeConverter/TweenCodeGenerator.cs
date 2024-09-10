using System.Collections.Generic;
using UnityEngine;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.IO;
using System.Text;
using UnityEditor;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Cr7Sund.TweenTimeLine.Editor;
using UnityEngine.Timeline;
using System;
using Cr7Sund.Timeline.Extension;

namespace Cr7Sund.TweenTimeLine
{
    public class GenTweenSequence
    {
        public string parentTrackName;
        public List<GenTrackInfo> trackInfos;
    }

    public class GenTrackInfo
    {
        public List<GenClipInfo> clipInfos = new();
        public string InstanceID;

        public GenTrackInfo(int instanceID)
        {
            this.InstanceID = instanceID.ToString();
        }
        public GenTrackInfo(string instanceID)
        {
            this.InstanceID = instanceID.ToString();
        }
    }

    public class GenClipInfo
    {
        public float DelayTime; // start- prevClip
        public float Duration;
        public object EndValue;
        public object StartValue;
        public string TweenMethod;
        public string EaseName;
        public string BindType;
        public string BindName;
        public List<GenMarkInfo> genMarkInfos = new();

    }

    public class GenMarkInfo
    {
        public float Time;
        public string value;
        public string addictiveValue;
        public string filedName;

        public GenMarkInfo()
        {

        }

        public GenMarkInfo(IValueMaker valueMaker)
        {
            this.Time = (float)valueMaker.time;
            this.value = ConvertValue(valueMaker.Value);
            this.filedName = valueMaker.FieldName;
        }
        public static string ConvertValue(object value)
        {
            if (value is bool boolValue)
            {
                return boolValue.ToString().ToLower(); // Convert bool to "true" or "false"
            }
            else if (value is string strValue)
            {
                return $"\"{strValue}\""; // Wrap string in quotes
            }
            else if (value is float floatValue)
            {
                return $"{floatValue}f";
            }
            return value.ToString(); // Default conversion
        }
    }

    public class TweenCodeGenerator
    {
        private const string inputFilePath = @"Assets/Plugins/TweenTimeline/Editor/Sample/Datas";
        private const string outFilePath = @"C:\Users\liux4\Documents\UnityProjects\MyMiniGame\Assets\Plugins\TweenTimeline\Editor\Sample\CodeGens";
        private const string TweenBehaviorMethods = @"C:\Users\liux4\Documents\UnityProjects\MyMiniGame\Assets\Plugins\TweenTimeline\Editor\Customs\";

        private Dictionary<string, string> _contentDict = new();
        private CurveLibraryCenter curveLibrary;
        private Regex _tweenPatternRegex = new Regex(@"PrimeTween\.Tween\.(\w+)");

        [MenuItem("Tools/GenerateRunTimeCode")]
        public static void Test()
        {
            var instance = new TweenCodeGenerator();
            instance.Generate();
        }

        public async void Generate()
        {
            EditorUtility.DisplayProgressBar("Generating Code", "Convert Animation Clip Curves", 0f);
            ConstructAnimationClipCurves();
            EditorUtility.ClearProgressBar();

            EditorUtility.DisplayProgressBar("Generating Code", "Constructing Method Content Dictionary", 0f);
            await ConstructMethodContentDict();
            EditorUtility.ClearProgressBar();

            EditorUtility.DisplayProgressBar("Generating Code", "Constructing Tween Sequences", 0.5f);
            var sequences = ConstructAllTweenSequence();
            EditorUtility.ClearProgressBar();

            EditorUtility.DisplayProgressBar("Generating Code", "Creating Tween Code File", 1f);
            await CreateTweenGenFile(sequences);
            EditorUtility.ClearProgressBar();
        }

        public async Task CreateTweenGenFile(Dictionary<string, GenTweenSequence> sequences)
        {
            string file = Path.Combine(outFilePath, "GenerateTween.cs");

            await using var writeStream = new StreamWriter(file);
            await GenerateTweenCodeAsync(sequences.Values.ToList(), writeStream);
        }

        public async Task GenerateTweenCodeAsync(List<GenTweenSequence> sequences, StreamWriter writer)
        {
            await writer.WriteLineAsync("using Cr7Sund.TweenTimeLine;");
            await writer.WriteLineAsync("using PrimeTween;");
            await writer.WriteLineAsync("using UnityEngine;");
            await writer.WriteLineAsync();
            await writer.WriteLineAsync("public static class GenerateTween");
            await writer.WriteLineAsync("{");

            for (int i = 0; i < sequences.Count; i++)
            {
                var sequence = sequences[i];
                float progress = (i + 1) / (float)sequences.Count * 0.5f + 0.5f; // Progress from 0.5 to 1.0

                EditorUtility.DisplayProgressBar("Generating Code", $"Generating {sequence.parentTrackName}Tween", progress);

                await writer.WriteLineAsync($"    public static void {sequence.parentTrackName}Tween(ITweenBinding binding)");
                await writer.WriteLineAsync("    {");

                // Multiple trackInfos, use Sequence.Create()
                await writer.WriteLineAsync("        Sequence.Create()");

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
                        if (string.IsNullOrEmpty(clip.TweenMethod))
                        {
                            await writer.WriteLineAsync($"           .Group(Sequence.Create()");
                        }
                        else
                        {
                            await writer.WriteLineAsync($"           .Group(Tween.{clip.TweenMethod}(binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), " +
                                                         $"startValue: {UnityValueFormatter.FormatValue(clip.StartValue)}, endValue: {UnityValueFormatter.FormatValue(clip.EndValue)}, duration: {clip.Duration}f, startDelay: {clip.DelayTime}f, ease: binding.GetEasing(\"{clip.EaseName}\")))");
                        }

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
                            else
                            {
                                await writer.WriteLineAsync($"                .InsertCallback({mark.Time}f, binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), (target) => {{ target.{mark.filedName}= {mark.value};}})");
                            }
                        }

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
                            await writer.WriteLineAsync($"                .Chain(Tween.{clip.TweenMethod}(binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), " +
                                                        $"startValue: {UnityValueFormatter.FormatValue(clip.StartValue)}, endValue: {UnityValueFormatter.FormatValue(clip.EndValue)}, duration: {clip.Duration}f, startDelay: {clip.DelayTime}f, ease: binding.GetEasing(\"{clip.EaseName}\")))");

                            // Generate code for genMarkInfos
                            foreach (var mark in clip.genMarkInfos)
                            {
                                await writer.WriteLineAsync($"                .InsertCallback({mark.Time}f, binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), (target) => {{ target.{mark.filedName}= {mark.value};}})");
                            }
                        }
                        await writer.WriteLineAsync("            )");
                    }

                }

                await writer.WriteLineAsync("            ;");
                await writer.WriteLineAsync("    }");
                await writer.WriteLineAsync();
            }

            await writer.WriteLineAsync("}");
            EditorUtility.ClearProgressBar();
        }

        private static string GetGenSequenceName(TrackAsset trackAsset)
        {
            string parentTrackName = string.Empty;
            var parent = trackAsset.parent as GroupTrack;
            if (parent != null)
            {
                parentTrackName += parent.name;
                var root = parent.parent as GroupTrack;
                if (root != null)
                {
                    parentTrackName += root.name;
                }
            }

            return parentTrackName;
        }

        private async Task ConstructMethodContentDict()
        {
            var files = Directory.EnumerateFiles(TweenBehaviorMethods, "*.cs", SearchOption.AllDirectories).ToList();

            for (int i = 0; i < files.Count; i++)
            {
                string filePath = files[i];
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                if (!fileName.EndsWith("Behaviour"))
                {
                    continue;
                }
                EditorUtility.DisplayProgressBar("Generating Code", $"Processing file {fileName}", (i + 1) / (float)files.Count);
                var methodContent = await AnalysisReturn(filePath);
                _contentDict.TryAdd(fileName, methodContent);
            }
            EditorUtility.ClearProgressBar();
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
                    return _tweenPatternRegex.Match(returnStatement.ToString()).Groups[1].Value;
                }
            }

            return string.Empty;
        }

        public Dictionary<string, GenTweenSequence> ConstructAllTweenSequence()
        {
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
                    string tweenGenName = GetGenSequenceName(trackAsset);
                    if (!resultSequences.TryGetValue(tweenGenName, out var sequence))
                    {
                        sequence = new GenTweenSequence();
                        sequence.parentTrackName = tweenGenName;
                        sequence.trackInfos = new List<GenTrackInfo>();
                        resultSequences.Add(tweenGenName, sequence);
                    }

                    GenTrackInfo trackInfo = null;
                    int instanceID = trackAsset.GetInstanceID();

                    if (trackAsset is CustomAnimationTrack customAnimationTrack)
                    {
                        var customClipAsset = clipAsset.asset as CustomAnimationPlayableAsset;
                        var keyframeDatas = AnimationClipConverter.GenerateKeyFrameDatas(customClipAsset.clip);
                        var properties = AnimationClipConverter.GenTrackInfos(customClipAsset.bindTarget, keyframeDatas);
                        foreach (var propertyName in properties)
                        {
                            trackInfo = new GenTrackInfo(instanceID.ToString() + propertyName);
                            GenAnimationSequence(trackInfo, propertyName, clipAsset, trackAsset, keyframeDatas);
                            sequence.trackInfos.Add(trackInfo);
                        }
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

                    if (trackAsset is CustomAnimationTrack)
                    {
                    }
                    else if (trackAsset is CustomAudioTrack)
                    {
                        GenAudioSequence(trackInfo, clipAsset, trackAsset);
                    }
                    else
                    {
                        GenCustomSequence(trackInfo, clipAsset, trackAsset);
                    }
                },
                tracks);

        }

        private void GenAnimationSequence(GenTrackInfo trackInfo, string propertyName, TimelineClip clipAsset, TrackAsset trackAsset, KeyframeDataWrapper keyWrapper)
        {
            if (clipAsset.asset is CustomAnimationPlayableAsset animationPlayableAsset)
            {
                var customClipAsset = clipAsset.asset as CustomAnimationPlayableAsset;
                var clipInfo = AnimationClipConverter.GenClipInfo(animationPlayableAsset.bindTarget, propertyName, customClipAsset.clip, keyWrapper);
                clipInfo.BindName = animationPlayableAsset.bindTarget;
                clipInfo.DelayTime = (float)clipAsset.start;
                trackInfo.clipInfos.Add(clipInfo);
            }
        }

        private void GenAudioSequence(GenTrackInfo trackInfo, TimelineClip clipAsset, TrackAsset trackAsset)
        {
            if (clipAsset.asset is CustomAudioPlayableAsset audioAsset)
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
                    clipInfo.BindName = audioAsset.bindTarget;
                    clipInfo.TweenMethod = string.Empty;

                    var endMarkInfo = new GenMarkInfo();
                    endMarkInfo.filedName = "StopAudioClip";
                    endMarkInfo.Time = (float)trackAsset.end;
                    trackInfo.clipInfos[0].genMarkInfos.Add(endMarkInfo);
                }

                var markInfo = new GenMarkInfo();
                markInfo.filedName = "PlayAudioClip";
                markInfo.Time = (float)clipAsset.start;
                markInfo.value = GenMarkInfo.ConvertValue((float)clipAsset.clipIn);
                markInfo.addictiveValue = GenMarkInfo.ConvertValue(audioAsset.clip.name);
                trackInfo.clipInfos[0].genMarkInfos.Add(markInfo);
            }
        }

        private void GenCustomSequence(GenTrackInfo trackInfo, TimelineClip clipAsset, TrackAsset trackAsset)
        {
            if (TimelineWindowExposer.GetBehaviourValue(clipAsset.asset, out var value))
            {
                var behaviour = value as IUniqueBehaviour;
                // var componentType = AniActionEditToolHelper.GetFirstGenericType(behaviour.GetType()).ToString();
                var methodContent = _contentDict[behaviour.GetType().Name];
                var genClipInfo = new GenClipInfo
                {
                    DelayTime = (float)clipAsset.start,
                    Duration = (float)clipAsset.duration,
                    EndValue = behaviour.EndPos,
                    StartValue = behaviour.StartPos,
                    BindType = behaviour.BindType,
                    BindName = behaviour.BindTarget,
                    TweenMethod = methodContent,
                    EaseName = behaviour.EasePreset.Name,
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

        private void ConstructAnimationClipCurves()
        {
            var guids = AssetDatabase.FindAssets("t:AnimationClip", new string[] { inputFilePath });
            curveLibrary = new CurveLibraryCenter();
            curveLibrary.CreateCurveWrapperLibrary();
            AnimationClipConverter.InitTweenConfigs();

            for (int i = 0; i < guids.Length; i++)
            {
                string filePath = AssetDatabase.GUIDToAssetPath(guids[i]);
                var fileName = Path.GetFileNameWithoutExtension(filePath);

                EditorUtility.DisplayProgressBar("Generate ClipCurve", $"Processing file {fileName}", (i + 1) / (float)guids.Length);
                var clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(filePath);
                curveLibrary.ConvertClip(clip);
            }
            curveLibrary.GenCurveInfoDict();
            EditorUtility.ClearProgressBar();
        }
    }
}
