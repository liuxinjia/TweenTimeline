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

            void CreaetTrack(int isIn, TrackAsset childSourceTrack, TrackAsset parentSourceTrack)
            {
                TrackAsset newTrack = null;
                TrackAsset parentTrack = null;
                if (newTrackDict.ContainsKey(parentSourceTrack))
                {
                    parentTrack = newTrackDict[parentSourceTrack];
                }
                else
                {
                    var groupName = isIn < 0 ? TweenTimelineDefine.InDefine : TweenTimelineDefine.OutDefine;
                    parentTrack = TweenTimelineManager.CreatGroupAsset(groupName, childSourceTrack.timelineAsset, null);
                    newTrackDict.Add(parentSourceTrack, newTrack);
                }

                if (childSourceTrack is GroupTrack groupSourceTrack)
                {
                    string groupName = childSourceTrack.name;
                    newTrack = TweenTimelineManager.CreatGroupAsset(groupName, childSourceTrack.timelineAsset, parentTrack);
                }
                else
                {
                    Component component = TweenTimeLineDataModel.TrackObjectDict[childSourceTrack] as Component;
                    var trackAssetType = childSourceTrack.GetType();
                    // LayoutElement_FlexibleHeightControlTrack
                    string trackName = trackAssetType.Name.ToString();
                    string animUnitTweenMethod = trackName.Replace("ControlTrack", "");
                    string componentType = trackAssetType.FullName.Split('.')[1];
                    var assetName = $"Cr7Sund.{componentType}.{animUnitTweenMethod}ControlAsset";

                    var trackInfo = GetReverseClipInfo(childSourceTrack, isIn);
                    var clipAssetType = trackAssetType.Assembly.GetType(assetName);
                    Assert.IsNotNull(clipAssetType);

                    newTrack = TweenTimelineManager.AddTrackWithParents(component,
                        trackAssetType,
                        clipAssetType,
                        trackInfo,
                        createNewTrack: true, // different direction
                        parentTrack: parentTrack
                    );
                }

                newTrackDict.Add(childSourceTrack, newTrack);
            }

            void AddTrack(int isIn, TrackAsset trackAsset, TrackAsset parentSourceTrack)
            {
                CreaetTrack(isIn, trackAsset, parentSourceTrack);

                foreach (var childTrack in trackAsset.GetChildTracks())
                {
                    AddTrack(isIn, childTrack, trackAsset);
                }
            }


            TimelineEditor.Refresh(RefreshReason.ContentsAddedOrRemoved);
            return true;
        }



        public TrackInfoContext GetReverseClipInfo(TrackAsset trackAsset, int isIn)
        {
            var resultTrackInfo = new TrackInfoContext();
            var childBehaviours = TweenTimeLineDataModel.TrackBehaviourDict[trackAsset];
            foreach (var clipBeahviour in childBehaviours)
            {
                var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[clipBeahviour];
                var clipInfoContext = new ClipInfoContext();
                resultTrackInfo.clipInfos.Add(clipInfoContext);

                clipInfoContext.start = clipInfo.start;
                clipInfoContext.duration = clipBeahviour.EasePreset.GetReverseDuration(clipInfo.duration, isIn);

                clipInfoContext.startPos = clipBeahviour.EndPos;
                clipInfoContext.endPos = clipBeahviour.StartPos;
                clipInfoContext.easePreset = clipBeahviour.EasePreset.GetReverseEasing(_easingTokenPresetLibrary);
            }

            return resultTrackInfo;
        }


        private void Reverse(TrackAsset sourceTrack, TrackAsset newTrack)
        {
            throw new NotImplementedException();
        }

        [TimelineShortcut("TweenReverseTrackAction", KeyCode.H)]
        public static void HandleShortCut(ShortcutArguments args)
        {
            Invoker.InvokeWithSelectedTracks<TweenReverseTrackAction>();
        }
    }
}
