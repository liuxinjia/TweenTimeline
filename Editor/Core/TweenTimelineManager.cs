using System;
using System.Collections.Generic;
using System.Reflection;
using Cr7Sund.Timeline.Extension;
using PrimeTween;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using System.Linq;
using Assert = UnityEngine.Assertions.Assert;

namespace Cr7Sund.TweenTimeLine
{

    public static class TweenTimelineManager
    {
        public static bool isPlay;
        public static double timeLineTime;
        public static bool isInit = false;

        public static bool InitTimeline()
        {

            InitPreTween();

            if (!isInit && TimelineWindowExposer.IsValidTimelineWindow())
            {
                bool hasTimelineWindow = TryRegisterTimelineEvents();

                if (hasTimelineWindow)
                {
                    Refresh();

                    isInit = true;
                    return true;
                }
            }

            return false;
        }

        public static void LogProfile(string message)
        {
            // Debug.Log("<color=green>" + message + "</color>");
        }

        [InitializeOnLoadMethod]
        static void OnInitTweenLineManager()
        {
            AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReload;
            AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
            EditorApplication.playModeStateChanged -= TweenTimelineManager.OnPlayModeChanged;
            EditorApplication.playModeStateChanged += TweenTimelineManager.OnPlayModeChanged;
        }


        #region Events
        private static bool TryRegisterTimelineEvents()
        {
            return TimelineWindowExposer.Init(OnPlayStateChange, OnRebuildGraphChange, OnTimeChange);
        }

        private static void OnTimeChange()
        {
            double time = TimelineWindowExposer.GetSequenceTime();
            UpdateTimeCache();

            if (!isPlay && timeLineTime != time)
            {
                PreviewAllClip();
            }
            timeLineTime = time;
        }

        private static void OnRebuildGraphChange()
        {
            Refresh();
        }

        private static void OnPlayStateChange(bool isPlay)
        {
            SetPlay(isPlay);
            // Debug.Log("IsPlay " + isPlay);
            if (isPlay)
            {
                TweenTimelineManager.PlayAllClip();
            }
            else
            {
                ResetDefaultAllClipWhenStop();
            }
        }

        private static void SetPlay(bool isPlay)
        {
            TweenTimelineManager.isPlay = isPlay;
        }

        private static void OnAfterAssemblyReload()
        {
            if (GetActionEditorWindow() != null)
            {
                InitTimeline();
            }
        }

        public static void OnPlayModeChanged(PlayModeStateChange change)
        {
            // incase of the static don't change when domain reload disable
            isInit = false;

            if (change == PlayModeStateChange.ExitingEditMode)
            {
                TryRemoveTweenManager();
            }
            else if (change == PlayModeStateChange.EnteredEditMode)
            {
                InitPreTween();
            }
            else if (change == PlayModeStateChange.EnteredPlayMode)
            {
                Application.targetFrameRate = 60;
                if (GetActionEditorWindow() != null)
                {
                    InitTimeline();
                }
                // Application.targetFrameRate = (int)TimelineWindowExposer.GetTimelineFrameRate();
            }
        }
        #endregion

        #region BindDatas
        public static bool InitPlayableBindings()
        {
            TweenTimeLineDataModel.TrackObjectDict.Clear();

            var playableDirector = TimelineWindowExposer.GetCurDirector();
            if (playableDirector != null && playableDirector.playableAsset != null)
            {
                InitDirector(playableDirector);
                return true;
            }
            return false;

            void InitDirector(PlayableDirector playableDirector)
            {
                PlayableAsset playableAsset = playableDirector.playableAsset;
                var outputs = playableAsset.outputs;
                foreach (PlayableBinding output in outputs)
                {
                    var trackAsset = output.sourceObject as TrackAsset;
                    if (trackAsset == null) continue;
                    var binding = playableDirector.GetGenericBinding(trackAsset);
                    if (binding == null)
                    {
                        // if (trackAsset is IBaseTrack)
                        Debug.LogWarning($"{trackAsset} don't bind target");
                        continue;
                    }

                    TweenTimeLineDataModel.TrackObjectDict.Add(trackAsset, binding);
                }
            }
        }

