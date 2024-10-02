using System.Collections.Generic;
using Assert = UnityEngine.Assertions.Assert;
using UnityEngine.Timeline;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;

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

        public void Reset()
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
                        tweenActions.AddRange(collection);
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
            BindAdapterEditorHelper.IterateTimeLineTrackAssets(timeLineAsset, (trackAsset) =>
            {
                if (trackAsset is GroupTrack)
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

                string[] splits = trackAsset.name.Split('_');
                if (splits.Length < 2)
                {
                    Debug.LogWarning($"Invalid TweenNames  TrackAsset:{trackAsset.name} int TimeLineAsset: {timeLineAsset}  ");
                    return;
                }
                findTween.bindTargets.Add(splits[0]);
                findTween.bindTypes.Add(splits[1]);

                findTween.trackTypeNames.Add(trackAsset.GetType().FullName);
            });

            List<ComponentTween> componentTweens = resultTweens.Values.ToList();
            return componentTweens;
        }

        private static string GetTweenTypesCategory(string trackName)
        {
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
