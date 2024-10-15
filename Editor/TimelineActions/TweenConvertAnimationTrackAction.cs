using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEditor.Timeline;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Timeline;

namespace Cr7Sund.TweenTimeLine
{
    [MenuEntry("Custom Actions/TweenConvertAnimationTrackAction")]
    public class TweenConvertAnimationTrackAction : TrackAction
    {
        private EasingTokenPresetLibrary _easingTokenPresetLibrary;

        public override ActionValidity Validate(IEnumerable<TrackAsset> tracks)
        {
            return ActionValidity.Valid;
        }

        public override bool Execute(IEnumerable<TrackAsset> tracks)
        {
            _easingTokenPresetLibrary = AssetDatabase.LoadAssetAtPath<EasingTokenPresetLibrary>(TweenTimelineDefine.easingTokenPresetsPath);

            TweenTimelineManager.InitTimeline();
            TweenConfigCacher.CacheTweenConfigs();
            AnimationClipConverter.CreateCurves(tracks, _easingTokenPresetLibrary);

            var newTracks = ProcessTracks(tracks);
            foreach (var trackInfo in newTracks)
            {
                TweenTimelineManager.AddTrack(trackInfo.parent, trackInfo, true, true);
            }

            if (newTracks.Count <= 0)
            {
                Debug.LogError("Check the select animation track is valid ");
                return false;
            }
            DeleteOriginalTracks(tracks);
            TimelineEditor.Refresh(RefreshReason.ContentsAddedOrRemoved);
            return true;
        }

        private List<TrackInfoContext> ProcessTracks(IEnumerable<TrackAsset> tracks)
        {
            List<TrackInfoContext> newTracks = new();
            foreach (TrackAsset track in tracks)
            {
                if (!TweenTimeLineDataModel.TrackObjectDict.ContainsKey(track)) continue;

                var targetComponent = TweenTimeLineDataModel.TrackObjectDict[track] as Component;
                GameObject targetGo = targetComponent.gameObject;

                if (track is AnimationTrack animationTrack)
                {
                    IEnumerable<TimelineClip> clips = animationTrack.GetClips();
                    foreach (var timelineClip in clips)
                    {
                        var clip = timelineClip.animationClip;
                        AddTrack(newTracks, clip, timelineClip.start, targetComponent.gameObject);
                    }

                    if (animationTrack.infiniteClip != null)
                    {
                        AddTrack(newTracks, animationTrack.infiniteClip, 0, targetComponent.gameObject);
                    }
                }
            }
            return newTracks;

            void AddTrack(List<TrackInfoContext> newTracks, AnimationClip clip, double start, GameObject targetGO)
            {
                var keyframeDatas = AnimationClipConverter.GenerateKeyFrameDatas(clip);
                newTracks.AddRange(
                   AnimationClipConverter.CreateTrackContexts(targetGO, clip
                     , _easingTokenPresetLibrary, keyframeDatas, start));

                keyframeDatas = AnimationClipConverter.GenerateObjectKeyFrameDatas(clip);
                newTracks.AddRange(
                      AnimationClipConverter.CreateTrackContexts(targetGO, clip, _easingTokenPresetLibrary,
                      keyframeDatas, start));
            }
        }

        private void DeleteOriginalTracks(IEnumerable<TrackAsset> tracks)
        {
            foreach (TrackAsset track in tracks)
            {
                if (track is AnimationTrack)
                    track.timelineAsset.DeleteTrack(track);
            }
        }


        [TimelineShortcut("TweenConvertAnimationTrackAction", KeyCode.C, ShortcutModifiers.Shift)]
        public static void HandleShortCut(ShortcutArguments args)
        {
            Invoker.InvokeWithSelectedTracks<TweenConvertAnimationTrackAction>();
        }
    }
}