        private static void RefreshTimeline()
        {
            foreach (var item in TweenTimeLineDataModel.ClipInfoDicts)
            {
                item.Value.Dispose();
            }

            var validClipStateSet = new List<Tuple<IUniqueBehaviour, ClipBehaviourState>>();
            // TweenTimeLineDataModel.ClipStateDict.Clear();
            TweenTimeLineDataModel.PlayableAssetTrackDict.Clear();
            TweenTimeLineDataModel.PlayBehaviourTrackDict.Clear();
            TweenTimeLineDataModel.TrackBehaviourDict.Clear();
            TweenTimeLineDataModel.ClipInfoDicts.Clear();
            TweenTimeLineDataModel.ClipAssetBehaviourDict.Clear();
            // TweenTimeLineDataModel.NotificationReceiverDict.Clear();
            TweenTimeLineDataModel.groupTracks.Clear();

            TimelineWindowExposer.IteratePlayableAssets((playableAsset, trackAsset) =>
            {
                TweenTimeLineDataModel.PlayableAssetTrackDict.Add(playableAsset, trackAsset);
            });

            TimelineWindowExposer.IterateTrackAssets((trackAsset) =>
            {
                if (typeof(GroupTrack).IsInstanceOfType(trackAsset))
                {
                    TweenTimeLineDataModel.groupTracks.Add(trackAsset as GroupTrack);
                }
            });

            TimelineWindowExposer.IterateClips((clip, trackAsset) =>
            {
                if (trackAsset is not IBaseTrack)
                {
                    if (!TweenTimeLineDataModel.TrackObjectDict.ContainsKey(trackAsset))
                    {
                        return;
                    }
                    var binComponent = TweenTimeLineDataModel.TrackObjectDict[trackAsset] as Component;
                    var bindTrans = binComponent.transform;

                    Assert.IsNotNull(bindTrans);

                    if (trackAsset is AudioTrack customAudioTrack)
                    {
                        var animationPlayableAsset = clip.asset as CustomAudioPlayableAsset;
                        animationPlayableAsset.bindTarget = bindTrans.name;
                    }
                }
            });

            TimelineWindowExposer.IterateClips((value, clip, trackAsset) =>
            {
                if (trackAsset is not IBaseTrack)
                {
                    return;
                }

                var behaviour = value as IUniqueBehaviour;
                if (behaviour == null) return;
                if (!TweenTimeLineDataModel.TrackObjectDict.ContainsKey(trackAsset))
                {
                    // just skip if the bind component is null
                    return;
                }
                BindTrackAsset(trackAsset, behaviour);
                BindClipAsset(clip, validClipStateSet, behaviour, trackAsset);
            });

            TweenTimeLineDataModel.ClipStateDict.Clear();
            foreach (var item in validClipStateSet)
            {
                TweenTimeLineDataModel.ClipStateDict.Add(item.Item1, item.Item2);
            }

            foreach (var item in TweenTimeLineDataModel.TrackObjectDict)
            {
                var binComponent = item.Value as Component;
                TrackAsset trackAsset = item.Key;
                if (trackAsset is not IBaseTrack)
                {
                    continue;
                }
                if (!TweenTimeLineDataModel.TrackBehaviourDict.TryGetValue(trackAsset, out var behaviourList)) return;
                {
                    foreach (var behaviour in behaviourList)
                    {
                        behaviour.BindTarget = binComponent.gameObject.name;
                        behaviour.BindType = binComponent.GetType().FullName;
                    }
                }
            }

            RecreateTween();
        }
        private static AnimationCurve GetBindClipCurve(TimelineClip clip)
        {
            // only handle first animation curve event it has multiple properties
            AnimationClip animationClip = clip.curves;
            if (animationClip == null) return null;
            var editorCurveBindings = AnimationUtility.GetCurveBindings(animationClip);
            foreach (var bind in editorCurveBindings)
            {
                AnimationUtility.SetEditorCurve(animationClip, bind, AnimationCurve.Constant(0, 1, 1));
                AnimationCurve curve = AnimationUtility.GetEditorCurve(animationClip, bind);
                return curve;
            }

            return null;
        }

