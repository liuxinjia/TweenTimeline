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
    // [CreateAssetMenu(menuName = "Cr7Sund", fileName = "ComponentTweenCollection")]
    public class ComponentTweenCollection : ScriptableObject
    {
        public List<ComponentTween> tweenActions = new();
        public List<ComponentBindTracks> GetTweenActions(string category)
        {
            var index = tweenActions.FindIndex(tweenAction => tweenAction.category == category);
            Assert.IsTrue(index >= 0, $"Can find  tweenActions,  category: {category}");
            return tweenActions[index].tweenNames;
        }

        [ContextMenu(nameof(Rebuild))]
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
                    if (collection != null)
                    {
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

            TweenCodeGenerator.GenerateRunTimeCode();
        }

        private static List<ComponentTween> GetTweenTypes(TimelineAsset timeLineAsset)
        {
            if (!IsValidTimelineAsset(timeLineAsset.name))
            {
                return null;
            }

            Dictionary<string, ComponentTween> resultTweens = new();
            BindAdapterEditorHelper.IterateTimeLineTrackAssets(timeLineAsset, (trackAsset) =>
            {
                if (trackAsset is GroupTrack)
                {
                    return;
                }
                if (trackAsset is not IBaseTrack)//Mark Track
                {
                    return;
                }

                var parentGroup = trackAsset.parent as GroupTrack;
                TweenTimelineManager.GetTrackSecondRoot(trackAsset);
                if (parentGroup.name == TweenTimelineDefine.InDefine) return;
                if (parentGroup.name == TweenTimelineDefine.OutDefine) return;
                var tweenCategory = GetTweenTypesCategory(parentGroup.name);
                if (string.IsNullOrEmpty(tweenCategory))
                {
                    Debug.LogWarning($"Invalid groupTrackName  TrackAsset:{parentGroup.name} int TimeLineAsset: {timeLineAsset}  ");
                    return;
                }
                if (!resultTweens.TryGetValue(tweenCategory, out var tweenAction))
                {
                    tweenAction = new ComponentTween();
                    tweenAction.category = tweenCategory;
                    resultTweens.Add(tweenCategory, tweenAction);
                }

                string tweenName = TweenCodeGenerator.GetGenSequenceName(trackAsset);
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

                var uniqueBehaviour = TweenTimelineManager.GetBehaviourByTrackAsset(trackAsset);

                findTween.bindTargets.Add(uniqueBehaviour.BindTarget);
                findTween.bindTypes.Add(uniqueBehaviour.BindType);

                findTween.trackTypeNames.Add(trackAsset.GetType().FullName);
            });

            List<ComponentTween> componentTweens = resultTweens.Values.ToList();
            return componentTweens;
        }

        private static string GetTweenTypesCategory(string trackName)
        {
            if(trackName.EndsWith(TweenTimelineDefine.CompositeTag)){
                return TweenTimelineDefine.CompositeTag;
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
            foreach (var item in TweenTimelineDefine.UIComponentTypeMatch)
            {
                if (timelineAssetName.EndsWith(item.Key))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
