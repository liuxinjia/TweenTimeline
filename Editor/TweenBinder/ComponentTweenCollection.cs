using System.Collections.Generic;
using Assert = UnityEngine.Assertions.Assert;
using UnityEngine.Timeline;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;
using Cr7Sund.Timeline.Extension;
using JetBrains.Annotations;

namespace Cr7Sund.TweenTimeLine
{
    [CreateAssetMenu(menuName = "Cr7Sund/TweenTimeLine/ComponentTweenCollection",
     fileName = "ComponentTweenCollection")]
    public class ComponentTweenCollection : ScriptableObject
    {
        public List<ComponentTween> tweenActions = new();
        public List<ComponentBindTracks> GetTweenActions(string category)
        {
            var index = tweenActions.FindIndex(tweenAction => tweenAction.category == category);
            Assert.IsTrue(index >= 0, $"Can find  tweenActions,  category: {category}");
            return tweenActions[index].tweenNames;
        }

        [ContextMenu(nameof(GeneTweenRuntimeCode))]
        [MenuItem("Tools/GeneTweenRuntimeCode")]

        public static void GeneTweenRuntimeCode()
        {
            var tweenActionCollection = AssetDatabase.LoadAssetAtPath<ComponentTweenCollection>(TweenTimelineDefine.componentTweenCollectionPath);
            tweenActionCollection.Rebuild();
            TweenCodeGenerator.GenerateRunTimeCode();
        }


        public void Rebuild()
        {
            string inputFilePath = TweenTimelineDefine.EditorDataSourcePath;
            var assetGUIDs = AssetDatabase.FindAssets("t:TimelineAsset", new[]
            {
                inputFilePath
            });

            tweenActions.Clear();
            foreach (var assetID in assetGUIDs)
            {
                var asset = AssetDatabase.LoadAssetAtPath<TimelineAsset>(AssetDatabase.GUIDToAssetPath(assetID));
                if (asset is TimelineAsset timelineAsset)
                {
                    List<ComponentTween> collection = GetTweenTypes(timelineAsset);
                    if (collection == null)
                    {
                        continue;
                    }
                    foreach (ComponentTween item in collection)
                    {
                        var findIndex = tweenActions.FindIndex(tweenAction =>
                        tweenAction.Equals(item));
                        ComponentTween componentTween = null;
                        if (findIndex < 0)
                        {
                            componentTween = new ComponentTween();
                            componentTween.category = item.category;
                            tweenActions.Add(componentTween);
                        }
                        else
                        {
                            componentTween = tweenActions[findIndex];
                        }
                        componentTween.tweenNames.AddRange(item.tweenNames);
                    }
                }
            }
        }

        private static List<ComponentTween> GetTweenTypes(TimelineAsset timeLineAsset)
        {
            if (!IsValidTimelineAsset(timeLineAsset.name))
            {
                return null;
            }

            Dictionary<string, ComponentTween> resultTweens = new();
            IterateTimeLineTrackAssets(timeLineAsset, (trackAsset) =>
            {
                if (trackAsset is GroupTrack)
                {
                    return;
                }
                if (trackAsset is not IBaseTrack
                && trackAsset is not AudioTrack)//Mark Track
                {
                    return;
                }

                var parentGroup = TweenTimelineManager.GetTrackSecondRoot(trackAsset);
                if (parentGroup.name == TweenTimelineDefine.InDefine) return;
                if (parentGroup.name == TweenTimelineDefine.OutDefine) return;
                var tweenCategory = GetTweenTypesCategory(parentGroup.name);
                if (string.IsNullOrEmpty(tweenCategory))
                {
                    if (!timeLineAsset.name.EndsWith(TweenTimelineDefine.PanelTag))
                    {
                        Debug.LogWarning($"Invalid groupTrackName  TrackAsset:{parentGroup.name}  TimeLineAsset: {timeLineAsset}  ");
                        return;
                    }
                    // e.g panelTimelineAsset, duplicate panels
                    return;
                }
                if (!resultTweens.TryGetValue(tweenCategory, out var tweenAction))
                {
                    tweenAction = new ComponentTween();
                    tweenAction.category = tweenCategory;
                    resultTweens.Add(tweenCategory, tweenAction);
                }

                string tweenName = TweenCodeGenerator.GetGenSequenceName(trackAsset);
                if (string.IsNullOrEmpty(tweenName))
                {
                    return;
                }
                var findIndex = tweenAction.tweenNames.FindIndex(ts => ts.tweenName == tweenName);

                ComponentBindTracks findTween = null;
                if (findIndex < 0)
                {
                    findTween = new ComponentBindTracks();
                    findTween.tweenName = tweenName;
                    findTween.bindTargets = new List<string>();
                    findTween.bindTypes = new List<string>();
                    tweenAction.tweenNames.Add(findTween);
                }
                else
                {
                    findTween = tweenAction.tweenNames[findIndex];
                }

                TweenTimelineManager.GetTrackBindInfos(trackAsset
                  , out var bindTarget, out var bindType);

                findTween.bindTargets.Add(bindTarget);
                findTween.bindTypes.Add(bindType);

                findTween.trackTypeNames.Add(trackAsset.GetType().FullName);
            });

            if (resultTweens.Count <= 0)
            {
                return null;
            }
            List<ComponentTween> componentTweens = resultTweens.Values.ToList();
            return componentTweens;
        }

        private static string GetTweenTypesCategory(string trackName)
        {
            if (trackName.EndsWith(TweenTimelineDefine.CompositeTag))
            {
                return TweenTimelineDefine.CompositeTag;
            }
            if (trackName.EndsWith(TweenTimelineDefine.PanelTag))
            {
                return TweenTimelineDefine.PanelTag;
            }
            foreach (var item in TweenTimelineDefine.UIComponentTypeMatch)
            {
                if (trackName.EndsWith(item.Key))
                {
                    return item.Key;
                }
            }

            return string.Empty;
        }

        private static bool IsValidTimelineAsset(string timelineAssetName)
        {
            if (timelineAssetName.EndsWith(TweenTimelineDefine.CompositeTag))
            {
                return true;
            }
            if (timelineAssetName.EndsWith(TweenTimelineDefine.PanelTag))
            {
                return true;
            }
            foreach (var item in TweenTimelineDefine.UIComponentTypeMatch)
            {
                if (timelineAssetName.EndsWith(item.Key))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IterateTimeLineTrackAssets(TimelineAsset asset, Action<TrackAsset> iterateAction)
        {
            var tracks = asset.GetRootTracks();

            iterateTrackAsset(iterateAction, tracks);

            return true;

            static void iterateTrackAsset(Action<TrackAsset> iterateAction, IEnumerable<TrackAsset> tracks)
            {
                foreach (var track in tracks)
                {
                    iterateAction?.Invoke(track);
                    iterateTrackAsset(iterateAction, track.GetChildTracks());
                }
            }
        }
    }
}
