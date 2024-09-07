using System.Collections.Generic;
using PrimeTween;
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
        public List<GenClipInfo> clipInfos;
        public TrackAsset trackAsset;
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
    }

    public interface ITweenBinding
    {
        public T GetBindObj<T>(string name);
        public Easing GetEasing(string curName);
        // binding.GetBindObj<RectTransform>(");
    }

    public class TweenCodeGenerator
    {
        private const string inputFilePath = @"Assets/Plugins/TweenTimeline/Editor/Sample/Datas";
        private const string outFilePath = @"C:\Users\liux4\Documents\UnityProjects\MyMiniGame\Assets\Plugins\TweenTimeline\Editor\Sample\CodeGens";
        private const string TweenBehaviourMethods = @"C:\Users\liux4\Documents\UnityProjects\MyMiniGame\Assets\Plugins\TweenTimeline\Editor\Customs\";
        private Dictionary<string, string> _contentDict = new();
        private Regex _tweenPatternRegex = new Regex(@"PrimeTween\.Tween\.(\w+)");

        [MenuItem("Tools/GenerateRunTimeCode")]
        public static void Test()
        {
            var instance = new TweenCodeGenerator();
            instance.Genearte();
        }

        public async void Genearte()
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
                        await writer.WriteLineAsync($"           .Group(Tween.{clip.TweenMethod}(binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), " +
                                                    $"startValue: {UnityValueFormatter.FormatValue(clip.StartValue)}, endValue: {UnityValueFormatter.FormatValue(clip.EndValue)}, duration: {clip.Duration}f, startDelay: {clip.DelayTime}f, ease: binding.GetEasing(\"{clip.EaseName}\"))");

                    }
                    else
                    {
                        await writer.WriteLineAsync("            .Group(Sequence.Create()");
                        for (int k = 0; k < track.clipInfos.Count; k++)
                        {
                            var clip = track.clipInfos[k];
                            await writer.WriteLineAsync($"                .Chain(Tween.{clip.TweenMethod}(binding.GetBindObj<{clip.BindType}>(\"{clip.BindName}\"), " +
                                                        $"startValue: {UnityValueFormatter.FormatValue(clip.StartValue)}, endValue: {UnityValueFormatter.FormatValue(clip.EndValue)}, duration: {clip.Duration}f, startDelay: {clip.DelayTime}f, ease: binding.GetEasing(\"{clip.EaseName}\")))");
                        }
                    }

                    await writer.WriteLineAsync("            )");
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
            var files = Directory.EnumerateFiles(TweenBehaviourMethods, "*.cs", SearchOption.AllDirectories).ToList();

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
            var assetGUIDs = AssetDatabase.FindAssets("", new[]
            {
                inputFilePath
            });
            var resultSequences = new Dictionary<string, GenTweenSequence>();

            foreach (var assetID in assetGUIDs)
            {
                var asset = AssetDatabase.LoadAssetAtPath<TimelineAsset>(AssetDatabase.GUIDToAssetPath(assetID));
                if (asset is TimelineAsset timelineAsset)
                {
                    IterateTimlineAsset(resultSequences, timelineAsset);
                }
            }

            return resultSequences;
        }
        public void IterateTimlineAsset(Dictionary<string, GenTweenSequence> resultSequences, TimelineAsset timelineAsset)
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
                    var trackIndex = sequence.trackInfos.FindIndex((trackInfo) => trackInfo.Equals(trackAsset));
                    if (trackIndex < 0)
                    {
                        trackInfo = new GenTrackInfo();
                        trackInfo.clipInfos = new List<GenClipInfo>();
                        trackInfo.trackAsset = trackAsset;
                        sequence.trackInfos.Add(trackInfo);
                    }
                    else
                    {
                        trackInfo = sequence.trackInfos[trackIndex];
                    }

                    if (TimelineWindowExposer.GetBehaviourValue(clipAsset.asset, out var value))
                    {
                        var behaviour = value as IUniqueBehaviour;
                        var componentType = AniActionEditToolHelper.GetFirstGenericType(behaviour.GetType()).ToString();
                        var methodContent = _contentDict[behaviour.GetType().Name];
                        var genClipInfo = new GenClipInfo
                        {
                            DelayTime = (float)clipAsset.start,
                            Duration = (float)clipAsset.duration,
                            EndValue = behaviour.EndPos,
                            StartValue = behaviour.StartPos,
                            BindType = componentType,
                            BindName = behaviour.BindTarget,
                            TweenMethod = methodContent,
                            EaseName = behaviour.EasePreset.Name,
                        };

                        trackInfo.clipInfos.Add(genClipInfo);
                    }

                },
                tracks);

        }


    }
}
