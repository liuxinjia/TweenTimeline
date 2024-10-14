using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Cr7Sund.Timeline.Extension
{
    public static class TimelineWindowExposer
    {
        private static Dictionary<Type, FieldInfo> _templateInfoCache = new Dictionary<Type, FieldInfo>();
        private static Action<WindowState, Event> windowOnGuiStarted;

        public static bool IteratePlayableAssets(Action<PlayableAsset, TrackAsset> iterateAction)
        {
            var timelineWindow = TimelineWindow.instance;

            if (timelineWindow == null)
            {
                return false;
            }
            var editSequence = timelineWindow.state.editSequence;
            if (editSequence.asset == null)
            {
                return false;
            }
            var tracks = editSequence.asset.GetRootTracks();

            iteratePlayTrackAsset(iterateAction, tracks);

            return true;

            static void iteratePlayTrackAsset(Action<PlayableAsset, TrackAsset> iterateAction, IEnumerable<TrackAsset> tracks)
            {
                foreach (TrackAsset track in tracks)
                {
                    var timelineClips = track.GetClips().ToList();
                    foreach (TimelineClip clip in timelineClips)
                    {
                        if (clip.asset is PlayableAsset playableAsset)
                        {
                            iterateAction?.Invoke(playableAsset, track);
                        }
                    }

                    iteratePlayTrackAsset(iterateAction, track.GetChildTracks());
                }

            }
        }

        public static bool IterateClips(Action<TimelineClip, TrackAsset> iterateAction)
        {
            var timelineWindow = TimelineWindow.instance;
            if (timelineWindow == null)
            {
                return false;
            }
            var editSequence = timelineWindow.state.editSequence;
            if (editSequence.asset == null)
            {
                return false;
            }
            var tracks = editSequence.asset.GetRootTracks();

            return IterateClips(iterateAction, tracks);
        }

        public static bool IterateClips(Action<TimelineClip, TrackAsset> iterateAction, IEnumerable<TrackAsset> tracks)
        {
            iterateTrackClips(iterateAction, tracks);

            return true;

            static void iterateTrackClips(Action<TimelineClip, TrackAsset> iterateAction, IEnumerable<TrackAsset> tracks)
            {
                foreach (var track in tracks)
                {
                    var timelineClips = track.GetClips().ToList();
                    foreach (TimelineClip clip in timelineClips)
                    {
                        var clipAsset = clip.asset;
                        if (clipAsset == null) continue;
                        iterateAction?.Invoke(clip, track);
                    }
                    iterateTrackClips(iterateAction, track.GetChildTracks());
                }
            }
        }

        public static bool IterateClips(Action<object, TimelineClip, TrackAsset> iterateAction)
        {
            var timelineWindow = TimelineWindow.instance;
            if (timelineWindow == null)
            {
                return false;
            }
            var editSequence = timelineWindow.state.editSequence;
            if (editSequence.asset == null)
            {
                return false;
            }
            var tracks = editSequence.asset.GetRootTracks();

            iterateTrackClips(iterateAction, tracks);

            return true;

            static void iterateTrackClips(Action<object, TimelineClip, TrackAsset> iterateAction, IEnumerable<TrackAsset> tracks)
            {
                foreach (var track in tracks)
                {
                    var timelineClips = track.GetClips().ToList();
                    foreach (TimelineClip clip in timelineClips)
                    {
                        var clipAsset = clip.asset;
                        if (clipAsset == null)
                        {
                            continue;
                        }
                        GetBehaviourValue(clipAsset, out var value);
                        iterateAction?.Invoke(value, clip, track);
                    }
                    iterateTrackClips(iterateAction, track.GetChildTracks());
                }
            }
        }

        public static bool IterateTrackAssets(Action<TrackAsset> iterateAction)
        {
            var timelineWindow = TimelineWindow.instance;
            if (timelineWindow == null)
            {
                return false;
            }
            var editSequence = timelineWindow.state.editSequence;
            if (editSequence.asset == null)
            {
                return false;
            }
            return IterateTrackAssets(iterateAction, editSequence.asset);
        }

        static bool IterateTrackAssets(Action<TrackAsset> iterateAction, TimelineAsset asset)
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

        public static bool IsTrackLocked(UnityEngine.Object[] targets)
        {
            if (!TimelineUtility.IsCurrentSequenceValid() || IsCurrentSequenceReadOnly())
                return true;

            return targets.Any(track => ((TrackAsset)track).lockedInHierarchy);
        }

        private static bool IsCurrentSequenceReadOnly()
        {
            return TimelineWindow.instance.state.editSequence.isReadOnly;
        }

        public static bool Init(Action<bool> onPlayStateChange, System.Action OnRebuildGraphChange,
            System.Action onTimeChange, Action onWindowOnGuiStarted = null)
        {
            if (!IsValidTimelineWindow()) return false;

            windowOnGuiStarted = (state, evt) => onWindowOnGuiStarted?.Invoke();

            TimelineWindow.instance.state.OnPlayStateChange -= onPlayStateChange;
            TimelineWindow.instance.state.OnRebuildGraphChange -= OnRebuildGraphChange;
            TimelineWindow.instance.state.OnTimeChange -= onTimeChange;
            TimelineWindow.instance.state.windowOnGuiStarted -= windowOnGuiStarted;

            TimelineWindow.instance.state.OnPlayStateChange += onPlayStateChange;
            TimelineWindow.instance.state.OnRebuildGraphChange += OnRebuildGraphChange;
            TimelineWindow.instance.state.OnTimeChange += onTimeChange;
            TimelineWindow.instance.state.windowOnGuiStarted += windowOnGuiStarted;

            return true;
        }

        public static bool RegisterOnTimeLineWindowGUIStart(Action onWindowOnGuiStarted)
        {
            if (!IsValidTimelineWindow()) return false;

            windowOnGuiStarted = (state, evt) => onWindowOnGuiStarted?.Invoke();

            TimelineWindow.instance.state.windowOnGuiStarted += windowOnGuiStarted;
            return true;
        }

        public static bool UnRegisterOnTimeLineWindowGUIStart()
        {
            if (!IsValidTimelineWindow()) return false;

            TimelineWindow.instance.state.windowOnGuiStarted -= windowOnGuiStarted;
            return true;
        }

        public static bool IsValidTimelineWindow()
        {
            if (TimelineWindow.instance == null
            || TimelineWindow.instance.state.editSequence == null
                || TimelineWindow.instance.state.editSequence.asset == null)
                return false;
            return true;
        }

        public static bool IsFocusTimeLineWindow()
        {
            return true;
        }

        public static double GetSequenceTime()
        {
            return TimelineWindow.instance.state.editSequence.time;
        }

        public static IEnumerable<TrackAsset> GetSelectTracks()
        {
            var tracks = SelectionManager.SelectedTracks();
            return tracks;
        }

        public static double GetPlayDuration()
        {
            // TODO : get current edit duration, excluding mute one
            return TimelineWindow.instance.state.editSequence.duration;
        }

        public static PlayableDirector GetCurDirector()
        {
            return TimelineWindow.instance.state.editSequence.director;
        }
        public static bool GetBehaviourValue(object clipAsset, out System.Object behaviour)
        {
            behaviour = null;

            if (!_templateInfoCache.TryGetValue(clipAsset.GetType(), out var fieldInfo))
            {
                fieldInfo = clipAsset.GetType().GetField("template", BindingFlags.Instance | BindingFlags.Public);
                if (fieldInfo == null) return false;
                _templateInfoCache.Add(clipAsset.GetType(), fieldInfo);
            }

            behaviour = fieldInfo.GetValue(clipAsset);
            if (behaviour == null) return false;
            else return true;
        }

        public static double GetTimelineFrameRate()
        {
            return TimelineWindow.instance.state.editSequence.frameRate;
        }

        public static void PlayEditTimeline()
        {
            if (TimelineWindow.instance != null)
                TimelineWindow.instance.state.Play();
        }

        public static void SkipToTimelinePos(double time)
        {
            if (TimelineWindow.instance.state.editSequence.time != time)
            {
                TimelineWindow.instance.state.editSequence.time = Math.Max(0.0, time);
                RepaintTimelineWindow();
            }
        }

        public static void RepaintTimelineWindow()
        {
            if (TimelineWindow.instance != null)
                TimelineWindow.instance.Repaint();
        }

        public static void Bind(PlayableDirector director, TrackAsset bindTo, UnityEngine.Object objectToBind)
        {
            BindingUtility.Bind(director, bindTo, objectToBind);
        }

        public static void SelectTimelineClip(TimelineClip timelineClip)
        {
            // EditorWindow.GetWindow<TimelineWindow>();
            TimelineEditor.selectedClip = timelineClip;
        }

    }
}
