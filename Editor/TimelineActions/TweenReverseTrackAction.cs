using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEditor.Timeline;
using UnityEditor.Timeline.Actions;
using UnityEngine;
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
                    // LayoutElement_FlexibleHeightControlTrack
                    string trackName = trackType.Name.ToString();
                    string animUnitTweenMethod = trackName.Replace("ControlTrack", "");
                    string componentType = trackType.FullName.Split('.')[1];
                    var assetName = $"Cr7Sund.{componentType}.{animUnitTweenMethod}ControlAsset";
                    var trackAssetType = trackType.Assembly.GetType(assetName);

                    var trackInfo = GetReverseClipInfo(childSourceTrack, isIn);
                    trackInfo.trackAssetType = trackAssetType;
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
            var resultTrackInfo = new TrackInfoContext();


            var childBehaviors = TweenTimeLineDataModel.TrackBehaviourDict[trackAsset];
            foreach (var clipBehaviour in childBehaviors)
            {
                var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[clipBehaviour];
                var clipInfoContext = new ClipInfoContext();
                resultTrackInfo.clipInfos.Add(clipInfoContext);

                clipInfoContext.start = clipInfo.start;
                clipInfoContext.duration = clipBehaviour.EasePreset.GetReverseDuration(clipInfo.duration, isIn);

                clipInfoContext.startPos = clipBehaviour.EndPos;
                clipInfoContext.endPos = clipBehaviour.StartPos;
                clipInfoContext.easePreset = clipBehaviour.EasePreset.GetReverseEasing(_easingTokenPresetLibrary);
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