        private static void BindClipAsset(TimelineClip clip, List<Tuple<IUniqueBehaviour, ClipBehaviourState>> validClipStateSet, IUniqueBehaviour behaviour, TrackAsset trackAsset)
        {
            TweenTimeLineDataModel.ClipAssetBehaviourDict.Add(clip.asset, behaviour);

            var clipInfo = new ClipInfo()
            {
                start = clip.start,
                duration = clip.duration,
            };
            clipInfo.valueMakers = new();
            foreach (IMarker marker in trackAsset.GetMarkers())
            {
                if (marker is IValueMaker valueMaker
                    && marker.time >= clip.start && marker.time <= clip.end)
                {
                    clipInfo.valueMakers.Add(new MarkInfo(valueMaker));
                }
            }

            TweenTimeLineDataModel.ClipInfoDicts.Add(behaviour, clipInfo);

            // state should keep when refresh
            if (!TweenTimeLineDataModel.ClipStateDict.TryGetValue(behaviour, out var clipBehaviourState))
            {
                clipBehaviourState = new ClipBehaviourState
                {
                    PreViewAction = (uniqueBehaviour, targetState) =>
                    {
                        if (targetState == ClipBehaviourStateEnum.Preview)
                        {
                            ActionCenters.StartPreview(uniqueBehaviour);
                        }
                        else
                        {
                            ActionCenters.Reset(uniqueBehaviour);
                        }
                    },
                    PlayAction = (uniqueBehaviour, targetState) =>
                    {
                        var stateInfo = TweenTimeLineDataModel.ClipStateDict[uniqueBehaviour];
                        if (targetState == ClipBehaviourStateEnum.Playing)
                        {
                            ActionCenters.StartPlay(uniqueBehaviour);
                        }
                        else
                        {
                            ActionCenters.StopPlay(uniqueBehaviour);
                            ActionCenters.Reset(uniqueBehaviour);
                        }
                    },
                    RecordAction = (uniqueBehaviour, targetState) =>
                    {
                        ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[uniqueBehaviour];
                        if (targetState == ClipBehaviourStateEnum.Recording)
                        {
                            ActionCenters.MoveToRecordPos(uniqueBehaviour, stateInfo.IsRecordStart);
                        }
                        else
                        {
                            ActionCenters.EndRecord(uniqueBehaviour, stateInfo.IsRecordStart);
                            ActionCenters.Reset(uniqueBehaviour);
                        }
                    }
                };

                TweenTimeLineDataModel.ClipStateDict.Add(behaviour, clipBehaviourState);
            }

            ResetInitPos(behaviour);
            ResetMarkFieldNames(behaviour);

            var validMarkers = new Dictionary<int, object>();
            foreach (var valueMarker in clipInfo.valueMakers)
            {
                if (clipBehaviourState.markerInitPosDict.TryGetValue(valueMarker.InstanceID, out var v))
                {
                    validMarkers.Add(valueMarker.InstanceID, v);
                }
            }
            clipBehaviourState.markerInitPosDict = validMarkers;

            validClipStateSet.Add(new(behaviour, clipBehaviourState));
        }

        public static void ResetInitPos(IUniqueBehaviour behaviour)
        {
            TrackAsset trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[behaviour];
            ClipBehaviourState clipBehaviourState = TweenTimeLineDataModel.ClipStateDict[behaviour];

            if (!clipBehaviourState.IsPreview
                && !clipBehaviourState.IsPlaying
                && !clipBehaviourState.IsRecording)
            {
                var target = TweenTimeLineDataModel.TrackObjectDict[trackAsset];
                clipBehaviourState.initPos = behaviour.Get(target);
            }
        }

        private static void ResetMarkFieldNames(IUniqueBehaviour behaviour)
        {
            TrackAsset trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[behaviour];
            ClipBehaviourState clipBehaviourState = TweenTimeLineDataModel.ClipStateDict[behaviour];
            var target = TweenTimeLineDataModel.TrackObjectDict[trackAsset];
            var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[behaviour];

            if (!clipBehaviourState.IsPreview
                && !clipBehaviourState.IsPlaying
                && !clipBehaviourState.IsRecording)
            {
                clipBehaviourState.markerInitPosDict = new();
                for (int i = 0; i < clipInfo.valueMakers.Count; i++)
                {
                    MarkInfo valueMarker = clipInfo.valueMakers[i];
                    clipBehaviourState.markerInitPosDict[valueMarker.InstanceID] = valueMarker.Get(target);
                }
            }
        }

