using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Assertions;
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
            var assets = GetTimeLineAssets();
            foreach (TimelineAsset asset in assets)
            {
                var tracks = asset.GetRootTracks();
                iteratePlayTrackAsset(iterateAction, tracks);
            }

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

        public static void IterateClips(Action<TimelineClip, TrackAsset> iterateAction)
        {
            var assets = GetTimeLineAssets();
            foreach (TimelineAsset asset in assets)
            {
                var tracks = asset.GetRootTracks();
                IterateClips(iterateAction, tracks);
            }
        }

        public static TimelineAsset GetEditTimeLineAsset()
        {
            var timelineWindow = TimelineWindow.instance;
            if (timelineWindow == null)
            {
                return null;
            }
            var editSequence = timelineWindow.state.editSequence;
            return editSequence.asset;
        }

        private static List<TimelineAsset> GetTimeLineAssets()
        {
            var rootAsset = GetEditTimeLineAsset();
            var playableSet = GetSubDirectors(rootAsset, GetCurDirector());
            return playableSet.Select(asset => asset.playableAsset as TimelineAsset).ToList();
        }

        public static List<PlayableDirector> GetPlayableDirectors()
        {
            var rootAsset = GetEditTimeLineAsset();
            return GetSubDirectors(rootAsset, GetCurDirector()).ToList();
        }
        public static void IterateSubTimelineClipAsset(
                    Action<TrackAsset, TimelineClip, PlayableDirector> action = null)
        {
            var rootAsset = GetEditTimeLineAsset();
            GetSubDirectors(rootAsset, GetCurDirector(), action);
        }

        private static HashSet<PlayableDirector> GetSubDirectors(
            TimelineAsset rootTimeLineAsset, PlayableDirector rootDirector,
             Action<TrackAsset, TimelineClip, PlayableDirector> action = null)
        {
            var tracks = rootTimeLineAsset.GetRootTracks();
            var subTimeLineAssetSet = new HashSet<TimelineAsset>();
            var resultDirectors = new HashSet<PlayableDirector>();
            if (rootTimeLineAsset != null)
            {
                resultDirectors.Add(rootDirector);
                subTimeLineAssetSet.Add(rootTimeLineAsset);
            }

            void IterateClips(IEnumerable<TrackAsset> tracks)
            {
                iterateTrackClips(tracks);

                void iterateTrackClips(IEnumerable<TrackAsset> tracks)
                {
                    foreach (var track in tracks)
                    {
                        var timelineClips = track.GetClips().ToList();
                        foreach (TimelineClip clip in timelineClips)
                        {
                            var clipAsset = clip.asset;
                            if (clipAsset == null) continue;
                            if (clipAsset is not ControlPlayableAsset controlPlayableAsset)
                            {
                                continue;
                            }
                            var go = controlPlayableAsset.sourceGameObject.Resolve(rootDirector);
                            if (go == null)
                            {
                                continue;
                            }
                            var director = go.GetComponent<PlayableDirector>();
                            var subTimeLineAsset = director.playableAsset as TimelineAsset;
                            if (subTimeLineAsset == null) continue;
                            if (!subTimeLineAssetSet.Contains(subTimeLineAsset))
                            {
                                action?.Invoke(track, clip, director);
                                resultDirectors.Add(director);
                                subTimeLineAssetSet.Add(subTimeLineAsset);
                            }
                            else
                            {
                                throw new System.Exception($"There will be cycle tween {subTimeLineAsset.name} of {go.name}");
                            }

                        }
                        iterateTrackClips(track.GetChildTracks());
                    }
                }
            }
            IterateClips(tracks);
            return resultDirectors;
        }

        public static void IterateClips(Action<TimelineClip, TrackAsset> iterateAction, IEnumerable<TrackAsset> tracks)
        {
            iterateTrackClips(iterateAction, tracks);

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

        public static void IterateClips(Action<object, TimelineClip, TrackAsset> iterateAction)
        {
            var assets = GetTimeLineAssets();
            foreach (TimelineAsset asset in assets)
            {
                var tracks = asset.GetRootTracks();
                iterateTrackClips(iterateAction, tracks);
            }

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

        public static void IterateTrackAssets(Action<TrackAsset> iterateAction)
        {
            var assets = GetTimeLineAssets();
            foreach (TimelineAsset asset in assets)
            {
                IterateTrackAsset(asset, iterateAction);
            }
        }

        public static bool IterateTrackAsset(TimelineAsset asset, Action<TrackAsset> iterateAction)
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

        public static void GetSelectCurveBindings(TrackAsset selectTrackAsset)
        {
            TimelineTreeViewGUI treeView = TimelineWindow.instance.treeView;
            TimelineDataSource m_DataSource = treeView.data as TimelineDataSource;
            foreach (TimelineTrackBaseGUI item in m_DataSource.allTrackGuis)
            {
                if (item is TimelineTrackGUI timelineTrackGUI)
                {
                    var clipCurveEditor = timelineTrackGUI.clipCurveEditor;
                    if (clipCurveEditor == null)
                    {
                        continue;
                    }
                    var curveEditor = clipCurveEditor.curveEditor;

                    if (((IClipCurveEditorOwner)timelineTrackGUI).owner != selectTrackAsset)
                    {
                        continue;
                    }

                    // var method = curveEditor.GetType().GetMethod("CreateKeyFromClick",
                    // BindingFlags.Instance | BindingFlags.NonPublic, null,
                    // new Type[] { typeof(System.Object) }, null);
                    // method.Invoke(curveEditor, new object[] { (Vector2)curveEditor.rect.position });

                }
            }
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

        public static void LockWindow(bool locked)
        {
            TimelineWindow.instance.locked = locked;
        }
    }
}
