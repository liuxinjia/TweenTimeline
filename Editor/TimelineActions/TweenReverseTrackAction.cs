using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEditor.Timeline;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    [MenuEntry("Custom Actions/TweenReverseTrackAction")]
    public class TweenReverseTrackAction : TrackAction
    {
        private EasingTokenPresetLibrary _easingTokenPresetLibrary;

        public override ActionValidity Validate(IEnumerable<TrackAsset> tracks)
        {
            return ActionValidity.Valid;
        }

        public override bool Execute(IEnumerable<TrackAsset> tracks)
        {
            TweenTimelineManager.InitTimeline();

            _easingTokenPresetLibrary = AssetDatabase.LoadAssetAtPath<EasingTokenPresetLibrary>(TweenTimelineDefine.easingTokenPresetsPath);

            var newTrackList = new List<TrackAsset>();
            var newTrackDict = new Dictionary<TrackAsset, TrackAsset>();
            int isIn = TweenTimelineManager.GetPanelTracks(tracks, out var trackHierarchy, out var rootTrack);

            foreach (var childTrack in trackHierarchy)
            {
                AddTrack(isIn, childTrack, rootTrack);
            }

            void CreateTrack(int isIn, TrackAsset childSourceTrack, TrackAsset parentSourceTrack)
            {
                TrackAsset newTrack = null;
                TrackAsset parentTrack = null;
                parentTrack = GetParentTrack(isIn, childSourceTrack, parentSourceTrack, newTrackDict, newTrack);

                if (childSourceTrack is GroupTrack groupSourceTrack)
                {
                    string groupName = childSourceTrack.name;
                    newTrack = TweenTimelineManager.CreateGroupAsset(groupName, childSourceTrack.timelineAsset, parentTrack);
                }
                else
                {
                    Component component = TweenTimeLineDataModel.TrackObjectDict[childSourceTrack] as Component;
                    var trackType = childSourceTrack.GetType();

                    var trackInfo = GetReverseClipInfo(childSourceTrack, isIn);
                    if (trackInfo == null)
                    {
                        return;
                    }
                    trackInfo.trackType = trackType;
                    trackInfo.component = component;

                    newTrack = TweenTimelineManager.AddTrackWithParents(
                    trackInfo,
                    createNewTrack: true, // different direction
                    parentTrack: parentTrack
                );
                }

                newTrackDict.Add(childSourceTrack, newTrack);
            }

            void AddTrack(int isIn, TrackAsset trackAsset, TrackAsset parentSourceTrack)
            {
                CreateTrack(isIn, trackAsset, parentSourceTrack);

                foreach (var childTrack in trackAsset.GetChildTracks())
                {
                    AddTrack(isIn, childTrack, trackAsset);
                }
            }


            TimelineEditor.Refresh(RefreshReason.ContentsAddedOrRemoved);
            return true;
        }

        private static TrackAsset GetParentTrack(int isIn, TrackAsset childSourceTrack, TrackAsset parentSourceTrack, Dictionary<TrackAsset, TrackAsset> newTrackDict, TrackAsset newTrack)
        {
            TrackAsset parentTrack;
            if (newTrackDict.ContainsKey(parentSourceTrack))
            {
                parentTrack = newTrackDict[parentSourceTrack];
            }
            else
            {
                var reverseGroupName = isIn < 0 ? TweenTimelineDefine.InDefine : TweenTimelineDefine.OutDefine;
                parentTrack = TweenTimelineManager.CreateGroupAsset(reverseGroupName, childSourceTrack.timelineAsset, null);
                newTrackDict.Add(parentSourceTrack, newTrack);
            }

            return parentTrack;
        }

        public TrackInfoContext GetReverseClipInfo(TrackAsset trackAsset, int isIn)
        {
            if (trackAsset is not IBaseTrack)
            {
                return null;
            }

            var resultTrackInfo = new TrackInfoContext();
            var childBehaviors = TweenTimeLineDataModel.TrackBehaviourDict[trackAsset];
            foreach (var clipBehaviour in childBehaviors)
            {
                var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[clipBehaviour];
                var clipInfoContext = new ClipInfoContext();

                Type trackAssetType = null;
                if (clipBehaviour is EmptyBehaviour)
                {
                    trackAssetType = typeof(EmptyControlAsset);
                }
                else
                {
                    var trackType = trackAsset.GetType();
                    string trackName = trackType.Name.ToString();
                    string animUnitTweenMethod = trackName.Replace("ControlTrack", "");
                    string componentType = trackType.FullName.Split('.')[1];
                    var assetName = $"Cr7Sund.{componentType}.{animUnitTweenMethod}ControlAsset";
                    trackAssetType = trackType.Assembly.GetType(assetName);
                }

                resultTrackInfo.clipInfos.Add(clipInfoContext);

                clipInfoContext.start = clipInfo.delayTime;
                clipInfoContext.duration = clipBehaviour.EasePreset?.GetReverseDuration(clipInfo.duration, isIn) ?? clipInfo.duration;
                clipInfoContext.trackAssetType = trackAssetType;
                if (clipBehaviour.EasePreset != null)
                {
                    clipInfoContext.startPos = clipBehaviour.EndPos;
                    clipInfoContext.endPos = clipBehaviour.StartPos;
                    clipInfoContext.easePreset = clipBehaviour.EasePreset?.GetReverseEasing(_easingTokenPresetLibrary);
                }
            }

            return resultTrackInfo;
        }



        [TimelineShortcut("TweenReverseTrackAction", KeyCode.H, ShortcutModifiers.Shift)]
        public static void HandleShortCut(ShortcutArguments args)
        {
            Invoker.InvokeWithSelectedTracks<TweenReverseTrackAction>();
        }
    }
}