        private static void BindTrackAsset(TrackAsset asset, IUniqueBehaviour behaviour)
        {
            TweenTimeLineDataModel.PlayBehaviourTrackDict.Add(behaviour, asset);
            if (!TweenTimeLineDataModel.TrackBehaviourDict.TryGetValue(asset, out var clipBehaviors))
            {
                clipBehaviors = new List<IUniqueBehaviour>();
                TweenTimeLineDataModel.TrackBehaviourDict.Add(asset, clipBehaviors);
            }
            clipBehaviors.Add(behaviour);

            // TweenTimeLineDataModel.NotificationReceiverDict.Add(behaviour, new TweenTimelineReceiver());
        }

        private static void RecreateTween()
        {
            ReusablePrimeTweens();
            foreach (var item in TweenTimeLineDataModel.ClipInfoDicts)
            {
                item.Value.CreateTween(item.Key);
            }
        }

        public static void InitPreTween()
        {
            TryGetTweenManager(out var manager);
            Assert.IsNotNull(manager);

            if (PrimeTweenManager.Instance == null)
            {
                var initMethod = typeof(PrimeTweenManager).GetMethod("init", BindingFlags.Instance | BindingFlags.NonPublic);
                initMethod.Invoke(manager, new object[]
                {
                    200
                });
                PrimeTweenManager.Instance = manager;
            }
        }

        private static void UpdateTimeCache()
        {
            double time = TimelineWindowExposer.GetSequenceTime();

            if (TweenTimeLineDataModel.ClipAssetBehaviourDict.Count <= 0)
            {
                return;
            }
            TimelineWindowExposer.IterateClips((clip, trackAsset) =>
            {
                if (trackAsset is not IBaseTrack)
                {
                    return;
                }
                if (!TweenTimeLineDataModel.ClipAssetBehaviourDict.TryGetValue(clip.asset, out var behaviour)) return;

                if (clip.end >= time && clip.start <= time)
                {
                    TweenTimeLineDataModel.ClipStateDict[behaviour].IsSelect = true;
                }
                else
                {
                    TweenTimeLineDataModel.ClipStateDict[behaviour].IsSelect = false;
                }
            });
        }

        private static void TryGetTweenManager(out PrimeTweenManager manager)
        {
            var curScene = SceneManager.GetActiveScene();
            manager = GameObject.FindFirstObjectByType<PrimeTweenManager>();
            if (manager != null)
            {
                return;
            }
            var go = new GameObject("PrimeTweenManager");
            manager = go.AddComponent<PrimeTweenManager>();
            SceneManager.MoveGameObjectToScene(go, curScene);
        }

        public static void TryRemoveTweenManager()
        {
            var manager = GameObject.FindFirstObjectByType<PrimeTweenManager>();
            if (manager == null)
            {
                return;
            }
            GameObject.DestroyImmediate(manager.gameObject);
        }

        private static void Refresh()
        {
            if (InitPlayableBindings())
            {
                RefreshTimeline();
                UpdateTimeCache();
            }
        }
        #endregion


        #region Actions
        public static void ResetDefaultAllClip()
        {
            ChangeTweenManagerState(ClipBehaviourStateEnum.Default);
            foreach (var item in TweenTimeLineDataModel.ClipStateDict)
            {
                ResetDefaultClip(item.Key);
            }
        }

        public static void ResetDefaultAllClipWhenStop()
        {
            ChangeTweenManagerState(ClipBehaviourStateEnum.Default);
            foreach (var item in TweenTimeLineDataModel.ClipStateDict)
            {
                // avoid state change when chagne select object when recording
                if (item.Value.BehaviourState == ClipBehaviourStateEnum.Recording)
                {
                    continue;
                }
                ResetDefaultClip(item.Key);
            }
        }

        public static void PreviewAllClip()
        {
            ReusablePrimeTweens();

            ChangeTweenManagerState(ClipBehaviourStateEnum.Preview);
            foreach (var item in TweenTimeLineDataModel.ClipStateDict)
            {
                PreviewClip(item.Key);
            }
        }

        public static void PreviewClip(IUniqueBehaviour key)
        {
            ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[key];

            stateInfo.ChangeState(key,
                ClipBehaviourStateEnum.Preview);
            //     stateInfo.IsSelect ?
            //     ClipBehaviourStateEnum.Preview :
            //     ClipBehaviourStateEnum.Default);
        }

        public static void ResetDefaultClip(IUniqueBehaviour key)
        {
            ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[key];

            stateInfo.ChangeState(key,
                ClipBehaviourStateEnum.Default);
        }

        public static void PlayClip(IUniqueBehaviour key)
        {
            ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[key];
            stateInfo.ChangeState(key, ClipBehaviourStateEnum.Playing);
        }

        public static void TogglePlayClip(IUniqueBehaviour key)
        {
            ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[key];
            stateInfo.ToggleState(key, ClipBehaviourStateEnum.Playing);
        }

        public static void ToggleAllPLayClips(bool controlPlay = false)
        {
            ToggleTweenManagerState(ClipBehaviourStateEnum.Playing);
            foreach (var item in TweenTimeLineDataModel.ClipStateDict)
            {
                TogglePlayClip(item.Key);
            }
        }

        public static void RecordClip(IUniqueBehaviour key)
        {
            ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[key];
            stateInfo.ChangeState(key, ClipBehaviourStateEnum.Recording);
        }

        public static void ToggleRecordClip(IUniqueBehaviour key)
        {
            ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[key];
            stateInfo.ToggleState(key, ClipBehaviourStateEnum.Recording);
        }

        public static void PlayAllClip(bool controlPlay = false)
        {
            ReusablePrimeTweens();

            ChangeTweenManagerState(ClipBehaviourStateEnum.Playing);
            foreach (var item in TweenTimeLineDataModel.ClipStateDict)
            {
                PlayClip(item.Key);
            }
        }

        public static void ReusablePrimeTweens()
        {
            if (PrimeTweenManager.Instance)
            {
                PrimeTweenManager.Instance.Update();
            }
        }

        private static void ChangeTweenManagerState(ClipBehaviourStateEnum behaviourStateType)
        {
            // Debug.Log(behaviourStateType + "===>" + TweenTimeLineDataModel.StateInfo.BehaviourState);
            TweenTimeLineDataModel.StateInfo.ChangeState(null, behaviourStateType);
            var window = GetActionEditorWindow();
            if (window != null)
                window.RefreshBtns();
        }

        private static void ToggleTweenManagerState(ClipBehaviourStateEnum behaviourStateType)
        {
            // Debug.Log(behaviourStateType + "===>" + TweenTimeLineDataModel.StateInfo.BehaviourState);
            TweenTimeLineDataModel.StateInfo.ToggleState(null, behaviourStateType);
            var window = GetActionEditorWindow();
            if (window != null)
                window.RefreshBtns();
        }

        public static void RecordAllSelectClip()
        {
            TweenTimeLineDataModel.StateInfo.IsSelect = true;
            ChangeTweenManagerState(ClipBehaviourStateEnum.Recording);

            foreach (var item in TweenTimeLineDataModel.ClipStateDict)
            {
                RecordClip(item.Key);
            }
        }

        public static void ToggleRecordAllSelectClip()
        {
            TweenTimeLineDataModel.StateInfo.IsSelect = true;
            ToggleTweenManagerState(ClipBehaviourStateEnum.Recording);

            foreach (var item in TweenTimeLineDataModel.ClipStateDict)
            {
                ToggleRecordClip(item.Key);
            }
        }
        #endregion


        #region TimeLine Tools
        public static void Play()
        {
            TimelineWindowExposer.PlayEditTimeline();
        }

        public static void AddTrack(Transform groupTrack,
            TrackInfoContext trackInfo, bool isIn, bool createNewTrack)
        {
            PlayableDirector playableDirector = GameObject.FindFirstObjectByType<PlayableDirector>();
            TimelineAsset timelineAsset = playableDirector.playableAsset as TimelineAsset;

            Undo.RecordObject(playableDirector.playableAsset, "Add Track");

            GroupTrack parentTrack = GetParentGroup(groupTrack, timelineAsset, isIn);

            AdjustStartTime(ref trackInfo);
            AddTrackWithParents(trackInfo, createNewTrack, parentTrack);
        }

        public static TrackAsset AddTrackWithParents(TrackInfoContext trackInfo,
            bool createNewTrack, TrackAsset parentTrack)
        {
            PlayableDirector playableDirector = GameObject.FindFirstObjectByType<PlayableDirector>();
            TimelineAsset timelineAsset = playableDirector.playableAsset as TimelineAsset;
            TrackAsset trackAsset = AddTrackToTimeline(trackInfo, timelineAsset, parentTrack, createNewTrack);
            var clipMethod = typeof(TrackAsset).GetMethod("CreateClip", BindingFlags.Instance | BindingFlags.NonPublic);
            var markMethod = typeof(TrackAsset).GetMethod("AddMarker", BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var clipInfo in trackInfo.clipInfos)
            {
                TimelineClip clip = clipMethod.Invoke(trackAsset, new object[]
                {
                    trackInfo.trackAssetType
                }) as TimelineClip;
                AddClip(clipInfo, clip);

                AddMarkers(trackAsset, markMethod, clipInfo);
            }

            TimelineWindowExposer.Bind(playableDirector, trackAsset, trackInfo.component);

            return trackAsset;
        }

        private static void AddMarkers(TrackAsset trackAsset, MethodInfo makrMethod, ClipInfoContext clipInfo)
        {
            foreach (var markInfo in clipInfo.markInfos)
            {
                ValueMaker so = null;

                if (markInfo.UpdateValue.GetType() == typeof(bool))
                    so = ScriptableObject.CreateInstance<BoolNotesMarker>();
                else if (markInfo.UpdateValue.GetType() == typeof(Sprite))
                    so = ScriptableObject.CreateInstance<SpriteNotesMarker>();
                else
                    so = ScriptableObject.CreateInstance<StringNotesMarker>();

                so.Value = markInfo.UpdateValue;
                so.FieldName = markInfo.FieldName;
                makrMethod.Invoke(trackAsset, new[]{so
                    });
                so.time = markInfo.Time;
                ((IMarker)so).Initialize(trackAsset);
            }
        }

        private static void AddClip(ClipInfoContext clipContextInfo, TimelineClip clip)
        {
            clip.start = clipContextInfo.start;
            clip.duration = clipContextInfo.duration;
            clip.displayName = clipContextInfo.GetDisplayName(clip.asset.GetType());

            if (!TimelineWindowExposer.GetBehaviourValue(clip.asset, out var value))
            {
                return;
            }

            var behaviour = value as IUniqueBehaviour;
            behaviour.EndPos = clipContextInfo.endPos;
            behaviour.StartPos = clipContextInfo.startPos;
            behaviour.EasePreset = clipContextInfo.easePreset;
        }

        private static void AdjustStartTime(ref TrackInfoContext trackInfo)
        {
            foreach (var item in TweenTimeLineDataModel.TrackObjectDict)
            {
                TrackAsset trackAsset = item.Key;
                if (item.Value.Equals(trackInfo.component)
                    && trackInfo.trackType == trackAsset.GetType())
                {
                    if (TweenTimeLineDataModel.TrackBehaviourDict.TryGetValue(trackAsset, out var behaviourList))
                    {
                        double clipEnd = 0;
                        foreach (var behaviour in behaviourList)
                        {
                            var clipInfo = TweenTimeLineDataModel.ClipInfoDicts[behaviour];
                            clipEnd = Math.Max(clipEnd, clipInfo.start + clipInfo.duration);
                        }

                        foreach (var clipInfoContext in trackInfo.clipInfos)
                        {
                            clipInfoContext.start += clipEnd;
                        }
                    }
                    break;
                }
            }
        }

        public static GroupTrack GetParentGroup(Transform groupTrack, TimelineAsset timelineAsset, bool isIn)
        {
            string grouTrackName = groupTrack.name;
            if (groupTrack.tag == TweenTimelineDefine.PanelTag
            && !groupTrack.name.EndsWith(TweenTimelineDefine.PanelTag))
            {
                grouTrackName = $"{grouTrackName}{TweenTimelineDefine.PanelTag}";
            }
            if (groupTrack.tag == TweenTimelineDefine.CompositeTag)
            {
                bool contains = false;
                foreach (var item in TweenTimelineDefine.UIComponentTypeMatch)
                {
                    if (groupTrack.name.EndsWith(item.Key))
                    {
                        contains = true;
                        break;
                    }
                }

                if (!contains &&
                    !groupTrack.name.EndsWith(TweenTimelineDefine.CompositeTag))
                {
                    grouTrackName = $"{grouTrackName}{TweenTimelineDefine.CompositeTag}";
                }
            }

            string rootTrackName = isIn ?
                  TweenTimelineDefine.InDefine :
                  TweenTimelineDefine.OutDefine;
            GroupTrack rootParentTrack = CreateRootTrack(timelineAsset, rootTrackName);

            return CreatGroupAsset(grouTrackName, timelineAsset, rootParentTrack);
        }

        private static GroupTrack CreateRootTrack(TimelineAsset timelineAsset, string rootTrackName)
        {
            GroupTrack rootParentTrack = null;
            var grandParentTrackIndex = TweenTimeLineDataModel.groupTracks.FindIndex(track => track.name == rootTrackName);
            rootParentTrack = AddGroup(rootTrackName, timelineAsset, null, grandParentTrackIndex);

            return rootParentTrack;
        }

        public static GroupTrack CreatGroupAsset(string grouTrackName, TimelineAsset timelineAsset, TrackAsset parentTrack)
        {
            GroupTrack groupTrack = null;

            bool MatchGroup(GroupTrack track)
            {
                if (track.name == grouTrackName)
                {
                    var root = GetTrackRoot(track);
                    if (parentTrack == null)
                    {
                        return true;
                    }
                    var parentRoot = GetTrackRoot(parentTrack);

                    return root.name == parentRoot.name;
                }
                return false;
            }

            var groupTrackIndex = TweenTimeLineDataModel.groupTracks.FindIndex(MatchGroup);
            groupTrack = AddGroup(grouTrackName, timelineAsset, parentTrack, groupTrackIndex);

            return groupTrack;
        }

        private static GroupTrack AddGroup(string grouTrackName, TimelineAsset timelineAsset,
        TrackAsset parentTrack, int groupTrackIndex)
        {
            GroupTrack groupTrack;
            if (groupTrackIndex < 0)
            {
                groupTrack = timelineAsset.CreateTrack<GroupTrack>(parentTrack, grouTrackName);
                TweenTimeLineDataModel.groupTracks.Add(groupTrack); // since we don't trigger timeline change immediately
            }
            else
            {
                groupTrack = TweenTimeLineDataModel.groupTracks[groupTrackIndex];
            }

            return groupTrack;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <param name="trackAssetType"></param>
        /// <param name="timelineAsset"></param>
        /// <param name="groupTrack"></param>
        /// <param name="createNewTrack">For example, if in different direction, it should be false</param>
        /// <returns></returns>
        private static TrackAsset AddTrackToTimeline(TrackInfoContext trackInfoContext,
            TimelineAsset timelineAsset, TrackAsset groupTrack, bool createNewTrack)
        {
            Component component = trackInfoContext.component;
            Type trackType = trackInfoContext.trackType;

            Assert.IsNotNull(trackType, nameof(trackType) + " != null");
            Assert.IsNotNull(component, nameof(component) + " != null");

            TrackAsset track = FindExistTrackAsset(component, trackType);
            if (track == null || createNewTrack)
            {
                var trackRoot = BindUtility.GetAttachRoot(component.transform);
                string trackName = GetTrackName(component.transform, trackRoot,
                 trackInfoContext.component.GetType(), trackType);
                track = timelineAsset.CreateTrack(trackType, groupTrack, trackName);
                Debug.Log("Track added: " + track.name);
            }

            return track;
        }

        private static string GetTrackName(Transform child, Transform root,
        Type componentType, Type trackType)
        {
            string trackName = TweenTimelinePreferencesProvider.GetBool(TweenGenSettings.UseFullPathName)
            ? child.GetFullPathTrans(root)
            : child.gameObject.name;

            return $"{trackName}_{componentType.FullName}_{trackType.Name}";
        }

        public static TrackAsset FindExistTrackAsset(Component component, Type trackAssetType)
        {
            return TweenTimeLineDataModel.TrackObjectDict.FirstOrDefault(item =>
                item.Key.GetType() == trackAssetType
                && item.Value == component).Key;
        }
        #endregion

        public static TweenActionEditorWindow GetActionEditorWindow()
        {
            if (EditorWindow.HasOpenInstances<TweenActionEditorWindow>())
            {
                return EditorWindow.GetWindow<TweenActionEditorWindow>();
            }
            return null;
        }

        public static bool EnsureCanPreview()
        {
            if (!InitTimeline())
            {
                // Debug.Log("Please open the timeLine window first");
                if (!isInit)
                {
                    Debug.LogError("Fail to Init Timeline");
                }
                return false;
            }
            return true;
        }

        #region Utility
        internal static object GetStartValue(IUniqueBehaviour behaviour, bool isStart)
        {
            TrackAsset trackAsset = TweenTimeLineDataModel.PlayBehaviourTrackDict[behaviour];
            if (TweenTimeLineDataModel.TrackBehaviourDict.TryGetValue(trackAsset, out var behaviourList))
            {
                for (int i = 0; i < behaviourList.Count; i++)
                {
                    if (behaviour.Equals(behaviourList[i]))
                    {
                        var target = TweenTimeLineDataModel.TrackObjectDict[trackAsset];

                        if (isStart)
                        {
                            if (i == 0)
                            {
                                return behaviour.Get(target);
                            }
                            return behaviourList[i - 1].EndPos;
                        }
                        else
                        {
                            if (behaviourList.Count > i + 1)
                            {
                                return behaviourList[i + 1].StartPos;
                            }
                            return behaviourList[i].StartPos;
                        }
                    }
                }
            }
            return null;
        }

        public static TrackAsset GetTrackRoot(TrackAsset trackAsset)
        {
            if (trackAsset == null)
            {
                return null;
            }
            var trackAssetParent = trackAsset.parent as TrackAsset;

            if (trackAssetParent == null)
            {
                return trackAsset;
            }

            return GetTrackRoot(trackAssetParent);
        }

        public static TrackAsset GetTrackSecondRoot(TrackAsset trackAsset)
        {
            if (trackAsset == null)
            {
                return null;
            }
            var trackAssetParent = trackAsset.parent as TrackAsset;
            if (trackAssetParent == null)
            {
                return trackAsset;
            }

            var grandTrackAssetParent = trackAssetParent.parent as TrackAsset;
            if (grandTrackAssetParent == null)
            {
                return trackAsset;
            }

            return GetTrackSecondRoot(trackAssetParent);
        }

        public static int GetPanelTracks(IEnumerable<TrackAsset> tracks, out List<TrackAsset> trackHierarchy, out TrackAsset rootTrack)
        {
            int isIn = -10;
            rootTrack = null;
            trackHierarchy = new List<TrackAsset>();
            foreach (var track in tracks)
            {
                rootTrack = TweenTimelineManager.GetTrackRoot(track);
                int inDir = rootTrack.name == TweenTimelineDefine.InDefine ? 1
                         : (rootTrack.name == TweenTimelineDefine.OutDefine ? -1 : 0);

                if (isIn == -10)
                {
                    isIn = inDir;
                }
                else
                {
                    if (inDir != isIn)
                    {
                        throw new InvalidOperationException($"A different track was detected: {track.name} vs {rootTrack.name}.");
                    }
                }

                if (track.name == rootTrack.name)
                {
                    trackHierarchy.Clear();
                    trackHierarchy.AddRange(rootTrack.GetChildTracks());
                    break;
                }
                if (track.parent.name == rootTrack.name)
                {
                    if (!trackHierarchy.Contains(track))
                    {
                        trackHierarchy.Add(track);
                    }
                }
            }

            if (trackHierarchy.Count <= 0)
            {
                throw new System.Exception("Please select panel track");
            }

            return isIn;
        }

        public static void SelectBeforeToggle(IUniqueBehaviour behaviour)
        {
            ClipInfo clipInfo = TweenTimeLineDataModel.ClipInfoDicts[behaviour];
            ClipBehaviourState stateInfo = TweenTimeLineDataModel.ClipStateDict[behaviour];
            var curTime = TimelineWindowExposer.GetSequenceTime();
            if (curTime >= clipInfo.start && curTime <= clipInfo.start + clipInfo.duration)
            {
                return;
            }
            TimelineWindowExposer.SkipToTimelinePos(stateInfo.IsRecordStart ?
            clipInfo.start : clipInfo.start + clipInfo.duration);
        }

        #endregion
    }

}
